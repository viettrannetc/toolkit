using BusinessLibrary.Models.Planning;
using CreateWorkPackages3.Utilities.GanntChart;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CreateWorkPackages3
{
	/// <summary>
	/// Details tab
	/// </summary>
	public partial class Form1 : Form
	{
		
		#region Test
		DataTable dt = new DataTable();
		private DataTable GetDescriptionTable()
		{			
			DataTable l_dtDescription = new DataTable();
			l_dtDescription.Columns.Add("Description", typeof(string));
			l_dtDescription.Columns.Add("Type", typeof(string));

			l_dtDescription.Rows.Add("Lunch", "Expense");
			l_dtDescription.Rows.Add("Dinner", "Expense");
			l_dtDescription.Rows.Add("Breakfast", "Expense");
			l_dtDescription.Rows.Add("Designing", "Service");
			l_dtDescription.Rows.Add("Drawing", "Service");
			l_dtDescription.Rows.Add("Paper", "Material");
			l_dtDescription.Rows.Add("DrawingBoard", "Material");

			return l_dtDescription;
		}

		/// <summary>  
		/// Get datatable of PaidWIth.  
		/// </summary>  
		/// <returns></returns>  
		private DataTable GetPaidWithTable()
		{
			DataTable l_dtPaidwith = new DataTable();
			l_dtPaidwith.Columns.Add("PaidWith", typeof(string));
			l_dtPaidwith.Columns.Add("Code", typeof(string));

			l_dtPaidwith.Rows.Add("CreditCard", "CC");
			l_dtPaidwith.Rows.Add("DebitCard", "DC");

			return l_dtPaidwith;
		}

		/// <summary>  
		/// Get the data for grid.  
		/// </summary>  
		/// <returns></returns>  
		private DataTable GetGridTable()
		{
			DataTable l_dtGridTable = new DataTable();
			l_dtGridTable.Columns.Add("PaidWith", typeof(string));
			l_dtGridTable.Columns.Add("Description", typeof(string));

			l_dtGridTable.Rows.Add("CreditCard", "Drawing");

			return l_dtGridTable;
		}


		GanttChart ganttChart1;
		private void Load_GanttChart()
		{
			//txtLog = new TextBox();
			//txtLog.Dock = DockStyle.Fill;
			//txtLog.Multiline = true;
			//txtLog.Enabled = false;
			//txtLog.ScrollBars = ScrollBars.Horizontal;
			//GanttChartPannel.Controls.Add(txtLog, 0, 3);

			//first Gantt Chart
			ganttChart1 = new GanttChart
			{
				AllowChange = false,
				Dock = DockStyle.Fill,
				FromDate = new DateTime(2015, 12, 12, 0, 0, 0),
				ToDate = new DateTime(2015, 12, 24, 0, 0, 0)
			};
			GanttChartPannel.Controls.Add(ganttChart1, 0, 1);

			ganttChart1.MouseMove += new MouseEventHandler(ganttChart1.GanttChart_MouseMove);
			//ganttChart1.MouseMove += new MouseEventHandler(GanttChart1_MouseMove);
			ganttChart1.MouseDragged += new MouseEventHandler(ganttChart1.GanttChart_MouseDragged);
			ganttChart1.MouseLeave += new EventHandler(ganttChart1.GanttChart_MouseLeave);
			//ganttChart1.ContextMenuStrip = ContextMenuGanttChart1;

			List<BarInformation> timeline = new List<BarInformation>();

			//timeline.Add(new BarInformation("Row 1", new DateTime(2015, 12, 12), new DateTime(2015, 12, 16), Color.Aqua, Color.Khaki, 0));
			//timeline.Add(new BarInformation("Row 2", new DateTime(2015, 12, 13), new DateTime(2015, 12, 20), Color.AliceBlue, Color.Khaki, 1));
			//timeline.Add(new BarInformation("Row 3", new DateTime(2015, 12, 14), new DateTime(2015, 12, 24), Color.Violet, Color.Khaki, 2));
			//timeline.Add(new BarInformation("Row 2", new DateTime(2015, 12, 21), new DateTime(2015, 12, 22, 12, 0, 0), Color.Yellow, Color.Khaki, 1));
			//timeline.Add(new BarInformation("Row 1", new DateTime(2015, 12, 17), new DateTime(2015, 12, 24), Color.LawnGreen, Color.Khaki, 0));

			//var clonedData = new List<WPItemModel>();
			//_lsvlocalData.ForEach(l => clonedData.Add(l.DeepCopy()));

			//foreach (var item in clonedData)
			//{
			//	if (item.WPStart.HasValue && item.WPDueDate.HasValue)
			//	{
			//		timeline.Add(new BarInformation($"{item.FeatureShow}-{item.WPTitle} ({item.WPType})",
			//		  item.WPStart.Value, //					new DateTime(2015, 12, 17),
			//		  item.WPDueDate.Value, //				new DateTime(2015, 12, 24),
			//		  Color.LawnGreen, Color.Khaki, 0));
			//	}
			//}

			foreach (BarInformation bar in timeline)
			{
				ganttChart1.AddChartBar(bar.RowText, bar, bar.FromTime, bar.ToTime, bar.Color, bar.HoverColor, bar.Index);
			}
		}
		#endregion
	}
}
