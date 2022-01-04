using BusinessLibrary.Models;
using BusinessLibrary.Models.Planning;
using BusinessLibrary.Ultilities;
using CreateWorkPackages3.Model;
using CreateWorkPackages3.Service;
using CreateWorkPackages3.Utilities;
using CreateWorkPackages3.Utilities.GanntChart;
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

namespace CreateWorkPackages3
{
	public partial class Form1 : Form
	{
		private readonly ToolkitService _service;

		/// <summary>
		/// latest data from toolkit
		/// </summary>
		private List<WPItemModel> _lsvlocalData = new List<WPItemModel>();

		/// <summary>
		/// Cloned latest data from toolkit
		/// </summary>
		private List<WPItemModel> _wpItemsLocal = new List<WPItemModel>();

		/// <summary>
		/// Store changes from cloned latest data from toolkit
		/// </summary>
		private List<WPItemChangesModel> _wpItemsLocalChanges = new List<WPItemChangesModel>();

		private DateTime EndDate = new DateTime(2022, 04, 15);

		private List<ToolKitFeatureModel> _features = new List<ToolKitFeatureModel>();
		private List<ToolKitUserStoryModel> _userstories = new List<ToolKitUserStoryModel>();
		private List<ToolKitWorkPackageModel> _workpackages = new List<ToolKitWorkPackageModel>();
		private List<ToolkitAllocationModel> _allocations = new List<ToolkitAllocationModel>();
		private List<ToolkitAllocationAdjustmentModel> _allocationAdjustments = new List<ToolkitAllocationAdjustmentModel>();

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
		private int _releaseId;
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

			//Load_GanttChart();
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
		//private void CopyableListView(ListView listView)
		//{
		//	listView.KeyDown += (object sender, KeyEventArgs e) =>
		//	{
		//		if (!(sender is ListView)) return;
		//		if (e.Control && e.KeyCode == Keys.C)
		//		{
		//			var builder = new StringBuilder();
		//			foreach (ListViewItem item in (sender as ListView).SelectedItems)
		//			{
		//				builder.AppendLine(item.SubItems[0].Text + ": " + item.SubItems[1].Text + Environment.NewLine);
		//				string i = item.SubItems[7].Text.Replace("_apis/wit/workItems", "_workitems/edit");
		//				builder.AppendLine(i + Environment.NewLine);
		//			}
		//			Clipboard.SetText(builder.ToString());
		//		}
		//	};
		//}

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
				_service.GetIterations("32");

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
		/// <summary>
		/// Add US
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_US_AddItem_Click(object sender, EventArgs e)
		{
			var usId = 0;
			var featureId = int.Parse(txtUSFeatureId.Text);
			var usTeam = int.Parse(txtUSTeamId.Text);
			var status = "10 - New";
			bool cwpResult = false;

			if (string.IsNullOrWhiteSpace(US_txt_US_Id.Text))
			{
				var toolKitItems = new List<ToolkitUSModel>();

				var usTitle = txtUSTitle.Text;
				var usDescription = txtUSNote.Text;
				var newUS = new ToolkitUSModel()
				{
					Case = featureId,
					Status = status,
					Team = usTeam,
					Title = usTitle,
					Note = usDescription
				};

				toolKitItems.Add(newUS);

				cwpResult = _service.CreateUS(toolKitItems);
				Log(cwpResult ? "US(s) created." : "No US is created.");

				usId = _service.GetUserStoryId(usTitle, featureId);

				Log(cwpResult ? $"US(s) created: {usId}" : "No US is created.");
			}
			else
			{
				usId = int.Parse(US_txt_US_Id.Text);
			}

			var releaseId = _service.GetFeatureReleaseIdById(featureId);

			var toolkitWPItems = new List<ToolkitWPModel>();
			if (US_checkbox_createAnalyzeWP.Checked)
			{
				var wpTitle = $"{featureId}-{usId}-{US_checkbox_createAnalyzeWP.Text}";
				var wpEstimate = US_txt_estimate_analyze_wp.Text;
				var wpNote = new StringBuilder();
				wpNote.AppendLine(@"<p>To understand the requirement, try to answer the below questions:</p>");
				wpNote.AppendLine(@"<p>-What do we need to deliver based on the real demand from the expected scope?</p>");
				wpNote.AppendLine(@"<p>-Do we need all of the requirements?</p>");
				wpNote.AppendLine(@"<p>-Do we have any better idea to improve the business flow?</p>");
				wpNote.AppendLine(@"<p>-Do we 100 % sure that we completely understand the requirements as well as how the solution will be used in the real life ?</p>");
				wpNote.AppendLine("</p>");

				wpNote.AppendLine(@"<p>-From your PoV, if you are the end - user, will you use our solution ?</p>");
				wpNote.AppendLine(@"<p>-Other expectations, browser support in some features?</p>");
				wpNote.AppendLine(@"<p>-What are our risks that we should aware of?</p>");
				wpNote.AppendLine(@"<p>-What is our estimation ?</p>");
				wpNote.AppendLine(@"<p>-What can we deliver on the expected date with the current situation at hand?</p>");

				toolkitWPItems.Add(new ToolkitWPModel(usId.ToString(), wpTitle, usTeam, wpEstimate, wpNote.ToString(), status, releaseId, "Analysis", DateTime.UtcNow.AddHours(double.Parse(wpEstimate)).ToString()));
			}

			if (US_checkbox_createDesignUIWP.Checked)
			{
				var wpTitle = $"{featureId}-{usId}-{US_checkbox_createDesignUIWP.Text}";
				var wpEstimate = US_txt_estimate_designui_wp.Text;
				var wpNote = new StringBuilder();
				wpNote.AppendLine(@"<p>Purpose: People understand about how the app looks like, how the features are connected (making prototype could be a good idea)</p>");
				wpNote.AppendLine(@"<p>- Update DO160 - Solution design should be updated: use case, business rules, security requirements, sample screen designs, etc.</p>");
				wpNote.AppendLine(@"<p>- The developer must participate, take more responsible in this step because we only have 1 designer and the bottle neck will happen very soon for sure</p>");

				toolkitWPItems.Add(new ToolkitWPModel(usId.ToString(), wpTitle, usTeam, wpEstimate, wpNote.ToString(), status, releaseId, "Functional design/UX", DateTime.UtcNow.AddHours(double.Parse(wpEstimate)).ToString()));
			}

			if (US_checkbox_createClientWorkShopWP.Checked)
			{
				var wpTitle = $"{featureId}-{usId}-{US_checkbox_createClientWorkShopWP.Text}";
				var wpEstimate = US_txt_estimate_designui_wp.Text;
				var wpNote = new StringBuilder();
				wpNote.AppendLine(@"<p>Purpose: Workshop with client</p>");
				wpNote.AppendLine(@"<p>- Getting overview/workshop, etc.</p>");
				wpNote.AppendLine(@"<p>- Scope refinement</p>");
				wpNote.AppendLine(@"<p>------------------</p>");
				wpNote.AppendLine(@"<p></p>");
				wpNote.AppendLine(@"<p>Purpose: Internal review meeting </p>");
				wpNote.AppendLine(@"<p>- This meeting will be hold before we have workshop with the client to finalize the scope, it also means that</p>");
				wpNote.AppendLine(@"<p>- We need to make sure that everyone in the team has the same view before calling client </p>");
				wpNote.AppendLine(@"<p>- Make sure that everyone in the project understand what do we need to deliver based on the true needed purpose from the expected scope and how the app looks like</p>");

				toolkitWPItems.Add(new ToolkitWPModel(usId.ToString(), wpTitle, usTeam, wpEstimate, wpNote.ToString(), status, releaseId, "Workshop/Interview", DateTime.UtcNow.AddHours(double.Parse(wpEstimate)).ToString()));
			}

			if (US_checkbox_createDesignArWP.Checked)
			{
				var wpTitle = $"{featureId}-{usId}-{US_checkbox_createDesignArWP.Text}";
				var wpEstimate = US_txt_estimate_designar_wp.Text;
				var wpNote = new StringBuilder();
				wpNote.AppendLine(@"<p>- Make sure the design can cover all business flow/scenarios as agreed with PO</p>");
				wpNote.AppendLine(@"<p>- Make sure all participants understand the design and know how to work on them at details level</p>");
				wpNote.AppendLine(@"<p>- Update related document (DD120, DD130, DD160, O0400, O0500)</p>");

				toolkitWPItems.Add(new ToolkitWPModel(usId.ToString(), wpTitle, usTeam, wpEstimate, wpNote.ToString(), status, releaseId, "Technical design", DateTime.UtcNow.AddHours(double.Parse(wpEstimate)).ToString()));
			}
			if (US_checkbox_createDesignIntegrationWP.Checked)
			{
				var wpTitle = $"{featureId}-{usId}-{US_checkbox_createDesignIntegrationWP.Text}";
				var wpEstimate = US_txt_estimate_designintegrate_wp.Text;
				var wpNote = new StringBuilder();
				wpNote.AppendLine(@"<p>Create WP - Design - Integration agreement (External teams, API, Mock API, etc.)</p>");
				wpNote.AppendLine(@"<p>- Make sure Frontend and backend can integrate and all business flow/scenarios are covered as agreed from the scope</p>");
				wpNote.AppendLine(@"<p>- Update relevant docs (D0180)</p>");
				wpNote.AppendLine(@"<p>- It should be break down into smaller WPs by technical team</p>");

				toolkitWPItems.Add(new ToolkitWPModel(usId.ToString(), wpTitle, usTeam, wpEstimate, wpNote.ToString(), status, releaseId, "Technical design", DateTime.UtcNow.AddHours(double.Parse(wpEstimate)).ToString()));
			}

			if (US_checkbox_createDevelopmentWP.Checked)
			{
				var wpTitle = $"{featureId}-{usId}-{US_checkbox_createDevelopmentWP.Text}";
				var wpEstimate = US_txt_estimate_development_wp.Text;
				var wpNote = new StringBuilder();
				wpNote.AppendLine(@"<p>- Build [unit test, integration test, functional test - whatever the test we're doing now to pass the quality gate]</p>");
				wpNote.AppendLine(@"<p>- Development - Review (review in the same WP with development, just change the assignee)</p>");
				wpNote.AppendLine(@"<p>- Make sure we cover detailed design, run test local, pass quality gate (assume that the quality gate won't change the current check condition)</p>");
				wpNote.AppendLine(@"<p>- Update relevant documentation (DD160) - if required</p>");
				wpNote.AppendLine(@"<p>- It should be break down into smaller WPs by technical team</p>");

				toolkitWPItems.Add(new ToolkitWPModel(usId.ToString(), wpTitle, usTeam, wpEstimate, wpNote.ToString(), status, releaseId, "Build", DateTime.UtcNow.AddHours(double.Parse(wpEstimate)).ToString()));
			}

			if (US_checkbox_createDevelopmentIntegrationWP.Checked)
			{
				var wpTitle = $"{featureId}-{usId}-{US_checkbox_createDevelopmentIntegrationWP.Text}";
				var wpEstimate = US_txt_estimate_developmentintegrate_wp.Text;
				var wpNote = new StringBuilder();
				wpNote.AppendLine(@"<p>Purpose: Make sure we cover integration flows are tested/fully covered</p>");

				toolkitWPItems.Add(new ToolkitWPModel(usId.ToString(), wpTitle, usTeam, wpEstimate, wpNote.ToString(), status, releaseId, "Internal test", DateTime.UtcNow.AddHours(double.Parse(wpEstimate)).ToString()));
			}

			if (US_checkbox_createTCWP.Checked)
			{
				var wpTitle = $"{featureId}-{usId}-{US_checkbox_createTCWP.Text}";
				var wpEstimate = US_txt_estimate_createTC_wp.Text;
				var wpNote = new StringBuilder();
				wpNote.AppendLine(@"<p>- Create test case</p>");
				wpNote.AppendLine(@"<p>- Create test data</p>");
				wpNote.AppendLine(@"<p>- Documentation (T0130, T0150, T0500)</p>");
				wpNote.AppendLine(@"<p>- Review test case/data</p>");
				wpNote.AppendLine(@"<p>- Make sure that test cases will use real test data as much as possible</p>");
				wpNote.AppendLine(@"<p>- Make sure to cover business flow/scenarios as much as possible as agreed with the client/PO</p>");
				wpNote.AppendLine(@"<p>- Make sure the test case are defined detailed enough so whenever we need back up, the new testers can join and execute the test, compare the expected result without having too many knowledge about the app</p>");

				toolkitWPItems.Add(new ToolkitWPModel(usId.ToString(), wpTitle, usTeam, wpEstimate, wpNote.ToString(), status, releaseId, "Test - create tc", DateTime.UtcNow.AddHours(double.Parse(wpEstimate)).ToString()));
			}

			if (US_checkbox_createExecuteTCWP.Checked)
			{
				var wpTitle = $"{featureId}-{usId}-{US_checkbox_createExecuteTCWP.Text}";
				var wpEstimate = US_txt_estimate_executeTC_wp.Text;
				var wpNote = new StringBuilder();
				wpNote.AppendLine(@"<p>- Running strictly with whatever is defined in test case, don't try to run anything outside of it; otherwise we need to update the corresponding test case</p>");
				wpNote.AppendLine(@"<p>- Create defect if it has with all necessary data as mentioned in Test handbook (platform, version, env, test data, step, screenshot, video, etc.)</p>");
				wpNote.AppendLine(@"<p>- Strictly follow the defect management process - ask Lan if you have any concerns about it; if you see any problem with this process, please discuss with Lan and update the process if required</p>");

				toolkitWPItems.Add(new ToolkitWPModel(usId.ToString(), wpTitle, usTeam, wpEstimate, wpNote.ToString(), status, releaseId, "Test - execution tc", DateTime.UtcNow.AddHours(double.Parse(wpEstimate)).ToString()));
			}

			if (US_checkbox_createBugFixingWP.Checked)
			{
				var wpTitle = $"{featureId}-{usId}-{US_checkbox_createBugFixingWP.Text}";
				var wpEstimate = US_txt_estimate_bugfixing_wp.Text;
				var wpNote = new StringBuilder();
				wpNote.AppendLine(@"<p>- A big one from beginning so we can do the burn down chart, then we can continue create other WPs for each defects</p>");
				wpNote.AppendLine(@"<p>- Basically, the team will only focus on the WP level, the defect level will be taken care by the team lead/sub lead, then they will create corresponding WP</p>");

				toolkitWPItems.Add(new ToolkitWPModel(usId.ToString(), wpTitle, usTeam, wpEstimate, wpNote.ToString(), status, releaseId, "Bug fixing", DateTime.UtcNow.AddHours(double.Parse(wpEstimate)).ToString()));
			}

			if (toolkitWPItems.Any())
			{
				cwpResult = _service.CreateWP(toolkitWPItems);
				Log(cwpResult ? "WP(s) created." : "No WP is created.");
			}
			else
			{
				Log("No WP is created");
			}
		}

		#region add WPs
		/// <summary>
		/// Add WP
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void WP_Random_Btn_AddItem_Click(object sender, EventArgs e)
		{
			var toolKitItems = new List<ToolkitWPModel>();

			var wpId = WP_Random_USId.Text;
			var wpTitle = WP_Random_Title.Text;
			var wpTeam = WP_Random_TeamId.Text;
			var wpEstimation = WP_Random_Estimate.Text;
			var wpNote = txtWPNote.Text;
			var status = "10 - New";

			var item1 = new ToolkitWPModel()
			{
				FunctionalScenario = int.Parse(wpId),
				Status = status,
				Team = int.Parse(wpTeam),
				Title = wpTitle,
				Note = wpNote,
				Estimate = wpEstimation,
				RemainingWork = wpEstimation,
				Release = _releaseId,
				WPType = WP_Random_WPType.SelectedItem.ToString(),
				DueDate = DateTime.UtcNow.AddHours(int.Parse(wpEstimation)).ToString()
			};

			toolKitItems.Add(item1);

			bool cwpResult = _service.CreateWP(toolKitItems);
			Log(cwpResult ? "WP(s) created." : "No WP is created.");
		}

		private void txtUSEstimation_TextChanged(object sender, EventArgs e)
		{
			//try
			//{
			//	var estimate = double.Parse(txtUSEstimation.Text);

			//	US_txt_estimate_analyze_wp.Text = (Math.Round(estimate * 5 / 100)).ToString();
			//	US_txt_estimate_bugfixing_wp.Text = (Math.Round(estimate * 10 / 100)).ToString();
			//	US_txt_estimate_clientworkshop_wp.Text = (Math.Round(estimate * 5 / 100)).ToString();
			//	US_txt_estimate_createTC_wp.Text = (Math.Round(estimate * 10 / 100)).ToString();
			//	US_txt_estimate_designar_wp.Text = (Math.Round(estimate * 10 / 100)).ToString();
			//	US_txt_estimate_designintegrate_wp.Text = (Math.Round(estimate * 5 / 100)).ToString();//TODO
			//	US_txt_estimate_designui_wp.Text = (Math.Round(estimate * 5 / 100)).ToString();//TODO
			//	US_txt_estimate_developmentintegrate_wp.Text = (Math.Round(estimate * 5 / 100)).ToString();
			//	US_txt_estimate_development_wp.Text = (Math.Round(estimate * 40 / 100)).ToString();
			//	US_txt_estimate_executeTC_wp.Text = (Math.Round(estimate * 5 / 100)).ToString();

			//}
			//catch (Exception ex)
			//{
			//	WrittenTextBoxResult(false, $"{ex.Message}");
			//}
		}

		private void US_btn_calculateEstimate_Click(object sender, EventArgs e)
		{
			try
			{
				var estimate = double.Parse(txtUSEstimation.Text);

				US_txt_estimate_analyze_wp.Text = US_checkbox_createAnalyzeWP.Checked ? (Math.Round(estimate * 5 / 100)).ToString() : "0";
				US_txt_estimate_bugfixing_wp.Text = US_checkbox_createBugFixingWP.Checked ? (Math.Round(estimate * 10 / 100)).ToString() : "0";
				US_txt_estimate_clientworkshop_wp.Text = US_checkbox_createClientWorkShopWP.Checked ? (Math.Round(estimate * 5 / 100)).ToString() : "0";
				US_txt_estimate_createTC_wp.Text = US_checkbox_createTCWP.Checked ? (Math.Round(estimate * 10 / 100)).ToString() : "0";
				US_txt_estimate_designar_wp.Text = US_checkbox_createDesignArWP.Checked ? (Math.Round(estimate * 10 / 100)).ToString() : "0";
				US_txt_estimate_designintegrate_wp.Text = US_checkbox_createDesignIntegrationWP.Checked ? (Math.Round(estimate * 5 / 100)).ToString() : "0";
				US_txt_estimate_designui_wp.Text = US_checkbox_createDesignUIWP.Checked ? (Math.Round(estimate * 5 / 100)).ToString() : "0";
				US_txt_estimate_developmentintegrate_wp.Text = US_checkbox_createDevelopmentIntegrationWP.Checked ? (Math.Round(estimate * 5 / 100)).ToString() : "0";
				US_txt_estimate_development_wp.Text = US_checkbox_createDevelopmentWP.Checked ? (Math.Round(estimate * 40 / 100)).ToString() : "0";
				US_txt_estimate_executeTC_wp.Text = US_checkbox_createExecuteTCWP.Checked ? (Math.Round(estimate * 5 / 100)).ToString() : "0";

				US_txt_remaining_estimate.Text =
					(estimate
						- double.Parse(US_txt_estimate_analyze_wp.Text)
						- double.Parse(US_txt_estimate_bugfixing_wp.Text)
						- double.Parse(US_txt_estimate_clientworkshop_wp.Text)
						- double.Parse(US_txt_estimate_createTC_wp.Text)
						- double.Parse(US_txt_estimate_designar_wp.Text)
						- double.Parse(US_txt_estimate_designintegrate_wp.Text)
						- double.Parse(US_txt_estimate_designui_wp.Text)
						- double.Parse(US_txt_estimate_developmentintegrate_wp.Text)
						- double.Parse(US_txt_estimate_development_wp.Text)
						- double.Parse(US_txt_estimate_executeTC_wp.Text))
					.ToString();
			}
			catch (Exception ex)
			{
				Log($"{ex.Message}");
			}
		}

		private void updateCheckBox(bool expectedValue)
		{
			US_checkbox_createAnalyzeWP.Checked = expectedValue;
			US_checkbox_createBugFixingWP.Checked = expectedValue;
			US_checkbox_createClientWorkShopWP.Checked = expectedValue;
			US_checkbox_createDesignArWP.Checked = expectedValue;
			US_checkbox_createDesignIntegrationWP.Checked = expectedValue;
			US_checkbox_createDesignUIWP.Checked = expectedValue;
			US_checkbox_createDevelopmentIntegrationWP.Checked = expectedValue;
			US_checkbox_createDevelopmentWP.Checked = expectedValue;
			US_checkbox_createExecuteTCWP.Checked = expectedValue;
			US_checkbox_createTCWP.Checked = expectedValue;
		}

		private void US_checkbox_IncludeStandardWP_CheckedChanged(object sender, EventArgs e)
		{
			if (US_checkbox_IncludeStandardWP.Checked)
			{
				updateCheckBox(true);
			}
			else
			{
				updateCheckBox(false);
			}
		}
		#endregion

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

		private void Daily_Filter_btn_filter_Click(object sender, EventArgs e)
		{
			var result = new List<ListViewItem>();

			var clonedData = new List<WPItemModel>();
			_lsvlocalData.ForEach(l => clonedData.Add(l.DeepCopy()));

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

		private void PullLatestData_Click(object sender, EventArgs e)
		{
			//lsvDaily.Items.Clear();
			_lsvlocalData = new List<WPItemModel>();

			if (cbbRelease.SelectedItem == null)
			{
				Log("You need to choose a release.");
				return;
			}

			var selectedReleaseId = cbbRelease.SelectedItem.GetPropValue("key").ToString();
			_releaseId = int.Parse(selectedReleaseId);
			var selectedIterationId = Daily_Filter_cbb_Iteration.SelectedItem.GetPropValue("key").ToString();

			Parallel.Invoke(() =>
			{
				try
				{
					_service.GetFeatures(selectedReleaseId, "32");

					_service.GetAllocation("32");
					_service.GetAllocationAdjustments("32");

					_service.GetUserStories();
					_service.GetWorkpackages(selectedReleaseId, selectedIterationId);

					BuildDailyTrack();
					//BuildLocalDataStored();
				}
				catch (Exception exp)
				{
					Log("Error connecting to Sharepoint API: " + exp.Message + "Feature disabled." + "\r\n");
				}
			});
		}

		private void BuildDailyTrack()
		{
			foreach (var fe in _service._features)
			{
				var selectedUSs = _service._us.Where(u => u.FieldValues["Case"] != null && ((Microsoft.SharePoint.Client.FieldLookupValue)u.FieldValues["Case"]).LookupId == fe.Id).ToList();
				foreach (var us in selectedUSs)
				{
					var selectedWPs = _service._wps.Where(u => u.FieldValues["FunctionalScenario"] != null && ((Microsoft.SharePoint.Client.FieldLookupValue)u.FieldValues["FunctionalScenario"]).LookupId == us.Id).ToList();
					foreach (var wp in selectedWPs)
					{
						var assignee = wp.FieldValues["AssignedTo"] == null ? string.Empty : ((Microsoft.SharePoint.Client.FieldLookupValue)wp.FieldValues["AssignedTo"]).LookupValue;
						var team = wp.FieldValues["Team"] == null ? string.Empty : ((Microsoft.SharePoint.Client.FieldLookupValue)wp.FieldValues["Team"]).LookupValue;

						DateTime? startDate = null;
						if (wp.FieldValues["StartDate"] != null)
							startDate = DateTime.Parse(wp.FieldValues["StartDate"].ToString()).Date;
						DateTime? dueDate = null;
						if (wp.FieldValues["DueDate"] != null)
							dueDate = DateTime.Parse(wp.FieldValues["DueDate"].ToString()).Date;

						var iterationId = wp.FieldValues["Iteration"] == null ? string.Empty : ((Microsoft.SharePoint.Client.FieldLookupValue)wp.FieldValues["Iteration"]).LookupId.ToString();
						var iterationName = wp.FieldValues["Iteration"] == null ? string.Empty : ((Microsoft.SharePoint.Client.FieldLookupValue)wp.FieldValues["Iteration"]).LookupValue;

						var dependOnWPIds = wp.FieldValues["Depend_x0020_on"] != null && (wp.FieldValues["Depend_x0020_on"] as Microsoft.SharePoint.Client.FieldLookupValue[]).Count() > 0
					? (wp.FieldValues["Depend_x0020_on"] as Microsoft.SharePoint.Client.FieldLookupValue[])[0].LookupValue.Contains(";")
						? (wp.FieldValues["Depend_x0020_on"] as Microsoft.SharePoint.Client.FieldLookupValue[])[0].LookupValue.Split(';')[0]
						: (wp.FieldValues["Depend_x0020_on"] as Microsoft.SharePoint.Client.FieldLookupValue[])[0].LookupValue
					: string.Empty;

						_lsvlocalData.Add(new WPItemModel()
						{
							FeatureId = fe.Id,
							Feature = fe.FieldValues["Title"].ToString(),
							USId = us.Id,
							USTitle = us.FieldValues["Title"].ToString(),
							WPAssignee = assignee,
							WPStart = startDate,
							WPDueDate = dueDate,
							WPEstimate = wp.FieldValues[_column_Estimate] == null ? string.Empty : wp.FieldValues[_column_Estimate].ToString(),
							WPId = wp.Id,
							WPRemainingHour = wp.FieldValues["RemainingWork"] == null ? string.Empty : wp.FieldValues["RemainingWork"].ToString(),
							WPSpentHour = wp.FieldValues["TimeSpent"] == null ? string.Empty : wp.FieldValues["TimeSpent"].ToString(),
							WPStatus = wp.FieldValues[_column_Status] == null ? string.Empty : wp.FieldValues[_column_Status].ToString(),
							WPTeam = team,
							WPTitle = wp.FieldValues["Title"] == null ? string.Empty : wp.FieldValues["Title"].ToString(),
							WPType = wp.FieldValues[_column_WpType] == null ? string.Empty : wp.FieldValues[_column_WpType].ToString(),
							WPIterationId = iterationId,
							WPIterationName = iterationName,
							WPDependOn = dependOnWPIds
						});
					}
				}
			}

			_lsvlocalData.SortPriority();
			_lsvlocalData = _lsvlocalData
				.OrderBy(d => d.FeatureShow)
				.ThenBy(d => d.WPPriority)
				.ToList();

			var clonedData = new List<WPItemModel>();
			_lsvlocalData.ForEach(l => clonedData.Add(l.DeepCopy()));
			Daily_DataGridView.DataSource = clonedData;
			Daily_Load_filter_combobox_data();
		}

		/// <summary>
		/// Currently doesn't work because missing assignee
		/// </summary>
		private void FollowingPlanTrack()
		{
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

		private void BuildLocalDataStored()
		{
			_features = _service._features.ConvertToFeatures();
			_userstories = _service._us.ConvertToUserStory();
			_workpackages = _service._wps.ConvertToWorkpackage();
			_allocations = _service._allocations.ConvertToAllocation();
			_allocationAdjustments = _service._allocationadjustments.ConvertToAllocationAdjustment();
		}

		private void BuildProgressTracking(List<WPItemModel> dataSource)
		{
			//var features = _service._features.ConvertToFeatures();
			//var userstories = _service._us.ConvertToUserStory();
			//var workpackages = _service._wps.ConvertToWorkpackage();
			//var allocations = _service._allocations.ConvertToAllocation();
			//var allocationAdjustments = _service._allocationadjustments.ConvertToAllocationAdjustment();

			_wpItemsLocal = new List<WPItemModel>();
			dataSource.ForEach(l => _wpItemsLocal.Add(l.DeepCopy()));

			DataTable table = new DataTable();

			//add columns: 
			//[0]: feature name
			//[10 --> x]: date
			table.Columns.Add(_column_Feature, typeof(string));         //0
			table.Columns.Add(_column_WpType, typeof(string));          //1
			table.Columns.Add(_column_Status, typeof(string));          //2
																		//table.Columns.Add("% Complete"));  //3 - calculated by startDate/estimate & remaining/duedate
			table.Columns.Add(_column_Iteration, typeof(string));       //3
			table.Columns.Add(_column_Start, typeof(string));           //4
			table.Columns.Add(_column_Assignee, typeof(string));        //5
			table.Columns.Add(_column_Estimate, typeof(string));        //6
			table.Columns.Add(_column_Remaining, typeof(string));       //7
			table.Columns.Add(_column_DependOn, typeof(string));        //8
																		//table.Columns.Add("Release"));
																		//table.Columns.Add("Team"));
			table.Columns.Add(_column_Spent, typeof(string));           //9
																		//table.Columns.Add(_column_Feature));
																		//table.Columns.Add("WP Title"));
			table.Columns.Add(_column_DueDate, typeof(string));         //10

			//TODO: take care of the weekends/holiday/vacation
			var minDate = _wpItemsLocal.OrderBy(d => d.WPStartDate).First().WPStartDate.AddDays(-1);
			var maxDate = _wpItemsLocal
				.SelectMany(d => d.WPDateProgressing)
				.OrderByDescending(d => d)
				.First()
				.AddDays(1);

			var days = (maxDate - minDate).Days;
			for (int i = 0; i <= days - 1; i++)
			{
				table.Columns.Add(minDate.AddDays(i).Date.ToShortDateString());
			}


			foreach (var wpRow in _wpItemsLocal)
			{
				DataRow row = table.NewRow();
				//Add data for main columns
				row[0] = wpRow.WPShow;
				row[1] = wpRow.WPType;
				row[2] = wpRow.WPStatus;
				row[3] = wpRow.WPIterationName;
				row[4] = wpRow.WPStart.HasValue ? wpRow.WPStart.Value.ToShortDateString() : string.Empty;
				row[5] = wpRow.WPAssignee;
				row[6] = wpRow.WPEstimate;
				row[7] = wpRow.WPRemainingHour;
				row[8] = wpRow.WPDependOn;
				row[9] = wpRow.WPSpentHour;
				row[10] = wpRow.WPDueDate.HasValue ? wpRow.WPDueDate.Value.ToShortDateString() : string.Empty;

				//Add data on each day
				foreach (var day in wpRow.WPDateProgressing)
				{
					var columnnIndex = table.Columns[day.Date.ToShortDateString()];
					row[columnnIndex] = "X";
				}

				table.Rows.Add(row);
			}

			tabDetailsPlan_GridView.DataSource = table;
			foreach (DataGridViewColumn gridColumn in tabDetailsPlan_GridView.Columns)
			{
				gridColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
			}

			foreach (DataGridViewRow item in tabDetailsPlan_GridView.Rows)
			{
				if (item.Cells[_column_Status].Value == null) continue;
				var wantedColor = Color.White;
				string reason = "";
				if (item.Cells[_column_Status].Value.ToString() == "90 - Closed")
				{
					wantedColor = Color.Green;
				}
				else
				{
					//Being late - YELLOW
					//Remaining estimation < reality remaining (from today to the last day)
					//Tooltip with the reason

					var rowData = _wpItemsLocal.First(d => item.Cells[_column_Feature].Value.ToString() == d.WPShow);
					var dueDate = rowData.WPDateProgressing.Any()
						? rowData.WPDateProgressing.OrderByDescending(d => d).First()
						: DateTime.Parse(item.Cells[_column_DueDate].Value.ToString());

					if (dueDate.Date >= DateTime.UtcNow.Date)
					{
						var remainingHoursReality = CalculateAvailableWorkingHoursFromNow(item.Cells[_column_Assignee].Value.ToString(), dueDate);
						var remainingHoursEstimation = item.Cells[_column_Remaining].Value != null ? decimal.Parse(item.Cells[_column_Remaining].Value.ToString()) : 0;

						if (remainingHoursReality - remainingHoursEstimation < -8)
						{
							wantedColor = Color.Yellow;
							reason = $"Remaining ({remainingHoursEstimation}) > reality ({remainingHoursReality})";
						}
					}
					else   //Overdude - RED					
					{
						wantedColor = Color.Red;
						reason = $"Lated - Due date ({dueDate}) is in the past";
					}
				}

				ChangeBackgroundColor(tabDetailsPlan_GridView, tabDetailsPlan_GridView[2, item.Index], wantedColor, reason);
			}


			tabDetailsPlan_GridView.Columns[0].Frozen = true;
			tabDetailsPlan_GridView.Columns[1].Frozen = true;
			tabDetailsPlan_GridView.Columns[2].Frozen = true;
			tabDetailsPlan_GridView.Columns[3].Frozen = true;
			tabDetailsPlan_GridView.Columns[4].Frozen = true;
			//tabDetailsPlan_GridView.Columns[5].Frozen = true;
			//tabDetailsPlan_GridView.Columns[6].Frozen = true;
			//tabDetailsPlan_GridView.Columns[7].Frozen = true;
			//tabDetailsPlan_GridView.Columns[8].Frozen = true;
		}

		/// <summary>
		/// Rebuild plan track from local data stored
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tabDetailsPlan_btn_refersh_Click(object sender, EventArgs e)
		{
			//Clean changes
			_wpItemsLocalChanges = new List<WPItemChangesModel>();

			BuildProgressTracking(_lsvlocalData);
			//FollowingPlanTrack();
		}

		private void tabDetailsPlan_GridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex >= 0)
			{
				AddNewChangesFromWPLevel(e.ColumnIndex, e.RowIndex, tabDetailsPlan_GridView.Columns[e.ColumnIndex].Name);

				tabDetailsPlan_btn_pushToToolkit.Enabled = false;
				tabDetailsPlan_GridView_changes.DataSource = null;
				tabDetailsPlan_GridView_changes.DataSource = _wpItemsLocalChanges;

				if (_wpItemsLocalChanges.Any())
					tabDetailsPlan_btn_apply_changes.Enabled = true;
			}
		}

		private void AddNewChangesFromWPLevel(int columnIndex, int rowIndex, string property)
		{
			var wpId = int.Parse(tabDetailsPlan_GridView[0, rowIndex].Value.ToString().Split('-')[tabDetailsPlan_GridView[0, rowIndex].Value.ToString().Split('-').Count() - 1].Trim());
			var currentRowData = _wpItemsLocal.FirstOrDefault(d => d.WPId == wpId);
			if (currentRowData == null) return;

			var cellOldData = string.Empty;

			switch (property)
			{
				case _column_Estimate:
					cellOldData = currentRowData.WPEstimate;
					break;
				case _column_Remaining:
					cellOldData = currentRowData.WPRemainingHour;
					break;
				case _column_DependOn:
					cellOldData = currentRowData.WPDependOn;
					break;
				case _column_DueDate:
					cellOldData = currentRowData.WPDueDate.HasValue
						? currentRowData.WPDueDate.Value.Date.ToShortDateString()
						: string.Empty;
					break;

				case _column_Assignee:
					cellOldData = currentRowData.WPAssignee;
					break;
				case _column_Start:
					cellOldData = currentRowData.WPStart.HasValue
						? currentRowData.WPStart.Value.Date.ToShortDateString()
						: string.Empty;
					break;
				case _column_Iteration:
					cellOldData = currentRowData.WPIterationName;
					break;
				default:
					break;
			}

			var cellNewData = tabDetailsPlan_GridView[columnIndex, rowIndex].Value.ToString();

			if (cellOldData == cellNewData) return;

			var existingChange = _wpItemsLocalChanges.FirstOrDefault(d => d.WPId == wpId && d.Property == property);
			if (existingChange == null)
			{
				_wpItemsLocalChanges.Add(new WPItemChangesModel
				{
					WPId = currentRowData.WPId,
					OldValue = cellOldData,
					NewValue = cellNewData,
					Property = property
				});
			}
			else
			{
				existingChange.NewValue = cellNewData;
			}
		}

		private void tabDetailsPlan_btn_apply_changes_Click(object sender, EventArgs e)
		{
			foreach (var change in _wpItemsLocalChanges)
			{
				var data = _wpItemsLocal.FirstOrDefault(d => d.WPId == change.WPId);

				switch (change.Property)
				{
					case _column_Iteration:
						if (string.IsNullOrEmpty(change.NewValue))
							data.WPIterationName = null;
						else
						{
							data.WPIterationName = change.NewValue;
							data.WPIterationId = _service._toolkitIterations.First(i => i.Title == change.NewValue).Id.ToString();
						}
						break;
					case _column_Start:
						if (string.IsNullOrEmpty(change.NewValue))
							data.WPStart = null;
						else
							data.WPStart = DateTime.Parse(change.NewValue).Date;
						break;
					case _column_Assignee:
						if (string.IsNullOrEmpty(change.NewValue))
							data.WPAssignee = null;
						else
							data.WPAssignee = change.NewValue;
						break;
					case _column_Estimate:
						if (string.IsNullOrEmpty(change.NewValue))
							data.WPEstimate = null;
						else
							data.WPEstimate = change.NewValue;
						break;
					case _column_Remaining:
						if (string.IsNullOrEmpty(change.NewValue))
							data.WPRemainingHour = null;
						else
							data.WPRemainingHour = change.NewValue;
						break;
					case _column_DependOn:
						if (string.IsNullOrEmpty(change.NewValue))
							data.WPDependOn = null;
						else
							data.WPDependOn = change.NewValue;
						break;
					case _column_DueDate:
						if (string.IsNullOrEmpty(change.NewValue))
							data.WPDueDate = null;
						else
							data.WPDueDate = DateTime.Parse(change.NewValue).Date;
						break;
					default:
						break;
				}
			}

			BuildProgressTracking(_wpItemsLocal);

			tabDetailsPlan_btn_pushToToolkit.Enabled = true;
		}

		private void tabDetailsPlan_btn_pushToToolkit_Click(object sender, EventArgs e)
		{
			var toolkitWPItems = new List<ToolkitWPModel>();

			var changesByWP = _wpItemsLocalChanges.GroupBy(c => c.WPId)
				.Select(c => new { c.Key, Changes = c.ToList() })
				.ToList();

			foreach (var changes in changesByWP)
			{
				var data = _wpItemsLocal.FirstOrDefault(d => d.WPId == changes.Key);
				var startDateString = string.Empty;
				if (data.WPStart.HasValue)
					startDateString = data.WPStart.Value.Date.ToShortDateString();

				var dueDateString = string.Empty;
				if (data.WPDueDate.HasValue)
					dueDateString = data.WPDueDate.Value.Date.ToShortDateString();

				int? iterationId = null;
				if (!string.IsNullOrWhiteSpace(data.WPIterationId))
					iterationId = int.Parse(data.WPIterationId);

				toolkitWPItems.Add(new ToolkitWPModel()
				{
					Id = changes.Key,
					StartDate = startDateString,
					AssigneeId = string.IsNullOrWhiteSpace(data.WPAssignee)
						? 213 //TODO: how to clear assignee?
						: Constains.Toolkit_Assignee.First(a => a.Value == data.WPAssignee).Key,
					Estimate = data.WPEstimate,
					RemainingWork = data.WPRemainingHour,
					DependOn = data.WPDependOn,
					DueDate = dueDateString,
					IterationId = iterationId
				});
			}
			var cwpResult = _service.UpdateWP(toolkitWPItems);

			_wpItemsLocalChanges = new List<WPItemChangesModel>();
			tabDetailsPlan_GridView_changes.DataSource = null;
			tabDetailsPlan_GridView_changes.DataSource = _wpItemsLocalChanges;
		}

		private DataTable GetAssignee()
		{
			DataTable l_dtDescription = new DataTable();
			l_dtDescription.Columns.Add("Key", typeof(string));
			l_dtDescription.Columns.Add(_column_Assignee, typeof(string));
			foreach (var item in Constains.Toolkit_Assignee)
			{
				l_dtDescription.Rows.Add(item.Key.ToString(), item.Value.ToString());
			}

			return l_dtDescription;
		}

		private DataTable GetIteration()
		{
			DataTable l_dtDescription = new DataTable();
			l_dtDescription.Columns.Add("Key", typeof(string));
			l_dtDescription.Columns.Add(_column_Iteration, typeof(string));

			foreach (var item in _service._toolkitIterations)
			{
				l_dtDescription.Rows.Add(item.Id.ToString(), item.Title.ToString());
			}

			return l_dtDescription;
		}

		private void tabDetailsPlan_GridView_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (e.ColumnIndex > -1)
				{
					DataGridViewComboBoxCell l_objGridDropbox = new DataGridViewComboBoxCell();
					//Show the list of assingee
					if (tabDetailsPlan_GridView.Columns[e.ColumnIndex].Name.Contains(_column_Assignee))
					{
						tabDetailsPlan_GridView[e.ColumnIndex, e.RowIndex] = l_objGridDropbox;
						l_objGridDropbox.DataSource = GetAssignee();
						l_objGridDropbox.ValueMember = _column_Assignee;
						l_objGridDropbox.DisplayMember = _column_Assignee;
					}

					//Show the list of iteration
					if (tabDetailsPlan_GridView.Columns[e.ColumnIndex].Name.Contains(_column_Iteration))
					{
						tabDetailsPlan_GridView[e.ColumnIndex, e.RowIndex] = l_objGridDropbox;
						l_objGridDropbox.DataSource = GetIteration();
						l_objGridDropbox.ValueMember = _column_Iteration;
						l_objGridDropbox.DisplayMember = _column_Iteration;
					}

					//Move to the column on the gridview which contains the start date of the WP
					if (tabDetailsPlan_GridView.Columns[e.ColumnIndex].Name.Contains(_column_Feature))
					{
						var rowIndex = tabDetailsPlan_GridView.SelectedCells[0].RowIndex;
						var selectedRow = tabDetailsPlan_GridView.Rows[rowIndex];

						var columnCount = selectedRow.Cells.Count;
						var expectedColumnIndex = 0;
						for (int i = 9; i < columnCount; i++)
						{
							if (selectedRow.Cells[i].Value.ToString().Trim() == "X")
							{
								expectedColumnIndex = i;
								break;
							}
						}

						if (expectedColumnIndex > 0)
						{
							tabDetailsPlan_GridView.FirstDisplayedScrollingColumnIndex = expectedColumnIndex;
							tabDetailsPlan_GridView.CurrentCell = tabDetailsPlan_GridView[expectedColumnIndex, e.RowIndex];
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log(ex.Message);
			}
		}

		private void tabDetailsPlan_GridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			try
			{
				if (e.Exception.Message == "DataGridViewComboBoxCell value is not valid.")
				{
					object value = tabDetailsPlan_GridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
					if (!((DataGridViewComboBoxColumn)tabDetailsPlan_GridView.Columns[e.ColumnIndex]).Items.Contains(value))
					{
						((DataGridViewComboBoxColumn)tabDetailsPlan_GridView.Columns[e.ColumnIndex]).Items.Add(value);
					}
				}

				throw e.Exception;
			}
			catch (Exception ex)
			{
				Log(string.Format(@"Failed to bind ComboBox. " + "Please contact support with this message:" + ex.Message));
			}
		}

		private void ChangeBackgroundColor(DataGridView gridView, DataGridViewCell selectedCell, Color color, string tooltipText)
		{
			gridView.Rows[selectedCell.RowIndex].Cells[selectedCell.ColumnIndex].Style.BackColor = color;
			gridView.Rows[selectedCell.RowIndex].Cells[selectedCell.ColumnIndex].ToolTipText = tooltipText;
		}

		/// <summary>
		/// TODO: refactor performance
		/// </summary>
		/// <param name="assignee"></param>
		/// <param name="dueDate"></param>
		/// <returns></returns>
		public decimal CalculateAvailableWorkingHoursFromNow(string assignee, DateTime dueDate)
		{
			decimal result = 0;

			//var expectedDateRange = (dueDate.Date - DateTime.Today.Date).TotalDays;
			var currentDate = DateTime.UtcNow;
			var expectedWorkingDays = new List<DateTime>();
			while (currentDate.Date <= dueDate.Date)
			{
				if (!currentDate.IsWeekend() && !currentDate.IsInHolidayGlobal() && !currentDate.IsInHolidayCountry("Viet Nam")) //&& availableWorkingDays.Contains(currentDate.Date))
					expectedWorkingDays.Add(currentDate.Date);
				currentDate = currentDate.Date.AddDays(1);
			}


			var resources = _service._toolkitAllocationsModel.Where(a => a.Resource.LookupValue == assignee).ToList();
			if (!resources.Any()) return 0;

			//var availableWorkingDays = new List<DateTime>();
			Dictionary<DateTime, decimal> availableWorkingDays = new Dictionary<DateTime, decimal>();
			var notAvailableWorkingDays = new List<DateTime>();

			foreach (var resource in resources)
			{
				var startDate = resource.DateFrom.Value.Date;
				var endDate = resource.DateTo.Value.Date;
				while (startDate.Date <= endDate.Date)
				{
					if (expectedWorkingDays.Contains(startDate.Date))
						result = result + resource.HoursCapacity;
					startDate = startDate.Date.AddDays(1);
				}
			}


			var resourceAdjustments = _service._toolkitAllocationAdjustmentsModel.Where(a => a.Resource.LookupValue == assignee).ToList();
			if (resourceAdjustments.Any())
			{
				foreach (var resource in resourceAdjustments)
				{
					var startDate = resource.DateFrom.Value.Date;
					var endDate = resource.DateTo.Value.Date;
					while (startDate.Date <= endDate.Date)
					{
						if (expectedWorkingDays.Contains(startDate.Date))
							result = result - resource.HoursCapacity;
						startDate = startDate.Date.AddDays(1);
					}
				}
			}

			return result;
		}

		private void tabDetailsPlan_btn_open_planning_Click(object sender, EventArgs e)
		{
			var openForm = Application.OpenForms.Cast<Form>().FirstOrDefault(form => form.Name == "PlanningForm");
			if (openForm != null)
			{
				openForm.Close();
			}

			var frm = new PlanningForm(_wpItemsLocal, _service);
			frm.Show();
		}

		#region Test
		DataTable dt = new DataTable();
		private DataTable GetDescriptionTable()
		{
			DataTable l_dtDescription = new DataTable();
			l_dtDescription.Columns.Add("Description", typeof(string));
			l_dtDescription.Columns.Add("Type", typeof(string));

			l_dtDescription.Rows.Add("Lunch", "Expense");
			l_dtDescription.Rows.Add("Dinner", "Expense");
			l_dtDescription.Rows.Add("Breakfast", "Expense");
			l_dtDescription.Rows.Add("Designing", "Service");
			l_dtDescription.Rows.Add("Drawing", "Service");
			l_dtDescription.Rows.Add("Paper", "Material");
			l_dtDescription.Rows.Add("DrawingBoard", "Material");

			return l_dtDescription;
		}

		/// <summary>  
		/// Get datatable of PaidWIth.  
		/// </summary>  
		/// <returns></returns>  
		private DataTable GetPaidWithTable()
		{
			DataTable l_dtPaidwith = new DataTable();
			l_dtPaidwith.Columns.Add("PaidWith", typeof(string));
			l_dtPaidwith.Columns.Add("Code", typeof(string));

			l_dtPaidwith.Rows.Add("CreditCard", "CC");
			l_dtPaidwith.Rows.Add("DebitCard", "DC");

			return l_dtPaidwith;
		}

		/// <summary>  
		/// Get the data for grid.  
		/// </summary>  
		/// <returns></returns>  
		private DataTable GetGridTable()
		{
			DataTable l_dtGridTable = new DataTable();
			l_dtGridTable.Columns.Add("PaidWith", typeof(string));
			l_dtGridTable.Columns.Add("Description", typeof(string));

			l_dtGridTable.Rows.Add("CreditCard", "Drawing");

			return l_dtGridTable;
		}


		GanttChart ganttChart1;
		private void Load_GanttChart()
		{
			//txtLog = new TextBox();
			//txtLog.Dock = DockStyle.Fill;
			//txtLog.Multiline = true;
			//txtLog.Enabled = false;
			//txtLog.ScrollBars = ScrollBars.Horizontal;
			//GanttChartPannel.Controls.Add(txtLog, 0, 3);

			//first Gantt Chart
			ganttChart1 = new GanttChart
			{
				AllowChange = false,
				Dock = DockStyle.Fill,
				FromDate = new DateTime(2015, 12, 12, 0, 0, 0),
				ToDate = new DateTime(2015, 12, 24, 0, 0, 0)
			};
			GanttChartPannel.Controls.Add(ganttChart1, 0, 1);

			ganttChart1.MouseMove += new MouseEventHandler(ganttChart1.GanttChart_MouseMove);
			//ganttChart1.MouseMove += new MouseEventHandler(GanttChart1_MouseMove);
			ganttChart1.MouseDragged += new MouseEventHandler(ganttChart1.GanttChart_MouseDragged);
			ganttChart1.MouseLeave += new EventHandler(ganttChart1.GanttChart_MouseLeave);
			//ganttChart1.ContextMenuStrip = ContextMenuGanttChart1;

			List<BarInformation> timeline = new List<BarInformation>();

			//timeline.Add(new BarInformation("Row 1", new DateTime(2015, 12, 12), new DateTime(2015, 12, 16), Color.Aqua, Color.Khaki, 0));
			//timeline.Add(new BarInformation("Row 2", new DateTime(2015, 12, 13), new DateTime(2015, 12, 20), Color.AliceBlue, Color.Khaki, 1));
			//timeline.Add(new BarInformation("Row 3", new DateTime(2015, 12, 14), new DateTime(2015, 12, 24), Color.Violet, Color.Khaki, 2));
			//timeline.Add(new BarInformation("Row 2", new DateTime(2015, 12, 21), new DateTime(2015, 12, 22, 12, 0, 0), Color.Yellow, Color.Khaki, 1));
			//timeline.Add(new BarInformation("Row 1", new DateTime(2015, 12, 17), new DateTime(2015, 12, 24), Color.LawnGreen, Color.Khaki, 0));

			var clonedData = new List<WPItemModel>();
			_lsvlocalData.ForEach(l => clonedData.Add(l.DeepCopy()));

			foreach (var item in clonedData)
			{
				if (item.WPStart.HasValue && item.WPDueDate.HasValue)
				{
					timeline.Add(new BarInformation($"{item.FeatureShow}-{item.WPTitle} ({item.WPType})",
					  item.WPStart.Value, //					new DateTime(2015, 12, 17),
					  item.WPDueDate.Value, //				new DateTime(2015, 12, 24),
					  Color.LawnGreen, Color.Khaki, 0));
				}
			}

			foreach (BarInformation bar in timeline)
			{
				ganttChart1.AddChartBar(bar.RowText, bar, bar.FromTime, bar.ToTime, bar.Color, bar.HoverColor, bar.Index);
			}
		}
		#endregion


		public delegate void SelectIteration(List<string> selectedIterations);

		private void HideIterationRows(List<string> selectedIterations)
		{
			var filterData = new List<WPItemModel>();
			_lsvlocalData.ForEach(l => filterData.Add(l.DeepCopy()));
			var selectedIteration = filterData.Where(wp => selectedIterations.Contains(wp.WPIterationName)).ToList();

			BuildProgressTracking(selectedIteration);
		}

		private void tabDetailsPlan_GridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (_wpItemsLocalChanges.Any()) return;//we dont do the filter when we have any changes, the changes need to be applied before filtering

			var openForm = Application.OpenForms.Cast<Form>().FirstOrDefault(form => form.Name == "IterationPopup");
			if (openForm != null)
			{
				openForm.Close();
			}

			if (e.Button == MouseButtons.Right)
			{
				IterationPopup m = new IterationPopup(tabDetailsPlan_GridView, HideIterationRows);
				m.Show();
				//m.ad.MenuItems.Add(new MenuItem("Release 3.0- Read team - Iteration 1"));
				//m.MenuItems.Add(new MenuItem("Release 3.0- Read team - Iteration 2"));
				//m.MenuItems.Add(new MenuItem("Release 3.0- Read team - Iteration 3"));
				//m.MenuItems.Add(new MenuItem("Release 3.0- Read team - Iteration 4"));
				//m.MenuItems.Add(new MenuItem("Release 3.0- Read team - Iteration 5"));
				//m.MenuItems.Add(new MenuItem("Release 3.0- Read team - Iteration 6"));
				//m.MenuItems.Add(new MenuItem("Release 3.0- Read team - Iteration 7"));
				//m.MenuItems.Add(new MenuItem("Release 3.0- Read team - Iteration 8"));

				//m.MenuItems.Add(new CheckBox() { Text = "Release 3.0- Read team - Iteration 7" }));
				//m.MenuItems.Add(new MenuItem("Release 3.0- Read team - Iteration 8"));

				//int currentMouseOverRow = tabDetailsPlan_GridView.HitTest(e.X, e.Y).RowIndex;

				//if (currentMouseOverRow >= 0)
				//{
				//	m.MenuItems.Add(new MenuItem(string.Format("Do something to row {0}", currentMouseOverRow.ToString())));
				//}

				////m.Show(tabDetailsPlan_GridView, new Point(e.X, e.Y));
				//m.Location(new Point(e.X, e.Y));
			}
		}
	}
}
