using System;
using System.Collections.Generic;

namespace BusinessLibrary.Models.Planning
{
	public class PIPlanningModel : PlanningModel
	{
		public PIPlanningModel()
		{
			Iterations = new List<IterationPlanningModel>();
		}

		public List<IterationPlanningModel> Iterations { get; set; }
		public override decimal AvailableHours { get => throw new NotImplementedException(); }

		public override decimal Workload => throw new NotImplementedException();

		public override decimal AllocatedHours => throw new NotImplementedException();

		public override IterationPlanningStatusModel Status => throw new NotImplementedException();
	}

}
