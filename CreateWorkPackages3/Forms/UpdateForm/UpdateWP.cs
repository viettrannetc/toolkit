using BusinessLibrary.Models.Planning;
using BusinessLibrary.Ultilities;
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
using static CreateWorkPackages3.PlanningForm;

namespace CreateWorkPackages3.Forms.UpdateForm
{
	public partial class UpdateWP : Form
	{
		public new UpdateWPAction updateWpAction;
		private WPItemModel _selectedWP;
		private ToolkitService _service;
		public UpdateWP(WPItemModel selectedWP, UpdateWPAction action, ToolkitService service)
		{
			InitializeComponent();
			this.updateWpAction = action;
			_selectedWP = selectedWP;
			_service = service;

			txt_WP_Id.Text = selectedWP.WPId.ToString();
			txt_WP_estimate.Text = selectedWP.WPEstimate;
			txt_WP_remaining.Text = selectedWP.WPRemainingHour;

			//Load Iteration combobox
			LoadComboboxIteration();

			//Load Assignee combobox
			LoadComboboxAssignee();
		}

		private void LoadComboboxAssignee()
		{
			var people = Constains.Toolkit_Assignee.ToList().Select(x => new { key = x.Key, value = x.Value }).ToList();

			people = people.OrderBy(i => i.key).ToList();
			cbb_WP_assignee.DataSource = people;
			cbb_WP_assignee.DisplayMember = "value";

			cbb_WP_assignee.SelectedItem = people.FirstOrDefault(it => it.value == _selectedWP.WPAssignee);
		}
		private void LoadComboboxIteration()
		{
			var iterations = _service._iterations.ToList().Select(x => new { key = x.Id, value = x.FieldValues["Title"].ToString() }).ToList();

			iterations = iterations.OrderBy(i => i.key).ToList();
			cbb_WP_Iteration.DataSource = iterations;
			cbb_WP_Iteration.DisplayMember = "value";

			cbb_WP_Iteration.SelectedItem = iterations.FirstOrDefault(it => it.value == _selectedWP.WPIterationName);
		}

		private void createItem_WP_Update_button_Click(object sender, EventArgs e)
		{
			dynamic selectedValue = cbb_WP_assignee.SelectedItem;
			if (selectedValue == null || selectedValue.key <= 0) return;

			dynamic selectedIterationValue = cbb_WP_Iteration.SelectedItem;
			if (selectedIterationValue == null) return;

			if (txt_WP_remaining.Text.Contains(",") || txt_WP_estimate.Text.Contains(".")) return;

			_selectedWP.WPRemainingHour = txt_WP_remaining.Text;
			_selectedWP.WPEstimate = txt_WP_estimate.Text;
			_selectedWP.WPAssignee = selectedValue.value;
			_selectedWP.WPIterationName = selectedIterationValue.value;
			_selectedWP.WPIterationId = selectedIterationValue.key.ToString();

			this.updateWpAction(_selectedWP);

			this.Close();
		}

		private void label_toolkitUrl_DoubleClick(object sender, EventArgs e)
		{
			//TODO: open toolkit url by WP Id

			var url = $@"https://goto.netcompany.com/cases/GTE747/NCDPP/Lists/WorkPackages/DispForm.aspx?ID={_selectedWP.WPId}";
			System.Diagnostics.Process.Start(url);
		}

		private void txt_WP_estimate_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
			{
				e.Handled = true;
			}
		}

		private void txt_WP_remaining_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
			{
				e.Handled = true;
			}
		}
	}
}
