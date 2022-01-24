using System;
using System.Windows.Documents;

namespace CreateWorkPackages3.Workpackages.Model
{
	public class WorkpackageModel
	{
		public string Title { get; set; }
		public string AssignedTo { get; set; }
		public int Sprint { get; set; }
		public string Estimate { get; set; }
		public string DueDate { get; set; }
		public int Team { get; set; }
		public int RelatedCase { get; set; }
		public int Application { get; set; }
	}

	public class ToolkitWPModel : ToolkitModel
	{
		public ToolkitWPModel()
		{
		}

		public ToolkitWPModel(string wpUsId, string wpTitle, int teamId, string wpEstimation, string wpNoter, string status, int release, string wpType, string dueDate)
		{
			FunctionalScenario = int.Parse(wpUsId);
			Status = status;
			Team = teamId;
			Title = wpTitle;
			Note = wpNoter;
			Estimate = wpEstimation;
			RemainingWork = wpEstimation;
			Release = release;
			WPType = wpType;
			DueDate = dueDate;

		}
		public int FunctionalScenario { get; set; }
		public string WPType { get; set; }
		public string DueDate { get; set; }
		public int Release { get; set; }
		public string Estimate { get; set; }
		public string RemainingWork { get; set; }
		public int? IterationId { get; set; }
		public string StartDate { get; set; }
		public string DependOn { get; set; }
	}

	public class ToolkitUSModel : ToolkitModel
	{
		public int Case { get; set; }
	}

	public abstract class ToolkitModel
	{
		public string Title { get; set; }
		public int Team { get; set; }
		public string Status { get; set; }
		public string Note { get; set; }
		public int Id { get; set; }
		public int AssigneeId { get; set; }
	}
}
