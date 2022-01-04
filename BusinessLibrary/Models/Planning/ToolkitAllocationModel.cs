using System;

namespace BusinessLibrary.Models.Planning
{
	public class ToolkitAllocationResponseModel
	{
		public string Title { get; set; }
		public ToolkitLookupModel Team { get; set; }
		public DateTime? DateFrom { get; set; }
		public DateTime? DateTo { get; set; }
		public decimal HoursCapacity { get; set; }
		public ToolkitLookupModel Resource { get; set; }
	}

	public class ToolkitLookupModel
	{
		public int LookupId { get; set; }
		public string LookupValue { get; set; }
	}


	public class ToolkitIterationModel
	{
		public int Id { get; set; }
		public ToolkitLookupModel Team { get; set; }
		public string Title { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
	}

	public class ToolkitFeatureResponseModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public ToolkitLookupModel Team { get; set; }
		//public ToolkitLookupModel AssignedTo { get; set; }
		public string Status { get; set; }
		public DateTime? DueDate { get; set; }
		public ToolkitLookupModel Release { get; set; }
		public decimal RemainingWork { get; set; }
	}

}
