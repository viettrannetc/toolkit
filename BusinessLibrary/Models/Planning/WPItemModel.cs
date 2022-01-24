using System;
using System.Collections.Generic;

namespace BusinessLibrary.Models.Planning
{
	public class WPItemModel
	{
		public int FeatureId { get; set; }
		public string Feature { get; set; }
		/// <summary>
		/// Automatically
		/// </summary>
		public string FeatureShow
		{
			get
			{
				return $"{FeatureId} - {Feature}";
			}
		}
		public int USId { get; set; }
		public string USTitle { get; set; }
		/// <summary>
		/// Automatically
		/// </summary>
		public string USShow
		{
			get
			{
				return $"{USId} - {USTitle}";
			}
		}
		public int WPId { get; set; }
		public string WPTitle { get; set; }
		/// <summary>
		/// Automatically
		/// </summary>
		public string WPShow
		{
			get
			{
				return $"{FeatureShow} - {USShow} - {WPId}";
			}
		}
		public string WPType { get; set; }
		public DateTime? WPStart { get; set; }
		public DateTime? WPDueDate { get; set; }
		public string WPStatus { get; set; }
		public string WPTeam { get; set; }
		public string WPAssignee { get; set; }
		public string WPEstimate { get; set; }
		public string WPRemainingHour { get; set; }
		public string WPIterationId { get; set; }
		public string WPIterationName { get; set; }
		public string WPSpentHour { get; set; }
		public string WPDependOn { get; set; }

		/// <summary>
		/// This is local data to sort from the ganttchart - calculated by Dependency 
		/// </summary>
		public int WPPriority { get; set; }

		/// <summary>
		/// This is local data to sort from the ganttchart - calculated by Dependency 
		/// </summary>
		public DateTime? WPStartDate { get; set; }
		/// <summary>
		/// This is local data to sort from the ganttchart - calculated by Dependency 
		/// </summary>
		public List<DateTime> WPDateProgressing { get; set; }

		public int Version { get; set; }
		public DateTime VersionDate { get; set; }
	}
}
