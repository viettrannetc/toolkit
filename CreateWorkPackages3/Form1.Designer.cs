using System.Windows.Forms;

namespace CreateWorkPackages3
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.btnLoadDaily = new System.Windows.Forms.Button();
			this.cbbRelease = new System.Windows.Forms.ComboBox();
			this.timeEntries = new System.Windows.Forms.Button();
			this.tabMain = new System.Windows.Forms.TabControl();
			this.tabDaily = new System.Windows.Forms.TabPage();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.Daily_DataGridView = new System.Windows.Forms.DataGridView();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.Daily_Filter_cbb_assignee = new System.Windows.Forms.ComboBox();
			this.Daily_Filter_cbb_Iteration = new System.Windows.Forms.ComboBox();
			this.Daily_Filter_cbb_Status = new System.Windows.Forms.ComboBox();
			this.Daily_Filter_btn_filter = new System.Windows.Forms.Button();
			this.Daily_Filter_cbb_Team = new System.Windows.Forms.ComboBox();
			this.Daily_Filter_cbb_Feature = new System.Windows.Forms.ComboBox();
			this.Daily_Filter_cbb_US = new System.Windows.Forms.ComboBox();
			this.tabCreateItem = new System.Windows.Forms.TabPage();
			this.tabControlCreateNewItem = new System.Windows.Forms.TabControl();
			this.tcCreateNewItem_tabUS = new System.Windows.Forms.TabPage();
			this.label15 = new System.Windows.Forms.Label();
			this.US_txt_US_Id = new System.Windows.Forms.TextBox();
			this.US_btn_calculateEstimate = new System.Windows.Forms.Button();
			this.US_txt_estimate_clientworkshop_wp = new System.Windows.Forms.TextBox();
			this.US_checkbox_createClientWorkShopWP = new System.Windows.Forms.CheckBox();
			this.txtUSTitle = new System.Windows.Forms.RichTextBox();
			this.US_txt_estimate_designintegrate_wp = new System.Windows.Forms.TextBox();
			this.US_txt_estimate_executeTC_wp = new System.Windows.Forms.TextBox();
			this.US_txt_estimate_createTC_wp = new System.Windows.Forms.TextBox();
			this.US_txt_remaining_estimate = new System.Windows.Forms.TextBox();
			this.US_txt_estimate_bugfixing_wp = new System.Windows.Forms.TextBox();
			this.US_txt_estimate_developmentintegrate_wp = new System.Windows.Forms.TextBox();
			this.US_txt_estimate_development_wp = new System.Windows.Forms.TextBox();
			this.US_txt_estimate_designar_wp = new System.Windows.Forms.TextBox();
			this.US_txt_estimate_designui_wp = new System.Windows.Forms.TextBox();
			this.US_txt_estimate_analyze_wp = new System.Windows.Forms.TextBox();
			this.US_checkbox_createBugFixingWP = new System.Windows.Forms.CheckBox();
			this.US_checkbox_createExecuteTCWP = new System.Windows.Forms.CheckBox();
			this.US_checkbox_createTCWP = new System.Windows.Forms.CheckBox();
			this.US_checkbox_createDevelopmentIntegrationWP = new System.Windows.Forms.CheckBox();
			this.US_checkbox_createDevelopmentWP = new System.Windows.Forms.CheckBox();
			this.US_checkbox_createDesignIntegrationWP = new System.Windows.Forms.CheckBox();
			this.US_checkbox_createDesignArWP = new System.Windows.Forms.CheckBox();
			this.US_checkbox_createDesignUIWP = new System.Windows.Forms.CheckBox();
			this.US_checkbox_createAnalyzeWP = new System.Windows.Forms.CheckBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtUSEstimation = new System.Windows.Forms.TextBox();
			this.US_checkbox_IncludeStandardWP = new System.Windows.Forms.CheckBox();
			this.btn_US_AddItem = new System.Windows.Forms.Button();
			this.txtUSNote = new System.Windows.Forms.RichTextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.txtUSTeamId = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtUSFeatureId = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.tcCreateNewItem_tabWP = new System.Windows.Forms.TabPage();
			this.tabControl2 = new System.Windows.Forms.TabControl();
			this.createItem_WP_random = new System.Windows.Forms.TabPage();
			this.WP_Random_USId = new System.Windows.Forms.TextBox();
			this.WP_Random_Btn_AddItem = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.WP_Random_Title = new System.Windows.Forms.TextBox();
			this.WP_Random_Estimate = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.WP_Random_Release = new System.Windows.Forms.ComboBox();
			this.WP_Random_WPType = new System.Windows.Forms.ComboBox();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.WP_Random_TeamId = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.txtWPNote = new System.Windows.Forms.RichTextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.createItem_WP_from_Defect = new System.Windows.Forms.TabPage();
			this.WP_Defect_DefectId = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.label14 = new System.Windows.Forms.Label();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.label17 = new System.Windows.Forms.Label();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.label20 = new System.Windows.Forms.Label();
			this.createItem_WP_update = new System.Windows.Forms.TabPage();
			this.createItem_WP_Update_duedate = new System.Windows.Forms.DateTimePicker();
			this.createItem_WP_Update_txt_WP_Id = new System.Windows.Forms.TextBox();
			this.button3 = new System.Windows.Forms.Button();
			this.createItem_WP_Update_button = new System.Windows.Forms.Button();
			this.label16 = new System.Windows.Forms.Label();
			this.createItem_WP_Update_title = new System.Windows.Forms.TextBox();
			this.createItem_WP_Update_estimate = new System.Windows.Forms.TextBox();
			this.label18 = new System.Windows.Forms.Label();
			this.createItem_WP_Update_release = new System.Windows.Forms.ComboBox();
			this.createItem_WP_Update_wpType = new System.Windows.Forms.ComboBox();
			this.label19 = new System.Windows.Forms.Label();
			this.label25 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.label22 = new System.Windows.Forms.Label();
			this.createItem_WP_Update_teamId = new System.Windows.Forms.TextBox();
			this.label23 = new System.Windows.Forms.Label();
			this.createItem_WP_Update_note = new System.Windows.Forms.RichTextBox();
			this.label24 = new System.Windows.Forms.Label();
			this.tabOthers = new System.Windows.Forms.TabPage();
			this.tabGanttchart = new System.Windows.Forms.TabPage();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.GanttChartPannel = new System.Windows.Forms.TableLayoutPanel();
			this.tabDetailPlan = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel_DetailsPlan = new System.Windows.Forms.TableLayoutPanel();
			this.tabDetailsPlan_TabControl = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.tabDetailsPlan_GridView_changes = new System.Windows.Forms.DataGridView();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.detailPlan_label_status = new System.Windows.Forms.Label();
			this.label27 = new System.Windows.Forms.Label();
			this.tabDetailsPlan_btn_open_planning = new System.Windows.Forms.Button();
			this.tabDetailsPlan_btn_apply_changes = new System.Windows.Forms.Button();
			this.tabDetails_btn_refersh = new System.Windows.Forms.Button();
			this.tabDetailsPlan_btn_pushToToolkit = new System.Windows.Forms.Button();
			this.tabDetailsPlan_GridView = new System.Windows.Forms.DataGridView();
			this.tabLog = new System.Windows.Forms.TabPage();
			this.Daily_log_textbox = new System.Windows.Forms.RichTextBox();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.label26 = new System.Windows.Forms.Label();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.tabMain.SuspendLayout();
			this.tabDaily.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Daily_DataGridView)).BeginInit();
			this.groupBox6.SuspendLayout();
			this.tabCreateItem.SuspendLayout();
			this.tabControlCreateNewItem.SuspendLayout();
			this.tcCreateNewItem_tabUS.SuspendLayout();
			this.tcCreateNewItem_tabWP.SuspendLayout();
			this.tabControl2.SuspendLayout();
			this.createItem_WP_random.SuspendLayout();
			this.createItem_WP_from_Defect.SuspendLayout();
			this.createItem_WP_update.SuspendLayout();
			this.tabOthers.SuspendLayout();
			this.tabGanttchart.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.tabDetailPlan.SuspendLayout();
			this.tableLayoutPanel_DetailsPlan.SuspendLayout();
			this.tabDetailsPlan_TabControl.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tabDetailsPlan_GridView_changes)).BeginInit();
			this.groupBox5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tabDetailsPlan_GridView)).BeginInit();
			this.tabLog.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnLoadDaily
			// 
			this.btnLoadDaily.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnLoadDaily.Location = new System.Drawing.Point(28, 54);
			this.btnLoadDaily.Name = "btnLoadDaily";
			this.btnLoadDaily.Size = new System.Drawing.Size(136, 34);
			this.btnLoadDaily.TabIndex = 1;
			this.btnLoadDaily.Text = "Renew local data";
			this.btnLoadDaily.UseVisualStyleBackColor = true;
			this.btnLoadDaily.Click += new System.EventHandler(this.PullLatestData_Click);
			// 
			// cbbRelease
			// 
			this.cbbRelease.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cbbRelease.FormattingEnabled = true;
			this.cbbRelease.Items.AddRange(new object[] {
            "Sprint 51"});
			this.cbbRelease.Location = new System.Drawing.Point(28, 19);
			this.cbbRelease.Name = "cbbRelease";
			this.cbbRelease.Size = new System.Drawing.Size(136, 21);
			this.cbbRelease.TabIndex = 2;
			this.cbbRelease.Text = "Choose Release";
			// 
			// timeEntries
			// 
			this.timeEntries.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.timeEntries.Location = new System.Drawing.Point(114, 397);
			this.timeEntries.Name = "timeEntries";
			this.timeEntries.Size = new System.Drawing.Size(146, 67);
			this.timeEntries.TabIndex = 8;
			this.timeEntries.Text = "Create Timeentries";
			this.timeEntries.UseVisualStyleBackColor = true;
			// 
			// tabMain
			// 
			this.tabMain.Controls.Add(this.tabDaily);
			this.tabMain.Controls.Add(this.tabCreateItem);
			this.tabMain.Controls.Add(this.tabOthers);
			this.tabMain.Controls.Add(this.tabGanttchart);
			this.tabMain.Controls.Add(this.tabDetailPlan);
			this.tabMain.Controls.Add(this.tabLog);
			this.tabMain.Controls.Add(this.tabPage3);
			this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabMain.Location = new System.Drawing.Point(0, 0);
			this.tabMain.Name = "tabMain";
			this.tabMain.SelectedIndex = 0;
			this.tabMain.Size = new System.Drawing.Size(1584, 913);
			this.tabMain.TabIndex = 19;
			// 
			// tabDaily
			// 
			this.tabDaily.Controls.Add(this.groupBox1);
			this.tabDaily.Controls.Add(this.groupBox6);
			this.tabDaily.Location = new System.Drawing.Point(4, 22);
			this.tabDaily.Name = "tabDaily";
			this.tabDaily.Padding = new System.Windows.Forms.Padding(3);
			this.tabDaily.Size = new System.Drawing.Size(1576, 887);
			this.tabDaily.TabIndex = 0;
			this.tabDaily.Text = "Daily";
			this.tabDaily.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.Daily_DataGridView);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(1570, 781);
			this.groupBox1.TabIndex = 10;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Daily data";
			// 
			// Daily_DataGridView
			// 
			this.Daily_DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.Daily_DataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Daily_DataGridView.Location = new System.Drawing.Point(3, 16);
			this.Daily_DataGridView.Name = "Daily_DataGridView";
			this.Daily_DataGridView.Size = new System.Drawing.Size(1564, 762);
			this.Daily_DataGridView.TabIndex = 8;
			// 
			// groupBox6
			// 
			this.groupBox6.Controls.Add(this.Daily_Filter_cbb_assignee);
			this.groupBox6.Controls.Add(this.cbbRelease);
			this.groupBox6.Controls.Add(this.Daily_Filter_cbb_Iteration);
			this.groupBox6.Controls.Add(this.btnLoadDaily);
			this.groupBox6.Controls.Add(this.Daily_Filter_cbb_Status);
			this.groupBox6.Controls.Add(this.Daily_Filter_btn_filter);
			this.groupBox6.Controls.Add(this.Daily_Filter_cbb_Team);
			this.groupBox6.Controls.Add(this.Daily_Filter_cbb_Feature);
			this.groupBox6.Controls.Add(this.Daily_Filter_cbb_US);
			this.groupBox6.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.groupBox6.Location = new System.Drawing.Point(3, 784);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(1570, 100);
			this.groupBox6.TabIndex = 9;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Filter data";
			// 
			// Daily_Filter_cbb_assignee
			// 
			this.Daily_Filter_cbb_assignee.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.Daily_Filter_cbb_assignee.FormattingEnabled = true;
			this.Daily_Filter_cbb_assignee.Items.AddRange(new object[] {
            "Sprint 51"});
			this.Daily_Filter_cbb_assignee.Location = new System.Drawing.Point(784, 19);
			this.Daily_Filter_cbb_assignee.Name = "Daily_Filter_cbb_assignee";
			this.Daily_Filter_cbb_assignee.Size = new System.Drawing.Size(136, 21);
			this.Daily_Filter_cbb_assignee.TabIndex = 8;
			this.Daily_Filter_cbb_assignee.Text = "-- Assignee -- ";
			// 
			// Daily_Filter_cbb_Iteration
			// 
			this.Daily_Filter_cbb_Iteration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.Daily_Filter_cbb_Iteration.FormattingEnabled = true;
			this.Daily_Filter_cbb_Iteration.Items.AddRange(new object[] {
            "Sprint 51"});
			this.Daily_Filter_cbb_Iteration.Location = new System.Drawing.Point(784, 54);
			this.Daily_Filter_cbb_Iteration.Name = "Daily_Filter_cbb_Iteration";
			this.Daily_Filter_cbb_Iteration.Size = new System.Drawing.Size(136, 21);
			this.Daily_Filter_cbb_Iteration.TabIndex = 5;
			this.Daily_Filter_cbb_Iteration.Text = "-- Iteration --";
			// 
			// Daily_Filter_cbb_Status
			// 
			this.Daily_Filter_cbb_Status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.Daily_Filter_cbb_Status.FormattingEnabled = true;
			this.Daily_Filter_cbb_Status.Items.AddRange(new object[] {
            "Sprint 51"});
			this.Daily_Filter_cbb_Status.Location = new System.Drawing.Point(624, 54);
			this.Daily_Filter_cbb_Status.Name = "Daily_Filter_cbb_Status";
			this.Daily_Filter_cbb_Status.Size = new System.Drawing.Size(136, 21);
			this.Daily_Filter_cbb_Status.TabIndex = 7;
			this.Daily_Filter_cbb_Status.Text = "-- Status -- ";
			// 
			// Daily_Filter_btn_filter
			// 
			this.Daily_Filter_btn_filter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.Daily_Filter_btn_filter.Location = new System.Drawing.Point(936, 19);
			this.Daily_Filter_btn_filter.Name = "Daily_Filter_btn_filter";
			this.Daily_Filter_btn_filter.Size = new System.Drawing.Size(136, 56);
			this.Daily_Filter_btn_filter.TabIndex = 1;
			this.Daily_Filter_btn_filter.Text = "Filter Local data";
			this.Daily_Filter_btn_filter.UseVisualStyleBackColor = true;
			this.Daily_Filter_btn_filter.Click += new System.EventHandler(this.Daily_Filter_btn_filter_Click);
			// 
			// Daily_Filter_cbb_Team
			// 
			this.Daily_Filter_cbb_Team.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.Daily_Filter_cbb_Team.FormattingEnabled = true;
			this.Daily_Filter_cbb_Team.Items.AddRange(new object[] {
            "Sprint 51"});
			this.Daily_Filter_cbb_Team.Location = new System.Drawing.Point(461, 54);
			this.Daily_Filter_cbb_Team.Name = "Daily_Filter_cbb_Team";
			this.Daily_Filter_cbb_Team.Size = new System.Drawing.Size(136, 21);
			this.Daily_Filter_cbb_Team.TabIndex = 6;
			this.Daily_Filter_cbb_Team.Text = "-- Team -- ";
			// 
			// Daily_Filter_cbb_Feature
			// 
			this.Daily_Filter_cbb_Feature.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.Daily_Filter_cbb_Feature.FormattingEnabled = true;
			this.Daily_Filter_cbb_Feature.Items.AddRange(new object[] {
            "Sprint 51"});
			this.Daily_Filter_cbb_Feature.Location = new System.Drawing.Point(461, 19);
			this.Daily_Filter_cbb_Feature.Name = "Daily_Filter_cbb_Feature";
			this.Daily_Filter_cbb_Feature.Size = new System.Drawing.Size(136, 21);
			this.Daily_Filter_cbb_Feature.TabIndex = 2;
			this.Daily_Filter_cbb_Feature.Text = "-- Feature --";
			// 
			// Daily_Filter_cbb_US
			// 
			this.Daily_Filter_cbb_US.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.Daily_Filter_cbb_US.FormattingEnabled = true;
			this.Daily_Filter_cbb_US.Items.AddRange(new object[] {
            "Sprint 51"});
			this.Daily_Filter_cbb_US.Location = new System.Drawing.Point(624, 19);
			this.Daily_Filter_cbb_US.Name = "Daily_Filter_cbb_US";
			this.Daily_Filter_cbb_US.Size = new System.Drawing.Size(136, 21);
			this.Daily_Filter_cbb_US.TabIndex = 5;
			this.Daily_Filter_cbb_US.Text = "-- US --";
			// 
			// tabCreateItem
			// 
			this.tabCreateItem.Controls.Add(this.tabControlCreateNewItem);
			this.tabCreateItem.Location = new System.Drawing.Point(4, 22);
			this.tabCreateItem.Name = "tabCreateItem";
			this.tabCreateItem.Padding = new System.Windows.Forms.Padding(3);
			this.tabCreateItem.Size = new System.Drawing.Size(1576, 887);
			this.tabCreateItem.TabIndex = 1;
			this.tabCreateItem.Text = "Create Items";
			this.tabCreateItem.UseVisualStyleBackColor = true;
			// 
			// tabControlCreateNewItem
			// 
			this.tabControlCreateNewItem.Controls.Add(this.tcCreateNewItem_tabUS);
			this.tabControlCreateNewItem.Controls.Add(this.tcCreateNewItem_tabWP);
			this.tabControlCreateNewItem.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControlCreateNewItem.Location = new System.Drawing.Point(3, 3);
			this.tabControlCreateNewItem.Name = "tabControlCreateNewItem";
			this.tabControlCreateNewItem.SelectedIndex = 0;
			this.tabControlCreateNewItem.Size = new System.Drawing.Size(1570, 881);
			this.tabControlCreateNewItem.TabIndex = 7;
			// 
			// tcCreateNewItem_tabUS
			// 
			this.tcCreateNewItem_tabUS.Controls.Add(this.label15);
			this.tcCreateNewItem_tabUS.Controls.Add(this.US_txt_US_Id);
			this.tcCreateNewItem_tabUS.Controls.Add(this.US_btn_calculateEstimate);
			this.tcCreateNewItem_tabUS.Controls.Add(this.US_txt_estimate_clientworkshop_wp);
			this.tcCreateNewItem_tabUS.Controls.Add(this.US_checkbox_createClientWorkShopWP);
			this.tcCreateNewItem_tabUS.Controls.Add(this.txtUSTitle);
			this.tcCreateNewItem_tabUS.Controls.Add(this.US_txt_estimate_designintegrate_wp);
			this.tcCreateNewItem_tabUS.Controls.Add(this.US_txt_estimate_executeTC_wp);
			this.tcCreateNewItem_tabUS.Controls.Add(this.US_txt_estimate_createTC_wp);
			this.tcCreateNewItem_tabUS.Controls.Add(this.US_txt_remaining_estimate);
			this.tcCreateNewItem_tabUS.Controls.Add(this.US_txt_estimate_bugfixing_wp);
			this.tcCreateNewItem_tabUS.Controls.Add(this.US_txt_estimate_developmentintegrate_wp);
			this.tcCreateNewItem_tabUS.Controls.Add(this.US_txt_estimate_development_wp);
			this.tcCreateNewItem_tabUS.Controls.Add(this.US_txt_estimate_designar_wp);
			this.tcCreateNewItem_tabUS.Controls.Add(this.US_txt_estimate_designui_wp);
			this.tcCreateNewItem_tabUS.Controls.Add(this.US_txt_estimate_analyze_wp);
			this.tcCreateNewItem_tabUS.Controls.Add(this.US_checkbox_createBugFixingWP);
			this.tcCreateNewItem_tabUS.Controls.Add(this.US_checkbox_createExecuteTCWP);
			this.tcCreateNewItem_tabUS.Controls.Add(this.US_checkbox_createTCWP);
			this.tcCreateNewItem_tabUS.Controls.Add(this.US_checkbox_createDevelopmentIntegrationWP);
			this.tcCreateNewItem_tabUS.Controls.Add(this.US_checkbox_createDevelopmentWP);
			this.tcCreateNewItem_tabUS.Controls.Add(this.US_checkbox_createDesignIntegrationWP);
			this.tcCreateNewItem_tabUS.Controls.Add(this.US_checkbox_createDesignArWP);
			this.tcCreateNewItem_tabUS.Controls.Add(this.US_checkbox_createDesignUIWP);
			this.tcCreateNewItem_tabUS.Controls.Add(this.US_checkbox_createAnalyzeWP);
			this.tcCreateNewItem_tabUS.Controls.Add(this.label8);
			this.tcCreateNewItem_tabUS.Controls.Add(this.label3);
			this.tcCreateNewItem_tabUS.Controls.Add(this.txtUSEstimation);
			this.tcCreateNewItem_tabUS.Controls.Add(this.US_checkbox_IncludeStandardWP);
			this.tcCreateNewItem_tabUS.Controls.Add(this.btn_US_AddItem);
			this.tcCreateNewItem_tabUS.Controls.Add(this.txtUSNote);
			this.tcCreateNewItem_tabUS.Controls.Add(this.label7);
			this.tcCreateNewItem_tabUS.Controls.Add(this.label6);
			this.tcCreateNewItem_tabUS.Controls.Add(this.txtUSTeamId);
			this.tcCreateNewItem_tabUS.Controls.Add(this.label4);
			this.tcCreateNewItem_tabUS.Controls.Add(this.txtUSFeatureId);
			this.tcCreateNewItem_tabUS.Controls.Add(this.label2);
			this.tcCreateNewItem_tabUS.Location = new System.Drawing.Point(4, 22);
			this.tcCreateNewItem_tabUS.Name = "tcCreateNewItem_tabUS";
			this.tcCreateNewItem_tabUS.Padding = new System.Windows.Forms.Padding(3);
			this.tcCreateNewItem_tabUS.Size = new System.Drawing.Size(1562, 855);
			this.tcCreateNewItem_tabUS.TabIndex = 0;
			this.tcCreateNewItem_tabUS.Text = "US";
			this.tcCreateNewItem_tabUS.UseVisualStyleBackColor = true;
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(31, 485);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(34, 13);
			this.label15.TabIndex = 61;
			this.label15.Text = "US Id";
			// 
			// US_txt_US_Id
			// 
			this.US_txt_US_Id.Location = new System.Drawing.Point(107, 482);
			this.US_txt_US_Id.Name = "US_txt_US_Id";
			this.US_txt_US_Id.Size = new System.Drawing.Size(272, 20);
			this.US_txt_US_Id.TabIndex = 60;
			// 
			// US_btn_calculateEstimate
			// 
			this.US_btn_calculateEstimate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.US_btn_calculateEstimate.Location = new System.Drawing.Point(794, 648);
			this.US_btn_calculateEstimate.Name = "US_btn_calculateEstimate";
			this.US_btn_calculateEstimate.Size = new System.Drawing.Size(101, 38);
			this.US_btn_calculateEstimate.TabIndex = 59;
			this.US_btn_calculateEstimate.Text = "Calculate";
			this.US_btn_calculateEstimate.UseVisualStyleBackColor = true;
			this.US_btn_calculateEstimate.Click += new System.EventHandler(this.US_btn_calculateEstimate_Click);
			// 
			// US_txt_estimate_clientworkshop_wp
			// 
			this.US_txt_estimate_clientworkshop_wp.Location = new System.Drawing.Point(667, 178);
			this.US_txt_estimate_clientworkshop_wp.Name = "US_txt_estimate_clientworkshop_wp";
			this.US_txt_estimate_clientworkshop_wp.Size = new System.Drawing.Size(101, 20);
			this.US_txt_estimate_clientworkshop_wp.TabIndex = 58;
			// 
			// US_checkbox_createClientWorkShopWP
			// 
			this.US_checkbox_createClientWorkShopWP.AutoSize = true;
			this.US_checkbox_createClientWorkShopWP.Location = new System.Drawing.Point(500, 181);
			this.US_checkbox_createClientWorkShopWP.Name = "US_checkbox_createClientWorkShopWP";
			this.US_checkbox_createClientWorkShopWP.Size = new System.Drawing.Size(69, 17);
			this.US_checkbox_createClientWorkShopWP.TabIndex = 35;
			this.US_checkbox_createClientWorkShopWP.Text = "Meetings";
			this.US_checkbox_createClientWorkShopWP.UseVisualStyleBackColor = true;
			// 
			// txtUSTitle
			// 
			this.txtUSTitle.Location = new System.Drawing.Point(107, 82);
			this.txtUSTitle.Name = "txtUSTitle";
			this.txtUSTitle.Size = new System.Drawing.Size(272, 102);
			this.txtUSTitle.TabIndex = 28;
			this.txtUSTitle.Text = "";
			// 
			// US_txt_estimate_designintegrate_wp
			// 
			this.US_txt_estimate_designintegrate_wp.Location = new System.Drawing.Point(667, 246);
			this.US_txt_estimate_designintegrate_wp.Name = "US_txt_estimate_designintegrate_wp";
			this.US_txt_estimate_designintegrate_wp.Size = new System.Drawing.Size(101, 20);
			this.US_txt_estimate_designintegrate_wp.TabIndex = 55;
			// 
			// US_txt_estimate_executeTC_wp
			// 
			this.US_txt_estimate_executeTC_wp.Location = new System.Drawing.Point(667, 400);
			this.US_txt_estimate_executeTC_wp.Name = "US_txt_estimate_executeTC_wp";
			this.US_txt_estimate_executeTC_wp.Size = new System.Drawing.Size(101, 20);
			this.US_txt_estimate_executeTC_wp.TabIndex = 53;
			// 
			// US_txt_estimate_createTC_wp
			// 
			this.US_txt_estimate_createTC_wp.Location = new System.Drawing.Point(667, 359);
			this.US_txt_estimate_createTC_wp.Name = "US_txt_estimate_createTC_wp";
			this.US_txt_estimate_createTC_wp.Size = new System.Drawing.Size(101, 20);
			this.US_txt_estimate_createTC_wp.TabIndex = 54;
			// 
			// US_txt_remaining_estimate
			// 
			this.US_txt_remaining_estimate.Location = new System.Drawing.Point(667, 500);
			this.US_txt_remaining_estimate.Name = "US_txt_remaining_estimate";
			this.US_txt_remaining_estimate.Size = new System.Drawing.Size(101, 20);
			this.US_txt_remaining_estimate.TabIndex = 51;
			// 
			// US_txt_estimate_bugfixing_wp
			// 
			this.US_txt_estimate_bugfixing_wp.Location = new System.Drawing.Point(667, 438);
			this.US_txt_estimate_bugfixing_wp.Name = "US_txt_estimate_bugfixing_wp";
			this.US_txt_estimate_bugfixing_wp.Size = new System.Drawing.Size(101, 20);
			this.US_txt_estimate_bugfixing_wp.TabIndex = 51;
			// 
			// US_txt_estimate_developmentintegrate_wp
			// 
			this.US_txt_estimate_developmentintegrate_wp.Location = new System.Drawing.Point(667, 320);
			this.US_txt_estimate_developmentintegrate_wp.Name = "US_txt_estimate_developmentintegrate_wp";
			this.US_txt_estimate_developmentintegrate_wp.Size = new System.Drawing.Size(101, 20);
			this.US_txt_estimate_developmentintegrate_wp.TabIndex = 52;
			// 
			// US_txt_estimate_development_wp
			// 
			this.US_txt_estimate_development_wp.Location = new System.Drawing.Point(667, 283);
			this.US_txt_estimate_development_wp.Name = "US_txt_estimate_development_wp";
			this.US_txt_estimate_development_wp.Size = new System.Drawing.Size(101, 20);
			this.US_txt_estimate_development_wp.TabIndex = 49;
			// 
			// US_txt_estimate_designar_wp
			// 
			this.US_txt_estimate_designar_wp.Location = new System.Drawing.Point(667, 213);
			this.US_txt_estimate_designar_wp.Name = "US_txt_estimate_designar_wp";
			this.US_txt_estimate_designar_wp.Size = new System.Drawing.Size(101, 20);
			this.US_txt_estimate_designar_wp.TabIndex = 50;
			// 
			// US_txt_estimate_designui_wp
			// 
			this.US_txt_estimate_designui_wp.Location = new System.Drawing.Point(667, 142);
			this.US_txt_estimate_designui_wp.Name = "US_txt_estimate_designui_wp";
			this.US_txt_estimate_designui_wp.Size = new System.Drawing.Size(101, 20);
			this.US_txt_estimate_designui_wp.TabIndex = 48;
			// 
			// US_txt_estimate_analyze_wp
			// 
			this.US_txt_estimate_analyze_wp.Location = new System.Drawing.Point(667, 102);
			this.US_txt_estimate_analyze_wp.Name = "US_txt_estimate_analyze_wp";
			this.US_txt_estimate_analyze_wp.Size = new System.Drawing.Size(101, 20);
			this.US_txt_estimate_analyze_wp.TabIndex = 48;
			// 
			// US_checkbox_createBugFixingWP
			// 
			this.US_checkbox_createBugFixingWP.AutoSize = true;
			this.US_checkbox_createBugFixingWP.Location = new System.Drawing.Point(500, 441);
			this.US_checkbox_createBugFixingWP.Name = "US_checkbox_createBugFixingWP";
			this.US_checkbox_createBugFixingWP.Size = new System.Drawing.Size(72, 17);
			this.US_checkbox_createBugFixingWP.TabIndex = 42;
			this.US_checkbox_createBugFixingWP.Text = "Bug fixing";
			this.US_checkbox_createBugFixingWP.UseVisualStyleBackColor = true;
			// 
			// US_checkbox_createExecuteTCWP
			// 
			this.US_checkbox_createExecuteTCWP.AutoSize = true;
			this.US_checkbox_createExecuteTCWP.Location = new System.Drawing.Point(500, 403);
			this.US_checkbox_createExecuteTCWP.Name = "US_checkbox_createExecuteTCWP";
			this.US_checkbox_createExecuteTCWP.Size = new System.Drawing.Size(115, 17);
			this.US_checkbox_createExecuteTCWP.TabIndex = 41;
			this.US_checkbox_createExecuteTCWP.Text = "Execute Test case";
			this.US_checkbox_createExecuteTCWP.UseVisualStyleBackColor = true;
			// 
			// US_checkbox_createTCWP
			// 
			this.US_checkbox_createTCWP.AutoSize = true;
			this.US_checkbox_createTCWP.Location = new System.Drawing.Point(500, 362);
			this.US_checkbox_createTCWP.Name = "US_checkbox_createTCWP";
			this.US_checkbox_createTCWP.Size = new System.Drawing.Size(108, 17);
			this.US_checkbox_createTCWP.TabIndex = 40;
			this.US_checkbox_createTCWP.Text = "Create Test Case";
			this.US_checkbox_createTCWP.UseVisualStyleBackColor = true;
			// 
			// US_checkbox_createDevelopmentIntegrationWP
			// 
			this.US_checkbox_createDevelopmentIntegrationWP.AutoSize = true;
			this.US_checkbox_createDevelopmentIntegrationWP.Location = new System.Drawing.Point(500, 323);
			this.US_checkbox_createDevelopmentIntegrationWP.Name = "US_checkbox_createDevelopmentIntegrationWP";
			this.US_checkbox_createDevelopmentIntegrationWP.Size = new System.Drawing.Size(148, 17);
			this.US_checkbox_createDevelopmentIntegrationWP.TabIndex = 39;
			this.US_checkbox_createDevelopmentIntegrationWP.Text = "Development - Integration";
			this.US_checkbox_createDevelopmentIntegrationWP.UseVisualStyleBackColor = true;
			// 
			// US_checkbox_createDevelopmentWP
			// 
			this.US_checkbox_createDevelopmentWP.AutoSize = true;
			this.US_checkbox_createDevelopmentWP.Location = new System.Drawing.Point(500, 283);
			this.US_checkbox_createDevelopmentWP.Name = "US_checkbox_createDevelopmentWP";
			this.US_checkbox_createDevelopmentWP.Size = new System.Drawing.Size(89, 17);
			this.US_checkbox_createDevelopmentWP.TabIndex = 38;
			this.US_checkbox_createDevelopmentWP.Text = "Development";
			this.US_checkbox_createDevelopmentWP.UseVisualStyleBackColor = true;
			// 
			// US_checkbox_createDesignIntegrationWP
			// 
			this.US_checkbox_createDesignIntegrationWP.AutoSize = true;
			this.US_checkbox_createDesignIntegrationWP.Location = new System.Drawing.Point(500, 249);
			this.US_checkbox_createDesignIntegrationWP.Name = "US_checkbox_createDesignIntegrationWP";
			this.US_checkbox_createDesignIntegrationWP.Size = new System.Drawing.Size(112, 17);
			this.US_checkbox_createDesignIntegrationWP.TabIndex = 37;
			this.US_checkbox_createDesignIntegrationWP.Text = "Design Integration";
			this.US_checkbox_createDesignIntegrationWP.UseVisualStyleBackColor = true;
			// 
			// US_checkbox_createDesignArWP
			// 
			this.US_checkbox_createDesignArWP.AutoSize = true;
			this.US_checkbox_createDesignArWP.Location = new System.Drawing.Point(500, 216);
			this.US_checkbox_createDesignArWP.Name = "US_checkbox_createDesignArWP";
			this.US_checkbox_createDesignArWP.Size = new System.Drawing.Size(104, 17);
			this.US_checkbox_createDesignArWP.TabIndex = 36;
			this.US_checkbox_createDesignArWP.Text = "Design Architect";
			this.US_checkbox_createDesignArWP.UseVisualStyleBackColor = true;
			// 
			// US_checkbox_createDesignUIWP
			// 
			this.US_checkbox_createDesignUIWP.AutoSize = true;
			this.US_checkbox_createDesignUIWP.Location = new System.Drawing.Point(500, 145);
			this.US_checkbox_createDesignUIWP.Name = "US_checkbox_createDesignUIWP";
			this.US_checkbox_createDesignUIWP.Size = new System.Drawing.Size(73, 17);
			this.US_checkbox_createDesignUIWP.TabIndex = 34;
			this.US_checkbox_createDesignUIWP.Text = "Design UI";
			this.US_checkbox_createDesignUIWP.UseVisualStyleBackColor = true;
			// 
			// US_checkbox_createAnalyzeWP
			// 
			this.US_checkbox_createAnalyzeWP.AutoSize = true;
			this.US_checkbox_createAnalyzeWP.Location = new System.Drawing.Point(500, 105);
			this.US_checkbox_createAnalyzeWP.Name = "US_checkbox_createAnalyzeWP";
			this.US_checkbox_createAnalyzeWP.Size = new System.Drawing.Size(63, 17);
			this.US_checkbox_createAnalyzeWP.TabIndex = 33;
			this.US_checkbox_createAnalyzeWP.Text = "Analyze";
			this.US_checkbox_createAnalyzeWP.UseVisualStyleBackColor = true;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(664, 482);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(108, 13);
			this.label8.TabIndex = 39;
			this.label8.Text = "Remaining Estimation";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(442, 36);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(55, 13);
			this.label3.TabIndex = 39;
			this.label3.Text = "Estimation";
			// 
			// txtUSEstimation
			// 
			this.txtUSEstimation.Location = new System.Drawing.Point(667, 36);
			this.txtUSEstimation.Name = "txtUSEstimation";
			this.txtUSEstimation.Size = new System.Drawing.Size(101, 20);
			this.txtUSEstimation.TabIndex = 31;
			this.txtUSEstimation.TextChanged += new System.EventHandler(this.txtUSEstimation_TextChanged);
			// 
			// US_checkbox_IncludeStandardWP
			// 
			this.US_checkbox_IncludeStandardWP.AutoSize = true;
			this.US_checkbox_IncludeStandardWP.Location = new System.Drawing.Point(445, 71);
			this.US_checkbox_IncludeStandardWP.Name = "US_checkbox_IncludeStandardWP";
			this.US_checkbox_IncludeStandardWP.Size = new System.Drawing.Size(139, 17);
			this.US_checkbox_IncludeStandardWP.TabIndex = 32;
			this.US_checkbox_IncludeStandardWP.Text = "Include Standard WPs?";
			this.US_checkbox_IncludeStandardWP.UseVisualStyleBackColor = true;
			this.US_checkbox_IncludeStandardWP.CheckedChanged += new System.EventHandler(this.US_checkbox_IncludeStandardWP_CheckedChanged);
			// 
			// btn_US_AddItem
			// 
			this.btn_US_AddItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_US_AddItem.Location = new System.Drawing.Point(401, 742);
			this.btn_US_AddItem.Name = "btn_US_AddItem";
			this.btn_US_AddItem.Size = new System.Drawing.Size(661, 71);
			this.btn_US_AddItem.TabIndex = 43;
			this.btn_US_AddItem.Text = "Add item";
			this.btn_US_AddItem.UseVisualStyleBackColor = true;
			this.btn_US_AddItem.Click += new System.EventHandler(this.btn_US_AddItem_Click);
			// 
			// txtUSNote
			// 
			this.txtUSNote.Location = new System.Drawing.Point(107, 288);
			this.txtUSNote.Name = "txtUSNote";
			this.txtUSNote.Size = new System.Drawing.Size(272, 170);
			this.txtUSNote.TabIndex = 30;
			this.txtUSNote.Text = "";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(26, 288);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(30, 13);
			this.label7.TabIndex = 33;
			this.label7.Text = "Note";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(31, 216);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(46, 13);
			this.label6.TabIndex = 32;
			this.label6.Text = "Team Id";
			// 
			// txtUSTeamId
			// 
			this.txtUSTeamId.Location = new System.Drawing.Point(107, 213);
			this.txtUSTeamId.Name = "txtUSTeamId";
			this.txtUSTeamId.Size = new System.Drawing.Size(272, 20);
			this.txtUSTeamId.TabIndex = 29;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(31, 36);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(55, 13);
			this.label4.TabIndex = 28;
			this.label4.Text = "Feature Id";
			// 
			// txtUSFeatureId
			// 
			this.txtUSFeatureId.Location = new System.Drawing.Point(107, 33);
			this.txtUSFeatureId.Name = "txtUSFeatureId";
			this.txtUSFeatureId.Size = new System.Drawing.Size(272, 20);
			this.txtUSFeatureId.TabIndex = 27;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(31, 82);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(27, 13);
			this.label2.TabIndex = 26;
			this.label2.Text = "Title";
			// 
			// tcCreateNewItem_tabWP
			// 
			this.tcCreateNewItem_tabWP.Controls.Add(this.tabControl2);
			this.tcCreateNewItem_tabWP.Location = new System.Drawing.Point(4, 22);
			this.tcCreateNewItem_tabWP.Name = "tcCreateNewItem_tabWP";
			this.tcCreateNewItem_tabWP.Padding = new System.Windows.Forms.Padding(3);
			this.tcCreateNewItem_tabWP.Size = new System.Drawing.Size(1562, 855);
			this.tcCreateNewItem_tabWP.TabIndex = 1;
			this.tcCreateNewItem_tabWP.Text = "WP";
			this.tcCreateNewItem_tabWP.UseVisualStyleBackColor = true;
			// 
			// tabControl2
			// 
			this.tabControl2.Controls.Add(this.createItem_WP_random);
			this.tabControl2.Controls.Add(this.createItem_WP_from_Defect);
			this.tabControl2.Controls.Add(this.createItem_WP_update);
			this.tabControl2.Location = new System.Drawing.Point(19, 33);
			this.tabControl2.Name = "tabControl2";
			this.tabControl2.SelectedIndex = 0;
			this.tabControl2.Size = new System.Drawing.Size(1422, 567);
			this.tabControl2.TabIndex = 42;
			// 
			// createItem_WP_random
			// 
			this.createItem_WP_random.Controls.Add(this.WP_Random_USId);
			this.createItem_WP_random.Controls.Add(this.WP_Random_Btn_AddItem);
			this.createItem_WP_random.Controls.Add(this.label5);
			this.createItem_WP_random.Controls.Add(this.WP_Random_Title);
			this.createItem_WP_random.Controls.Add(this.WP_Random_Estimate);
			this.createItem_WP_random.Controls.Add(this.label13);
			this.createItem_WP_random.Controls.Add(this.WP_Random_Release);
			this.createItem_WP_random.Controls.Add(this.WP_Random_WPType);
			this.createItem_WP_random.Controls.Add(this.label12);
			this.createItem_WP_random.Controls.Add(this.label11);
			this.createItem_WP_random.Controls.Add(this.label1);
			this.createItem_WP_random.Controls.Add(this.WP_Random_TeamId);
			this.createItem_WP_random.Controls.Add(this.label10);
			this.createItem_WP_random.Controls.Add(this.txtWPNote);
			this.createItem_WP_random.Controls.Add(this.label9);
			this.createItem_WP_random.Location = new System.Drawing.Point(4, 22);
			this.createItem_WP_random.Name = "createItem_WP_random";
			this.createItem_WP_random.Padding = new System.Windows.Forms.Padding(3);
			this.createItem_WP_random.Size = new System.Drawing.Size(1414, 541);
			this.createItem_WP_random.TabIndex = 0;
			this.createItem_WP_random.Text = "Create Random";
			this.createItem_WP_random.UseVisualStyleBackColor = true;
			// 
			// WP_Random_USId
			// 
			this.WP_Random_USId.Location = new System.Drawing.Point(102, 15);
			this.WP_Random_USId.Name = "WP_Random_USId";
			this.WP_Random_USId.Size = new System.Drawing.Size(389, 20);
			this.WP_Random_USId.TabIndex = 29;
			// 
			// WP_Random_Btn_AddItem
			// 
			this.WP_Random_Btn_AddItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.WP_Random_Btn_AddItem.Location = new System.Drawing.Point(29, 437);
			this.WP_Random_Btn_AddItem.Name = "WP_Random_Btn_AddItem";
			this.WP_Random_Btn_AddItem.Size = new System.Drawing.Size(462, 57);
			this.WP_Random_Btn_AddItem.TabIndex = 35;
			this.WP_Random_Btn_AddItem.Text = "Add item";
			this.WP_Random_Btn_AddItem.UseVisualStyleBackColor = true;
			this.WP_Random_Btn_AddItem.Click += new System.EventHandler(this.WP_Random_Btn_AddItem_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(26, 215);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(55, 13);
			this.label5.TabIndex = 41;
			this.label5.Text = "Estimation";
			// 
			// WP_Random_Title
			// 
			this.WP_Random_Title.Location = new System.Drawing.Point(102, 52);
			this.WP_Random_Title.Name = "WP_Random_Title";
			this.WP_Random_Title.Size = new System.Drawing.Size(389, 20);
			this.WP_Random_Title.TabIndex = 25;
			// 
			// WP_Random_Estimate
			// 
			this.WP_Random_Estimate.Location = new System.Drawing.Point(102, 212);
			this.WP_Random_Estimate.Name = "WP_Random_Estimate";
			this.WP_Random_Estimate.Size = new System.Drawing.Size(389, 20);
			this.WP_Random_Estimate.TabIndex = 40;
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(26, 55);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(27, 13);
			this.label13.TabIndex = 26;
			this.label13.Text = "Title";
			// 
			// WP_Random_Release
			// 
			this.WP_Random_Release.FormattingEnabled = true;
			this.WP_Random_Release.Items.AddRange(new object[] {
            "Analysis",
            "Automatic testing",
            "Functional design/UX",
            "Technical design",
            "Build",
            "Demo feedback",
            "Internal test",
            "Test",
            "Test - create tc",
            "Test - review tc",
            "Test - execution tc",
            "Bug fixing",
            "Governance",
            "Documentation",
            "Workshop/Interview",
            "Deliverable",
            "Other"});
			this.WP_Random_Release.Location = new System.Drawing.Point(102, 138);
			this.WP_Random_Release.Name = "WP_Random_Release";
			this.WP_Random_Release.Size = new System.Drawing.Size(389, 21);
			this.WP_Random_Release.TabIndex = 39;
			// 
			// WP_Random_WPType
			// 
			this.WP_Random_WPType.FormattingEnabled = true;
			this.WP_Random_WPType.Items.AddRange(new object[] {
            "Analysis",
            "Automatic testing",
            "Functional design/UX",
            "Technical design",
            "Build",
            "Demo feedback",
            "Internal test",
            "Test",
            "Test - create tc",
            "Test - review tc",
            "Test - execution tc",
            "Bug fixing",
            "Governance",
            "Documentation",
            "Workshop/Interview",
            "Deliverable",
            "Other"});
			this.WP_Random_WPType.Location = new System.Drawing.Point(102, 177);
			this.WP_Random_WPType.Name = "WP_Random_WPType";
			this.WP_Random_WPType.Size = new System.Drawing.Size(389, 21);
			this.WP_Random_WPType.TabIndex = 39;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(26, 180);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(52, 13);
			this.label12.TabIndex = 38;
			this.label12.Text = "WP Type";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(26, 18);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(34, 13);
			this.label11.TabIndex = 30;
			this.label11.Text = "US Id";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(26, 141);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(46, 13);
			this.label1.TabIndex = 38;
			this.label1.Text = "Release";
			// 
			// WP_Random_TeamId
			// 
			this.WP_Random_TeamId.Location = new System.Drawing.Point(102, 96);
			this.WP_Random_TeamId.Name = "WP_Random_TeamId";
			this.WP_Random_TeamId.Size = new System.Drawing.Size(389, 20);
			this.WP_Random_TeamId.TabIndex = 31;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(26, 99);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(46, 13);
			this.label10.TabIndex = 32;
			this.label10.Text = "Team Id";
			// 
			// txtWPNote
			// 
			this.txtWPNote.Location = new System.Drawing.Point(102, 247);
			this.txtWPNote.Name = "txtWPNote";
			this.txtWPNote.Size = new System.Drawing.Size(389, 163);
			this.txtWPNote.TabIndex = 34;
			this.txtWPNote.Text = "";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(26, 250);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(30, 13);
			this.label9.TabIndex = 33;
			this.label9.Text = "Note";
			// 
			// createItem_WP_from_Defect
			// 
			this.createItem_WP_from_Defect.Controls.Add(this.WP_Defect_DefectId);
			this.createItem_WP_from_Defect.Controls.Add(this.button1);
			this.createItem_WP_from_Defect.Controls.Add(this.label14);
			this.createItem_WP_from_Defect.Controls.Add(this.textBox3);
			this.createItem_WP_from_Defect.Controls.Add(this.label17);
			this.createItem_WP_from_Defect.Controls.Add(this.richTextBox1);
			this.createItem_WP_from_Defect.Controls.Add(this.label20);
			this.createItem_WP_from_Defect.Location = new System.Drawing.Point(4, 22);
			this.createItem_WP_from_Defect.Name = "createItem_WP_from_Defect";
			this.createItem_WP_from_Defect.Padding = new System.Windows.Forms.Padding(3);
			this.createItem_WP_from_Defect.Size = new System.Drawing.Size(1414, 541);
			this.createItem_WP_from_Defect.TabIndex = 1;
			this.createItem_WP_from_Defect.Text = "Create from defect";
			this.createItem_WP_from_Defect.UseVisualStyleBackColor = true;
			// 
			// WP_Defect_DefectId
			// 
			this.WP_Defect_DefectId.Location = new System.Drawing.Point(108, 40);
			this.WP_Defect_DefectId.Name = "WP_Defect_DefectId";
			this.WP_Defect_DefectId.Size = new System.Drawing.Size(389, 20);
			this.WP_Defect_DefectId.TabIndex = 44;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(35, 462);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(462, 57);
			this.button1.TabIndex = 50;
			this.button1.Text = "Add item";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(32, 81);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(55, 13);
			this.label14.TabIndex = 56;
			this.label14.Text = "Estimation";
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(108, 78);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(389, 20);
			this.textBox3.TabIndex = 55;
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(32, 43);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(51, 13);
			this.label17.TabIndex = 45;
			this.label17.Text = "Defect Id";
			// 
			// richTextBox1
			// 
			this.richTextBox1.Location = new System.Drawing.Point(108, 115);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(389, 163);
			this.richTextBox1.TabIndex = 49;
			this.richTextBox1.Text = "";
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.Location = new System.Drawing.Point(32, 118);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(30, 13);
			this.label20.TabIndex = 48;
			this.label20.Text = "Note";
			// 
			// createItem_WP_update
			// 
			this.createItem_WP_update.Controls.Add(this.createItem_WP_Update_duedate);
			this.createItem_WP_update.Controls.Add(this.createItem_WP_Update_txt_WP_Id);
			this.createItem_WP_update.Controls.Add(this.button3);
			this.createItem_WP_update.Controls.Add(this.createItem_WP_Update_button);
			this.createItem_WP_update.Controls.Add(this.label16);
			this.createItem_WP_update.Controls.Add(this.createItem_WP_Update_title);
			this.createItem_WP_update.Controls.Add(this.createItem_WP_Update_estimate);
			this.createItem_WP_update.Controls.Add(this.label18);
			this.createItem_WP_update.Controls.Add(this.createItem_WP_Update_release);
			this.createItem_WP_update.Controls.Add(this.createItem_WP_Update_wpType);
			this.createItem_WP_update.Controls.Add(this.label19);
			this.createItem_WP_update.Controls.Add(this.label25);
			this.createItem_WP_update.Controls.Add(this.label21);
			this.createItem_WP_update.Controls.Add(this.label22);
			this.createItem_WP_update.Controls.Add(this.createItem_WP_Update_teamId);
			this.createItem_WP_update.Controls.Add(this.label23);
			this.createItem_WP_update.Controls.Add(this.createItem_WP_Update_note);
			this.createItem_WP_update.Controls.Add(this.label24);
			this.createItem_WP_update.Location = new System.Drawing.Point(4, 22);
			this.createItem_WP_update.Name = "createItem_WP_update";
			this.createItem_WP_update.Size = new System.Drawing.Size(1414, 541);
			this.createItem_WP_update.TabIndex = 2;
			this.createItem_WP_update.Text = "Update";
			this.createItem_WP_update.UseVisualStyleBackColor = true;
			// 
			// createItem_WP_Update_duedate
			// 
			this.createItem_WP_Update_duedate.Location = new System.Drawing.Point(551, 34);
			this.createItem_WP_Update_duedate.Name = "createItem_WP_Update_duedate";
			this.createItem_WP_Update_duedate.Size = new System.Drawing.Size(200, 20);
			this.createItem_WP_Update_duedate.TabIndex = 57;
			// 
			// createItem_WP_Update_txt_WP_Id
			// 
			this.createItem_WP_Update_txt_WP_Id.Location = new System.Drawing.Point(95, 35);
			this.createItem_WP_Update_txt_WP_Id.Name = "createItem_WP_Update_txt_WP_Id";
			this.createItem_WP_Update_txt_WP_Id.Size = new System.Drawing.Size(225, 20);
			this.createItem_WP_Update_txt_WP_Id.TabIndex = 44;
			// 
			// button3
			// 
			this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button3.Location = new System.Drawing.Point(22, 75);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(298, 57);
			this.button3.TabIndex = 50;
			this.button3.Text = "Get";
			this.button3.UseVisualStyleBackColor = true;
			// 
			// createItem_WP_Update_button
			// 
			this.createItem_WP_Update_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.createItem_WP_Update_button.Location = new System.Drawing.Point(478, 453);
			this.createItem_WP_Update_button.Name = "createItem_WP_Update_button";
			this.createItem_WP_Update_button.Size = new System.Drawing.Size(462, 57);
			this.createItem_WP_Update_button.TabIndex = 50;
			this.createItem_WP_Update_button.Text = "Update item";
			this.createItem_WP_Update_button.UseVisualStyleBackColor = true;
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(475, 231);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(55, 13);
			this.label16.TabIndex = 56;
			this.label16.Text = "Estimation";
			// 
			// createItem_WP_Update_title
			// 
			this.createItem_WP_Update_title.Location = new System.Drawing.Point(551, 68);
			this.createItem_WP_Update_title.Name = "createItem_WP_Update_title";
			this.createItem_WP_Update_title.Size = new System.Drawing.Size(389, 20);
			this.createItem_WP_Update_title.TabIndex = 42;
			// 
			// createItem_WP_Update_estimate
			// 
			this.createItem_WP_Update_estimate.Location = new System.Drawing.Point(551, 228);
			this.createItem_WP_Update_estimate.Name = "createItem_WP_Update_estimate";
			this.createItem_WP_Update_estimate.Size = new System.Drawing.Size(389, 20);
			this.createItem_WP_Update_estimate.TabIndex = 55;
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(475, 71);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(27, 13);
			this.label18.TabIndex = 43;
			this.label18.Text = "Title";
			// 
			// createItem_WP_Update_release
			// 
			this.createItem_WP_Update_release.FormattingEnabled = true;
			this.createItem_WP_Update_release.Items.AddRange(new object[] {
            "Analysis",
            "Automatic testing",
            "Functional design/UX",
            "Technical design",
            "Build",
            "Demo feedback",
            "Internal test",
            "Test",
            "Test - create tc",
            "Test - review tc",
            "Test - execution tc",
            "Bug fixing",
            "Governance",
            "Documentation",
            "Workshop/Interview",
            "Deliverable",
            "Other"});
			this.createItem_WP_Update_release.Location = new System.Drawing.Point(551, 154);
			this.createItem_WP_Update_release.Name = "createItem_WP_Update_release";
			this.createItem_WP_Update_release.Size = new System.Drawing.Size(389, 21);
			this.createItem_WP_Update_release.TabIndex = 53;
			// 
			// createItem_WP_Update_wpType
			// 
			this.createItem_WP_Update_wpType.FormattingEnabled = true;
			this.createItem_WP_Update_wpType.Items.AddRange(new object[] {
            "Analysis",
            "Automatic testing",
            "Functional design/UX",
            "Technical design",
            "Build",
            "Demo feedback",
            "Internal test",
            "Test",
            "Test - create tc",
            "Test - review tc",
            "Test - execution tc",
            "Bug fixing",
            "Governance",
            "Documentation",
            "Workshop/Interview",
            "Deliverable",
            "Other"});
			this.createItem_WP_Update_wpType.Location = new System.Drawing.Point(551, 193);
			this.createItem_WP_Update_wpType.Name = "createItem_WP_Update_wpType";
			this.createItem_WP_Update_wpType.Size = new System.Drawing.Size(389, 21);
			this.createItem_WP_Update_wpType.TabIndex = 54;
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(475, 196);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(52, 13);
			this.label19.TabIndex = 51;
			this.label19.Text = "WP Type";
			// 
			// label25
			// 
			this.label25.AutoSize = true;
			this.label25.Location = new System.Drawing.Point(19, 38);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(37, 13);
			this.label25.TabIndex = 45;
			this.label25.Text = "WP Id";
			// 
			// label21
			// 
			this.label21.AutoSize = true;
			this.label21.Location = new System.Drawing.Point(475, 34);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(53, 13);
			this.label21.TabIndex = 45;
			this.label21.Text = "Due Date";
			// 
			// label22
			// 
			this.label22.AutoSize = true;
			this.label22.Location = new System.Drawing.Point(475, 157);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(46, 13);
			this.label22.TabIndex = 52;
			this.label22.Text = "Release";
			// 
			// createItem_WP_Update_teamId
			// 
			this.createItem_WP_Update_teamId.Location = new System.Drawing.Point(551, 112);
			this.createItem_WP_Update_teamId.Name = "createItem_WP_Update_teamId";
			this.createItem_WP_Update_teamId.Size = new System.Drawing.Size(389, 20);
			this.createItem_WP_Update_teamId.TabIndex = 46;
			// 
			// label23
			// 
			this.label23.AutoSize = true;
			this.label23.Location = new System.Drawing.Point(475, 115);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(46, 13);
			this.label23.TabIndex = 47;
			this.label23.Text = "Team Id";
			// 
			// createItem_WP_Update_note
			// 
			this.createItem_WP_Update_note.Location = new System.Drawing.Point(551, 263);
			this.createItem_WP_Update_note.Name = "createItem_WP_Update_note";
			this.createItem_WP_Update_note.Size = new System.Drawing.Size(389, 163);
			this.createItem_WP_Update_note.TabIndex = 49;
			this.createItem_WP_Update_note.Text = "";
			// 
			// label24
			// 
			this.label24.AutoSize = true;
			this.label24.Location = new System.Drawing.Point(475, 266);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(30, 13);
			this.label24.TabIndex = 48;
			this.label24.Text = "Note";
			// 
			// tabOthers
			// 
			this.tabOthers.Controls.Add(this.timeEntries);
			this.tabOthers.Location = new System.Drawing.Point(4, 22);
			this.tabOthers.Name = "tabOthers";
			this.tabOthers.Padding = new System.Windows.Forms.Padding(3);
			this.tabOthers.Size = new System.Drawing.Size(1576, 887);
			this.tabOthers.TabIndex = 2;
			this.tabOthers.Text = "Others";
			this.tabOthers.UseVisualStyleBackColor = true;
			// 
			// tabGanttchart
			// 
			this.tabGanttchart.Controls.Add(this.groupBox3);
			this.tabGanttchart.Location = new System.Drawing.Point(4, 22);
			this.tabGanttchart.Name = "tabGanttchart";
			this.tabGanttchart.Padding = new System.Windows.Forms.Padding(3);
			this.tabGanttchart.Size = new System.Drawing.Size(1576, 887);
			this.tabGanttchart.TabIndex = 3;
			this.tabGanttchart.Text = "Gantt Chart";
			this.tabGanttchart.UseVisualStyleBackColor = true;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.GanttChartPannel);
			this.groupBox3.Location = new System.Drawing.Point(25, 31);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(1056, 469);
			this.groupBox3.TabIndex = 1;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Chart";
			// 
			// GanttChartPannel
			// 
			this.GanttChartPannel.BackColor = System.Drawing.SystemColors.Control;
			this.GanttChartPannel.ColumnCount = 1;
			this.GanttChartPannel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.GanttChartPannel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.GanttChartPannel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.GanttChartPannel.Location = new System.Drawing.Point(3, 16);
			this.GanttChartPannel.Name = "GanttChartPannel";
			this.GanttChartPannel.RowCount = 1;
			this.GanttChartPannel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.GanttChartPannel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 450F));
			this.GanttChartPannel.Size = new System.Drawing.Size(1050, 450);
			this.GanttChartPannel.TabIndex = 0;
			// 
			// tabDetailPlan
			// 
			this.tabDetailPlan.Controls.Add(this.tableLayoutPanel_DetailsPlan);
			this.tabDetailPlan.Location = new System.Drawing.Point(4, 22);
			this.tabDetailPlan.Name = "tabDetailPlan";
			this.tabDetailPlan.Padding = new System.Windows.Forms.Padding(3);
			this.tabDetailPlan.Size = new System.Drawing.Size(1576, 887);
			this.tabDetailPlan.TabIndex = 4;
			this.tabDetailPlan.Text = "Details plan";
			this.tabDetailPlan.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel_DetailsPlan
			// 
			this.tableLayoutPanel_DetailsPlan.ColumnCount = 2;
			this.tableLayoutPanel_DetailsPlan.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
			this.tableLayoutPanel_DetailsPlan.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel_DetailsPlan.Controls.Add(this.tabDetailsPlan_TabControl, 1, 0);
			this.tableLayoutPanel_DetailsPlan.Controls.Add(this.tabDetailsPlan_GridView, 0, 0);
			this.tableLayoutPanel_DetailsPlan.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel_DetailsPlan.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel_DetailsPlan.Name = "tableLayoutPanel_DetailsPlan";
			this.tableLayoutPanel_DetailsPlan.RowCount = 1;
			this.tableLayoutPanel_DetailsPlan.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel_DetailsPlan.Size = new System.Drawing.Size(1570, 881);
			this.tableLayoutPanel_DetailsPlan.TabIndex = 4;
			// 
			// tabDetailsPlan_TabControl
			// 
			this.tabDetailsPlan_TabControl.Controls.Add(this.tabPage1);
			this.tabDetailsPlan_TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabDetailsPlan_TabControl.Location = new System.Drawing.Point(1259, 3);
			this.tabDetailsPlan_TabControl.Name = "tabDetailsPlan_TabControl";
			this.tabDetailsPlan_TabControl.SelectedIndex = 0;
			this.tabDetailsPlan_TabControl.Size = new System.Drawing.Size(308, 875);
			this.tabDetailsPlan_TabControl.TabIndex = 4;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.tableLayoutPanel2);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(300, 849);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Changes";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.tabDetailsPlan_GridView_changes, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.groupBox5, 0, 1);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(294, 843);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// tabDetailsPlan_GridView_changes
			// 
			this.tabDetailsPlan_GridView_changes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.tabDetailsPlan_GridView_changes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabDetailsPlan_GridView_changes.Location = new System.Drawing.Point(3, 3);
			this.tabDetailsPlan_GridView_changes.Name = "tabDetailsPlan_GridView_changes";
			this.tabDetailsPlan_GridView_changes.Size = new System.Drawing.Size(288, 668);
			this.tabDetailsPlan_GridView_changes.TabIndex = 25;
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.detailPlan_label_status);
			this.groupBox5.Controls.Add(this.label27);
			this.groupBox5.Controls.Add(this.tabDetailsPlan_btn_open_planning);
			this.groupBox5.Controls.Add(this.tabDetailsPlan_btn_apply_changes);
			this.groupBox5.Controls.Add(this.tabDetails_btn_refersh);
			this.groupBox5.Controls.Add(this.tabDetailsPlan_btn_pushToToolkit);
			this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox5.Location = new System.Drawing.Point(3, 677);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(288, 163);
			this.groupBox5.TabIndex = 3;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Controls";
			// 
			// detailPlan_label_status
			// 
			this.detailPlan_label_status.AutoSize = true;
			this.detailPlan_label_status.Location = new System.Drawing.Point(52, 119);
			this.detailPlan_label_status.Name = "detailPlan_label_status";
			this.detailPlan_label_status.Size = new System.Drawing.Size(28, 13);
			this.detailPlan_label_status.TabIndex = 26;
			this.detailPlan_label_status.Text = ".......";
			// 
			// label27
			// 
			this.label27.AutoSize = true;
			this.label27.Location = new System.Drawing.Point(6, 119);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(40, 13);
			this.label27.TabIndex = 25;
			this.label27.Text = "Status:";
			// 
			// tabDetailsPlan_btn_open_planning
			// 
			this.tabDetailsPlan_btn_open_planning.Location = new System.Drawing.Point(6, 62);
			this.tabDetailsPlan_btn_open_planning.Name = "tabDetailsPlan_btn_open_planning";
			this.tabDetailsPlan_btn_open_planning.Size = new System.Drawing.Size(94, 37);
			this.tabDetailsPlan_btn_open_planning.TabIndex = 24;
			this.tabDetailsPlan_btn_open_planning.Text = "Open Planning";
			this.tabDetailsPlan_btn_open_planning.UseVisualStyleBackColor = true;
			this.tabDetailsPlan_btn_open_planning.Click += new System.EventHandler(this.tabDetailsPlan_btn_open_planning_Click);
			// 
			// tabDetailsPlan_btn_apply_changes
			// 
			this.tabDetailsPlan_btn_apply_changes.Location = new System.Drawing.Point(106, 19);
			this.tabDetailsPlan_btn_apply_changes.Name = "tabDetailsPlan_btn_apply_changes";
			this.tabDetailsPlan_btn_apply_changes.Size = new System.Drawing.Size(84, 37);
			this.tabDetailsPlan_btn_apply_changes.TabIndex = 24;
			this.tabDetailsPlan_btn_apply_changes.Text = "Apply local data changes";
			this.tabDetailsPlan_btn_apply_changes.UseVisualStyleBackColor = true;
			this.tabDetailsPlan_btn_apply_changes.Click += new System.EventHandler(this.tabDetailsPlan_btn_apply_changes_Click);
			// 
			// tabDetails_btn_refersh
			// 
			this.tabDetails_btn_refersh.Location = new System.Drawing.Point(6, 19);
			this.tabDetails_btn_refersh.Name = "tabDetails_btn_refersh";
			this.tabDetails_btn_refersh.Size = new System.Drawing.Size(94, 37);
			this.tabDetails_btn_refersh.TabIndex = 2;
			this.tabDetails_btn_refersh.Text = "Reload local data";
			this.tabDetails_btn_refersh.UseVisualStyleBackColor = true;
			this.tabDetails_btn_refersh.Click += new System.EventHandler(this.tabDetailsPlan_btn_refersh_Click);
			// 
			// tabDetailsPlan_btn_pushToToolkit
			// 
			this.tabDetailsPlan_btn_pushToToolkit.Location = new System.Drawing.Point(196, 19);
			this.tabDetailsPlan_btn_pushToToolkit.Name = "tabDetailsPlan_btn_pushToToolkit";
			this.tabDetailsPlan_btn_pushToToolkit.Size = new System.Drawing.Size(73, 37);
			this.tabDetailsPlan_btn_pushToToolkit.TabIndex = 23;
			this.tabDetailsPlan_btn_pushToToolkit.Text = "Push local data to toolkit";
			this.tabDetailsPlan_btn_pushToToolkit.UseVisualStyleBackColor = true;
			this.tabDetailsPlan_btn_pushToToolkit.Click += new System.EventHandler(this.tabDetailsPlan_btn_pushToToolkit_Click);
			// 
			// tabDetailsPlan_GridView
			// 
			this.tabDetailsPlan_GridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.tabDetailsPlan_GridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabDetailsPlan_GridView.Location = new System.Drawing.Point(3, 3);
			this.tabDetailsPlan_GridView.Name = "tabDetailsPlan_GridView";
			this.tabDetailsPlan_GridView.Size = new System.Drawing.Size(1250, 875);
			this.tabDetailsPlan_GridView.TabIndex = 0;
			this.tabDetailsPlan_GridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tabDetailsPlan_GridView_CellClick);
			this.tabDetailsPlan_GridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.tabDetailsPlan_GridView_CellEndEdit);
			this.tabDetailsPlan_GridView.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.tabDetailsPlan_GridView_CellMouseClick);
			this.tabDetailsPlan_GridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.tabDetailsPlan_GridView_DataError);
			// 
			// tabLog
			// 
			this.tabLog.Controls.Add(this.Daily_log_textbox);
			this.tabLog.Location = new System.Drawing.Point(4, 22);
			this.tabLog.Name = "tabLog";
			this.tabLog.Padding = new System.Windows.Forms.Padding(3);
			this.tabLog.Size = new System.Drawing.Size(1576, 887);
			this.tabLog.TabIndex = 5;
			this.tabLog.Text = "Log";
			this.tabLog.UseVisualStyleBackColor = true;
			// 
			// Daily_log_textbox
			// 
			this.Daily_log_textbox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Daily_log_textbox.Location = new System.Drawing.Point(3, 3);
			this.Daily_log_textbox.Name = "Daily_log_textbox";
			this.Daily_log_textbox.Size = new System.Drawing.Size(1570, 881);
			this.Daily_log_textbox.TabIndex = 20;
			this.Daily_log_textbox.Text = "";
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.label26);
			this.tabPage3.Controls.Add(this.tableLayoutPanel3);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(1576, 887);
			this.tabPage3.TabIndex = 6;
			this.tabPage3.Text = "Planning";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// label26
			// 
			this.label26.Location = new System.Drawing.Point(84, 111);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(13, 14);
			this.label26.TabIndex = 0;
			this.label26.DragDrop += new System.Windows.Forms.DragEventHandler(this.label26_DragDrop);
			this.label26.DragEnter += new System.Windows.Forms.DragEventHandler(this.label26_DragEnter);
			this.label26.DragOver += new System.Windows.Forms.DragEventHandler(this.label26_DragOver);
			this.label26.DragLeave += new System.EventHandler(this.label26_DragLeave);
			this.label26.MouseLeave += new System.EventHandler(this.label26_MouseLeave);
			this.label26.MouseHover += new System.EventHandler(this.label26_MouseHover);
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.tableLayoutPanel3.ColumnCount = 2;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.Location = new System.Drawing.Point(87, 147);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 2;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(463, 236);
			this.tableLayoutPanel3.TabIndex = 0;
			this.tableLayoutPanel3.DragDrop += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel3_DragDrop);
			this.tableLayoutPanel3.DragEnter += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel3_DragEnter);
			this.tableLayoutPanel3.DragOver += new System.Windows.Forms.DragEventHandler(this.tableLayoutPanel3_DragOver);
			this.tableLayoutPanel3.DragLeave += new System.EventHandler(this.tableLayoutPanel3_DragLeave);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(1584, 913);
			this.Controls.Add(this.tabMain);
			this.Name = "Form1";
			this.Text = "Netcompany";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.tabMain.ResumeLayout(false);
			this.tabDaily.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Daily_DataGridView)).EndInit();
			this.groupBox6.ResumeLayout(false);
			this.tabCreateItem.ResumeLayout(false);
			this.tabControlCreateNewItem.ResumeLayout(false);
			this.tcCreateNewItem_tabUS.ResumeLayout(false);
			this.tcCreateNewItem_tabUS.PerformLayout();
			this.tcCreateNewItem_tabWP.ResumeLayout(false);
			this.tabControl2.ResumeLayout(false);
			this.createItem_WP_random.ResumeLayout(false);
			this.createItem_WP_random.PerformLayout();
			this.createItem_WP_from_Defect.ResumeLayout(false);
			this.createItem_WP_from_Defect.PerformLayout();
			this.createItem_WP_update.ResumeLayout(false);
			this.createItem_WP_update.PerformLayout();
			this.tabOthers.ResumeLayout(false);
			this.tabGanttchart.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.tabDetailPlan.ResumeLayout(false);
			this.tableLayoutPanel_DetailsPlan.ResumeLayout(false);
			this.tabDetailsPlan_TabControl.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.tabDetailsPlan_GridView_changes)).EndInit();
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.tabDetailsPlan_GridView)).EndInit();
			this.tabLog.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnLoadDaily;
        private System.Windows.Forms.ComboBox cbbRelease;
        private Button timeEntries;
		private TabControl tabMain;
		private TabPage tabDaily;
		private TabPage tabCreateItem;
		private TabPage tabOthers;
		private ComboBox Daily_Filter_cbb_Iteration;
		private TabControl tabControlCreateNewItem;
		private TabPage tcCreateNewItem_tabUS;
		private TabPage tcCreateNewItem_tabWP;
		private Button WP_Random_Btn_AddItem;
		private RichTextBox txtWPNote;
		private Label label9;
		private Label label10;
		private TextBox WP_Random_TeamId;
		private Label label11;
		private TextBox WP_Random_USId;
		private Label label13;
		private TextBox WP_Random_Title;
		private Button btn_US_AddItem;
		private RichTextBox txtUSNote;
		private Label label7;
		private Label label6;
		private TextBox txtUSTeamId;
		private Label label4;
		private TextBox txtUSFeatureId;
		private Label label2;
		private ComboBox WP_Random_WPType;
		private Label label1;
		private Label label3;
		private TextBox txtUSEstimation;
		private Label label5;
		private TextBox WP_Random_Estimate;
		private CheckBox US_checkbox_createAnalyzeWP;
		private CheckBox US_checkbox_createDesignUIWP;
		private CheckBox US_checkbox_createExecuteTCWP;
		private CheckBox US_checkbox_createTCWP;
		private CheckBox US_checkbox_createDevelopmentIntegrationWP;
		private CheckBox US_checkbox_createDevelopmentWP;
		private CheckBox US_checkbox_createDesignIntegrationWP;
		private CheckBox US_checkbox_createDesignArWP;
		private CheckBox US_checkbox_createBugFixingWP;
		private CheckBox US_checkbox_IncludeStandardWP;
		private TextBox US_txt_estimate_designintegrate_wp;
		private TextBox US_txt_estimate_executeTC_wp;
		private TextBox US_txt_estimate_createTC_wp;
		private TextBox US_txt_estimate_bugfixing_wp;
		private TextBox US_txt_estimate_developmentintegrate_wp;
		private TextBox US_txt_estimate_development_wp;
		private TextBox US_txt_estimate_designar_wp;
		private TextBox US_txt_estimate_designui_wp;
		private TextBox US_txt_estimate_analyze_wp;
		private RichTextBox txtUSTitle;
		private TextBox US_txt_estimate_clientworkshop_wp;
		private CheckBox US_checkbox_createClientWorkShopWP;
		private Button US_btn_calculateEstimate;
		private TextBox US_txt_remaining_estimate;
		private Label label8;
		private TabControl tabControl2;
		private TabPage createItem_WP_random;
		private TabPage createItem_WP_from_Defect;
		private ComboBox WP_Random_Release;
		private Label label12;
		private TextBox WP_Defect_DefectId;
		private Button button1;
		private Label label14;
		private TextBox textBox3;
		private Label label17;
		private RichTextBox richTextBox1;
		private Label label20;
		private ComboBox Daily_Filter_cbb_Feature;
		private ComboBox Daily_Filter_cbb_US;
		private Button Daily_Filter_btn_filter;
		private ComboBox Daily_Filter_cbb_assignee;
		private ComboBox Daily_Filter_cbb_Status;
		private ComboBox Daily_Filter_cbb_Team;
		private Label label15;
		private TextBox US_txt_US_Id;
		private RichTextBox Daily_log_textbox;
		private DataGridView Daily_DataGridView;
		private TabPage createItem_WP_update;
		private DateTimePicker createItem_WP_Update_duedate;
		private TextBox createItem_WP_Update_txt_WP_Id;
		private Button button3;
		private Button createItem_WP_Update_button;
		private Label label16;
		private TextBox createItem_WP_Update_title;
		private TextBox createItem_WP_Update_estimate;
		private Label label18;
		private ComboBox createItem_WP_Update_release;
		private ComboBox createItem_WP_Update_wpType;
		private Label label19;
		private Label label25;
		private Label label21;
		private Label label22;
		private TextBox createItem_WP_Update_teamId;
		private Label label23;
		private RichTextBox createItem_WP_Update_note;
		private Label label24;
		private TabPage tabGanttchart;
		private TableLayoutPanel GanttChartPannel;
		private GroupBox groupBox3;
		private TabPage tabDetailPlan;
		private DataGridView tabDetailsPlan_GridView;
		private TabPage tabLog;
		private GroupBox groupBox5;
		private Button tabDetails_btn_refersh;
		private GroupBox groupBox1;
		private GroupBox groupBox6;
		private Button tabDetailsPlan_btn_apply_changes;
		private Button tabDetailsPlan_btn_pushToToolkit;
		private TableLayoutPanel tableLayoutPanel_DetailsPlan;
		private TabControl tabDetailsPlan_TabControl;
		private TabPage tabPage1;
		private TableLayoutPanel tableLayoutPanel2;
		private TabPage tabPage3;
		private DataGridView tabDetailsPlan_GridView_changes;
		private TableLayoutPanel tableLayoutPanel3;
		private Button tabDetailsPlan_btn_open_planning;
		private Label label26;
		private Label detailPlan_label_status;
		private Label label27;
	}
}

