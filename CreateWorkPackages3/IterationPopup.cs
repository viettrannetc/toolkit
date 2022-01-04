using BusinessLibrary.Models.Planning;
using CreateWorkPackages3.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CreateWorkPackages3.Form1;

namespace CreateWorkPackages3
{
	public partial class IterationPopup : Form
	{
		//public ToolkitService _service = new ToolkitService();
		//public List<WPItemModel> _lsvlocalData = new List<WPItemModel>();
		//public List<WPItemModel> _wpItemsLocal = new List<WPItemModel>();
		//public ToolTip yourToolTip = new ToolTip();
		public new SelectIteration Select;

		public IterationPopup(DataGridView tabDetailsPlan_GridView, SelectIteration select)
		{
			InitializeComponent();

			foreach (DataGridViewRow row in tabDetailsPlan_GridView.Rows)
			{
				var iterationValue = row.Cells[3] == null
					? string.Empty
					: row.Cells[3].ToString();

				if (iterationValue == string.Empty)
					IterationPopup_checkbox_0.Checked = true;

				if (iterationValue == IterationPopup_checkbox_1.Text)
					IterationPopup_checkbox_1.Checked = true;
				if (iterationValue == IterationPopup_checkbox_2.Text)
					IterationPopup_checkbox_2.Checked = true;
				if (iterationValue == IterationPopup_checkbox_3.Text)
					IterationPopup_checkbox_3.Checked = true;
				if (iterationValue == IterationPopup_checkbox_4.Text)
					IterationPopup_checkbox_4.Checked = true;
				if (iterationValue == IterationPopup_checkbox_5.Text)
					IterationPopup_checkbox_5.Checked = true;
				if (iterationValue == IterationPopup_checkbox_6.Text)
					IterationPopup_checkbox_6.Checked = true;
				if (iterationValue == IterationPopup_checkbox_7.Text)
					IterationPopup_checkbox_7.Checked = true;
				if (iterationValue == IterationPopup_checkbox_8.Text)
					IterationPopup_checkbox_8.Checked = true;
			}

			

			////MapDataFromDetailGridViewToPlanningGridView(tabDetailsPlan_Planning_Iteration);

			//yourToolTip.ToolTipIcon = ToolTipIcon.Warning;
			//yourToolTip.IsBalloon = true;
			//yourToolTip.ShowAlways = true;

			this.Select = select;
		}

		private void checkedOrNot()
		{
			var checkedItems = new List<string>();
			if (IterationPopup_checkbox_0.Checked)
				checkedItems.Add(string.Empty);
			if (IterationPopup_checkbox_1.Checked)
				checkedItems.Add(IterationPopup_checkbox_1.Text);
			if (IterationPopup_checkbox_2.Checked)
				checkedItems.Add(IterationPopup_checkbox_2.Text);
			if (IterationPopup_checkbox_3.Checked)
				checkedItems.Add(IterationPopup_checkbox_3.Text);
			if (IterationPopup_checkbox_4.Checked)
				checkedItems.Add(IterationPopup_checkbox_4.Text);
			if (IterationPopup_checkbox_5.Checked)
				checkedItems.Add(IterationPopup_checkbox_5.Text);
			if (IterationPopup_checkbox_6.Checked)
				checkedItems.Add(IterationPopup_checkbox_6.Text);
			if (IterationPopup_checkbox_7.Checked)
				checkedItems.Add(IterationPopup_checkbox_7.Text);
			if (IterationPopup_checkbox_8.Checked)
				checkedItems.Add(IterationPopup_checkbox_8.Text);

			this.Select(checkedItems);
		}

		private void IterationPopup_checkbox_1_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void IterationPopup_checkbox_2_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void IterationPopup_checkbox_3_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void IterationPopup_checkbox_4_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void IterationPopup_checkbox_5_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void IterationPopup_checkbox_6_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void IterationPopup_checkbox_7_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void IterationPopup_checkbox_8_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void btn_SelectedDone_Click(object sender, EventArgs e)
		{
			checkedOrNot();
		}
	}
}
