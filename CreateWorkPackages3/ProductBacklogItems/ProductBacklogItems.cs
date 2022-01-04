using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CreateWorkPackages3.ProductBacklogItems.Model;
using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Client;

namespace CreateWorkPackages3.ProductBacklogItems
{
    class ProductBacklogItems
    {
        private const string SystemAreaPathField = "System.AreaPath";
        private const string MicrosoftVstsSchedulingEffortField = "Microsoft.VSTS.Scheduling.Effort";
        private const string SystemAssignedToField = "System.AssignedTo";
        private const string SystemTitleField = "System.Title";
        private const string SystemWorkItemTypeField = "System.WorkItemType";

        internal const string azureDevOpsOrganizationUrl = "https://source.netcompany.com/tfs/netcompany"; //change to the URL of your Azure DevOps account; NOTE: This must use HTTPS
        private VssConnection _connection;
        private WorkItemTrackingHttpClient _witClient;

        public void Connect()
        {
            //Prompt user for credential
            _connection = new VssConnection(new Uri(azureDevOpsOrganizationUrl), new VssClientCredentials());
            //create http client and query for resutls
            _witClient = _connection.GetClient<WorkItemTrackingHttpClient>();
        }

        public List<ProductBacklogItemModel> GetProductBacklogItemsInSprint(string sprint, string team, bool onlyMe)
        {
            List<ProductBacklogItemModel> pbiModelList = new List<ProductBacklogItemModel>();
            string assignedToOnlyMe = onlyMe ? "AND [Assigned To] = @Me ": String.Empty;
            
            Wiql query = new Wiql() { Query = "SELECT * FROM workitems WHERE [State] = 'Committed' AND ([Work Item Type] = 'Product Backlog Item' OR [Work Item Type] = 'Bug') AND [Iteration Path] = 'UIB0006\\" + sprint + " - " + team + "' "+ assignedToOnlyMe +" " };
            Cursor.Current = Cursors.WaitCursor;
            WorkItemQueryResult queryResults = _witClient.QueryByWiqlAsync(query).Result;
            Cursor.Current = Cursors.Arrow;
            if (queryResults == null || !queryResults.WorkItems.Any())
            {
                return null;
            }

            IEnumerable<WorkItemReference> tasksRefs = queryResults.WorkItems.OrderBy(x => x.Id);

            var workItemList = _witClient.GetWorkItemsAsync(tasksRefs.Select(wir => wir.Id), null, null, WorkItemExpand.Relations).Result;

            List<ListViewItem> m = new List<ListViewItem>();
            foreach (var wi in workItemList)
            {
                ProductBacklogItemModel pbiModel = new ProductBacklogItemModel();
                pbiModel.Id = wi.Id.ToString();
                pbiModel.Title = wi.Fields[SystemTitleField].ToString();
                pbiModel.Url = wi.Url;
                pbiModel.WorkItemType = wi.Fields[SystemWorkItemTypeField].ToString();

                pbiModel.RelatedFeatureTitle = GetRelatedFeatureTitle(wi);

                if (wi.Fields.ContainsKey(SystemAssignedToField))
                {
                    if (wi.Fields[SystemAssignedToField] is IdentityRef assignedTo)
                    {
                        pbiModel.AssignedTo = assignedTo.DisplayName;
                    }
                }

                if (wi.Fields.ContainsKey(SystemAreaPathField))
                {
                    pbiModel.AreaPath = wi.Fields[SystemAreaPathField].ToString();
                }

                if (wi.Fields.ContainsKey(MicrosoftVstsSchedulingEffortField))
                {
                    pbiModel.Effort = wi.Fields[MicrosoftVstsSchedulingEffortField].ToString();
                }

                pbiModel.Sprint = sprint;
                pbiModelList.Add(pbiModel);
            }
            return pbiModelList;
        }

        private string GetRelatedFeatureTitle(WorkItem wi)
        {
            var relationList = wi.Relations;
            var relatedTypes = relationList.Where(x => x.Rel == "System.LinkTypes.Related");

            foreach (var r in relatedTypes)
            {
                var id = int.Parse(r.Url.Split('/').Last()); // Get the feature id
                var relWorkItem = _witClient.GetWorkItemAsync(id).Result;
                var wit = relWorkItem.Fields["System.WorkItemType"].ToString();
                if (wit == "Feature")
                {
                    return relWorkItem.Fields["System.Title"].ToString(); 
                }
            }
            return string.Empty;
        }
    }
}
