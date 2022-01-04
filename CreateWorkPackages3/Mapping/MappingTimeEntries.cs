using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreateWorkPackages3.ProductBacklogItems.Model;
using CreateWorkPackages3.TimeEntries.Model;
using Microsoft.SharePoint.WebControls;

namespace CreateWorkPackages3.Mapping
{
    internal static class MappingTimeEntries
    {
        public static TimeEntryModel MappingTask(ProductBacklogItemModel pbiModel)
        {
            TimeEntryModel timeEntryModel = new TimeEntryModel();
            timeEntryModel.Description = pbiModel.Id + ": " + pbiModel.Title;
            timeEntryModel.ProjectName = pbiModel.AreaPath;
            return timeEntryModel;
        }
    }
}
