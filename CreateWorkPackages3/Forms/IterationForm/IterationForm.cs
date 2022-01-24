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
	public partial class IterationForm : Form
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
		private const int _defaultReleaseId = 13;
		private const int _defaultTeamId = 32;

		public IterationForm(List<WPItemModel> wpItemsByIteration, ToolkitService service)
		{
			InitializeComponent();

			detailPlan_label_status.Clear();
			tabDetailsPlan_btn_pushToToolkit.Enabled = false;
			tabDetailsPlan_btn_apply_changes.Enabled = false;

			_lsvlocalData = wpItemsByIteration;
			_service = service;

			BuildProgressTracking(_lsvlocalData);
		}

		~IterationForm()
		{
			//lsvDaily = null;
		}

		void Log(string output)
		{
			detailPlan_label_status.AppendText("\r\n" + output);
			detailPlan_label_status.ScrollToCaret();
		}

		private void IterationForm_Load(object sender, EventArgs e)
		{
			//Daily_DataGridView.DataSource = GetGridTable();

		}
	}
}
