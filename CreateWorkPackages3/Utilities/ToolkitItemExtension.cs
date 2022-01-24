using BusinessLibrary.Models;
using BusinessLibrary.Models.Planning;
using BusinessLibrary.Ultilities;
using CreateWorkPackages3.Model;
using CreateWorkPackages3.Workpackages.Model;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateWorkPackages3.Utilities
{
	public static class ToolkitItemExtension
	{
		public static List<ToolKitFeatureModel> ConvertToFeatures(this ListItemCollection toolkitData)
		{
			var result = new List<ToolKitFeatureModel>();

			foreach (var fe in toolkitData)
			{
				var team = fe.FieldValues["Team"] == null ? string.Empty : ((FieldLookupValue)fe.FieldValues["Team"]).LookupValue;
				var release = fe.FieldValues["Release"] != null ? ((FieldLookupValue)fe.FieldValues["Release"]).LookupValue : string.Empty;

				result.Add(new ToolKitFeatureModel()
				{
					//Application
					CurrentStatus = fe.FieldValues["Status"] == null ? string.Empty : fe.FieldValues["Status"].ToString(),
					Id = fe.Id.ToString(),
					Name = fe.FieldValues["Title"].ToString(),
					//Priority = 
					Release = release,
					RemainingHours = fe.FieldValues["RemainingWork"] == null ? 0 : decimal.Parse(fe.FieldValues["RemainingWork"].ToString()),
					Team = team
				});
			}

			return result;
		}

		public static List<ToolKitUserStoryModel> ConvertToUserStory(this ListItemCollection toolkitData)
		{
			var result = new List<ToolKitUserStoryModel>();

			foreach (var fe in toolkitData)
			{
				var team = fe.FieldValues["Team"] == null ? string.Empty : ((FieldLookupValue)fe.FieldValues["Team"]).LookupValue;
				var featureId = fe.FieldValues["Case"] != null ? ((FieldLookupValue)fe.FieldValues["Case"]).LookupId : 0;

				result.Add(new ToolKitUserStoryModel()
				{
					FeatureId = featureId.ToString(),
					CurrentStatus = fe.FieldValues["Status"] == null ? string.Empty : fe.FieldValues["Status"].ToString(),
					Id = fe.Id.ToString(),
					Name = fe.FieldValues["Title"].ToString(),
					Team = team
				});
			}

			return result;
		}

		public static List<ToolKitWorkPackageModel> ConvertToWorkpackage(this ListItemCollection toolkitData)
		{
			var result = new List<ToolKitWorkPackageModel>();

			foreach (var wp in toolkitData)
			{
				var team = wp.FieldValues["Team"] == null ? string.Empty : ((FieldLookupValue)wp.FieldValues["Team"]).LookupValue;
				var assignee = wp.FieldValues["AssignedTo"] == null ? string.Empty : ((Microsoft.SharePoint.Client.FieldLookupValue)wp.FieldValues["AssignedTo"]).LookupValue;
				var featureId = wp.FieldValues["RelatedCase"] != null ? ((FieldLookupValue)wp.FieldValues["RelatedCase"]).LookupId : 0;
				var featureName = wp.FieldValues["RelatedCase"] != null ? ((FieldLookupValue)wp.FieldValues["RelatedCase"]).LookupValue : string.Empty;
				var userStoryId = wp.FieldValues["FunctionalScenario"] != null ? ((FieldLookupValue)wp.FieldValues["FunctionalScenario"]).LookupId : 0;
				var release = wp.FieldValues["Release"] != null ? ((FieldLookupValue)wp.FieldValues["Release"]).LookupValue : string.Empty;
				//var dependOnWPIds = wp.FieldValues["Depend_x0020_on"] != null && (wp.FieldValues["Depend_x0020_on"] as FieldLookupValue[]).Count() > 0
				//	? wp.FieldValues["Depend_x0020_on"].ToString().Contains(";")
				//		? wp.FieldValues["Depend_x0020_on"].ToString().Split(';')[0]
				//		: wp.FieldValues["Depend_x0020_on"].ToString()
				//	: string.Empty;
				var dependOnWPIds = wp.FieldValues["Depend_x0020_on"] != null && (wp.FieldValues["Depend_x0020_on"] as Microsoft.SharePoint.Client.FieldLookupValue[]).Count() > 0
					? (wp.FieldValues["Depend_x0020_on"] as Microsoft.SharePoint.Client.FieldLookupValue[])[0].LookupValue.Contains(";")
						? (wp.FieldValues["Depend_x0020_on"] as Microsoft.SharePoint.Client.FieldLookupValue[])[0].LookupValue.Split(';')[0]
						: (wp.FieldValues["Depend_x0020_on"] as Microsoft.SharePoint.Client.FieldLookupValue[])[0].LookupValue
					: string.Empty;

				result.Add(new ToolKitWorkPackageModel()
				{
					//Application

					CurrentStatus = wp.FieldValues["Status"] == null ? string.Empty : wp.FieldValues["Status"].ToString(),
					Id = wp.Id.ToString(),
					Name = wp.FieldValues["Title"].ToString(),
					//Priority = 
					Release = release,
					RemainingHours = wp.FieldValues["RemainingWork"] == null ? 0 : decimal.Parse(wp.FieldValues["RemainingWork"].ToString()),
					Team = team,
					WpType = wp.FieldValues["WPType"] == null ? string.Empty : wp.FieldValues["WPType"].ToString(),
					Assignee = assignee,
					UserStoryId = userStoryId.ToString(),
					FeatureId = featureId.ToString(),
					FeatureName = featureName,
					HasPlan = false,
					DependOnWPId = dependOnWPIds
				});
			}

			return result;
		}

		public static List<ToolkitAllocationModel> ConvertToAllocation(this ListItemCollection toolkitData)
		{
			var result = new List<ToolkitAllocationModel>();

			foreach (var al in toolkitData)
			{
				var team = al.FieldValues["Team"] == null ? string.Empty : ((FieldLookupValue)al.FieldValues["Team"]).LookupValue;
				DateTime startDate = DateTime.Parse(al.FieldValues["DateFrom"].ToString()).Date;
				DateTime endDate = DateTime.Parse(al.FieldValues["DateTo"].ToString()).Date;

				result.Add(new ToolkitAllocationModel()
				{
					Name = al.FieldValues["Title"].ToString(),
					Team = team,
					Hours = al.FieldValues["HoursCapacity"] == null ? 0 : decimal.Parse(al.FieldValues["HoursCapacity"].ToString()),
					From = startDate,
					To = endDate
				});
			}

			return result;
		}

		public static List<ToolkitAllocationAdjustmentModel> ConvertToAllocationAdjustment(this ListItemCollection toolkitData)
		{
			var result = new List<ToolkitAllocationAdjustmentModel>();

			foreach (var al in toolkitData)
			{
				var team = al.FieldValues["Team"] == null ? string.Empty : ((FieldLookupValue)al.FieldValues["Team"]).LookupValue;
				DateTime startDate = DateTime.Parse(al.FieldValues["DateFrom"].ToString()).Date;
				DateTime endDate = DateTime.Parse(al.FieldValues["DateTo"].ToString()).Date;

				result.Add(new ToolkitAllocationAdjustmentModel()
				{
					Name = al.FieldValues["Title"].ToString(),
					Team = team,
					Hours = al.FieldValues["HoursCapacity"] == null ? 0 : decimal.Parse(al.FieldValues["HoursCapacity"].ToString()),
					From = startDate,
					To = endDate
				});
			}

			return result;
		}


		public static List<WPItemModel> SortPriority(this List<WPItemModel> data)
		{
			data = data.OrderBy(d => d.WPId).ToList();

			foreach (var item in data)
			{
				//var currentPriority = 0;
				//if (string.IsNullOrWhiteSpace(item.WPDependOn))
				//{
				//	item.WPPriority = 0;
				//	item.WPStartDate = item.WPStart ?? DateTime.UtcNow.Date; //start time should the same as start date if no dependency on others
				//	item.WPDateProgressing = CalculateDateProgressing(item);
				//	continue;
				//}

				////otherwise; start time depends on others
				//var pendingOnWP = data.FirstOrDefault(d => int.Parse(item.WPDependOn) == d.WPId);

				//item.WPPriority = GetPriority(data, pendingOnWP, currentPriority);
				//item.WPStartDate = GetStartDateByPriority(data, pendingOnWP, (decimal.Parse(pendingOnWP.WPEstimate)));
				//item.WPDateProgressing = CalculateDateProgressing(item);


				var currentPriority = 0;

				//if (!item.WPStart.HasValue)
				//{

				//}

				if (string.IsNullOrWhiteSpace(item.WPDependOn))
				{
					item.WPPriority = 0;
					item.WPStartDate = item.WPStart ?? DateTime.UtcNow.Date; //start time should the same as start date if no dependency on others
					item.WPDateProgressing = CalculateDateProgressing(item);
					continue;
				}

				//otherwise; start time depends on others
				var pendingOnWP = data.FirstOrDefault(d => int.Parse(item.WPDependOn) == d.WPId);

				item.WPPriority = GetPriority(data, pendingOnWP, currentPriority);
				item.WPStartDate = item.WPStart ?? GetStartDateByPriority(data, pendingOnWP, (decimal.Parse(pendingOnWP.WPEstimate)));
				item.WPDateProgressing = CalculateDateProgressing(item);
			}

			return data;
		}

		private static int GetPriority(List<WPItemModel> data, WPItemModel currentWp, int currentPriority)
		{
			if (currentWp == null || string.IsNullOrWhiteSpace(currentWp.WPDependOn))
			{
				return currentPriority++;
			}

			var pendingOnWP = data.FirstOrDefault(d => int.Parse(currentWp.WPDependOn) == d.WPId);
			currentPriority++;
			return GetPriority(data, pendingOnWP, currentPriority);
		}

		private static DateTime GetStartDateByPriority(List<WPItemModel> data, WPItemModel currentWp, decimal periodOfEstimate)
		{
			if (currentWp.WPDueDate.HasValue)
			{
				return currentWp.WPDueDate.Value.AddDays(1).Date;
			}

			if (currentWp.WPStart.HasValue)
			{
				return currentWp.WPStart.Value.AddDateTime(periodOfEstimate);
			}

			if (string.IsNullOrWhiteSpace(currentWp.WPDependOn))
			{
				return DateTime.UtcNow.Date.AddDateTime(periodOfEstimate);
			}

			var pendingOnWP = data.FirstOrDefault(d => int.Parse(currentWp.WPDependOn) == d.WPId);
			periodOfEstimate = periodOfEstimate + (int.Parse(pendingOnWP.WPEstimate));
			return GetStartDateByPriority(data, pendingOnWP, periodOfEstimate);
		}

		/// <summary>
		/// return start date of the next WP
		/// </summary>
		/// <param name="currentDate"></param>
		/// <param name="numberOfHours"></param>
		/// <returns></returns>
		public static DateTime AddDateTime(this DateTime currentDate, decimal numberOfHours)
		{
			while (currentDate.IsWeekend() || currentDate.IsInHolidayGlobal())
			{
				currentDate = currentDate.AddDays(1);
			}

			//var remainingHours = numberOfHours % 8; //TODO: 8 is standard working hours per day

			while (numberOfHours - 8 > 0) //TODO: 8 is standard working hours per day - move it to config
			{
				currentDate = currentDate.AddDays(1);
				while (currentDate.IsWeekend() || currentDate.IsInHolidayGlobal())
				{
					currentDate = currentDate.AddDays(1);
				}

				numberOfHours = numberOfHours - 8;
			}

			currentDate = currentDate.AddDays(1);
			return currentDate;
		}

		/// <summary>
		/// WP is handled in these days - can be changed because of allocated resource
		/// </summary>
		/// <param name="currentDate"></param>
		/// <param name="numberOfHours"></param>
		/// <returns></returns>
		public static List<DateTime> CalculateDateProgressing(WPItemModel workpackage)
		{
			if (!workpackage.WPStartDate.HasValue) return new List<DateTime>();

			DateTime startDate = workpackage.WPStartDate.Value;
			string allocatedResource = workpackage.WPAssignee;//TODO: implement allocated assignee
			var workingDays = new List<DateTime>();

			if (workpackage.WPDueDate.HasValue)
			{
				var days = (workpackage.WPDueDate.Value.Date - startDate.Date).Days;
				for (int i = 0; i <= days; i++)
				{
					if (startDate.Date.AddDays(i).Date.IsWeekend() || startDate.Date.AddDays(i).Date.IsInHolidayGlobal())
					{
						continue;
					}

					workingDays.Add(startDate.Date.AddDays(i).Date);
				}
			}
			else
			{
				decimal estimateInHours = decimal.Parse(workpackage.WPEstimate);
				while (startDate.IsWeekend() || startDate.IsInHolidayGlobal())
				{
					startDate = startDate.AddDays(1);
				}

				while (estimateInHours - 8 > 0) //TODO: 8 is standard working hours per day - move it to config
				{
					workingDays.Add(startDate);

					startDate = startDate.AddDays(1);
					while (startDate.IsWeekend() || startDate.IsInHolidayGlobal())
					{
						startDate = startDate.AddDays(1);
					}

					estimateInHours = estimateInHours - 8;
				}

				//startDate = startDate.AddDays(1);
				workingDays.Add(startDate);
			}



			return workingDays;
		}
	}
}
