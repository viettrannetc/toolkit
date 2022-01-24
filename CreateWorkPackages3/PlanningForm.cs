using BusinessLibrary.Models.Planning;
using BusinessLibrary.Models.Planning.Extension;
using BusinessLibrary.Ultilities;
using CreateWorkPackages3.Extension;
using CreateWorkPackages3.Forms.UpdateForm;
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

namespace CreateWorkPackages3
{
	public partial class PlanningForm : Form
	{
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

		public ToolkitService _service = new ToolkitService();
		public List<WPItemModel> _currentWpItemsLocal = new List<WPItemModel>();
		public ToolTip yourToolTip = new ToolTip();
		private Dictionary<string, List<Label>> actionLabels = new Dictionary<string, List<Label>>();
		List<IterationPlanningModel> _iterationsPlanning = new List<IterationPlanningModel>();

		public PlanningForm(List<WPItemModel> currentWpsList, ToolkitService service)
		{
			_service = service;
			_currentWpItemsLocal = currentWpsList;
			InitializeComponent();

			DrawPlanningGridView(tabDetailsPlan_Planning_Iteration);

			yourToolTip.ToolTipIcon = ToolTipIcon.Warning;
			yourToolTip.IsBalloon = true;
			yourToolTip.ShowAlways = true;
			yourToolTip.AutoPopDelay = 10000;
		}

		private void tableLayoutPanel_Planning_Paint(object sender, PaintEventArgs e)
		{

		}

		#region Planning - Iteration




		/// <summary>
		/// https://stackoverflow.com/questions/22420503/add-row-dynamically-in-tablelayoutpanel
		/// </summary>
		/// <param name="planningRow"></param>
		public void DrawPlanningGridView(TableLayoutPanel planningRow)
		{
			tabDetailsPlan_Planning_Iteration.ColumnStyles.Clear();
			tabDetailsPlan_Planning_Iteration.RowStyles.Clear();
			tabDetailsPlan_Planning_Iteration.Controls.Clear();
			planningRow = tabDetailsPlan_Planning_Iteration;

			planningRow.HorizontalScroll.Enabled = true;
			planningRow.AutoScroll = true;
			planningRow.AutoSize = false;

			planningRow.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
			_iterationsPlanning = new List<IterationPlanningModel>();
			var witdh1stColumn = 300F;
			//var witdhMinumumColumn = 85F;
			//var witdhMinumumColumn = 120F;
			var witdhIterationColumn = 190F;
			var heightMinumumRow = 30F;
			var wphistories = _service._wpHistories.SelectMany(wh => wh.Value).ToList();
			foreach (var iteration in _service._toolkitIterations)
			{
				var planningIteration = _iterationsPlanning.FirstOrDefault(i => i.IterationName == iteration.Title);
				if (planningIteration == null)
				{
					planningIteration = new IterationPlanningModel(iteration, _service._toolkitAllocationsModel, _service._toolkitAllocationAdjustmentsModel, wphistories);

					//var wpsInIteration = _currentWpItemsLocal.Where(w => !string.IsNullOrEmpty(w.WPIterationId) && int.Parse(w.WPIterationId) == iteration.Id).ToList();
					//planningIteration.AllocatedWp(wpsInIteration);

					_iterationsPlanning.Add(planningIteration);
				}
			}

			_iterationsPlanning = _iterationsPlanning
				.OrderBy(d => d.Year)
				.ThenBy(d => d.Month)
				.ThenBy(d => d.IterationOrder)
				.ToList();

			planningRow.RowStyles.Clear();
			planningRow.ColumnStyles.Clear();

			planningRow.Dock = DockStyle.Fill;
			planningRow.ColumnCount = 1;
			planningRow.RowCount = 1;// numberOfRows;

			#region 2nd row - Iteration
			var iterations = _iterationsPlanning.Select(i => i.IterationOrder).Distinct().ToList();
			var iterationRow = new TableLayoutPanel()
			{
				Dock = DockStyle.Fill,
				ColumnCount = iterations.Count + 1,
				RowCount = 1,
			};
			for (int i = 0; i < iterationRow.ColumnCount; i++)
			{
				var showingIteration = _iterationsPlanning.FirstOrDefault(d => d.IterationOrder == i);

				iterationRow.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute) { Width = i == 0 ? witdh1stColumn : witdhIterationColumn });
			}

			var titleIterationLabel = new Label() { TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill, Text = "Iteration" };
			titleIterationLabel.DoubleClick += new System.EventHandler(ActionLabel_Click_ReloadTheToolkitData);
			iterationRow.Controls.Add(titleIterationLabel, 0, 0);
			iterationRow.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
			for (int i = 1; i <= iterations.Count; i++)
			{
				var showingIteration = _iterationsPlanning.First(d => d.IterationOrder == i);
				var iterationLabel = new Label() { Name = showingIteration.IterationName, TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill, Text = $"{iterations[i - 1].ToString()} - Week ({string.Join(",", showingIteration.Weeks)})", BackColor = showingIteration.Status.Color };
				iterationLabel.DoubleClick += new System.EventHandler(ActionLabel_Click_OpenIterationOverview);
				iterationRow.Controls.Add(iterationLabel, i, 0);
				var content = $"{showingIteration.Status.ToolTip}\r\n{Environment.NewLine}{Label_Iteration_DetailContent(showingIteration)}";
				yourToolTip.SetToolTip(iterationLabel, content);
			}
			planningRow.Controls.Add(iterationRow, 0, planningRow.RowCount - 1);
			planningRow.RowStyles.Insert(planningRow.RowCount - 1, new RowStyle(SizeType.Absolute, heightMinumumRow));
			#endregion

			#region 3rd row - Available / Workload / Allocated
			planningRow.RowCount = planningRow.RowCount + 1;
			var numberOfColumnIn4thRow = _iterationsPlanning.Select(i => i.IterationOrder).Distinct().ToList();
			var availableRow = new TableLayoutPanel()
			{
				Dock = DockStyle.Fill,
				ColumnCount = numberOfColumnIn4thRow.Count + 1,
				RowCount = 1,
			};
			for (int i = 0; i < availableRow.ColumnCount; i++)
			{
				var showingIteration = _iterationsPlanning.FirstOrDefault(d => d.IterationOrder == i);

				availableRow.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute) { Width = i == 0 ? witdh1stColumn : witdhIterationColumn });
			}
			availableRow.Controls.Add(new Label() { TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill, Text = "Available/Workload/Allocated" }, 0, 0);
			for (int i = 1; i <= numberOfColumnIn4thRow.Count; i++)
			{
				var iteration = _iterationsPlanning.First(it => it.IterationOrder == numberOfColumnIn4thRow[i - 1]);
				availableRow.Controls.Add(new Label() { TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill, Text = $"{iteration.AvailableHours}/{iteration.Workload}/{iteration.AllocatedHours}" }, i, 0);
			}
			planningRow.Controls.Add(availableRow, 0, planningRow.RowCount - 1);
			planningRow.RowStyles.Insert(planningRow.RowCount - 1, new RowStyle(SizeType.Absolute, heightMinumumRow));
			#endregion



			#region From 4th rows, the game is start
			planningRow.RowCount = planningRow.RowCount + 1;
			var featuresRow = new TableLayoutPanel()
			{
				Dock = DockStyle.Fill,
				ColumnCount = 1,
				RowCount = 1,
				AutoScroll = true
			};
			planningRow.Controls.Add(featuresRow, 0, planningRow.RowCount - 1);

			var numberOfColumnInRow = _iterationsPlanning.Select(i => i.IterationOrder).Distinct().ToList();

			for (int featureIndex = 0; featureIndex < _service._toolkitFeatures.Count; featureIndex++)
			{
				var feature = _service._toolkitFeatures[featureIndex];
				featuresRow.RowCount = featuresRow.RowCount/* + 1*/;

				//create panel
				var featureRow = new TableLayoutPanel()
				{
					Dock = DockStyle.Fill,
					ColumnCount = numberOfColumnInRow.Count + 1,
					RowCount = 1,
					CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
				};
				var relavantIterations = _iterationsPlanning.Where(i => i.WorkPackagesActualInIteration.Any(w => w.FeatureId == feature.Id)).ToList();

				//auto generate style for all columns
				for (int i = 0; i < featureRow.ColumnCount; i++)
				{
					var showingIteration = _iterationsPlanning.FirstOrDefault(d => d.IterationOrder == i);
					featureRow.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute) { Width = i == 0 ? witdh1stColumn : witdhIterationColumn });// witdhMinumumColumn * showingIteration.Weeks.Count() });
				}

				//Add feature name to the 1st column
				featureRow.Controls.Add(new Label() { TextAlign = ContentAlignment.TopLeft, Dock = DockStyle.Fill, Text = $"{feature.Id} - {feature.Title}" }, 0, 0);


				//generate data for each cell - based on column & 1st row (start with row # = 0)
				for (int i = 1; i <= numberOfColumnInRow.Count; i++)
				{
					var iteration = relavantIterations.FirstOrDefault(it => it.IterationOrder == numberOfColumnIn4thRow[i - 1]);
					if (iteration == null) continue;

					var selectedFeature = iteration.ActionItems.First(f => f.Feature == $"{feature.Id} - {feature.Title}");
					if (!selectedFeature.Actions.Any()) continue;

					var lastActionRow = 0;
					var iterationAndFeatureCell = new TableLayoutPanel()
					{
						Dock = DockStyle.Fill,
						ColumnCount = 1,
					};

					for (int actionIndex = 0; actionIndex < selectedFeature.Actions.Count; actionIndex++)
					{
						var action = selectedFeature.Actions[actionIndex];
						iterationAndFeatureCell.RowCount = lastActionRow + 1;
						var actionRow = new TableLayoutPanel()
						{
							Dock = DockStyle.Fill,
							ColumnCount = 1
						};

						var actionLabel = new Label()
						{
							Name = action.Feature,
							Dock = DockStyle.Fill,
							Text = action.WPText,
							TextAlign = ContentAlignment.TopLeft,
							Image = action.Icon.SetImage(12),
							ImageAlign = ContentAlignment.TopRight,
							//AutoSize = false
							MaximumSize = new Size((int)witdhIterationColumn - 20, 0),//((int)(witdhMinumumColumn * iteration.Weeks.Count()) - 20, 0),
							AutoSize = false
						};
						actionLabel.MouseLeave += new System.EventHandler(ActionLabel_MouseLeave);
						actionLabel.MouseHover += new System.EventHandler(ActionLabel_MouseOver);
						actionLabel.DoubleClick += new System.EventHandler(ActionLabel_Click_UpdateWPs);
						ToolTip toolTip = new ToolTip();
						toolTip.SetToolTip(actionLabel, action.UserStory);

						ControlExtension.Draggable(actionLabel, true);

						if (!actionLabels.ContainsKey(action.Feature))
							actionLabels.Add(action.Feature, new List<Label>() { actionLabel });
						else
							actionLabels[action.Feature].Add(actionLabel);

						actionRow.Controls.Add(actionLabel, 0, 0);
						actionRow.RowStyles.Insert(0, new RowStyle(SizeType.Absolute) { Height = heightMinumumRow });

						iterationAndFeatureCell.Controls.Add(actionRow, 0, actionIndex);

						iterationAndFeatureCell.RowStyles.Insert(actionIndex, new RowStyle(SizeType.Absolute, heightMinumumRow));

						lastActionRow++;
					}
					featureRow.Controls.Add(iterationAndFeatureCell, i, 0);
				}

				var findMaxHeight = 1;
				foreach (var iteration in relavantIterations)
				{
					var actionsInFeatures = iteration.ActionItems.Where(i => i.Feature.Contains(feature.Title)).ToList();
					foreach (var item in actionsInFeatures)
					{
						if (item.Actions.Count > findMaxHeight)
							findMaxHeight = item.Actions.Count;
					}
				}

				//add feature to the 1st column of the feature panel
				featuresRow.Controls.Add(featureRow, 0, featureIndex); //Why feature indx + 1?
																	   //featureRow.RowStyles.Add(new RowStyle(SizeType.AutoSize));
				featuresRow.RowStyles.Insert(featureIndex, new RowStyle(SizeType.Absolute) { Height = findMaxHeight * heightMinumumRow });
			}

			#endregion
		}


		#endregion

		protected string Label_Iteration_DetailContent(IterationPlanningModel selectedIteration)
		{
			//Bind data from selected iteration to "tabDetailsPlan_Planning_Iteration_Details_GridView"
			//-- Available/workload/allocated
			//-- TODO: define

			var stringContent = new StringBuilder();
			foreach (var person in selectedIteration.People)
			{
				stringContent.AppendLine($"{person.Name} - {person.AvailableHours}\r\n{Environment.NewLine}");
			}

			return stringContent.ToString();
		}

		private void label26_MouseHover(object sender, EventArgs e)
		{

		}

		private void label26_MouseLeave(object sender, EventArgs e)
		{

		}

		private void ActionLabel_MouseOver(object sender, EventArgs e)
		{
			var usId = ((Label)sender).Name.Split('-')[2];

			var item = actionLabels.Keys.Where(k => k.Contains(usId.Trim()) == true).ToList();
			if (item == null || !item.Any()) return;

			foreach (var key in item)
			{
				var lstControls = actionLabels[key];
				foreach (var label in lstControls)
				{
					label.BackColor = Color.LightGray;
				}
			}


		}

		private void ActionLabel_MouseLeave(object sender, EventArgs e)
		{
			var usId = ((Label)sender).Name.Split('-')[2];

			var item = actionLabels.Keys.Where(k => k.Contains(usId.Trim()) == true).ToList();
			if (item == null || !item.Any()) return;

			foreach (var key in item)
			{
				var lstControls = actionLabels[key];
				foreach (var label in lstControls)
				{
					label.BackColor = Color.Transparent;
				}
			}
		}

		private void ActionLabel_Click_OpenIterationOverview(object sender, EventArgs e)
		{
			var stringIteration = ((Label)sender).Name;
			if (!string.IsNullOrEmpty(stringIteration))
			{
				var openForm = Application.OpenForms.Cast<Form>().FirstOrDefault(form => form.Name == "IterationForm");
				if (openForm != null)
				{
					openForm.Close();
				}

				var wpsInSelectedIteration = _currentWpItemsLocal.Where(wp => wp.WPIterationName == stringIteration).ToList();

				var frm = new IterationForm(wpsInSelectedIteration, _service);
				frm.Show();
			}
		}

		private void ActionLabel_Click_ReloadTheToolkitData(object sender, EventArgs e)
		{
			//TODO:
			DrawPlanningGridView(tabDetailsPlan_Planning_Iteration);
		}

		#region Transfer data between Planning form vs Update WP 
		private void ActionLabel_Click_UpdateWPs(object sender, EventArgs e)
		{
			//TODO:
			//Change assignee
			//Change iteration
			//Change remaining hours/estimation
			//Change status
			var wpIdString = ((Label)sender).Name.Split('-')[((Label)sender).Name.Split('-').Count() - 1];
			int wpId = 0;
			if (!string.IsNullOrEmpty(wpIdString) && int.TryParse(wpIdString, out wpId))
			{
				var openForm = Application.OpenForms.Cast<Form>().FirstOrDefault(form => form.Name == "UpdateWP");
				if (openForm != null)
				{
					openForm.Close();
				}

				var selectedWP = _currentWpItemsLocal.First(wp => wp.WPId == wpId);

				var frm = new UpdateWP(selectedWP, RedrawPlanningGridView, _service);
				frm.Show();
			}
		}

		#region Delegation between Planning form vs Update WP 
		/// <summary>
		/// UpdateWP Form will call back to this action
		/// </summary>
		/// <param name="selectedWP"></param>
		public delegate void UpdateWPAction(WPItemModel selectedWP);

		private void RedrawPlanningGridView(WPItemModel selectedWP)
		{
			//Update expected WP to Local		- _wpItemsLocal
			var replacedWp = _currentWpItemsLocal.First(wp => wp.WPId == selectedWP.WPId);
			replacedWp.WPEstimate = selectedWP.WPEstimate;
			replacedWp.WPRemainingHour = selectedWP.WPRemainingHour;
			replacedWp.WPAssignee = selectedWP.WPAssignee;
			replacedWp.WPIterationId = selectedWP.WPIterationId;

			//update expected WP to Toolkit
			Parallel.Invoke(() =>
			{
				try
				{
					var toolkitWPItems = new List<ToolkitWPModel>();

					toolkitWPItems.Add(new ToolkitWPModel()
					{
						Id = replacedWp.WPId,
						AssigneeId = string.IsNullOrWhiteSpace(replacedWp.WPAssignee)
							? 213 //TODO: default is Vitra - how to clear assignee?
							: Constains.Toolkit_Assignee.First(a => a.Value == replacedWp.WPAssignee).Key,
						Estimate = replacedWp.WPEstimate,
						RemainingWork = replacedWp.WPRemainingHour,
						IterationId = int.Parse(replacedWp.WPIterationId)
					});

					_service.UpdateWP(toolkitWPItems);
				}
				catch (Exception exp)
				{
					//Log("Error connecting to Sharepoint API: " + exp.Message + "Feature disabled." + "\r\n");
				}
			});

			DrawPlanningGridView(tabDetailsPlan_Planning_Iteration);
		}
		#endregion

		#endregion
	}
}
