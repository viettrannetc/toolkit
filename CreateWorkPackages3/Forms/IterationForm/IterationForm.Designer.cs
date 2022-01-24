using System.Windows.Forms;

namespace CreateWorkPackages3
{
    public partial class IterationForm
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
			this.tabDetailPlan = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel_DetailsPlan = new System.Windows.Forms.TableLayoutPanel();
			this.tabDetailsPlan_GridView = new System.Windows.Forms.DataGridView();
			this.tabDetailsPlan_GridView_changes = new System.Windows.Forms.DataGridView();
			this.tabMain = new System.Windows.Forms.TabControl();
			this.tabDetailsPlan_btn_pushToToolkit = new System.Windows.Forms.Button();
			this.tabDetails_btn_refersh = new System.Windows.Forms.Button();
			this.tabDetailsPlan_btn_apply_changes = new System.Windows.Forms.Button();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.tabDetailsPlan_TabControl = new System.Windows.Forms.TabControl();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.detailPlan_label_status = new System.Windows.Forms.RichTextBox();
			this.tabDetailPlan.SuspendLayout();
			this.tableLayoutPanel_DetailsPlan.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tabDetailsPlan_GridView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tabDetailsPlan_GridView_changes)).BeginInit();
			this.tabMain.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tabDetailsPlan_TabControl.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabDetailPlan
			// 
			this.tabDetailPlan.Controls.Add(this.tableLayoutPanel_DetailsPlan);
			this.tabDetailPlan.Location = new System.Drawing.Point(4, 22);
			this.tabDetailPlan.Name = "tabDetailPlan";
			this.tabDetailPlan.Padding = new System.Windows.Forms.Padding(3);
			this.tabDetailPlan.Size = new System.Drawing.Size(1576, 887);
			this.tabDetailPlan.TabIndex = 4;
			this.tabDetailPlan.Text = "Overview";
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
			// tabDetailsPlan_GridView_changes
			// 
			this.tabDetailsPlan_GridView_changes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.tabDetailsPlan_GridView_changes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabDetailsPlan_GridView_changes.Location = new System.Drawing.Point(3, 3);
			this.tabDetailsPlan_GridView_changes.Name = "tabDetailsPlan_GridView_changes";
			this.tabDetailsPlan_GridView_changes.Size = new System.Drawing.Size(288, 668);
			this.tabDetailsPlan_GridView_changes.TabIndex = 25;
			// 
			// tabMain
			// 
			this.tabMain.Controls.Add(this.tabDetailPlan);
			this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabMain.Location = new System.Drawing.Point(0, 0);
			this.tabMain.Name = "tabMain";
			this.tabMain.SelectedIndex = 0;
			this.tabMain.Size = new System.Drawing.Size(1584, 913);
			this.tabMain.TabIndex = 19;
			// 
			// tabDetailsPlan_btn_pushToToolkit
			// 
			this.tabDetailsPlan_btn_pushToToolkit.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabDetailsPlan_btn_pushToToolkit.Location = new System.Drawing.Point(189, 3);
			this.tabDetailsPlan_btn_pushToToolkit.Name = "tabDetailsPlan_btn_pushToToolkit";
			this.tabDetailsPlan_btn_pushToToolkit.Size = new System.Drawing.Size(90, 36);
			this.tabDetailsPlan_btn_pushToToolkit.TabIndex = 23;
			this.tabDetailsPlan_btn_pushToToolkit.Text = "Push local data to toolkit";
			this.tabDetailsPlan_btn_pushToToolkit.UseVisualStyleBackColor = true;
			this.tabDetailsPlan_btn_pushToToolkit.Click += new System.EventHandler(this.tabDetailsPlan_btn_pushToToolkit_Click);
			// 
			// tabDetails_btn_refersh
			// 
			this.tabDetails_btn_refersh.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabDetails_btn_refersh.Location = new System.Drawing.Point(3, 3);
			this.tabDetails_btn_refersh.Name = "tabDetails_btn_refersh";
			this.tabDetails_btn_refersh.Size = new System.Drawing.Size(87, 36);
			this.tabDetails_btn_refersh.TabIndex = 2;
			this.tabDetails_btn_refersh.Text = "Reload local data";
			this.tabDetails_btn_refersh.UseVisualStyleBackColor = true;
			this.tabDetails_btn_refersh.Click += new System.EventHandler(this.tabDetailsPlan_btn_refersh_Click);
			// 
			// tabDetailsPlan_btn_apply_changes
			// 
			this.tabDetailsPlan_btn_apply_changes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabDetailsPlan_btn_apply_changes.Location = new System.Drawing.Point(96, 3);
			this.tabDetailsPlan_btn_apply_changes.Name = "tabDetailsPlan_btn_apply_changes";
			this.tabDetailsPlan_btn_apply_changes.Size = new System.Drawing.Size(87, 36);
			this.tabDetailsPlan_btn_apply_changes.TabIndex = 24;
			this.tabDetailsPlan_btn_apply_changes.Text = "Apply local data changes";
			this.tabDetailsPlan_btn_apply_changes.UseVisualStyleBackColor = true;
			this.tabDetailsPlan_btn_apply_changes.Click += new System.EventHandler(this.tabDetailsPlan_btn_apply_changes_Click);
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
			this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 1);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(294, 843);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// tabDetailsPlan_TabControl
			// 
			this.tabDetailsPlan_TabControl.Controls.Add(this.tabPage2);
			this.tabDetailsPlan_TabControl.Controls.Add(this.tabPage1);
			this.tabDetailsPlan_TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabDetailsPlan_TabControl.Location = new System.Drawing.Point(1259, 3);
			this.tabDetailsPlan_TabControl.Name = "tabDetailsPlan_TabControl";
			this.tabDetailsPlan_TabControl.SelectedIndex = 0;
			this.tabDetailsPlan_TabControl.Size = new System.Drawing.Size(308, 875);
			this.tabDetailsPlan_TabControl.TabIndex = 4;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.detailPlan_label_status, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 677);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(288, 163);
			this.tableLayoutPanel1.TabIndex = 26;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 3;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel3.Controls.Add(this.tabDetails_btn_refersh, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.tabDetailsPlan_btn_apply_changes, 1, 0);
			this.tableLayoutPanel3.Controls.Add(this.tabDetailsPlan_btn_pushToToolkit, 2, 0);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 1;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(282, 42);
			this.tableLayoutPanel3.TabIndex = 0;
			// 
			// tabPage2
			// 
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(300, 849);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Overview";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// detailPlan_label_status
			// 
			this.detailPlan_label_status.Dock = System.Windows.Forms.DockStyle.Fill;
			this.detailPlan_label_status.Location = new System.Drawing.Point(3, 51);
			this.detailPlan_label_status.Name = "detailPlan_label_status";
			this.detailPlan_label_status.Size = new System.Drawing.Size(282, 109);
			this.detailPlan_label_status.TabIndex = 1;
			this.detailPlan_label_status.Text = "";
			// 
			// IterationForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(1584, 913);
			this.Controls.Add(this.tabMain);
			this.Name = "IterationForm";
			this.Text = "Netcompany";
			this.Load += new System.EventHandler(this.IterationForm_Load);
			this.tabDetailPlan.ResumeLayout(false);
			this.tableLayoutPanel_DetailsPlan.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.tabDetailsPlan_GridView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tabDetailsPlan_GridView_changes)).EndInit();
			this.tabMain.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tabDetailsPlan_TabControl.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.ResumeLayout(false);

        }

		#endregion

		private TabPage tabDetailPlan;
		private TableLayoutPanel tableLayoutPanel_DetailsPlan;
		private DataGridView tabDetailsPlan_GridView_changes;
		private DataGridView tabDetailsPlan_GridView;
		private TabControl tabMain;
		private TabControl tabDetailsPlan_TabControl;
		private TabPage tabPage1;
		private TableLayoutPanel tableLayoutPanel2;
		private Button tabDetailsPlan_btn_apply_changes;
		private Button tabDetails_btn_refersh;
		private Button tabDetailsPlan_btn_pushToToolkit;
		private TableLayoutPanel tableLayoutPanel1;
		private TableLayoutPanel tableLayoutPanel3;
		private TabPage tabPage2;
		private RichTextBox detailPlan_label_status;
	}
}

