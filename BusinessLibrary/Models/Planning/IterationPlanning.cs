using BusinessLibrary.Models.Planning.Icon;
using BusinessLibrary.Ultilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.Models.Planning
{
	public abstract class PlanningModel
	{
		public int Year { get; set; }
		public int Month { get; set; }
		public List<int> Weeks { get; set; }
		public int IterationOrder { get; set; }
		public int IterationId { get; set; }
		public string IterationName { get; set; }
		public abstract decimal AvailableHours { get; }
		public abstract decimal Workload { get; }
		public abstract decimal AllocatedHours { get; }

		public abstract IterationPlanningStatusModel Status { get; }
	}

	public partial class IterationPlanningModel : PlanningModel
	{
		public IterationPlanningModel()
		{
			People = new List<IterationPeoplePlanningModel>();
			WorkPackagesActualInIteration = new List<WPItemModel>();
			Weeks = new List<int>();
			DaysInIteration = new List<DateTime>();
			WorkingDaysInIteration = new List<DateTime>();
			WorkPackagesHistory = new List<WPItemModel>();
		}

		public IterationPlanningModel(ToolkitIterationModel toolkitIterationModel, List<ToolkitAllocationResponseModel> toolkitAllocations, List<ToolkitAllocationResponseModel> toolkitAllocationAdjustments, List<WPItemModel> workPackageHistories)
			: base()
		{
			People = new List<IterationPeoplePlanningModel>();
			WorkPackagesActualInIteration = new List<WPItemModel>();
			Weeks = new List<int>();
			DaysInIteration = new List<DateTime>();
			WorkingDaysInIteration = new List<DateTime>();
			WorkPackagesHistory = new List<WPItemModel>();

			WorkPackagesHistory = workPackageHistories;



			//From Iteration Model
			//	Get all working days in iteration
			//		- Identify Year								(DONE)
			//		- Identify Month							(DONE)
			//		- Identify Weeks							(DONE)
			//		- Identify Iteration Name					(DONE)

			//From Allocation/Allocation Adjustment Models
			//	Get all working days of all team members
			//		- During iteration period				
			//			- Identify available working hours		(DONE)
			//			- Identify non-available working hours	(NO)
			//			- Identify allocated working hours		(NO)

			Year = toolkitIterationModel.StartDate.Date.Year;
			Month = toolkitIterationModel.StartDate.Date.Month;
			IterationOrder = int.Parse(toolkitIterationModel.Title.Split(' ')[toolkitIterationModel.Title.Split(' ').Count() - 1]);
			IterationName = toolkitIterationModel.Title;
			IterationId = toolkitIterationModel.Id;
			//var workingDaysInIteration = new List<DateTime>();
			var currentDate = toolkitIterationModel.StartDate.Date;
			while (currentDate <= toolkitIterationModel.EndDate)
			{
				var week = currentDate.GetIso8601WeekOfYear();
				if (!this.Weeks.Contains(week)) this.Weeks.Add(week);

				if (!currentDate.IsWeekend() && !currentDate.IsInHolidayGlobal() && !currentDate.IsInHolidayCountry("Viet Nam"))
				{
					WorkingDaysInIteration.Add(currentDate.Date);
				}

				DaysInIteration.Add(currentDate.Date);

				currentDate = currentDate.AddDays(1);
			}

			//plus working hours by working days
			var teamNameBySelectedIteration = toolkitIterationModel.Team.LookupValue;
			var allocationsByTeam = toolkitAllocations.Where(a => a.Team.LookupValue == teamNameBySelectedIteration).ToList();
			foreach (var allocationMemberByTeam in allocationsByTeam)
			{
				var member = new IterationPeoplePlanningModel();
				member.Name = allocationMemberByTeam.Resource.LookupValue;
				foreach (var workingDay in WorkingDaysInIteration)
				{
					if (allocationMemberByTeam.DateFrom.Value.Date <= workingDay.Date && workingDay.Date <= allocationMemberByTeam.DateTo.Value.Date)
					{
						member.AvailableHours += allocationMemberByTeam.HoursCapacity;
						member.WorkingDays.Add(new WorkingDayModel { Date = workingDay, WorkingHours = allocationMemberByTeam.HoursCapacity });
					}
				}

				People.Add(member);
			}

			//minus working hours for vacation
			var allocationAdjustmentsByTeam = toolkitAllocationAdjustments.Where(a => a.Team.LookupValue == teamNameBySelectedIteration).ToList();
			foreach (var allocationAdjustmentMemberByTeam in allocationAdjustmentsByTeam)
			{
				var member = People.FirstOrDefault(p => p.Name == allocationAdjustmentMemberByTeam.Resource.LookupValue);
				if (member == null) continue;
				foreach (var workingDay in WorkingDaysInIteration)
				{
					if (allocationAdjustmentMemberByTeam.DateFrom.Value.Date <= workingDay.Date && workingDay.Date <= allocationAdjustmentMemberByTeam.DateTo.Value.Date)
					{
						member.AvailableHours = member.AvailableHours - allocationAdjustmentMemberByTeam.HoursCapacity;

						var day = member.WorkingDays.FirstOrDefault(wkd => wkd.Date.Date == workingDay.Date);
						if (day != null)
						{
							day.NonWorkingHours = allocationAdjustmentMemberByTeam.HoursCapacity;
							day.WorkingHours = day.WorkingHours - allocationAdjustmentMemberByTeam.HoursCapacity;
						}
					}
				}
			}

			AllocatedWp();
		}

		public List<IterationFeaturePlanningModel> ActionItems
		{
			get
			{
				var result = new List<IterationFeaturePlanningModel>();

				foreach (var workPackage in WorkPackagesActualInIteration)
				{
					var existingFeature = result.FirstOrDefault(r => r.Feature == workPackage.FeatureShow);
					if (existingFeature == null)
					{
						existingFeature = new IterationFeaturePlanningModel { Feature = workPackage.FeatureShow };
					}

					var existingAction = existingFeature.Actions.FirstOrDefault(r => r.Feature == workPackage.USShow && r.WPText == $"{workPackage.WPType} ({workPackage.WPRemainingHour}/{workPackage.WPEstimate})");
					if (existingAction == null)
					{
						existingFeature.Actions.Add(new IterationFeatureItemPlanningModel
						{
							UserStory = workPackage.USShow,
							Feature = workPackage.WPShow,
							WPText = $"{workPackage.WPType} ({workPackage.WPRemainingHour}/{workPackage.WPEstimate})",
							Status = workPackage.WPStatus,
							WPId = workPackage.WPId
						});
						existingFeature.TotalHours += decimal.Parse(workPackage.WPRemainingHour);
					}

					if (!result.Any(r => r.Feature == existingFeature.Feature))
						result.Add(existingFeature);
				}

				return result;
			}
		}

		public List<DateTime> DaysInIteration { get; set; }
		public List<DateTime> WorkingDaysInIteration { get; set; }

		public List<IterationPeoplePlanningModel> People { get; set; }
		public List<WPItemModel> WorkPackagesHistory { get; set; }
		public List<WPItemModel> WorkPackagesActualInIteration { get; set; }
		public override decimal AvailableHours
		{
			get
			{
				return People.Sum(p => p.AvailableHours);
			}
		}
		public override decimal Workload
		{
			get
			{
				//StringBuilder a = new StringBuilder();
				//a.AppendLine($"

				return WorkPackagesActualInIteration.Sum(p => decimal.Parse(p.WPRemainingHour));
			}
		}
		public override decimal AllocatedHours
		{
			get
			{
				var employees = People.Select(p => p.Name).ToList();
				return WorkPackagesActualInIteration.Where(w => employees.Contains(w.WPAssignee)).Sum(p => decimal.Parse(p.WPRemainingHour));
			}
		}



		public override IterationPlanningStatusModel Status
		{
			get
			{
				var availableHours = AvailableHours;
				var workloadInIteration = Workload;
				var allocatedHours = AllocatedHours;
				if (availableHours * 1.1M <= workloadInIteration)
					return new IterationPlanningStatusModel(Color.Red, $"Workload ({workloadInIteration}) is much higher than available hours ({availableHours})");

				if (availableHours * 0.9M <= workloadInIteration)
					return new IterationPlanningStatusModel(Color.Yellow, $"Workload ({workloadInIteration}) is also the same as available hours ({availableHours}) - please prepare for urgent leave situations");

				//if (allocatedHours <= workloadInIteration * 0.5M)
				//	return new IterationPlanningStatusModel(Color.Red, $"Please allocate the WP for the team member - there are {workloadInIteration - allocatedHours} hours haven't been assigned");

				//if (availableHours * 1.15M <= workloadInIteration)
				//	return new IterationPlanningStatusModel(Color.Yellow, $"Workload ({workloadInIteration}) is higher than available hours ({availableHours})");

				if (allocatedHours <= workloadInIteration * 0.8M)
					return new IterationPlanningStatusModel(Color.Yellow, $"There are {workloadInIteration - allocatedHours} hours haven't been assigned");

				//if (availableHours * 0.8M >= workloadInIteration)
				//	return new IterationPlanningStatusModel(Color.LightGreen, $"The Iteration will properly need more work packages - there are {availableHours - workloadInIteration} hours haven't been used");

				//if (availableHours * 0.5M >= workloadInIteration)
				//	return new IterationPlanningStatusModel(Color.Green, $"The Iteration needs more work packages - there are {availableHours - workloadInIteration} hours haven't been used");

				return new IterationPlanningStatusModel(Color.Green, $"");
			}
		}

		private void AllocatedWp()
		{
			var workPackagesInDictionary = WorkPackagesHistory
				.GroupBy(wh => wh.WPId)
				.ToDictionary(wh => wh.Key, wh => wh.OrderByDescending(whi => whi.Version).First());
			var workPackages = workPackagesInDictionary.Select(wp => wp.Value).ToList();


			var wpsInIteration = workPackages.Where(w => !string.IsNullOrEmpty(w.WPIterationId) && int.Parse(w.WPIterationId) == IterationId).ToList();
			//planningIteration.AllocatedWp(wpsInIteration);

			WorkPackagesActualInIteration = new List<WPItemModel>();
			WorkPackagesActualInIteration = wpsInIteration;

			foreach (var person in People)
			{
				var wpsForPerson = WorkPackagesActualInIteration.Where(w => w.WPAssignee == person.Name).ToList();
				person.WorkPackages = wpsForPerson;
			}
		}

		//private void AssignedWp()
		//{

		//}

	}

	public class IterationFeaturePlanningModel
	{
		public IterationFeaturePlanningModel()
		{
			Actions = new List<IterationFeatureItemPlanningModel>();
		}
		public string Feature { get; set; }

		public List<IterationFeatureItemPlanningModel> Actions { get; set; }

		public decimal TotalHours { get; set; }
	}

	public class IterationItemDetailsPlanningModel
	{
		public string Feature { get; set; }
		public string WPType { get; set; }
		public decimal Hours { get; set; }
	}

	public class IterationFeatureItemPlanningModel
	{
		public string Feature { get; set; }
		public string UserStory { get; set; }
		public string WPText { get; set; }
		public string Status { get; set; }
		public IconType Icon
		{
			get
			{
				switch (this.Status)
				{
					case "10 - New":
						return IconType.New;
					case "31 - Running":
						return IconType.Running;
					case "32 - Ready for review":
						return IconType.InReview;
					case "70 - Blocked":
						return IconType.Blocked;
					default:
						return IconType.None;
				}
			}
		}
		public int WPId { get; set; }
	}

	public class IterationPlanningStatusModel
	{
		public IterationPlanningStatusModel(Color color, string tooltip)
		{
			Color = color;
			ToolTip = tooltip;
		}
		public Color Color { get; set; }
		public string ToolTip { get; set; }
	}

	public class IterationPeoplePlanningModel
	{
		public IterationPeoplePlanningModel()
		{
			WorkPackages = new List<WPItemModel>();
			WorkingDays = new List<WorkingDayModel>();
		}
		public string Name { get; set; }
		public List<IterationItemDetailsPlanningModel> Actions
		{
			get
			{
				var result = new List<IterationItemDetailsPlanningModel>();

				foreach (var workPackage in WorkPackages)
				{
					if (result.Any(r => r.Feature == workPackage.Feature && r.WPType == workPackage.WPType)) continue;

					var selectedFeature = result.FirstOrDefault(r => r.Feature == workPackage.Feature);
					if (selectedFeature == null)
					{
						selectedFeature = new IterationItemDetailsPlanningModel { Feature = workPackage.Feature };
					}
					selectedFeature.WPType = workPackage.WPType;
					selectedFeature.Hours = decimal.Parse(workPackage.WPRemainingHour);

					result.Add(selectedFeature);
				}

				return result;
			}
		}
		public decimal AvailableHours { get; set; }
		public decimal AllocatedHours
		{
			get
			{
				return WorkPackages.Sum(p => decimal.Parse(p.WPRemainingHour));
			}
		}
		public List<WPItemModel> WorkPackages { get; set; }

		public List<WorkingDayModel> WorkingDays { get; set; }
		//public List<WorkingDayModel> NonWorkingDays { get; set; }
	}

	public class WorkingDayModel
	{
		public DateTime Date { get; set; }
		public decimal WorkingHours { get; set; }
		public decimal NonWorkingHours { get; set; }
	}
}
