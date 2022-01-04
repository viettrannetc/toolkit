using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BusinessLibrary.Models;
using BusinessLibrary.Ultilities;

namespace BusinessLibrary.Ultilities
{
	public static class MappingDatatableToObjectExtension
	{
		public static ToolKitFeatureModel CreateFeature(this DataRow row)
		{
			return new ToolKitFeatureModel
			{
				Id = row[0].ToString(),
				Name = row[1].ToString(),
				Release = row[11].ToString(),
				Team = row[5].ToString(),
				CurrentStatus = row[6].ToString(),
				Application = row[19].ToString(),
				RemainingHours = string.IsNullOrWhiteSpace(row[15].ToString())
					? 0
					: decimal.Parse(row[15].ToString()),
				Priority = Constains.FE_Priority.IndexOf(row[1].ToString()) >= 0
					? Constains.FE_Priority.IndexOf(row[1].ToString())
					: Constains.FE_Priority.Count() + 1
			};
		}
		public static List<ToolKitFeatureModel> MappingFeature(this DataTable table)
		{
			var result = new List<ToolKitFeatureModel>();
			foreach (DataRow item in table.Rows)
			{
				result.Add(item.CreateFeature());
			}

			return result;
		}

		public static ToolKitUserStoryModel CreateUserStory(this DataRow row)
		{
			return new ToolKitUserStoryModel
			{
				Id = row[0].ToString(),
				Name = row[1].ToString(),
				CurrentStatus = row[3].ToString(),
				FeatureId = row[11].ToString(),
				Team = row[12].ToString()
			};
		}
		public static List<ToolKitUserStoryModel> MappingUserStory(this DataTable table)
		{
			var result = new List<ToolKitUserStoryModel>();
			foreach (DataRow item in table.Rows)
			{
				result.Add(item.CreateUserStory());
			}

			return result;
		}

		public static ToolKitWorkPackageModel CreateWorkPackage(this DataRow row)
		{
			return new ToolKitWorkPackageModel
			{
				Id = row[0].ToString(),
				Name = row[1].ToString(),
				Assignee = row[2].ToString(),
				Team = row[3].ToString(),
				Release = row[4].ToString(),
				CurrentStatus = row[7].ToString(),
				RemainingHours = (string.IsNullOrWhiteSpace(row[9].ToString()))
					? 0
					: decimal.Parse(row[9].ToString()),
				FeatureName = row[10].ToString(),
				WpType = row[13].ToString(),
				DependOnDefectId = row[15].ToString(),
				DependOnWPId = !string.IsNullOrWhiteSpace(row[16].ToString())
					? row[16].ToString().Contains(";")
						? row[16].ToString().Split(';')[0]
						: row[16].ToString()
					: string.Empty,
				UserStoryId = row[17].ToString(),
				HasPlan = false
			};
		}
		public static List<ToolKitWorkPackageModel> MappingWorkPackage(this DataTable table)
		{
			var result = new List<ToolKitWorkPackageModel>();
			foreach (DataRow item in table.Rows)
			{
				result.Add(item.CreateWorkPackage());
			}

			return result;
		}

		public static ToolKitDefectModel CreateDefect(this DataRow row)
		{
			return new ToolKitDefectModel
			{
				Id = row[0].ToString(),
				Name = row[1].ToString(),
				Application = row[2].ToString(),
				CurrentStatus = row[4].ToString(),
				Team = row[14].ToString(),
				Release = row[15].ToString()
			};
		}
		public static List<ToolKitDefectModel> MappingDefect(this DataTable table)
		{
			var result = new List<ToolKitDefectModel>();
			foreach (DataRow item in table.Rows)
			{
				result.Add(item.CreateDefect());
			}

			return result;
		}

		public static ToolkitAllocationModel CreateAllocation(this DataRow row)
		{
			return new ToolkitAllocationModel
			{
				Name = row[1].ToString(),
				Team = row[2].ToString(),
				From = string.IsNullOrEmpty(row[3].ToString())
					? DateTime.UtcNow
					: DateTime.Parse(row[3].ToString()),
				To = string.IsNullOrEmpty(row[4].ToString())
					? DateTime.UtcNow
					: DateTime.Parse(row[4].ToString()),
				Hours = decimal.Parse(row[5].ToString())
			};
		}
		public static List<ToolkitAllocationModel> MappingAllocation(this DataTable table)
		{
			var result = new List<ToolkitAllocationModel>();
			foreach (DataRow item in table.Rows)
			{
				result.Add(item.CreateAllocation());
			}

			return result;
		}

		public static ToolkitAllocationAdjustmentModel CreateAllocationAdjustment(this DataRow row)
		{
			return new ToolkitAllocationAdjustmentModel
			{
				Name = row[1].ToString(),
				Team = row[2].ToString(),
				From = string.IsNullOrEmpty(row[3].ToString())
					? DateTime.UtcNow
					: DateTime.Parse(row[3].ToString()),
				To = string.IsNullOrEmpty(row[4].ToString())
					? DateTime.UtcNow
					: DateTime.Parse(row[4].ToString()),
				Hours = decimal.Parse(row[5].ToString())
			};
		}
		public static List<ToolkitAllocationAdjustmentModel> MappingAllocationAdjustment(this DataTable table)
		{
			var result = new List<ToolkitAllocationAdjustmentModel>();
			foreach (DataRow item in table.Rows)
			{
				result.Add(item.CreateAllocationAdjustment());
			}

			return result;
		}
	}
}
