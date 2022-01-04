using BusinessLibrary.Ultilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace BusinessLibrary.Models
{
	public class FeatureOverview
	{
		public FeatureOverview()
		{
			NextActions = new List<FeatureNextAction>();
		}
		public string FeatureId { get; set; }
		public string FeatureName { get; set; }
		public string FeatureStatus { get; set; }
		public string Color
		{
			get
			{
				var result = "Green";
				if (this.NextActions.Any(a => a.Severity >= 2))
				{
					result = "Yellow";
				}
				if (this.NextActions.Any(a => a.Severity >= 4))
				{
					result = "Red";
				}

				return result;
			}
		}
		public List<FeatureNextAction> NextActions { get; set; }
	}

	public class FeatureNextAction
	{
		public FeatureNextAction(string team = null)
		{
			NextActions = new List<FeatureNextAction>();
			Team = string.IsNullOrEmpty(team)
				? Constains.TeamFunctional
				: team;
		}

		public string Team { get; set; }
		public string Who { get; set; }
		public string DoWhat { get; set; }
		public string When { get; set; }
		public decimal HowLong { get; set; }
		public bool IsPossible { get; set; }
		public int Severity { get; set; }
		public List<FeatureNextAction> NextActions { get; set; }
		public string URL { get {
				if (this.Level == TicketLevel.Feature)
					return $"https://goto.netcompany.com/cases/GTE747/NCDPP/Lists/Tasks/DispForm.aspx?ID=18{this.Id}";
				if (this.Level == TicketLevel.UserStory)
					return $"https://goto.netcompany.com/cases/GTE747/NCDPP/Lists/FunctionalScenarios/DispForm.aspx?ID={this.Id}";
				if (this.Level == TicketLevel.WorkPackage)
					return $"https://goto.netcompany.com/cases/GTE747/NCDPP/Lists/WorkPackages/DispForm.aspx?ID={this.Id}";

				return string.Empty;
			} }
		public string Id { get; set; }
		public TicketLevel Level { get; set; }
	}


	public enum TicketLevel {
		Feature,
		UserStory,
		WorkPackage
	}

	public class Reason
	{
		public string Defect { get; set; }
		public string Issue { get; set; }
		public string Blocker { get; set; }
	}

}
