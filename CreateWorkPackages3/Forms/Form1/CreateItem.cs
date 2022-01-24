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

namespace CreateWorkPackages3
{
	/// <summary>
	/// Details tab
	/// </summary>
	public partial class Form1: Form
	{
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
				Release = BusinessLibrary.Common.Configuration._defaultReleaseId,
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
	}
}
