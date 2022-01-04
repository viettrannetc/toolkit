using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLibrary.Ultilities;

namespace BusinessLibrary.Models.Planning
{
	public class PlanningBasicModel
	{
		public string Id { get; set; }
		public string Name { get; set; }
		/// <summary>
		/// Block or in progress with specific stage
		/// </summary>
		public string Status { get; set; }
		public DateTime? ReleaseDate { get; set; }
	}

	public class PlanningApplicationModel : PlanningBasicModel
	{
		public PlanningApplicationModel()
		{

		}

		public PlanningApplicationModel(List<ToolKitFeatureModel> features
			, List<ToolKitUserStoryModel> userStories
			, List<ToolKitWorkPackageModel> workPackages
			, List<PeopleAllocation> resources)
		{
			Features = new List<PlanningFeatureModel>();
			ResourceData = resources;
			var openFeatures = features.Where(f => /*Constains.Release_1st.Contains(f.Release) && f.Application == Constains.Application && */ !Constains.WP_Status_Un_Expected_For_Planning.Contains(f.CurrentStatus)).ToList();
			var openUserStories = userStories.Where(f => !Constains.WP_Status_Un_Expected_For_Planning.Contains(f.CurrentStatus)).ToList();
			var openWorkPackages = workPackages.Where(f => /*Constains.Release_1st.Contains(f.Release) &&*/ !Constains.WP_Status_Un_Expected_For_Planning.Contains(f.CurrentStatus)).ToList();
			//var devWorkPackages = openWorkPackages.Where(f => Constains.Team_Functional_Development.Contains(f.Team)).ToList();
			var devWorkPackages = openWorkPackages.ToList();
			var devBuildWorkPackages = devWorkPackages.Where(w => Constains.WP_TYPE_BuildingPhase.Contains(w.WpType)).ToList();

			foreach (var feature in openFeatures)
			{
				var planningFeature = new PlanningFeatureModel
				{
					Id = feature.Id,
					Name = feature.Name,
					Status = feature.CurrentStatus
				};

				var uss = openUserStories.Where(us => us.FeatureId == feature.Id).ToList();
				foreach (var us in uss)
				{
					var planningUs = new PlanningUserStoryModel();
					var wps = devBuildWorkPackages.Where(wp => wp.UserStoryId == us.Id).ToList();
					planningUs.Id = us.Id;
					planningUs.Name = us.Name;
					planningUs.Status = us.CurrentStatus;

					foreach (var wp in wps)
					{
						var planningWp = new PlanningWpModel();
						planningWp.Id = wp.Id;
						planningWp.Name = wp.Name;
						planningWp.Status = wp.CurrentStatus;
						planningWp.Team = wp.Team;
						planningWp.FeatureName = feature.Name;
						planningWp.ReleaseDate(resources.FirstOrDefault(r => r.Name == wp.Assignee));
						planningUs.Wps.Add(planningWp);
					}

					planningUs.ReleaseDate();
					planningFeature.UserStories.Add(planningUs);
				}
				planningFeature.ReleaseDate();
				Features.Add(planningFeature);
			}
		}

		public List<PlanningFeatureModel> Features { get; set; }
		public List<PeopleAllocation> ResourceData { get; set; }
	}

	public class PlanningFeatureModel : PlanningBasicModel
	{
		public PlanningFeatureModel()
		{
			UserStories = new List<PlanningUserStoryModel>();
		}
		public List<PlanningUserStoryModel> UserStories { get; set; }
		public int Order { get; set; }
	}

	public class PlanningUserStoryModel : PlanningBasicModel
	{
		public PlanningUserStoryModel()
		{
			Wps = new List<PlanningWpModel>();
		}
		public List<PlanningWpModel> Wps { get; set; }
	}

	public class PlanningWpModel : PlanningBasicModel
	{
		public decimal RemainingHours { get; set; }
		public string Team { get; set; }
		public string FeatureName { get; set; }
	}
}