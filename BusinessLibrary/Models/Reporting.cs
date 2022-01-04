using BusinessLibrary.Ultilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using BusinessLibrary.Models.Planning;

namespace BusinessLibrary.Models
{
	public class Reporting
	{
		private DateTime _endDate;
		public PlanningApplicationModel GenerateReport(List<ToolKitFeatureModel> featuresFromToolKit
			, List<ToolKitUserStoryModel> userStoriesFromToolKit
			, List<ToolKitWorkPackageModel> workPackagesFromToolKit
			, List<ToolkitAllocationModel> allocationFromToolKit
			, List<ToolkitAllocationAdjustmentModel> allocationAdjustmentFromToolKit
			, DateTime endDate) //TODO: end date should be the last day of the release
		{
			_endDate = endDate;
			var featureOverview = new List<FeatureOverview>();
			var teamFeature = featuresFromToolKit.Where(f => Constains.Release_1st.Contains(f.Release) /*&& f.Application == Constains.Application*/ && !Constains.Status_Un_Expected.Contains(f.CurrentStatus)).ToList();
			var openUserStories = userStoriesFromToolKit.Where(f => !Constains.Status_Un_Expected.Contains(f.CurrentStatus)).ToList();
			var openWorkPackages = workPackagesFromToolKit.Where(f => Constains.Release_1st.Contains(f.Release) && !Constains.Status_Un_Expected.Contains(f.CurrentStatus)).ToList();

			foreach (var feature in teamFeature)
			{
				var expected = new FeatureOverview();
				expected.FeatureId = feature.Id;
				expected.FeatureName = feature.Name;
				expected.FeatureStatus = feature.CurrentStatus;

				//------------------Feature level
				var feature_is_in_Phase1 = Constains.FE_Status_In_Phase1_Initial.Contains(feature.CurrentStatus)
					|| Constains.FE_Status_In_Phase1_Analysis.Contains(feature.CurrentStatus)
					|| Constains.FE_Status_In_Phase1_PO_Review.Contains(feature.CurrentStatus)
					? true
					: false;
				if (feature_is_in_Phase1)
				{
					expected.NextActions.Add(new FeatureNextAction() { Who = Constains.TeamFunctionalTeamLead, DoWhat = "plan to finalize the requirement", Severity = 5 });
					featureOverview.Add(expected);
					continue;
				}

				var feature_is_in_Phase2 = Constains.FE_Status_In_Phase2_Design.Contains(feature.CurrentStatus)
					|| Constains.FE_Status_In_Phase2_Review.Contains(feature.CurrentStatus)
					? true
					: false;

				var feature_is_in_Phase3 = Constains.FE_Status_In_Phase3_Implementation.Contains(feature.CurrentStatus)
					? true
					: false;

				var feature_is_in_Phase4 = Constains.FE_Status_In_Phase4_Testing.Contains(feature.CurrentStatus)
					? true
					: false;


				var stories = openUserStories.Where(us => us.FeatureId == feature.Id).ToList();

				var stories_In_Analysis = openUserStories.Where(us => us.FeatureId == feature.Id && Constains.US_Status_In_Analysis.Contains(us.CurrentStatus)).ToList();
				var stories_In_Design = openUserStories.Where(us => us.FeatureId == feature.Id && Constains.US_Status_In_Analysis.Contains(us.CurrentStatus)).ToList();


				if (!stories.Any()) //TODO: based on status not ready for dev
				{
					expected.NextActions.Add(new FeatureNextAction() { Who = Constains.TeamFunctionalTeamLead, DoWhat = "Create US - there is 0 US", Severity = 5, Id = feature.Id, Level = TicketLevel.Feature });
					featureOverview.Add(expected);
					continue;
				}

				var expectedFeatureStatus = feature.CurrentStatus;
				foreach (var story in stories)
				{
					var wpsUnderUS = openWorkPackages.Where(us => us.UserStoryId == story.Id).ToList();
					if (!wpsUnderUS.Any()) //TODO: based on status not ready for dev
					{
						expected.NextActions.Add(new FeatureNextAction() { Who = Constains.TeamFunctionalTeamLead, DoWhat = $"create some WPs - currently there is 0 WP in US {story.Id}", Severity = 5, Id = story.Id, Level = TicketLevel.UserStory });
						continue;
					}


					//-----------Build phase					
					var buildWps = wpsUnderUS.Where(w => Constains.WP_TYPE_BuildingPhase.Contains(w.WpType)).ToList();
					if (!buildWps.Any())
					{
						//-----------Test phase

					}
					else
					{
						//-----------Build phase ---- all closed
						var builtWps = buildWps.Where(w => Constains.WP_Status_Closed == w.CurrentStatus).ToList();
						if (builtWps.Any() && builtWps.Count() == wpsUnderUS.Count(w => Constains.WP_TYPE_BuildingPhase.Contains(w.WpType))) //TODO: based on status not ready for dev
						{
							if (builtWps.Sum(wp => wp.RemainingHours) > 0)
							{
								expected.NextActions.Add(new FeatureNextAction() { Who = Constains.TeamFunctionalTeamLead, DoWhat = $"Reduce remaining hours to 0 in US {story.Id}", Severity = 1, Id = story.Id, Level = TicketLevel.UserStory });
							}

							if (!Constains.US_Status_Done_For_DEV_AND_Non_Technical.Contains(story.CurrentStatus))
								expected.NextActions.Add(new FeatureNextAction() { Who = Constains.TeamFunctionalTeamLead, DoWhat = $"Update status US {story.Id} because all 'Build' WPs are closed", Severity = 1, Id = story.Id, Level = TicketLevel.UserStory });
						}

						//-----------Build phase ---- WP is not closed
						var buildingWps = buildWps.Where(w => Constains.WP_Status_Closed != w.CurrentStatus).ToList();
						if (buildingWps.Any()) //TODO: based on status not ready for dev
						{
							foreach (var buildingWp in buildingWps)
							{
								var isPossible = buildingWp.CurrentStatus != Constains.WP_Status_Blocked;

								var teamLeader = Constains.TeamFunctionalTeamLead;
								if (buildingWp.Team == Constains.TeamDesign)
								{
									teamLeader = Constains.TeamDesignTeamLead;
								}
								else if (buildingWp.Team == Constains.TeamSS)
								{
									teamLeader = Constains.TeamSSTeamLead;
								}

								if (!string.IsNullOrWhiteSpace(buildingWp.Assignee))
								{
									if (buildingWp.RemainingHours == 0)
										expected.NextActions.Add(new FeatureNextAction(buildingWp.Team) { Who = buildingWp.Assignee, DoWhat = $"Update status WP {buildingWp.Id} in US {story.Id} to Close because remaining hours is 0 but its status is '{buildingWp.CurrentStatus}'", HowLong = buildingWp.RemainingHours, IsPossible = isPossible, Severity = 1, Id = buildingWp.Id, Level = TicketLevel.WorkPackage });
									else
										expected.NextActions.Add(new FeatureNextAction(buildingWp.Team) { Who = buildingWp.Assignee, DoWhat = $"{buildingWp.CurrentStatus} WP {buildingWp.Id} in US {story.Id}", HowLong = buildingWp.RemainingHours, IsPossible = isPossible, Severity = 2, Id = buildingWp.Id, Level = TicketLevel.WorkPackage });
								}
								else //TODO: suggest any name?
								{
									if (buildingWp.RemainingHours == 0)
										expected.NextActions.Add(new FeatureNextAction(buildingWp.Team) { Who = teamLeader, DoWhat = $"Update status WP {buildingWp.Id} in US {story.Id} to Close because remaining hours is 0 but its status is '{buildingWp.CurrentStatus}'", HowLong = buildingWp.RemainingHours, IsPossible = isPossible, Severity = 1, Id = buildingWp.Id, Level = TicketLevel.WorkPackage });
									else
									{
										if (buildingWp.Team == Constains.TeamDesign)
											expected.NextActions.Add(new FeatureNextAction(buildingWp.Team) { Who = teamLeader, DoWhat = $"What else do we need from design in WP {buildingWp.Id} in US {story.Id}", HowLong = buildingWp.RemainingHours, IsPossible = isPossible, Severity = 2, Id = buildingWp.Id, Level = TicketLevel.WorkPackage });
										else
											expected.NextActions.Add(new FeatureNextAction(buildingWp.Team) { Who = teamLeader, DoWhat = $"Find assignee for WP {buildingWp.Id} in US {story.Id}", HowLong = buildingWp.RemainingHours, IsPossible = isPossible, Severity = buildingWp.Team == Constains.TeamSS ? 4 : 2, Id = buildingWp.Id, Level = TicketLevel.WorkPackage });
									}
								}
							}
						}
					}
				}
				featureOverview.Add(expected);
			}
			featureOverview = featureOverview.OrderBy(o => o.Color).ToList();


			//----------------allocation
			var resourceOverview = new List<PeopleAllocation>();

			var allTeamMemberAllocations = allocationFromToolKit.Where(a => Constains.Team_Functional_Development.Contains(a.Team) && a.From >= DateTime.UtcNow || a.To >= DateTime.UtcNow)
				.GroupBy(u => u.Name)
				.Select(grp => grp.ToList())
				.Select(grp => new { key = grp.First().Name, values = grp.ToList() })
				.ToList();

			var allTeamMemberAllocationsNoGroup = allocationFromToolKit.Where(a => Constains.Team_Functional_Development.Contains(a.Team) && a.From >= DateTime.UtcNow || a.To >= DateTime.UtcNow).ToList();
			var teamMemberAllocationAdjustments = allocationAdjustmentFromToolKit.Where(a => Constains.Team_Functional_Development.Contains(a.Team))
				.GroupBy(u => u.Name)
				.Select(grp => grp.ToList())
				.Select(grp => new { key = grp.First().Name, values = grp.ToList() })
				.ToList();
			var allTeamMemberAllocationAdjustments = allocationAdjustmentFromToolKit.Where(a => Constains.Team_Functional_Development.Contains(a.Team) && a.From >= DateTime.UtcNow || a.To >= DateTime.UtcNow).ToList();

			foreach (var teamMember in allTeamMemberAllocations)
			{
				var wpMembers = openWorkPackages.Where(wp => wp.Assignee == teamMember.key).ToList();
				var wpsInBuildPhaseOfMember = wpMembers.Where(f => Constains.WP_Status_Closed != f.CurrentStatus && Constains.WP_TYPE_BuildingPhase.Contains(f.WpType)).ToList();

				var memberAllocations = teamMember.values.Where(v => v.From >= DateTime.UtcNow || v.To >= DateTime.UtcNow).ToList();
				if (!memberAllocations.Any()) continue;

				var resource = new PeopleAllocation(teamMember.key, _endDate, wpsInBuildPhaseOfMember, allTeamMemberAllocationsNoGroup, allTeamMemberAllocationAdjustments, workPackagesFromToolKit);
				resourceOverview.Add(resource);
			}
			//----------------end allocation


			var planning = new PlanningApplicationModel(featuresFromToolKit, userStoriesFromToolKit, workPackagesFromToolKit, resourceOverview);

			//ExportToExcel(featureOverview, resourceOverview, planning, workPackagesFromToolKit);

			return planning;
		}
	}
}
