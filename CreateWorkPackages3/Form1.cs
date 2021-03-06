using BusinessLibrary.Models;
using BusinessLibrary.Models.Planning;
using BusinessLibrary.Ultilities;
using CreateWorkPackages3.Model;
using CreateWorkPackages3.Service;
using CreateWorkPackages3.Utilities;
using CreateWorkPackages3.Workpackages.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CreateWorkPackages3.Extension.ObjectExtension;
using NDragDrop;
using FluentDragDrop;

namespace CreateWorkPackages3
{
	public partial class Form1 : Form
	{
		private readonly ToolkitService _service;



		/// <summary>
		/// Cloned latest data from toolkit
		/// </summary>
		private List<WPItemModel> _wpItemsLocal = new List<WPItemModel>();

		/// <summary>
		/// Store changes from cloned latest data from toolkit
		/// </summary>
		private List<WPItemChangesModel> _wpItemsLocalChanges = new List<WPItemChangesModel>();



		private const string _column_Feature = "Feature";
		private const string _column_WpType = "WPType";
		private const string _column_Status = "Status";
		private const string _column_Start = "Start";
		private const string _column_Assignee = "Assignee";
		private const string _column_Estimate = "Estimate";
		private const string _column_Remaining = "Remaining";
		private const string _column_DependOn = "DependOn";
		private const string _column_Spent = "Spent";
		private const string _column_DueDate = "DueDate";
		private const string _column_Iteration = "Iteration";


		public Form1()
		{
			InitializeComponent();
			_service = new ToolkitService();

			timeEntries.Enabled = false;
			btnLoadDaily.Enabled = false;

			Daily_log_textbox.Clear();
			timeEntries.Enabled = true;
			btnLoadDaily.Enabled = true;
			tabDetailsPlan_btn_pushToToolkit.Enabled = false;
			tabDetailsPlan_btn_apply_changes.Enabled = false;

			CreateConnectionToSharepoint();
			GetMetadataFromSharepoint();
			PullLatestData(BusinessLibrary.Common.Configuration._defaultReleaseId.ToString(), string.Empty, BusinessLibrary.Common.Configuration._defaultTeamId.ToString());


			//string fname = $@"{System.IO.Directory.GetCurrentDirectory()}\..\..\..\BusinessLibrary\Resource\Icon\running.png";
			//var bmp = Bitmap.FromFile(fname);
			//var thumb = (Bitmap)bmp.GetThumbnailImage(8, 8, null, IntPtr.Zero);
			//thumb.MakeTransparent();
			//var icon = Icon.FromHandle(thumb.GetHicon());
			//label26.Image = icon.ToBitmap();
			////label26.Width = 100;
			////label26.AutoSize = false;
			//label26.Text = "If you set the label to AutoSize, it will automatically grow with whatever text you put in it. (This includes vertical growth.)";
			//label26.TextAlign = ContentAlignment.MiddleLeft;
			//label26.ImageAlign = ContentAlignment.MiddleRight;
			//label26.MaximumSize = new Size(100, 0);
			//label26.AutoSize = true;

			//tableLayoutPanel3.HorizontalScroll.Enabled = true;
			//tableLayoutPanel3.AutoSize = false;
			//tableLayoutPanel3.AutoScroll = true;
			//tableLayoutPanel3.ColumnStyles.Insert(0, new ColumnStyle() { SizeType = SizeType.Absolute, Width = 800F });
			//tableLayoutPanel3.ColumnStyles.Insert(1, new ColumnStyle() { SizeType = SizeType.Absolute, Width = 900F });
		}
		~Form1()
		{
			//lsvDaily = null;
		}

		void Log(string output)
		{
			Daily_log_textbox.AppendText("\r\n" + output);
			Daily_log_textbox.ScrollToCaret();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			//Daily_DataGridView.DataSource = GetGridTable();

		}

		private void CreateConnectionToSharepoint()
		{
			try
			{
				var username = @"ncdmz\vitra";
				var password = "XuanVietConTre01";
				_service.Connect(username, password);
			}
			catch (Exception exp)
			{
				Log($"Error connecting to Sharepoint API: {exp.Message}");
			}
		}
		private void GetMetadataFromSharepoint()
		{
			try
			{
				_service.GetReleases();
				_service.GetIterations(BusinessLibrary.Common.Configuration._defaultTeamId.ToString());

				var release = _service._releases.ToList().Select(x => new { key = x.Id, value = x.FieldValues["Title"] }).ToList();
				cbbRelease.DataSource = release;
				cbbRelease.DisplayMember = "value";

				var iterations = _service._iterations.ToList().Select(x => new { key = x.Id, value = x.FieldValues["Title"].ToString() }).ToList();

				iterations.Add(new { key = 0, value = "--Iteration--" });
				iterations = iterations.OrderBy(i => i.key).ToList();
				Daily_Filter_cbb_Iteration.DataSource = iterations;
				Daily_Filter_cbb_Iteration.DisplayMember = "value";
			}
			catch (Exception exp)
			{
				Log("Error connecting to Sharepoint API: " + exp.Message + "Feature disabled." + "\r\n");
			}
		}
		private void Daily_Load_filter_combobox_data()
		{
			var features = _service._features.ToList().Select(x => new { key = x.Id, value = x.FieldValues["Title"].ToString() }).ToList();
			features.Add(new { key = 0, value = "  All Feature--" });
			features = features.Distinct().OrderBy(i => i.value).ToList();
			Daily_Filter_cbb_Feature.DataSource = features;
			Daily_Filter_cbb_Feature.DisplayMember = "value";


			var uss = _service._us.ToList().Select(x => new { key = x.Id, value = x.FieldValues["Title"].ToString() }).ToList();
			uss.Add(new { key = 0, value = "  US--" });
			uss = uss.Distinct().OrderBy(i => i.value).ToList();
			Daily_Filter_cbb_US.DataSource = uss;
			Daily_Filter_cbb_US.DisplayMember = "value";


			var assignees = _service._wps.ToList().Select(x => new
			{
				key = x.FieldValues["AssignedTo"] == null ? 1 : ((Microsoft.SharePoint.Client.FieldLookupValue)(x.FieldValues["AssignedTo"])).LookupId,
				value = x.FieldValues["AssignedTo"] == null ? string.Empty : ((Microsoft.SharePoint.Client.FieldLookupValue)(x.FieldValues["AssignedTo"])).LookupValue.ToString()
			}).ToList();
			assignees.Add(new { key = 0, value = "  All Assignee--" });
			assignees = assignees.Distinct().OrderBy(i => i.value).ToList();
			Daily_Filter_cbb_assignee.DataSource = assignees;
			Daily_Filter_cbb_assignee.DisplayMember = "value";


			var teams = _service._wps.ToList().Select(x => new
			{
				key = x.FieldValues["Team"] == null ? 1 : ((Microsoft.SharePoint.Client.FieldLookupValue)(x.FieldValues["Team"])).LookupId,
				value = x.FieldValues["Team"] == null ? string.Empty : ((Microsoft.SharePoint.Client.FieldLookupValue)(x.FieldValues["Team"])).LookupValue.ToString()
			}).ToList();
			teams.Add(new { key = 0, value = "  All Team--" });
			teams = teams.Distinct().OrderBy(i => i.value).ToList();
			Daily_Filter_cbb_Team.DataSource = teams;
			Daily_Filter_cbb_Team.DisplayMember = "value";


			var statuses = _service._wps.ToList().Select(x => new { key = x.FieldValues[_column_Status].ToString(), value = x.FieldValues[_column_Status].ToString() }).ToList();
			statuses.Add(new { key = "0", value = "  All Status--" });
			statuses = statuses.Distinct().OrderBy(i => i.value).ToList();
			Daily_Filter_cbb_Status.DataSource = statuses;
			Daily_Filter_cbb_Status.DisplayMember = "value";
		}


		private void PullLatestData_Click(object sender, EventArgs e)
		{
			if (cbbRelease.SelectedItem == null)
			{
				Log("You need to choose a release.");
				return;
			}

			//var selectedReleaseId = cbbRelease.SelectedItem.GetPropValue("key").ToString();			
			var selectedIterationId = Daily_Filter_cbb_Iteration.SelectedItem.GetPropValue("key").ToString();

			PullLatestData(BusinessLibrary.Common.Configuration._defaultReleaseId.ToString(), selectedIterationId, BusinessLibrary.Common.Configuration._defaultTeamId.ToString());
		}


		private void Daily_Filter_btn_filter_Click(object sender, EventArgs e)
		{
			var result = new List<ListViewItem>();

			var clonedData = new List<WPItemModel>();
			var currentWps = _service._currentWPLocalData;
			currentWps.ForEach(l => clonedData.Add(l.DeepCopy()));

			if (Daily_Filter_cbb_assignee.SelectedItem != null && int.Parse(Daily_Filter_cbb_assignee.SelectedItem.GetPropValue("key").ToString()) > 1)
			{
				clonedData = clonedData.Where(i => i.WPAssignee == Daily_Filter_cbb_assignee.SelectedItem.GetPropValue("value").ToString()).ToList();
			}

			if (Daily_Filter_cbb_Feature.SelectedItem != null && int.Parse(Daily_Filter_cbb_Feature.SelectedItem.GetPropValue("key").ToString()) > 1)
			{
				clonedData = clonedData.Where(i => i.FeatureId.ToString() == Daily_Filter_cbb_Feature.SelectedItem.GetPropValue("key").ToString()).ToList();
			}

			if (Daily_Filter_cbb_US.SelectedItem != null && int.Parse(Daily_Filter_cbb_US.SelectedItem.GetPropValue("key").ToString()) > 1)
			{
				clonedData = clonedData.Where(i => i.USId.ToString() == Daily_Filter_cbb_US.SelectedItem.GetPropValue("key").ToString()).ToList();
			}

			if (Daily_Filter_cbb_Team.SelectedItem != null && int.Parse(Daily_Filter_cbb_Team.SelectedItem.GetPropValue("key").ToString()) > 1)
			{
				clonedData = clonedData.Where(i => i.WPTeam == Daily_Filter_cbb_Team.SelectedItem.GetPropValue("value").ToString()).ToList();
			}

			if (Daily_Filter_cbb_Status.SelectedItem != null && Daily_Filter_cbb_Status.SelectedItem.GetPropValue("value").ToString() != "  All Status--")
			{
				clonedData = clonedData.Where(i => i.WPStatus == Daily_Filter_cbb_Status.SelectedItem.GetPropValue("value").ToString()).ToList();
			}

			if (Daily_Filter_cbb_Iteration.SelectedItem != null && int.Parse(Daily_Filter_cbb_Iteration.SelectedItem.GetPropValue("key").ToString()) > 1)
			{
				clonedData = clonedData.Where(i => i.WPIterationId == Daily_Filter_cbb_Iteration.SelectedItem.GetPropValue("key").ToString()).ToList();
			}


			//var lstItems = MapLocalLisViewItemToListViewItem(clonedData);

			//lsvDaily.Items.Clear();
			//lsvDaily.Items.AddRange(lstItems.ToArray());

			Daily_DataGridView.DataSource = clonedData;
		}

		private void PullLatestData(string selectedReleaseId, string selectedIterationId, string selectedTeamId)
		{
			Parallel.Invoke(() =>
			{
				try
				{
					_service.GetFeaturesByTeam(BusinessLibrary.Common.Configuration._defaultTeamId.ToString());
					_service.GetAllocationByTeam(BusinessLibrary.Common.Configuration._defaultTeamId.ToString());
					_service.GetAllocationAdjustmentsByTeam(BusinessLibrary.Common.Configuration._defaultTeamId.ToString());
					_service.GetUserStoriesByFeatureIds();
					_service.GetWorkpackagesByUsIds();
					_service.BuildWorkpackpageModel(_service._features, _service._us, _service._wps);

					LoadDailyTrackTab();
					BuildProgressTracking(_service._currentWPLocalData);
					OpenPlanningForm();
				}
				catch (Exception exp)
				{
					Log("Error connecting to Sharepoint API: " + exp.Message + "Feature disabled." + "\r\n");
				}
			});
		}



		private void LoadDailyTrackTab()
		{
			var clonedData = new List<WPItemModel>();
			var currentWps = _service._currentWPLocalData;
			currentWps.ForEach(l => clonedData.Add(l.DeepCopy()));
			Daily_DataGridView.DataSource = clonedData;
			Daily_Load_filter_combobox_data();
		}

		/// <summary>
		/// TODO: Currently doesn't work because missing assignee
		/// </summary>
		private void UnuseFollowingPlanTrack()
		{
			DateTime EndDate = new DateTime(2022, 04, 15);
			var features = _service._features.ConvertToFeatures();
			var userstories = _service._us.ConvertToUserStory();
			var workpackages = _service._wps.ConvertToWorkpackage();
			var allocations = _service._allocations.ConvertToAllocation();
			var allocationAdjustments = _service._allocationadjustments.ConvertToAllocationAdjustment();

			var reporting = new Reporting();
			var planning = reporting.GenerateReport(features, userstories, workpackages, allocations, allocationAdjustments, EndDate);

			var allWorkPackagesFromToolKit = new List<ToolKitWorkPackageModel>();
			workpackages.ForEach(l => allWorkPackagesFromToolKit.Add(l.DeepCopy()));

			DataTable table = new DataTable();

			//add columns: 
			//[0]: feature name
			//[1 --> x]: date
			table.Columns.Add(new DataColumn(_column_Feature));
			var days = (EndDate - DateTime.UtcNow).Days;
			for (int i = 0; i <= days - 1; i++)
			{
				table.Columns.Add(DateTime.UtcNow.AddDays(i).Date.ToShortDateString());
			}


			var planningForRunningWps = new List<PlanningWpModel>();
			foreach (var f in planning.Features)
			{
				foreach (var us in f.UserStories)
				{
					planningForRunningWps.AddRange(us.Wps);
				}
			}
			var planningForRunningWpsFromFeModels = planningForRunningWps.GroupBy(s => s.FeatureName).Select(s => new { s.Key, Values = s.ToList() }).ToList();
			int recordIndex = 2;
			planning.Features = planning.Features.OrderBy(f => f.Status).ToList();
			var schduleForRunningFeatures = planning.ResourceData.SelectMany(rs => rs.ScheduleForWorking).ToList();
			foreach (var f in planning.Features)
			{
				DataRow row = table.NewRow();

				var feature = planningForRunningWpsFromFeModels.FirstOrDefault(fe => fe.Key == f.Name);

				#region if features are implemented or released
				if (feature == null)
				{
					row[1] = f.Status == Constains.FE_Status_DevImplemented_38
						? $"{f.Name} ({f.Id}) - Implemented"
						: $"{f.Name} ({f.Id}) - Released";
					//workSheet.Cells[recordIndex, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
					//workSheet.Cells[recordIndex, 1].Style.Fill.BackgroundColor.SetColor(Constains.TeamQA_Color);
					recordIndex++;
					continue;
				}
				#endregion

				#region Main function - Features haven't released yet, need the overview release plan for them
				var teams = feature.Values.GroupBy(s => s.Team).Select(s => new { s.Key, Values = s.ToList() }).ToList();
				foreach (var team in teams)
				{
					//Get all existing WPs
					//Get scheduled WPs
					//if all of existing WPs are scheduled, then it's fine, otherwise; there is no release plan
					var wps = team.Values;
					var wpSchedule = schduleForRunningFeatures.Where(s => wps.Select(wp => wp.Id).Contains(s.WpId)).ToList();
					var allExistingWps = allWorkPackagesFromToolKit.Where(w => w.FeatureName == f.Name && w.Team == team.Key && w.IsInBuildPhase).ToList();
					var nonPlannedWps = allExistingWps.Where(w => !w.HasPlan).ToList();

					StringBuilder featureDescription = new StringBuilder();
					featureDescription.AppendLine($"{feature.Key} - {team.Key}");

					if (nonPlannedWps.Any())
					{
						featureDescription.AppendLine($"No plan {nonPlannedWps.Sum(w => w.RemainingHours)} hrs:");
						foreach (var nonPlannedWp in nonPlannedWps)
						{
							var assignee = string.IsNullOrWhiteSpace(nonPlannedWp.Assignee) ? "No assignee" : nonPlannedWp.Assignee;
							featureDescription.AppendLine($"WP-{nonPlannedWp.Id}-{nonPlannedWp.RemainingHours} hrs-{assignee}-{nonPlannedWp.CurrentStatus}");
						}

						//workSheet.Cells[recordIndex, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
						//workSheet.Cells[recordIndex, 1].Style.Fill.BackgroundColor.SetColor(Constains.FE_Risk_Color);
					}

					//workSheet.Cells[recordIndex, 1].Value = featureDescription.ToString();
					row[1] = featureDescription.ToString();
					if (!wpSchedule.Any())
					{
						recordIndex++;
						continue;
					}

					for (int i = 0; i <= days - 1; i++)
					{
						var dateOnTimeLine = DateTime.UtcNow.AddDays(i);
						var wpScheduledInDate = wpSchedule.Where(wp => wp.Date.Date == dateOnTimeLine.Date).ToList();

						if (wpScheduledInDate.Any())
						{
							//workSheet.Cells[recordIndex, i + 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
							//if (Constains.Team_Functional.Contains(team.Key))
							//{
							//	workSheet.Cells[recordIndex, i + 2].Style.Fill.BackgroundColor.SetColor(Constains.TeamFUN_Color);
							//}
							//if (Constains.Team_SS.Contains(team.Key))
							//{
							//	workSheet.Cells[recordIndex, i + 2].Style.Fill.BackgroundColor.SetColor(Constains.TeamSS_Color);
							//}

							StringBuilder detail = new StringBuilder();
							foreach (var wp in wpScheduledInDate)
							{
								detail.AppendLine($"WP-{wp.WpId} - {wp.Name} ({wp.WorkingHours})");
							}

							//workSheet.Cells[recordIndex, i + 2].Value = detail.ToString();
							row[i + 2] = detail.ToString();
						}
					}
					recordIndex++;
				}
				#endregion

				table.Rows.Add(row);
			}

			tabDetailsPlan_GridView.DataSource = table;
		}
		private void UnuseBuildLocalDataStored()
		{
			List<ToolKitFeatureModel> _features = new List<ToolKitFeatureModel>();
			List<ToolKitUserStoryModel> _userstories = new List<ToolKitUserStoryModel>();
			List<ToolKitWorkPackageModel> _workpackages = new List<ToolKitWorkPackageModel>();
			List<ToolkitAllocationModel> _allocations = new List<ToolkitAllocationModel>();
			List<ToolkitAllocationAdjustmentModel> _allocationAdjustments = new List<ToolkitAllocationAdjustmentModel>();

			_features = _service._features.ConvertToFeatures();
			_userstories = _service._us.ConvertToUserStory();
			_workpackages = _service._wps.ConvertToWorkpackage();
			_allocations = _service._allocations.ConvertToAllocation();
			_allocationAdjustments = _service._allocationadjustments.ConvertToAllocationAdjustment();
		}



		private void tableLayoutPanel3_DragDrop(object sender, DragEventArgs e)
		{

		}

		private void tableLayoutPanel3_DragOver(object sender, DragEventArgs e)
		{

		}

		private void tableLayoutPanel3_DragEnter(object sender, DragEventArgs e)
		{

		}

		private void tableLayoutPanel3_DragLeave(object sender, EventArgs e)
		{

		}

		private void label26_DragDrop(object sender, DragEventArgs e)
		{

		}

		private void label26_DragEnter(object sender, DragEventArgs e)
		{

		}

		private void label26_DragLeave(object sender, EventArgs e)
		{

		}

		private void label26_DragOver(object sender, DragEventArgs e)
		{

		}

		private void label26_MouseHover(object sender, EventArgs e)
		{

		}

		private void label26_MouseLeave(object sender, EventArgs e)
		{

		}

		private void OpenPlanningForm()
		{
			var openForm = Application.OpenForms.Cast<Form>().FirstOrDefault(form => form.Name == "PlanningForm");
			if (openForm != null)
			{
				openForm.Close();
			}

			var frm = new PlanningForm(_wpItemsLocal, _service);
			frm.Show();
		}

		private async void label26_MouseDown(object sender, MouseEventArgs e)
		{
			//var source = (Label)sender;
			//var target = source.Equals(listLeft) ? listRight : listLeft;

			//source.InitializeDragAndDrop()
			//	.Move()
			//	.OnMouseMove()
			//	.If(() => source.SelectedIndices.Count > 0)
			//	.WithData(() => source.SelectedItems.OfType<ListViewItem>().ToArray())
			//	.WithPreview((_, data) => RenderPreview(data)).BehindCursor()
			//	.To(target, MoveItems);

			//Services s = new Services();
			//string url = "https://localhost:44385/WeatherForecast";
			//var result = await s.Get<dynamic>(url);



			//var expectedWps = _lsvlocalData;
			//var expectedFeatures = new List<ToolKitFeatureModel>();
			//foreach (var item in _lsvlocalData)
			//{
			//	if (!expectedFeatures.Any(f => f.Id == item.FeatureId.ToString() && f.Name == item.Feature))
			//		expectedFeatures.Add(new ToolKitFeatureModel { Id = item.FeatureId.ToString(), Name = item.Feature });
			//}


			//url = "https://localhost:44385/toolkit/feature";
			//await s.Post<bool>(url, expectedFeatures);

			//url = "https://localhost:44385/toolkit/wp";
			//await s.Post<bool>(url, expectedWps);

			//url = "https://localhost:44385/toolkit/iteration";
			//await s.Post<bool>(url, _service._toolkitIterations);

			//url = "https://localhost:44385/toolkit/allocation";
			//await s.Post<bool>(url, _service._toolkitAllocationAdjustmentsModel);

			//url = "https://localhost:44385/toolkit/allocationadjustment";
			//await s.Post<bool>(url, _service._toolkitAllocationsModel);
		}
	}
}
