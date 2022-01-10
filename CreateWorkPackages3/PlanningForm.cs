﻿using BusinessLibrary.Models.Planning;
using CreateWorkPackages3.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CreateWorkPackages3
{
	public partial class PlanningForm : Form
	{
		public ToolkitService _service = new ToolkitService();
		public List<WPItemModel> _lsvlocalData = new List<WPItemModel>();
		public List<WPItemModel> _wpItemsLocal = new List<WPItemModel>();
		public ToolTip yourToolTip = new ToolTip();
		private Dictionary<string, List<Label>> actionLabels = new Dictionary<string, List<Label>>();

		public PlanningForm(List<WPItemModel> wpsList, ToolkitService service)
		{
			_service = service;
			_wpItemsLocal = wpsList;
			InitializeComponent();

			MapDataFromDetailGridViewToPlanningGridView(tabDetailsPlan_Planning_Iteration);

			yourToolTip.ToolTipIcon = ToolTipIcon.Warning;
			yourToolTip.IsBalloon = true;
			yourToolTip.ShowAlways = true;
			yourToolTip.AutoPopDelay = 10000;
		}

		private void tableLayoutPanel_Planning_Paint(object sender, PaintEventArgs e)
		{

		}

		#region Planning - Iteration


		List<IterationPlanningModel> _iterationsPlanning = new List<IterationPlanningModel>();

		/// <summary>
		/// https://stackoverflow.com/questions/22420503/add-row-dynamically-in-tablelayoutpanel
		/// </summary>
		/// <param name="planningRow"></param>
		//public void MapDataFromDetailGridViewToPlanningGridView(TableLayoutPanel panel)
		//{
		//	//Map Data From DetailGridView To _iterationPlanning
		//	//ReLoad tabDetailsPlan_Planning_Iteration by _iterationPlanning
		//	panel.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
		//	_iterationsPlanning = new List<IterationPlanningModel>();
		//	var witdh1stColumn = 300F;
		//	var witdhMinumumColumn = 80F;
		//	var heightMinumumRow = 30F;

		//	foreach (var iteration in _service._toolkitIterations)
		//	{
		//		var planningIteration = _iterationsPlanning.FirstOrDefault(i => i.IterationName == iteration.Title);
		//		if (planningIteration == null)
		//		{
		//			planningIteration = new IterationPlanningModel(iteration, _service._toolkitAllocationsModel, _service._toolkitAllocationAdjustmentsModel);
		//			var allocatedWps = _wpItemsLocal.Where(w => !string.IsNullOrEmpty(w.WPIterationId) && int.Parse(w.WPIterationId) == iteration.Id).ToList();
		//			planningIteration.RefreshWp(allocatedWps);

		//			_iterationsPlanning.Add(planningIteration);
		//		}
		//	}

		//	_iterationsPlanning = _iterationsPlanning
		//		.OrderBy(d => d.Year)
		//		.ThenBy(d => d.Month)
		//		//.ThenBy(d => d.Weeks)
		//		.ThenBy(d => d.Iteration)
		//		.ToList();

		//	//var numberOfRows = _iterationsPlanning.SelectMany(i => i.Items.SelectMany(f => f.Feature)).Distinct().ToList().Count + 5;

		//	panel.RowStyles.Clear();
		//	panel.ColumnStyles.Clear();

		//	panel.Dock = DockStyle.Fill;
		//	panel.ColumnCount = 1;
		//	panel.RowCount = 1;// numberOfRows;

		//	//#region 1st row - Month
		//	//var durationInMonths = _iterationsPlanning.Select(i => i.Month).Distinct().ToList();
		//	//var firstRow = new TableLayoutPanel()
		//	//{
		//	//	Dock = DockStyle.Fill,
		//	//	ColumnCount = durationInMonths.Count + 1,
		//	//	RowCount = 1,
		//	//};
		//	//for (int i = 0; i < firstRow.ColumnCount; i++)
		//	//{
		//	//	firstRow.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute) { Width = i == 0 ? witdh1stColumn : witdhminumumColumn * 4 });
		//	//}
		//	//firstRow.Controls.Add(new Label() { TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill, Text = "Month" }, 0, 0);

		//	//for (int i = 1; i <= durationInMonths.Count; i++)
		//	//{
		//	//	firstRow.Controls.Add(new Label() { TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill, Text = durationInMonths[i - 1].ToString() }, i, 0);
		//	//}
		//	//panel.Controls.Add(firstRow, 0, 0);
		//	//panel.RowStyles.Insert(0, new RowStyle(SizeType.Absolute, heightMinumumRow)); 
		//	//#endregion

		//	#region 1st row - Week
		//	//panel.RowCount = panel.RowCount + 1;
		//	var durationInWeeks = _iterationsPlanning.SelectMany(i => i.Weeks).Distinct().ToList();
		//	var secondRow = new TableLayoutPanel()
		//	{
		//		Dock = DockStyle.Fill,
		//		ColumnCount = durationInWeeks.Count + 1,
		//		RowCount = 1,
		//	};
		//	for (int i = 0; i < secondRow.ColumnCount; i++)
		//	{
		//		secondRow.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute) { Width = i == 0 ? witdh1stColumn : witdhMinumumColumn });
		//	}
		//	secondRow.Controls.Add(new Label() { TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill, Text = "Week" }, 0, 0);

		//	for (int i = 1; i <= durationInWeeks.Count; i++)
		//	{
		//		secondRow.Controls.Add(new Label() { TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill, Text = durationInWeeks[i - 1].ToString() }, i, 0);
		//	}
		//	panel.Controls.Add(secondRow, 0, panel.RowCount - 1);
		//	panel.RowStyles.Insert(panel.RowCount - 1, new RowStyle(SizeType.Absolute, heightMinumumRow));
		//	#endregion

		//	#region 2nd row - Iteration
		//	panel.RowCount = panel.RowCount + 1;
		//	var iterations = _iterationsPlanning.Select(i => i.Iteration).Distinct().ToList();
		//	var thirdRow = new TableLayoutPanel()
		//	{
		//		Dock = DockStyle.Fill,
		//		ColumnCount = iterations.Count + 1,
		//		RowCount = 1,
		//	};
		//	for (int i = 0; i < thirdRow.ColumnCount; i++)
		//	{
		//		var showingIteration = _iterationsPlanning.FirstOrDefault(d => d.Iteration == i);

		//		thirdRow.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute) { Width = i == 0 ? witdh1stColumn : witdhMinumumColumn * showingIteration.Weeks.Count() });
		//	}
		//	thirdRow.Controls.Add(new Label() { TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill, Text = "Iteration" }, 0, 0);
		//	thirdRow.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
		//	for (int i = 1; i <= iterations.Count; i++)
		//	{
		//		var showingIteration = _iterationsPlanning.First(d => d.Iteration == i);
		//		var label = new Label() { TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill, Text = iterations[i - 1].ToString(), BackColor = showingIteration.Status.Color };
		//		thirdRow.Controls.Add(label, i, 0);
		//		var content = $"{showingIteration.Status.ToolTip}\r\n{Environment.NewLine}{Label_Iteration_DetailContent(showingIteration)}";
		//		yourToolTip.SetToolTip(label, content);
		//	}
		//	panel.Controls.Add(thirdRow, 0, panel.RowCount - 1);
		//	panel.RowStyles.Insert(panel.RowCount - 1, new RowStyle(SizeType.Absolute, heightMinumumRow));
		//	#endregion

		//	#region 3rd row - Available / Workload / Allocated
		//	panel.RowCount = panel.RowCount + 1;
		//	var numberOfColumnIn4thRow = _iterationsPlanning.Select(i => i.Iteration).Distinct().ToList();
		//	var fourthRow = new TableLayoutPanel()
		//	{
		//		Dock = DockStyle.Fill,
		//		ColumnCount = numberOfColumnIn4thRow.Count + 1,
		//		RowCount = 1,
		//	};
		//	for (int i = 0; i < fourthRow.ColumnCount; i++)
		//	{
		//		var showingIteration = _iterationsPlanning.FirstOrDefault(d => d.Iteration == i);

		//		fourthRow.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute) { Width = i == 0 ? witdh1stColumn : witdhMinumumColumn * showingIteration.Weeks.Count() });
		//	}
		//	fourthRow.Controls.Add(new Label() { TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill, Text = "Available/Workload/Allocated" }, 0, 0);
		//	for (int i = 1; i <= numberOfColumnIn4thRow.Count; i++)
		//	{
		//		var iteration = _iterationsPlanning.First(it => it.Iteration == numberOfColumnIn4thRow[i - 1]);
		//		fourthRow.Controls.Add(new Label() { TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill, Text = $"{iteration.AvailableHours}/{iteration.Workload}/{iteration.AllocatedHours}" }, i, 0);

		//		//var showingIteration = _iterationsPlanning.First(d => d.Iteration == i);
		//		//var label = new Label() { TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill, Text = $"{showingIteration.AvailableHours}/{showingIteration.Workload}/{showingIteration.AllocatedHours}", BackColor = showingIteration.Status.Color };
		//		//thirdRow.Controls.Add(label, i, 0);
		//		//yourToolTip.SetToolTip(label, showingIteration.Status.ToolTip);
		//	}
		//	panel.Controls.Add(fourthRow, 0, panel.RowCount - 1);
		//	panel.RowStyles.Insert(panel.RowCount - 1, new RowStyle(SizeType.Absolute, heightMinumumRow));
		//	#endregion



		//	#region From 5th rows, the game is start

		//	panel.RowCount = panel.RowCount + 1;
		//	var lastPanelRow = new TableLayoutPanel()
		//	{
		//		Dock = DockStyle.Fill,
		//		ColumnCount = 1,
		//		RowCount = 1,
		//		AutoScroll = true
		//	};
		//	panel.Controls.Add(lastPanelRow, 0, panel.RowCount - 1);

		//	var numberOfColumnInRow = _iterationsPlanning.Select(i => i.Iteration).Distinct().ToList();
		//	var lastRow = 3;
		//	foreach (var feature in _service._toolkitFeatures)
		//	{
		//		lastPanelRow.RowCount = lastPanelRow.RowCount + 1;
		//		var nextRow = new TableLayoutPanel()
		//		{
		//			Dock = DockStyle.Fill,
		//			ColumnCount = numberOfColumnInRow.Count + 1,
		//			RowCount = 1,
		//			CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble
		//		};

		//		var relavantIterations = _iterationsPlanning.Where(i => i.WorkPackages.Any(w => w.FeatureId == feature.Id)).ToList();

		//		for (int i = 0; i < nextRow.ColumnCount; i++)
		//		{
		//			var showingIteration = _iterationsPlanning.FirstOrDefault(d => d.Iteration == i);
		//			nextRow.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute) { Width = i == 0 ? witdh1stColumn : witdhMinumumColumn * showingIteration.Weeks.Count() });
		//		}
		//		nextRow.Controls.Add(new Label() { TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill, Text = $"{feature.Title} - ({feature.Id})" }, 0, 0);

		//		for (int i = 1; i <= numberOfColumnInRow.Count; i++)
		//		{
		//			var iteration = relavantIterations.FirstOrDefault(it => it.Iteration == numberOfColumnIn4thRow[i - 1]);
		//			if (iteration == null) continue;

		//			var selectedFeature = iteration.Items.First(f => f.Feature == $"{feature.Id} - {feature.Title}");
		//			var actions = selectedFeature.Actions;
		//			var content = new StringBuilder();
		//			foreach (var action in actions)
		//			{
		//				content.AppendLine($"{action.Item2}{Environment.NewLine}");
		//			}

		//			//nextRow.Controls.Add(new Label() { TextAlign = ContentAlignment.TopLeft, Dock = DockStyle.Fill, Text = $"{string.Join(" + ", actions)}" }, i, 0);
		//			nextRow.Controls.Add(new Label()
		//			{
		//				TextAlign = ContentAlignment.TopLeft,
		//				Dock = DockStyle.Fill,
		//				Text = $"{content}",
		//			}, i, 0);
		//		}
		//		lastPanelRow.Controls.Add(nextRow, 0, lastRow + 1);

		//		lastRow++;
		//		panel.RowStyles.Insert(lastRow - 1, new RowStyle(SizeType.AutoSize));
		//	}

		//	#endregion
		//}

		public void MapDataFromDetailGridViewToPlanningGridView(TableLayoutPanel planningRow)
		{
			planningRow.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
			_iterationsPlanning = new List<IterationPlanningModel>();
			var witdh1stColumn = 300F;
			var witdhMinumumColumn = 85F;
			var heightMinumumRow = 30F;

			foreach (var iteration in _service._toolkitIterations)
			{
				var planningIteration = _iterationsPlanning.FirstOrDefault(i => i.IterationName == iteration.Title);
				if (planningIteration == null)
				{
					planningIteration = new IterationPlanningModel(iteration, _service._toolkitAllocationsModel, _service._toolkitAllocationAdjustmentsModel);
					var allocatedWps = _wpItemsLocal.Where(w => !string.IsNullOrEmpty(w.WPIterationId) && int.Parse(w.WPIterationId) == iteration.Id).ToList();
					planningIteration.RefreshWp(allocatedWps);

					_iterationsPlanning.Add(planningIteration);
				}
			}

			_iterationsPlanning = _iterationsPlanning
				.OrderBy(d => d.Year)
				.ThenBy(d => d.Month)
				.ThenBy(d => d.IterationId)
				.ToList();

			planningRow.RowStyles.Clear();
			planningRow.ColumnStyles.Clear();

			planningRow.Dock = DockStyle.Fill;
			planningRow.ColumnCount = 1;
			planningRow.RowCount = 1;// numberOfRows;
			planningRow.AutoScroll = true;

			#region 1st row - Week
			var durationInWeeks = _iterationsPlanning.SelectMany(i => i.Weeks).Distinct().ToList();

			var weekRow = new TableLayoutPanel()
			{
				Dock = DockStyle.Fill,
				ColumnCount = durationInWeeks.Count + 1,
				RowCount = 1,
			};
			for (int i = 0; i < weekRow.ColumnCount; i++)
			{
				weekRow.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute) { Width = i == 0 ? witdh1stColumn : witdhMinumumColumn });
			}
			weekRow.Controls.Add(new Label() { TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill, Text = "Week" }, 0, 0);

			for (int i = 1; i <= durationInWeeks.Count; i++)
			{
				weekRow.Controls.Add(new Label() { TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill, Text = durationInWeeks[i - 1].ToString() }, i, 0);
			}
			planningRow.Controls.Add(weekRow, 0, planningRow.RowCount - 1);
			planningRow.RowStyles.Insert(planningRow.RowCount - 1, new RowStyle(SizeType.Absolute, heightMinumumRow));
			#endregion

			#region 2nd row - Iteration
			planningRow.RowCount = planningRow.RowCount + 1;
			var iterations = _iterationsPlanning.Select(i => i.IterationId).Distinct().ToList();
			var iterationRow = new TableLayoutPanel()
			{
				Dock = DockStyle.Fill,
				ColumnCount = iterations.Count + 1,
				RowCount = 1,
			};
			for (int i = 0; i < iterationRow.ColumnCount; i++)
			{
				var showingIteration = _iterationsPlanning.FirstOrDefault(d => d.IterationId == i);

				iterationRow.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute) { Width = i == 0 ? witdh1stColumn : witdhMinumumColumn * showingIteration.Weeks.Count() });
			}
			iterationRow.Controls.Add(new Label() { TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill, Text = "Iteration" }, 0, 0);
			iterationRow.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
			for (int i = 1; i <= iterations.Count; i++)
			{
				var showingIteration = _iterationsPlanning.First(d => d.IterationId == i);
				var label = new Label() { TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill, Text = iterations[i - 1].ToString(), BackColor = showingIteration.Status.Color };
				iterationRow.Controls.Add(label, i, 0);
				var content = $"{showingIteration.Status.ToolTip}\r\n{Environment.NewLine}{Label_Iteration_DetailContent(showingIteration)}";
				yourToolTip.SetToolTip(label, content);
			}
			planningRow.Controls.Add(iterationRow, 0, planningRow.RowCount - 1);
			planningRow.RowStyles.Insert(planningRow.RowCount - 1, new RowStyle(SizeType.Absolute, heightMinumumRow));
			#endregion

			#region 3rd row - Available / Workload / Allocated
			planningRow.RowCount = planningRow.RowCount + 1;
			var numberOfColumnIn4thRow = _iterationsPlanning.Select(i => i.IterationId).Distinct().ToList();
			var availableRow = new TableLayoutPanel()
			{
				Dock = DockStyle.Fill,
				ColumnCount = numberOfColumnIn4thRow.Count + 1,
				RowCount = 1,
			};
			for (int i = 0; i < availableRow.ColumnCount; i++)
			{
				var showingIteration = _iterationsPlanning.FirstOrDefault(d => d.IterationId == i);

				availableRow.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute) { Width = i == 0 ? witdh1stColumn : witdhMinumumColumn * showingIteration.Weeks.Count() });
			}
			availableRow.Controls.Add(new Label() { TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill, Text = "Available/Workload/Allocated" }, 0, 0);
			for (int i = 1; i <= numberOfColumnIn4thRow.Count; i++)
			{
				var iteration = _iterationsPlanning.First(it => it.IterationId == numberOfColumnIn4thRow[i - 1]);
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

			var numberOfColumnInRow = _iterationsPlanning.Select(i => i.IterationId).Distinct().ToList();

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
				var relavantIterations = _iterationsPlanning.Where(i => i.WorkPackages.Any(w => w.FeatureId == feature.Id)).ToList();

				//auto generate style for all columns
				for (int i = 0; i < featureRow.ColumnCount; i++)
				{
					var showingIteration = _iterationsPlanning.FirstOrDefault(d => d.IterationId == i);
					featureRow.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute) { Width = i == 0 ? witdh1stColumn : witdhMinumumColumn * showingIteration.Weeks.Count() });
				}

				//Add feature name to the 1st column
				featureRow.Controls.Add(new Label() { TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill, Text = $"{feature.Title} - ({feature.Id})" }, 0, 0);


				//generate data for each cell - based on column & 1st row (start with row # = 0)

				for (int i = 1; i <= numberOfColumnInRow.Count; i++)
				{
					//var iteration = relavantIterations.FirstOrDefault(it => it.Iteration == numberOfColumnIn4thRow[i - 1]);
					//if (iteration == null) continue;

					//var selectedFeature = iteration.Items.First(f => f.Feature == $"{feature.Id} - {feature.Title}");
					//var actions = selectedFeature.Actions;
					//var content = new StringBuilder();
					//foreach (var action in actions)
					//{
					//	content.AppendLine($"{action.Item2}{Environment.NewLine}");
					//}

					//featureRow.Controls.Add(new Label()
					//{
					//	TextAlign = ContentAlignment.TopLeft,
					//	Dock = DockStyle.Fill,
					//	Text = $"{content}",
					//}, i, 0);


					var iteration = relavantIterations.FirstOrDefault(it => it.IterationId == numberOfColumnIn4thRow[i - 1]);
					if (iteration == null) continue;

					var selectedFeature = iteration.Items.First(f => f.Feature == $"{feature.Id} - {feature.Title}");
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
							ColumnCount = 1,
						};

						var actionLabel = new Label()
						{
							Name = $"{action.Item1}_{Guid.NewGuid()}_{actionIndex}",
							TextAlign = ContentAlignment.TopLeft,
							Dock = DockStyle.Fill,
							Text = action.Item2,
						};
						actionLabel.MouseLeave += new System.EventHandler(ActionLabel_MouseLeave);
						actionLabel.MouseHover += new System.EventHandler(ActionLabel_MouseOver);
						ToolTip toolTip = new ToolTip();
						toolTip.SetToolTip(actionLabel, action.Item1);
						ControlExtension.Draggable(actionLabel, true);

						if (!actionLabels.ContainsKey(action.Item1))
							actionLabels.Add(action.Item1, new List<Label>() { actionLabel });
						else
						{
							actionLabels[action.Item1].Add(actionLabel);
						}

						actionRow.Controls.Add(actionLabel, 0, 0);
						actionRow.RowStyles.Insert(0, new RowStyle(SizeType.Absolute) { Height = heightMinumumRow });

						iterationAndFeatureCell.Controls.Add(actionRow, 0, actionIndex);

						iterationAndFeatureCell.RowStyles.Insert(actionIndex, new RowStyle(SizeType.Absolute, heightMinumumRow));

						lastActionRow++;
					}
					featureRow.Controls.Add(iterationAndFeatureCell, i, 0);

					//iterationAndFeatureCell.RowStyles.Add(new RowStyle(SizeType.Absolute, heightMinumumRow));

					//featureRow.RowStyles.Add(new RowStyle(SizeType.Absolute) { Height = selectedFeature.Actions.Count * heightMinumumRow });
				}

				var findMaxHeight = 1;
				foreach (var iteration in relavantIterations)
				{
					var actionsInFeatures = iteration.Items.Where(i => i.Feature.Contains(feature.Title)).ToList();
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
			//find all label in the same US
			//set the background to Orange
			var usName = ((Label)sender).Name.Split('_')[0];

			var lstControls = actionLabels[usName];

			foreach (var item in lstControls)
			{
				item.BackColor = Color.LightGray;
			}
		}

		private void ActionLabel_MouseLeave(object sender, EventArgs e)
		{
			//find all label in the same US
			//set the background to Orange
			var usName = ((Label)sender).Name.Split('_')[0];

			//var lstControls = new List<Label>();
			//foreach (var control in this.Controls)
			//{
			//	if (control is Label && ((Label)control).Name.Contains(usName))
			//	{
			//		lstControls.Add((Label)control);
			//	}
			//}

			var lstControls = actionLabels[usName];

			foreach (var item in lstControls)
			{
				item.BackColor = Color.Transparent;
			}
		}
	}
}
