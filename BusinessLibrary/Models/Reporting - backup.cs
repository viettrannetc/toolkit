//using OfficeOpenXml;
//using OfficeOpenXml.Style;
//using BusinessLibrary.Ultilities;
//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.IO;
//using System.Linq;
//using System.Text;
//using BusinessLibrary.Models.Planning;

//namespace BusinessLibrary.Models
//{
//	public class Reporting
//	{
//		private DateTime EndDate = new DateTime(2021, 11, 30);
//		public void GenerateReport(List<Feature> featuresFromToolKit
//			, List<UserStory> userStoriesFromToolKit
//			, List<WorkPackage> workPackagesFromToolKit
//			, List<Allocation> allocationFromToolKit
//			, List<AllocationAdjustment> allocationAdjustmentFromToolKit
//			)
//		{

//			var featureOverview = new List<FeatureOverview>();
//			var teamFeature = featuresFromToolKit.Where(f => Constains.Release_1st.Contains(f.Release) && f.Application == Constains.Application && !Constains.Status_Un_Expected.Contains(f.CurrentStatus)).ToList();
//			var openUserStories = userStoriesFromToolKit.Where(f => !Constains.Status_Un_Expected.Contains(f.CurrentStatus)).ToList();
//			var openWorkPackages = workPackagesFromToolKit.Where(f => Constains.Release_1st.Contains(f.Release) && !Constains.Status_Un_Expected.Contains(f.CurrentStatus)).ToList();

//			foreach (var feature in teamFeature)
//			{
//				var expected = new FeatureOverview();
//				expected.FeatureId = feature.Id;
//				expected.FeatureName = feature.Name;
//				expected.FeatureStatus = feature.CurrentStatus;

//				//------------------Feature level
//				var feature_is_in_Phase1 = Constains.FE_Status_In_Phase1_Initial.Contains(feature.CurrentStatus)
//					|| Constains.FE_Status_In_Phase1_Analysis.Contains(feature.CurrentStatus)
//					|| Constains.FE_Status_In_Phase1_PO_Review.Contains(feature.CurrentStatus)
//					? true
//					: false;
//				if (feature_is_in_Phase1)
//				{
//					expected.NextActions.Add(new FeatureNextAction() { Who = Constains.TeamFunctionalTeamLead, DoWhat = "plan to finallize the requirement", Severity = 5 });
//					featureOverview.Add(expected);
//					continue;
//				}

//				var feature_is_in_Phase2 = Constains.FE_Status_In_Phase2_Design.Contains(feature.CurrentStatus)
//					|| Constains.FE_Status_In_Phase2_Review.Contains(feature.CurrentStatus)
//					? true
//					: false;

//				var feature_is_in_Phase3 = Constains.FE_Status_In_Phase3_Implementation.Contains(feature.CurrentStatus)
//					? true
//					: false;

//				var feature_is_in_Phase4 = Constains.FE_Status_In_Phase4_Testing.Contains(feature.CurrentStatus)
//					? true
//					: false;


//				var stories = openUserStories.Where(us => us.FeatureId == feature.Id).ToList();

//				var stories_In_Analysis = openUserStories.Where(us => us.FeatureId == feature.Id && Constains.US_Status_In_Analysis.Contains(us.CurrentStatus)).ToList();
//				var stories_In_Design = openUserStories.Where(us => us.FeatureId == feature.Id && Constains.US_Status_In_Analysis.Contains(us.CurrentStatus)).ToList();


//				if (!stories.Any()) //TODO: based on status not ready for dev
//				{
//					expected.NextActions.Add(new FeatureNextAction() { Who = Constains.TeamFunctionalTeamLead, DoWhat = "Create US - there is 0 US", Severity = 5, Id = feature.Id, Level = TicketLevel.Feature });
//					featureOverview.Add(expected);
//					continue;
//				}

//				var expectedFeatureStatus = feature.CurrentStatus;
//				foreach (var story in stories)
//				{
//					var wpsUnderUS = openWorkPackages.Where(us => us.UserStoryId == story.Id).ToList();
//					if (!wpsUnderUS.Any()) //TODO: based on status not ready for dev
//					{
//						expected.NextActions.Add(new FeatureNextAction() { Who = Constains.TeamFunctionalTeamLead, DoWhat = $"create some WPs - currently there is 0 WP in US {story.Id}", Severity = 5, Id = story.Id, Level = TicketLevel.UserStory });
//						continue;
//					}


//					//-----------Build phase					
//					var buildWps = wpsUnderUS.Where(w => Constains.WP_TYPE_BuildingPhase.Contains(w.WpType)).ToList();
//					if (!buildWps.Any())
//					{
//						//-----------Test phase

//					}
//					else
//					{
//						//-----------Build phase ---- all closed
//						var builtWps = buildWps.Where(w => Constains.WP_Status_Closed == w.CurrentStatus).ToList();
//						if (builtWps.Any() && builtWps.Count() == wpsUnderUS.Count(w => Constains.WP_TYPE_BuildingPhase.Contains(w.WpType))) //TODO: based on status not ready for dev
//						{
//							if (builtWps.Sum(wp => wp.RemainingHours) > 0)
//							{
//								expected.NextActions.Add(new FeatureNextAction() { Who = Constains.TeamFunctionalTeamLead, DoWhat = $"Reduce remaining hours to 0 in US {story.Id}", Severity = 1, Id = story.Id, Level = TicketLevel.UserStory });
//							}

//							if (!Constains.US_Status_Done_For_DEV_AND_Non_Technical.Contains(story.CurrentStatus))
//								expected.NextActions.Add(new FeatureNextAction() { Who = Constains.TeamFunctionalTeamLead, DoWhat = $"Update status US {story.Id} because all 'Build' WPs are closed", Severity = 1, Id = story.Id, Level = TicketLevel.UserStory });
//						}

//						//-----------Build phase ---- WP is not closed
//						var buildingWps = buildWps.Where(w => Constains.WP_Status_Closed != w.CurrentStatus).ToList();
//						if (buildingWps.Any()) //TODO: based on status not ready for dev
//						{
//							foreach (var buildingWp in buildingWps)
//							{
//								var isPossible = buildingWp.CurrentStatus != Constains.WP_Status_Blocked;

//								var teamLeader = Constains.TeamFunctionalTeamLead;
//								if (buildingWp.Team == Constains.TeamDesign)
//								{
//									teamLeader = Constains.TeamDesignTeamLead;
//								}
//								else if (buildingWp.Team == Constains.TeamSS)
//								{
//									teamLeader = Constains.TeamSSTeamLead;
//								}

//								if (!string.IsNullOrWhiteSpace(buildingWp.Assignee))
//								{
//									if (buildingWp.RemainingHours == 0)
//										expected.NextActions.Add(new FeatureNextAction(buildingWp.Team) { Who = buildingWp.Assignee, DoWhat = $"Update status WP {buildingWp.Id} in US {story.Id} to Close because remaining hours is 0 but its status is '{buildingWp.CurrentStatus}'", HowLong = buildingWp.RemainingHours, IsPossible = isPossible, Severity = 1, Id = buildingWp.Id, Level = TicketLevel.WorkPackage });
//									else
//										expected.NextActions.Add(new FeatureNextAction(buildingWp.Team) { Who = buildingWp.Assignee, DoWhat = $"{buildingWp.CurrentStatus} WP {buildingWp.Id} in US {story.Id}", HowLong = buildingWp.RemainingHours, IsPossible = isPossible, Severity = 2, Id = buildingWp.Id, Level = TicketLevel.WorkPackage });
//								}
//								else //TODO: suggest any name?
//								{
//									if (buildingWp.RemainingHours == 0)
//										expected.NextActions.Add(new FeatureNextAction(buildingWp.Team) { Who = teamLeader, DoWhat = $"Update status WP {buildingWp.Id} in US {story.Id} to Close because remaining hours is 0 but its status is '{buildingWp.CurrentStatus}'", HowLong = buildingWp.RemainingHours, IsPossible = isPossible, Severity = 1, Id = buildingWp.Id, Level = TicketLevel.WorkPackage });
//									else
//									{
//										if (buildingWp.Team == Constains.TeamDesign)
//											expected.NextActions.Add(new FeatureNextAction(buildingWp.Team) { Who = teamLeader, DoWhat = $"What else do we need from design in WP {buildingWp.Id} in US {story.Id}", HowLong = buildingWp.RemainingHours, IsPossible = isPossible, Severity = 2, Id = buildingWp.Id, Level = TicketLevel.WorkPackage });
//										else
//											expected.NextActions.Add(new FeatureNextAction(buildingWp.Team) { Who = teamLeader, DoWhat = $"Find assignee for WP {buildingWp.Id} in US {story.Id}", HowLong = buildingWp.RemainingHours, IsPossible = isPossible, Severity = buildingWp.Team == Constains.TeamSS ? 4 : 2, Id = buildingWp.Id, Level = TicketLevel.WorkPackage });
//									}
//								}
//							}
//						}
//					}
//				}
//				featureOverview.Add(expected);
//			}
//			featureOverview = featureOverview.OrderBy(o => o.Color).ToList();


//			//----------------allocation
//			var resourceOverview = new List<PeopleAllocation>();

//			var allTeamMemberAllocations = allocationFromToolKit.Where(a => Constains.Team_Functional_Development.Contains(a.Team) && a.From >= DateTime.UtcNow || a.To >= DateTime.UtcNow)
//				.GroupBy(u => u.Name)
//				.Select(grp => grp.ToList())
//				.Select(grp => new { key = grp.First().Name, values = grp.ToList() })
//				.ToList();

//			var allTeamMemberAllocationsNoGroup = allocationFromToolKit.Where(a => Constains.Team_Functional_Development.Contains(a.Team) && a.From >= DateTime.UtcNow || a.To >= DateTime.UtcNow).ToList();
//			var teamMemberAllocationAdjustments = allocationAdjustmentFromToolKit.Where(a => Constains.Team_Functional_Development.Contains(a.Team))
//				.GroupBy(u => u.Name)
//				.Select(grp => grp.ToList())
//				.Select(grp => new { key = grp.First().Name, values = grp.ToList() })
//				.ToList();
//			var allTeamMemberAllocationAdjustments = allocationAdjustmentFromToolKit.Where(a => Constains.Team_Functional_Development.Contains(a.Team) && a.From >= DateTime.UtcNow || a.To >= DateTime.UtcNow).ToList();

//			foreach (var teamMember in allTeamMemberAllocations)
//			{
//				var wpMembers = openWorkPackages.Where(wp => wp.Assignee == teamMember.key).ToList();
//				var wpsInBuildPhaseOfMember = wpMembers.Where(f => Constains.WP_Status_Closed != f.CurrentStatus && Constains.WP_TYPE_BuildingPhase.Contains(f.WpType)).ToList();

//				var memberAllocations = teamMember.values.Where(v => v.From >= DateTime.UtcNow || v.To >= DateTime.UtcNow).ToList();
//				if (!memberAllocations.Any()) continue;

//				var resource = new PeopleAllocation(teamMember.key, EndDate, wpsInBuildPhaseOfMember, allTeamMemberAllocationsNoGroup, allTeamMemberAllocationAdjustments, workPackagesFromToolKit);
//				resourceOverview.Add(resource);
//			}
//			//----------------end allocation


//			var planning = new PlanningApplicationModel(featuresFromToolKit, userStoriesFromToolKit, workPackagesFromToolKit, resourceOverview);

//			ExportToExcel(featureOverview, resourceOverview, planning, workPackagesFromToolKit);



//			Console.WriteLine($"");
//		}

//		public void ExportToExcel(List<FeatureOverview> feature, List<PeopleAllocation> resource, PlanningApplicationModel planning, List<WorkPackage> allWorkPackagesFromToolKit)
//		{
//			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
//			string excelName = $"{Guid.NewGuid()}";
//			string filePath = $@"C:\Projects\Personal\Sharepoint\Sharepoint\{excelName}.xlsx";

//			using (ExcelPackage excel = new ExcelPackage(new FileInfo(filePath)))
//			{
//				ExtractFeatures(feature, excel);
//				ExtractResourceSuggestion(resource, excel);
//				ExtractPlanningAtLevelOfDetails(planning, excel);
//				ExtractPlanningV1(planning, excel, allWorkPackagesFromToolKit);
//				ExtractPlanningV2(planning, excel, allWorkPackagesFromToolKit);
//			}
//		}

//		private void ExtractPlanningAtLevelOfDetails(PlanningApplicationModel planning, ExcelPackage excel)
//		{
//			var workSheet = excel.Workbook.Worksheets.Add("Planning");
//			workSheet.TabColor = Color.Black;
//			workSheet.DefaultRowHeight = 12;
//			//Header of table
//			workSheet.Row(1).Height = 20;
//			workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
//			workSheet.Row(1).Style.Font.Bold = true;

//			workSheet.Cells[1, 1].Value = "Feature";
//			workSheet.Cells[1, 2].Value = "Release Date";
//			workSheet.Cells[1, 3].Value = "User story";
//			workSheet.Cells[1, 4].Value = "Work Package";
//			workSheet.Cells[1, 5].Value = "Team";
//			workSheet.Cells[1, 6].Value = "Implemented date";

//			int recordIndex = 2;
//			foreach (var feature in planning.Features)
//			{
//				if (Constains.FE_Status_For_Planning.Contains(feature.Status))
//				{
//					workSheet.Cells[recordIndex, 1].Value = feature.Name;
//					workSheet.Cells[recordIndex, 2].Value = "Released";
//					workSheet.Cells[recordIndex, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
//					workSheet.Cells[recordIndex, 2].Style.Fill.BackgroundColor.SetColor(Constains.FE_Good_Color);
//					recordIndex++;
//					continue;
//				}

//				workSheet.Cells[recordIndex, 1].Value = feature.Name;
//				workSheet.Cells[recordIndex, 2].Value = feature.ReleaseDate.HasValue ? feature.ReleaseDate.Value.ToShortDateString() : feature.Status;

//				if (!feature.UserStories.Any())
//				{
//					recordIndex++;
//					continue;
//				}

//				foreach (var userStory in feature.UserStories)
//				{
//					if (!userStory.Wps.Any())
//					{
//						//recordIndex++;
//						continue;
//					}

//					workSheet.Cells[recordIndex, 3].Value = userStory.Name;
//					foreach (var wp in userStory.Wps)
//					{
//						workSheet.Cells[recordIndex, 4].Value = wp.Name;
//						workSheet.Cells[recordIndex, 5].Value = wp.Team;
//						workSheet.Cells[recordIndex, 6].Value = wp.ReleaseDate.HasValue ? wp.ReleaseDate.Value.ToShortDateString() : wp.Status;
//						recordIndex++;
//					}
//				}
//			}

//			workSheet.Column(1).AutoFit();
//			workSheet.Column(2).AutoFit();
//			workSheet.Column(3).AutoFit();
//			workSheet.Column(4).AutoFit();
//			workSheet.Column(5).AutoFit();
//			workSheet.Column(6).AutoFit();
//			workSheet.Column(7).AutoFit();
//			workSheet.Column(8).AutoFit();

//			excel.Save();
//		}

//		private void ExtractResourceSuggestion(List<PeopleAllocation> resource, ExcelPackage excel)
//		{
//			var workSheet = excel.Workbook.Worksheets.Add("Resource");
//			workSheet.TabColor = Color.Black;
//			workSheet.DefaultRowHeight = 12;
//			//Header of table
//			workSheet.Row(1).Height = 20;
//			workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
//			workSheet.Row(1).Style.Font.Bold = true;
//			workSheet.Cells[1, 1].Value = "Name";
//			workSheet.Cells[1, 2].Value = "Allocated";
//			workSheet.Cells[1, 3].Value = "Allocated blocked";
//			workSheet.Cells[1, 4].Value = "Allocated available";
//			workSheet.Cells[1, 5].Value = "Have nothing to do from";
//			workSheet.Cells[1, 6].Value = "Total Available";
//			workSheet.Cells[1, 7].Value = "Free hours";
//			workSheet.Cells[1, 8].Value = "From now til";
//			workSheet.Cells[1, 9].Value = "Team";

//			var lastDate = resource.SelectMany(r => r.ScheduleForWorking).GroupBy(s => s.Date).Select(s => new { Key = s.Key }).OrderByDescending(s => s.Key).FirstOrDefault();

//			int recordIndex = 2;

//			var resourceOrderedByDate = resource.OrderBy(r => r.AvailableFrom.Date.Date).ToList();
//			foreach (var row in resourceOrderedByDate)
//			{
//				workSheet.Cells[recordIndex, 1].Value = row.Name;
//				workSheet.Cells[recordIndex, 2].Value = row.HoursAllocatedInTotal;
//				workSheet.Cells[recordIndex, 3].Value = row.HoursAllocatedBlocked;
//				workSheet.Cells[recordIndex, 4].Value = row.HoursAllocatedUnBlocked;
//				workSheet.Cells[recordIndex, 5].Value = row.AvailableFrom.Date.ToShortDateString();
//				workSheet.Cells[recordIndex, 6].Value = row.HoursAvailable;
//				workSheet.Cells[recordIndex, 7].Value = row.HoursNonWork;
//				workSheet.Cells[recordIndex, 8].Value = EndDate.Date.ToShortDateString();
//				workSheet.Cells[recordIndex, 9].Value = row.Team;

//				recordIndex++;
//			}

//			workSheet.Column(1).AutoFit();
//			workSheet.Column(2).AutoFit();
//			workSheet.Column(3).AutoFit();
//			workSheet.Column(4).AutoFit();
//			workSheet.Column(5).AutoFit();
//			workSheet.Column(6).AutoFit();
//			workSheet.Column(7).AutoFit();
//			workSheet.Column(8).AutoFit();

//			excel.Save();
//		}

//		private static void ExtractFeatures(List<FeatureOverview> data, ExcelPackage excel)
//		{
//			var workSheet = excel.Workbook.Worksheets.Add("Features");
//			workSheet.TabColor = System.Drawing.Color.Black;
//			workSheet.DefaultRowHeight = 12;
//			//Header of table
//			workSheet.Row(1).Height = 20;
//			workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
//			workSheet.Row(1).Style.Font.Bold = true;
//			workSheet.Cells[1, 1].Value = "Feature";

//			workSheet.Cells[1, 2].Value = "Status";
//			workSheet.Cells[1, 3].Value = "Next action";
//			workSheet.Cells[1, 4].Value = "Who";
//			workSheet.Cells[1, 5].Value = "Remaining hours";
//			workSheet.Cells[1, 6].Value = "Blocked?";
//			workSheet.Cells[1, 7].Value = "Team";
//			workSheet.Cells[1, 8].Value = "URL";

//			int recordIndex = 2;
//			foreach (var row in data)
//			{
//				if (row.NextActions.Any())
//				{
//					foreach (var nextAction in row.NextActions)
//					{
//						workSheet.Cells[recordIndex, 1].Value = $"{row.FeatureName} ({row.FeatureId})";
//						workSheet.Cells[recordIndex, 2].Value = row.FeatureStatus;
//						workSheet.Cells[recordIndex, 3].Value = nextAction.DoWhat;
//						workSheet.Cells[recordIndex, 4].Value = nextAction.Who;
//						workSheet.Cells[recordIndex, 5].Value = nextAction.HowLong;
//						workSheet.Cells[recordIndex, 6].Value = nextAction.IsPossible ? "" : "Blocked";
//						workSheet.Cells[recordIndex, 7].Value = nextAction.Team;
//						workSheet.Cells[recordIndex, 8].Value = nextAction.URL;
//						recordIndex++;
//					}
//				}
//				else
//				{
//					workSheet.Cells[recordIndex, 1].Value = $"{row.FeatureName} ({row.FeatureId})";
//					workSheet.Cells[recordIndex, 2].Value = row.FeatureStatus;
//					workSheet.Cells[recordIndex, 5].Value = 0;
//					recordIndex++;
//				}
//			}
//			workSheet.Column(1).AutoFit();
//			workSheet.Column(2).AutoFit();
//			workSheet.Column(3).AutoFit();
//			workSheet.Column(4).AutoFit();
//			workSheet.Column(5).AutoFit();
//			workSheet.Column(6).AutoFit();
//			workSheet.Column(7).AutoFit();
//			workSheet.Column(8).AutoFit();
//			excel.Save();
//		}

//		/// <summary>
//		/// V1 presents the data at the high level with date
//		/// </summary>
//		/// <param name="planning"></param>
//		/// <param name="excel"></param>
//		private void ExtractPlanningV1(PlanningApplicationModel planningDrawData, ExcelPackage excel, List<WorkPackage> allWorkPackagesFromToolKit)
//		{
//			var workSheet = excel.Workbook.Worksheets.Add("Planning-High Level");
//			workSheet.TabColor = Color.Black;
//			workSheet.DefaultRowHeight = 12;


//			#region //-------------Header of table
//			workSheet.Row(1).Height = 20;
//			workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
//			workSheet.Row(1).Style.Font.Bold = true;
//			workSheet.Cells[1, 1].Value = "Feature";

//			var days = (EndDate - DateTime.UtcNow).Days;
//			for (int i = 0; i <= days - 1; i++)
//			{
//				workSheet.Cells[1, i + 2].Value = DateTime.UtcNow.AddDays(i).Date.ToShortDateString();
//			}
//			#endregion

//			var planningForRunningWps = new List<PlanningWpModel>();
//			foreach (var f in planningDrawData.Features)
//			{
//				foreach (var us in f.UserStories)
//				{
//					planningForRunningWps.AddRange(us.Wps);
//				}
//			}
//			var planningForRunningWpsFromFeModels = planningForRunningWps.GroupBy(s => s.FeatureName).Select(s => new { Key = s.Key, Values = s.ToList() }).ToList();
//			int recordIndex = 2;
//			planningDrawData.Features = planningDrawData.Features.OrderBy(f => f.Status).ToList();
//			var schduleForRunningFeatures = planningDrawData.ResourceData.SelectMany(rs => rs.ScheduleForWorking).ToList();

//			foreach (var f in planningDrawData.Features)
//			{
//				var feature = planningForRunningWpsFromFeModels.FirstOrDefault(fe => fe.Key == f.Name);

//				#region if features are implemented or released
//				if (feature == null)
//				{
//					workSheet.Cells[recordIndex, 1].Value = f.Status == Constains.FE_Status_DevImplemented_38
//						? $"{f.Name} ({f.Id}) - Implemented"
//						: $"{f.Name} ({f.Id}) - Released";
//					workSheet.Cells[recordIndex, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
//					workSheet.Cells[recordIndex, 1].Style.Fill.BackgroundColor.SetColor(Constains.TeamQA_Color);
//					recordIndex++;
//					continue;
//				}
//				#endregion

//				#region Main function - Features haven't released yet, need the overview release plan for them
//				var teams = feature.Values.GroupBy(s => s.Team).Select(s => new { Key = s.Key, Values = s.ToList() }).ToList();
//				foreach (var team in teams)
//				{
//					//Get all existing WPs
//					//Get scheduled WPs
//					//if all of existing WPs are scheduled, then it's fine, otherwise; there is no release plan
//					var wps = team.Values;
//					var wpSchedule = schduleForRunningFeatures.Where(s => wps.Select(wp => wp.Id).Contains(s.WpId)).ToList();
//					var allExistingWps = allWorkPackagesFromToolKit.Where(w => w.FeatureName == f.Name && w.Team == team.Key && w.IsInBuildPhase).ToList();
//					var nonPlannedWps = allExistingWps.Where(w => !w.HasPlan).ToList();

//					StringBuilder featureDescription = new StringBuilder();
//					featureDescription.AppendLine($"{feature.Key} - {team.Key}");

//					if (nonPlannedWps.Any())
//					{
//						//featureDescription.AppendLine($"No plan {nonPlannedWps.Sum(w => w.RemainingHours)} hrs:");
//						//foreach (var nonPlannedWp in nonPlannedWps)
//						//{
//						//	var assignee = string.IsNullOrWhiteSpace(nonPlannedWp.Assignee) ? "No assignee" : nonPlannedWp.Assignee;
//						//	featureDescription.AppendLine($"WP-{nonPlannedWp.Id}-{nonPlannedWp.RemainingHours} hrs-{assignee}-{nonPlannedWp.CurrentStatus}");
//						//}

//						featureDescription.Append(" - No plan");

//						workSheet.Cells[recordIndex, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
//						workSheet.Cells[recordIndex, 1].Style.Fill.BackgroundColor.SetColor(Constains.FE_Risk_Color);
//					}

//					workSheet.Cells[recordIndex, 1].Value = featureDescription.ToString();
//					if (!wpSchedule.Any())
//					{
//						recordIndex++;
//						continue;
//					}

//					for (int i = 0; i <= days - 1; i++)
//					{
//						var dateOnTimeLine = DateTime.UtcNow.AddDays(i);
//						var wpScheduledInDate = wpSchedule.Where(wp => wp.Date.Date == dateOnTimeLine.Date).ToList();

//						if (wpScheduledInDate.Any())
//						{
//							workSheet.Cells[recordIndex, i + 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
//							if (Constains.Team_Functional.Contains(team.Key))
//							{
//								workSheet.Cells[recordIndex, i + 2].Style.Fill.BackgroundColor.SetColor(Constains.TeamFUN_Color);
//							}
//							if (Constains.Team_SS.Contains(team.Key))
//							{
//								workSheet.Cells[recordIndex, i + 2].Style.Fill.BackgroundColor.SetColor(Constains.TeamSS_Color);
//							}

//							//StringBuilder detail = new StringBuilder();
//							//foreach (var wp in wpScheduledInDate)
//							//{
//							//	detail.AppendLine($"WP-{wp.WpId} - {wp.Worker} ({wp.WorkingHours})");
//							//}

//							//workSheet.Cells[recordIndex, i + 2].Value = detail.ToString();
//						}
//					}
//					recordIndex++;
//				}
//				#endregion
//			}

//			recordIndex++;
//			recordIndex++;
//			recordIndex++;

//			workSheet.Cells[recordIndex, 1].Value = Constains.TeamFunctional;
//			workSheet.Cells[recordIndex, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
//			workSheet.Cells[recordIndex, 2].Style.Fill.BackgroundColor.SetColor(Constains.TeamFUN_Color);

//			recordIndex++;
//			workSheet.Cells[recordIndex, 1].Value = Constains.TeamSS;
//			workSheet.Cells[recordIndex, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
//			workSheet.Cells[recordIndex, 2].Style.Fill.BackgroundColor.SetColor(Constains.TeamSS_Color);

//			recordIndex++;
//			workSheet.Cells[recordIndex, 1].Value = Constains.TeamQA;
//			workSheet.Cells[recordIndex, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
//			workSheet.Cells[recordIndex, 2].Style.Fill.BackgroundColor.SetColor(Constains.TeamQA_Color);

//			workSheet.Column(1).AutoFit();

//			excel.Save();
//		}

//		/// <summary>
//		/// See the allocated on each WP and Day 
//		/// </summary>
//		/// <param name="planningDrawData"></param>
//		/// <param name="excel"></param>
//		private void ExtractPlanningV2(PlanningApplicationModel planningDrawData, ExcelPackage excel, List<WorkPackage> allWorkPackagesFromToolKit)
//		{
//			var workSheet = excel.Workbook.Worksheets.Add("Planning-Details");
//			workSheet.TabColor = Color.Black;
//			workSheet.DefaultRowHeight = 12;

//			#region //-------------Header of table
//			workSheet.Row(1).Height = 20;
//			workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
//			workSheet.Row(1).Style.Font.Bold = true;
//			workSheet.Cells[1, 1].Value = "Feature";

//			var days = (EndDate - DateTime.UtcNow).Days;
//			for (int i = 0; i <= days - 1; i++)
//			{
//				workSheet.Cells[1, i + 2].Value = DateTime.UtcNow.AddDays(i).Date.ToShortDateString();
//			}
//			#endregion

//			var planningForRunningWps = new List<PlanningWpModel>();
//			foreach (var f in planningDrawData.Features)
//			{
//				foreach (var us in f.UserStories)
//				{
//					planningForRunningWps.AddRange(us.Wps);
//				}
//			}
//			var planningForRunningWpsFromFeModels = planningForRunningWps.GroupBy(s => s.FeatureName).Select(s => new { Key = s.Key, Values = s.ToList() }).ToList();
//			int recordIndex = 2;
//			planningDrawData.Features = planningDrawData.Features.OrderBy(f => f.Status).ToList();
//			var schduleForRunningFeatures = planningDrawData.ResourceData.SelectMany(rs => rs.ScheduleForWorking).ToList();
//			planningDrawData.Features.Order();
//			foreach (var f in planningDrawData.Features)
//			{
//				var feature = planningForRunningWpsFromFeModels.FirstOrDefault(fe => fe.Key == f.Name);

//				#region if features are implemented or released
//				if (feature == null)
//				{
//					workSheet.Cells[recordIndex, 1].Value = f.Status == Constains.FE_Status_DevImplemented_38
//						? $"{f.Name} ({f.Id}) - Implemented"
//						: $"{f.Name} ({f.Id}) - Released";
//					workSheet.Cells[recordIndex, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
//					workSheet.Cells[recordIndex, 1].Style.Fill.BackgroundColor.SetColor(Constains.TeamQA_Color);
//					recordIndex++;
//					continue;
//				}
//				#endregion

//				#region Main function - Features haven't released yet, need the overview release plan for them
//				var teams = feature.Values.GroupBy(s => s.Team).Select(s => new { Key = s.Key, Values = s.ToList() }).ToList();
//				foreach (var team in teams)
//				{
//					//Get all existing WPs
//					//Get scheduled WPs
//					//if all of existing WPs are scheduled, then it's fine, otherwise; there is no release plan
//					var wps = team.Values;
//					var wpSchedule = schduleForRunningFeatures.Where(s => wps.Select(wp => wp.Id).Contains(s.WpId)).ToList();
//					var allExistingWps = allWorkPackagesFromToolKit.Where(w => w.FeatureName == f.Name && w.Team == team.Key && w.IsInBuildPhase).ToList();
//					var nonPlannedWps = allExistingWps.Where(w => !w.HasPlan).ToList();

//					StringBuilder featureDescription = new StringBuilder();
//					featureDescription.AppendLine($"{feature.Key} - {team.Key}");

//					if (nonPlannedWps.Any())
//					{
//						featureDescription.AppendLine($"No plan {nonPlannedWps.Sum(w => w.RemainingHours)} hrs:");
//						foreach (var nonPlannedWp in nonPlannedWps)
//						{
//							var assignee = string.IsNullOrWhiteSpace(nonPlannedWp.Assignee) ? "No assignee" : nonPlannedWp.Assignee;
//							featureDescription.AppendLine($"WP-{nonPlannedWp.Id}-{nonPlannedWp.RemainingHours} hrs-{assignee}-{nonPlannedWp.CurrentStatus}");
//						}

//						workSheet.Cells[recordIndex, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
//						workSheet.Cells[recordIndex, 1].Style.Fill.BackgroundColor.SetColor(Constains.FE_Risk_Color);
//					}

//					workSheet.Cells[recordIndex, 1].Value = featureDescription.ToString();
//					if (!wpSchedule.Any())
//					{
//						recordIndex++;
//						continue;
//					}

//					for (int i = 0; i <= days - 1; i++)
//					{
//						var dateOnTimeLine = DateTime.UtcNow.AddDays(i);
//						var wpScheduledInDate = wpSchedule.Where(wp => wp.Date.Date == dateOnTimeLine.Date).ToList();

//						if (wpScheduledInDate.Any())
//						{
//							workSheet.Cells[recordIndex, i + 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
//							if (Constains.Team_Functional.Contains(team.Key))
//							{
//								workSheet.Cells[recordIndex, i + 2].Style.Fill.BackgroundColor.SetColor(Constains.TeamFUN_Color);
//							}
//							if (Constains.Team_SS.Contains(team.Key))
//							{
//								workSheet.Cells[recordIndex, i + 2].Style.Fill.BackgroundColor.SetColor(Constains.TeamSS_Color);
//							}

//							StringBuilder detail = new StringBuilder();
//							foreach (var wp in wpScheduledInDate)
//							{
//								detail.AppendLine($"WP-{wp.WpId} - {wp.Name} ({wp.WorkingHours})");
//							}

//							workSheet.Cells[recordIndex, i + 2].Value = detail.ToString();
//						}
//					}
//					recordIndex++;
//				}
//				#endregion
//			}

//			recordIndex++;
//			workSheet.Cells[recordIndex, 1].Value = "Bug fixing";

//			var scheduleForAvailableHours = new List<ResourceFree>();
//			planningDrawData.ResourceData.ForEach(r =>
//			{
//				scheduleForAvailableHours.AddRange(r.ScheduleForAvailable);
//			});
//			//var scheduleForAvailableHours = planningDrawData.ResourceData.SelectMany(r => r.ScheduleForAvailable).ToList();
//			var bugfixings = scheduleForAvailableHours
//				.GroupBy(u => u.Date)
//				.Select(grp => grp.ToList())
//				.Select(grp => new { Date = grp.First().Date.Date, AvailableResources = grp.ToList() })
//				.ToList();

//			for (int i = 0; i <= days - 1; i++)
//			{
//				var dateOnTimeLine = DateTime.UtcNow.AddDays(i);
//				var availableResource = bugfixings.FirstOrDefault(b => b.Date == dateOnTimeLine.Date);

//				if (availableResource != null && availableResource.AvailableResources.Any())
//				{
//					//workSheet.Cells[recordIndex, i + 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
//					//if (Constains.Team_Functional.Contains(team.Key))
//					//{
//					//	workSheet.Cells[recordIndex, i + 2].Style.Fill.BackgroundColor.SetColor(Constains.TeamFUN_Color);
//					//}
//					//if (Constains.Team_SS.Contains(team.Key))
//					//{
//					//	workSheet.Cells[recordIndex, i + 2].Style.Fill.BackgroundColor.SetColor(Constains.TeamSS_Color);
//					//}

//					StringBuilder detail = new StringBuilder();
//					foreach (var resource in availableResource.AvailableResources)
//					{
//						if (Constains.Team_Functional.Contains(resource.Team) && !Constains.Team_QA_Members.Contains(resource.Name))
//							detail.AppendLine($"{resource.Name} ({resource.FreeHours})");
//					}

//					workSheet.Cells[recordIndex, i + 2].Value = detail.ToString();
//				}
//			}


//			recordIndex++;
//			recordIndex++;
//			recordIndex++;

//			workSheet.Cells[recordIndex, 1].Value = Constains.TeamFunctional;
//			workSheet.Cells[recordIndex, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
//			workSheet.Cells[recordIndex, 2].Style.Fill.BackgroundColor.SetColor(Constains.TeamFUN_Color);

//			recordIndex++;
//			workSheet.Cells[recordIndex, 1].Value = Constains.TeamSS;
//			workSheet.Cells[recordIndex, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
//			workSheet.Cells[recordIndex, 2].Style.Fill.BackgroundColor.SetColor(Constains.TeamSS_Color);

//			recordIndex++;
//			workSheet.Cells[recordIndex, 1].Value = Constains.TeamQA;
//			workSheet.Cells[recordIndex, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
//			workSheet.Cells[recordIndex, 2].Style.Fill.BackgroundColor.SetColor(Constains.TeamQA_Color);

//			workSheet.Column(1).AutoFit();

//			excel.Save();
//		}
//	}
//}
