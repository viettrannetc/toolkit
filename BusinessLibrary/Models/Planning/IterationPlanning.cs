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
		public int Iteration { get; set; }
		public string IterationName { get; set; }
		public abstract decimal AvailableHours { get; }
		public abstract decimal Workload { get; }
		public abstract decimal AllocatedHours { get; }

		public abstract IterationPlanningStatusModel Status { get; }
	}

	public class IterationPlanningModel : PlanningModel
	{
		public IterationPlanningModel()
		{
			People = new List<IterationPeoplePlanningModel>();
			WorkPackages = new List<WPItemModel>();
			Weeks = new List<int>();
		}

		public IterationPlanningModel(ToolkitIterationModel toolkitIterationModel, List<ToolkitAllocationResponseModel> toolkitAllocations, List<ToolkitAllocationResponseModel> toolkitAllocationAdjustments)
			: base()
		{
			People = new List<IterationPeoplePlanningModel>();
			WorkPackages = new List<WPItemModel>();
			Weeks = new List<int>();

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
			Iteration = int.Parse(toolkitIterationModel.Title.Split(' ')[toolkitIterationModel.Title.Split(' ').Count() - 1]);
			IterationName = toolkitIterationModel.Title;

			var workingDaysInIteration = new List<DateTime>();
			var currentDate = toolkitIterationModel.StartDate.Date;
			while (currentDate <= toolkitIterationModel.EndDate)
			{
				var week = currentDate.GetIso8601WeekOfYear();
				if (!this.Weeks.Contains(week)) this.Weeks.Add(week);

				if (!currentDate.IsWeekend() && !currentDate.IsInHolidayGlobal() && !currentDate.IsInHolidayCountry("Viet Nam"))
				{
					workingDaysInIteration.Add(currentDate.Date);
				}
				currentDate = currentDate.AddDays(1);
			}

			//plus working hours by working days
			var teamNameBySelectedIteration = toolkitIterationModel.Team.LookupValue;
			var allocationsByTeam = toolkitAllocations.Where(a => a.Team.LookupValue == teamNameBySelectedIteration).ToList();
			foreach (var allocationMemberByTeam in allocationsByTeam)
			{
				var member = new IterationPeoplePlanningModel();
				member.Name = allocationMemberByTeam.Resource.LookupValue;
				foreach (var workingDay in workingDaysInIteration)
				{
					if (allocationMemberByTeam.DateFrom.Value.Date <= workingDay.Date && workingDay.Date <= allocationMemberByTeam.DateTo.Value.Date)
					{
						member.AvailableHours += allocationMemberByTeam.HoursCapacity;
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
				foreach (var workingDay in workingDaysInIteration)
				{
					if (allocationAdjustmentMemberByTeam.DateFrom.Value.Date <= workingDay.Date && workingDay.Date <= allocationAdjustmentMemberByTeam.DateTo.Value.Date)
					{
						member.AvailableHours = member.AvailableHours - allocationAdjustmentMemberByTeam.HoursCapacity;
					}
				}
			}
		}

		public List<IterationItemPlanningModel> Items
		{
			get
			{
				var result = new List<IterationItemPlanningModel>();

				foreach (var workPackage in WorkPackages)
				{
					var existingFeature = result.FirstOrDefault(r => r.Feature == workPackage.FeatureShow);
					if (existingFeature == null)
					{
						existingFeature = new IterationItemPlanningModel { Feature = workPackage.FeatureShow };
					}

					var existingAction = existingFeature.Actions.FirstOrDefault(r => r.Item1 == workPackage.USShow && r.Item2 == $"{workPackage.WPType} ({workPackage.WPRemainingHour})");
					if (existingAction == null)
					{
						existingFeature.Actions.Add(new Tuple<string, string>(workPackage.USShow, $"{workPackage.WPType} ({workPackage.WPRemainingHour})"));
						result.Add(existingFeature);
					}
				}

				return result;
			}
		}

		public List<IterationPeoplePlanningModel> People { get; set; }
		public List<WPItemModel> WorkPackages { get; set; }
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
				return WorkPackages.Sum(p => decimal.Parse(p.WPRemainingHour));
			}
		}
		public override decimal AllocatedHours
		{
			get
			{
				var employees = People.Select(p => p.Name).ToList();
				return WorkPackages.Where(w => employees.Contains(w.WPAssignee)).Sum(p => decimal.Parse(p.WPRemainingHour));
			}
		}

		public override IterationPlanningStatusModel Status
		{
			get
			{
				if (AvailableHours * 1.15M <= Workload)
					return new IterationPlanningStatusModel(Color.Yellow, $"Workload ({Workload}) is higher than available hours ({AvailableHours})");

				if (AvailableHours * 1.3M <= Workload)
					return new IterationPlanningStatusModel(Color.Red, $"Workload ({Workload}) is much higher than available hours ({AvailableHours})");

				if (AvailableHours * 0.8M >= Workload)
					return new IterationPlanningStatusModel(Color.LightGreen, $"The Iteration will properly need more work packages - there are {AvailableHours - Workload} hours haven't been used");

				if (AvailableHours * 0.5M >= Workload)
					return new IterationPlanningStatusModel(Color.Green, $"The Iteration needs more work packages - there are {AvailableHours - Workload} hours haven't been used");

				if (AllocatedHours <= Workload * 0.8M)
					return new IterationPlanningStatusModel(Color.Yellow, $"There are {Workload - AllocatedHours} hours haven't been assigned");

				if (AllocatedHours <= Workload * 0.5M)
					return new IterationPlanningStatusModel(Color.Red, $"Please allocate the WP for the team member - there are {Workload - AllocatedHours} hours haven't been assigned");

				return new IterationPlanningStatusModel(Color.White, $"");
			}
		}

		public void RefreshWp(List<WPItemModel> workPackages)
		{
			WorkPackages = new List<WPItemModel>();
			WorkPackages = workPackages;

			RefreshAllocatedWp();
		}

		private void RefreshAllocatedWp()
		{
			foreach (var person in People)
			{
				var wpsForPerson = WorkPackages.Where(w => w.WPAssignee == person.Name).ToList();
				person.WorkPackages = wpsForPerson;
			}
		}

	}

	public class IterationItemPlanningModel
	{
		public IterationItemPlanningModel()
		{
			Actions = new List<Tuple<string, string>>();// new Dictionary<string, List<string>>();// new List<string>();
		}
		public string Feature { get; set; }
		public List<Tuple<string, string>> Actions { get; set; }
		//public decimal TotalHours { get; set; }
	}

	public class IterationItemDetailsPlanningModel
	{
		public string Feature { get; set; }
		public string WPType { get; set; }
		public decimal Hours { get; set; }
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
	}

}
