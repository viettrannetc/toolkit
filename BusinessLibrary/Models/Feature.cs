using System.Collections.Generic;
using BusinessLibrary.Models.Planning;
using BusinessLibrary.Ultilities;

namespace BusinessLibrary.Models
{
	public class ToolKitFeatureModel
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string CurrentStatus { get; set; }
		public string Team { get; set; }
		/// <summary>
		/// Currently no use
		/// </summary>
		public string Application { get; set; }
		public string Release { get; set; }
		public decimal RemainingHours { get; set; }
		public int Priority { get; set; }
	}

	public class ToolKitUserStoryModel
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string FeatureId { get; set; }
		public string CurrentStatus { get; set; }
		public string Team { get; set; }
	}

	public class ToolKitWorkPackageModel
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string UserStoryId { get; set; }
		public string CurrentStatus { get; set; }
		public string Team { get; set; }
		public string Release { get; set; }
		public decimal RemainingHours { get; set; }
		public string Assignee { get; set; }
		public string DependOnDefectId { get; set; }
		public string DependOnWPId { get; set; }
		public string WpType { get; set; }
		public string FeatureName { get; set; }
		public string FeatureId { get; set; }
		/// <summary>
		/// Currently no use
		/// </summary>
		public List<PlanningWpModel> PlanningWps { get; set; }
		public bool HasPlan { get; set; }
		public bool IsAvailable
		{
			get
			{
				var result = Constains.Release_1st.Contains(Release) && !Constains.Status_Un_Expected.Contains(this.CurrentStatus);
				return result;
			}
		}
		public bool IsInBuildPhase
		{
			get
			{
				var result = IsAvailable && IsOpen && Constains.WP_TYPE_BuildingPhase.Contains(this.WpType);
				return result;
			}
		}
		public bool IsOpen
		{
			get
			{
				var result = IsAvailable && Constains.WP_Status_Closed != this.CurrentStatus && this.RemainingHours > 0;
				return result;
			}
		}
	}

	public class ToolKitDefectModel
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string UserStoryId { get; } //TODO: how to get from name?
		public string CurrentStatus { get; set; }
		public string Team { get; set; }
		public string Release { get; set; }
		public string Application { get; set; }
	}
}
