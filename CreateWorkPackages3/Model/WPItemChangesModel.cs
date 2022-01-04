using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateWorkPackages3.Model
{
	//public class WPItemChangesModel : WPItemModel
	//{
	//	public string NewAssignee { get; set; }
	//	public string NewEstimate { get; set; }
	//	public string NewRemaining { get; set; }
	//	public string NewDependOn { get; set; }
	//	public DateTime? NewWPStart { get; set; }
	//	public DateTime? NewWPDueDate { get; set; }
	//}

	public class WPItemChangesModel
	{
		public int WPId { get; set; }
		public string Property { get; set; }
		public string OldValue { get; set; }
		public string NewValue { get; set; }
	}
}
