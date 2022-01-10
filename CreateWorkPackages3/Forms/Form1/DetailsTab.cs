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
	public partial class Form1 : Form
	{
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
			tabDetailsPlan_GridView_Changes_ClearChanges();

			BuildProgressTracking(_lsvlocalData);
		}

		private void tabDetailsPlan_GridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex >= 0)
			{
				AddNewChangesFromWPLevel(e.ColumnIndex, e.RowIndex, tabDetailsPlan_GridView.Columns[e.ColumnIndex].Name);

				tabDetailsPlan_GridView_Changes_Reload();
			}
		}

		private void AddNewChangesFromWPLevel(int columnIndex, int rowIndex, string property, string newData = "")
		{
			var wpId = int.Parse(tabDetailsPlan_GridView[0, rowIndex].Value.ToString().Split('-')[tabDetailsPlan_GridView[0, rowIndex].Value.ToString().Split('-').Count() - 1].Trim());
			var currentRowData = _wpItemsLocal.FirstOrDefault(d => d.WPId == wpId);
			if (currentRowData == null) return;

			var cellNewData = string.IsNullOrEmpty(newData)
				? tabDetailsPlan_GridView[columnIndex, rowIndex].Value.ToString()
				: newData;
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
					var indexDueDate = 10;
					var dueDateByIteration = _service._toolkitIterations.FirstOrDefault(i => i.Title == cellNewData);
					if (dueDateByIteration != null)
						AddNewChangesFromWPLevel(indexDueDate, rowIndex, tabDetailsPlan_GridView.Columns[10].Name, dueDateByIteration.EndDate.Date.ToShortDateString());
					break;
				case _column_Status:
					cellOldData = currentRowData.WPStatus;					
					break;
					
				default:
					return;
			}

			if (cellOldData == cellNewData)//remove 			
			{
				var unnecesaryChange = _wpItemsLocalChanges.FirstOrDefault(d => d.WPId == wpId && d.Property == property);
				_wpItemsLocalChanges.Remove(unnecesaryChange);
				return;
			}

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
					case _column_Status:
						if (string.IsNullOrEmpty(change.NewValue))
							data.WPStatus = null;
						else
							data.WPStatus = change.NewValue;
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
						? 213 //TODO: default is Vitra - how to clear assignee?
						: Constains.Toolkit_Assignee.First(a => a.Value == data.WPAssignee).Key,
					Estimate = data.WPEstimate,
					RemainingWork = data.WPRemainingHour,
					DependOn = data.WPDependOn,
					DueDate = dueDateString,
					IterationId = iterationId,
					Status = data.WPStatus
				});
			}
			Parallel.Invoke(() =>
			{
				tabDetailsPlan_Log($"Saving changes for {toolkitWPItems.Count} WP(s)");
				var cwpResult = _service.UpdateWP(toolkitWPItems);
				tabDetailsPlan_Log($"Saving changes for {toolkitWPItems.Count} WP(s) - Done");
			});

			tabDetailsPlan_GridView_Changes_ClearChanges();
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
			}
		}

		private void tabDetailsPlan_GridView_Changes_ClearChanges()
		{
			tabDetailsPlan_btn_pushToToolkit.Enabled = false;
			_wpItemsLocalChanges = new List<WPItemChangesModel>();
			tabDetailsPlan_GridView_changes.DataSource = null;
			tabDetailsPlan_GridView_changes.DataSource = _wpItemsLocalChanges;
		}

		private void tabDetailsPlan_GridView_Changes_Reload()
		{
			tabDetailsPlan_btn_pushToToolkit.Enabled = false;
			tabDetailsPlan_GridView_changes.DataSource = null;
			tabDetailsPlan_GridView_changes.DataSource = _wpItemsLocalChanges;


			if (_wpItemsLocalChanges.Any())
				tabDetailsPlan_btn_apply_changes.Enabled = true;
		}

		private void tabDetailsPlan_Log(string message)
		{
			detailPlan_label_status.Text = $"tabDetailsPlan - {message}";
			Log(message);
		}
	}
}
