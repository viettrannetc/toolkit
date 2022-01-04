using System;

namespace BusinessLibrary.Models
{
	public class ToolkitAllocationAdjustmentModel
	{
		public string Team { get; set; }
		public string Name { get; set; }
		public DateTime From { get; set; }
		public DateTime To { get; set; }
		public decimal Hours { get; set; }
	}

	public class ToolkitAllocationModel
	{
		public string Team { get; set; }
		public string Name { get; set; }
		public DateTime From { get; set; }
		public DateTime To { get; set; }
		public decimal Hours { get; set; }
	}

}
