
namespace CreateWorkPackages3.Forms.UpdateForm
{
	partial class UpdateWP
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
			this.txt_WP_Id = new System.Windows.Forms.TextBox();
			this.createItem_WP_Update_button = new System.Windows.Forms.Button();
			this.label16 = new System.Windows.Forms.Label();
			this.txt_WP_estimate = new System.Windows.Forms.TextBox();
			this.label_toolkitUrl = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.txt_WP_remaining = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.cbb_WP_Iteration = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cbb_WP_assignee = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// txt_WP_Id
			// 
			this.txt_WP_Id.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txt_WP_Id.Location = new System.Drawing.Point(153, 3);
			this.txt_WP_Id.Name = "txt_WP_Id";
			this.txt_WP_Id.Size = new System.Drawing.Size(469, 20);
			this.txt_WP_Id.TabIndex = 1;
			// 
			// createItem_WP_Update_button
			// 
			this.createItem_WP_Update_button.Dock = System.Windows.Forms.DockStyle.Fill;
			this.createItem_WP_Update_button.Location = new System.Drawing.Point(153, 178);
			this.createItem_WP_Update_button.Name = "createItem_WP_Update_button";
			this.createItem_WP_Update_button.Size = new System.Drawing.Size(469, 64);
			this.createItem_WP_Update_button.TabIndex = 6;
			this.createItem_WP_Update_button.Text = "Update item";
			this.createItem_WP_Update_button.UseVisualStyleBackColor = true;
			this.createItem_WP_Update_button.Click += new System.EventHandler(this.createItem_WP_Update_button_Click);
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label16.Location = new System.Drawing.Point(3, 35);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(144, 35);
			this.label16.TabIndex = 74;
			this.label16.Text = "Estimation";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txt_WP_estimate
			// 
			this.txt_WP_estimate.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txt_WP_estimate.Location = new System.Drawing.Point(153, 38);
			this.txt_WP_estimate.Name = "txt_WP_estimate";
			this.txt_WP_estimate.Size = new System.Drawing.Size(469, 20);
			this.txt_WP_estimate.TabIndex = 2;
			this.txt_WP_estimate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_WP_estimate_KeyPress);
			// 
			// label_toolkitUrl
			// 
			this.label_toolkitUrl.AutoSize = true;
			this.label_toolkitUrl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label_toolkitUrl.Location = new System.Drawing.Point(3, 0);
			this.label_toolkitUrl.Name = "label_toolkitUrl";
			this.label_toolkitUrl.Size = new System.Drawing.Size(144, 35);
			this.label_toolkitUrl.TabIndex = 61;
			this.label_toolkitUrl.Text = "WP Id";
			this.label_toolkitUrl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label_toolkitUrl.DoubleClick += new System.EventHandler(this.label_toolkitUrl_DoubleClick);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Location = new System.Drawing.Point(3, 70);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(144, 35);
			this.label1.TabIndex = 76;
			this.label1.Text = "Remaining hours";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txt_WP_remaining
			// 
			this.txt_WP_remaining.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txt_WP_remaining.Location = new System.Drawing.Point(153, 73);
			this.txt_WP_remaining.Name = "txt_WP_remaining";
			this.txt_WP_remaining.Size = new System.Drawing.Size(469, 20);
			this.txt_WP_remaining.TabIndex = 3;
			this.txt_WP_remaining.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_WP_remaining_KeyPress);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.cbb_WP_Iteration, 1, 4);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.cbb_WP_assignee, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.label_toolkitUrl, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.createItem_WP_Update_button, 1, 5);
			this.tableLayoutPanel1.Controls.Add(this.txt_WP_remaining, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.txt_WP_Id, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.txt_WP_estimate, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.label16, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 6;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(625, 245);
			this.tableLayoutPanel1.TabIndex = 77;
			// 
			// cbb_WP_Iteration
			// 
			this.cbb_WP_Iteration.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cbb_WP_Iteration.FormattingEnabled = true;
			this.cbb_WP_Iteration.Items.AddRange(new object[] {
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
			this.cbb_WP_Iteration.Location = new System.Drawing.Point(153, 143);
			this.cbb_WP_Iteration.Name = "cbb_WP_Iteration";
			this.cbb_WP_Iteration.Size = new System.Drawing.Size(469, 21);
			this.cbb_WP_Iteration.TabIndex = 5;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label3.Location = new System.Drawing.Point(3, 140);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(144, 35);
			this.label3.TabIndex = 79;
			this.label3.Text = "Iteration";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cbb_WP_assignee
			// 
			this.cbb_WP_assignee.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cbb_WP_assignee.FormattingEnabled = true;
			this.cbb_WP_assignee.Items.AddRange(new object[] {
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
			this.cbb_WP_assignee.Location = new System.Drawing.Point(153, 108);
			this.cbb_WP_assignee.Name = "cbb_WP_assignee";
			this.cbb_WP_assignee.Size = new System.Drawing.Size(469, 21);
			this.cbb_WP_assignee.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new System.Drawing.Point(3, 105);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(144, 35);
			this.label2.TabIndex = 77;
			this.label2.Text = "Assignee";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// UpdateWP
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(625, 245);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "UpdateWP";
			this.Text = "Update Work package";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.TextBox txt_WP_Id;
		private System.Windows.Forms.Button createItem_WP_Update_button;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.TextBox txt_WP_estimate;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txt_WP_remaining;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.ComboBox cbb_WP_Iteration;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cbb_WP_assignee;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label_toolkitUrl;
	}
}