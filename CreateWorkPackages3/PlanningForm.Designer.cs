
namespace CreateWorkPackages3
{
	partial class PlanningForm
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
			this.tableLayoutPanel_Planning = new System.Windows.Forms.TableLayoutPanel();
			this.tabDetailsPlan_Planning_Iteration = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel_Planning.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel_Planning
			// 
			this.tableLayoutPanel_Planning.ColumnCount = 1;
			this.tableLayoutPanel_Planning.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel_Planning.Controls.Add(this.tabDetailsPlan_Planning_Iteration, 0, 0);
			this.tableLayoutPanel_Planning.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel_Planning.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel_Planning.Name = "tableLayoutPanel_Planning";
			this.tableLayoutPanel_Planning.RowCount = 1;
			this.tableLayoutPanel_Planning.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel_Planning.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel_Planning.Size = new System.Drawing.Size(1073, 665);
			this.tableLayoutPanel_Planning.TabIndex = 1;
			this.tableLayoutPanel_Planning.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel_Planning_Paint);
			// 
			// tabDetailsPlan_Planning_Iteration
			// 
			this.tabDetailsPlan_Planning_Iteration.AllowDrop = true;
			this.tabDetailsPlan_Planning_Iteration.AutoScroll = true;
			this.tabDetailsPlan_Planning_Iteration.ColumnCount = 2;
			this.tabDetailsPlan_Planning_Iteration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tabDetailsPlan_Planning_Iteration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tabDetailsPlan_Planning_Iteration.Location = new System.Drawing.Point(3, 3);
			this.tabDetailsPlan_Planning_Iteration.Name = "tabDetailsPlan_Planning_Iteration";
			this.tabDetailsPlan_Planning_Iteration.RowCount = 2;
			this.tabDetailsPlan_Planning_Iteration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tabDetailsPlan_Planning_Iteration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tabDetailsPlan_Planning_Iteration.Size = new System.Drawing.Size(200, 100);
			this.tabDetailsPlan_Planning_Iteration.TabIndex = 2;
			// 
			// PlanningForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1073, 665);
			this.Controls.Add(this.tableLayoutPanel_Planning);
			this.Name = "PlanningForm";
			this.Text = "Planning";
			this.tableLayoutPanel_Planning.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Planning;
		private System.Windows.Forms.TableLayoutPanel tabDetailsPlan_Planning_Iteration;
	}
}