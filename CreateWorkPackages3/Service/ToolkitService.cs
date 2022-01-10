using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using BusinessLibrary.Models;
using BusinessLibrary.Models.Planning;
using CreateWorkPackages3.Workpackages.Model;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Client;

namespace CreateWorkPackages3.Service
{
	public class ToolkitService
	{
		private ClientContext _context;
		readonly CamlQuery _query = CamlQuery.CreateAllItemsQuery(10000);
		public ListItemCollection _allocationadjustments;
		public List<ToolkitAllocationResponseModel> _toolkitAllocationAdjustmentsModel;
		public ListItemCollection _features;
		public List<ToolkitFeatureResponseModel> _toolkitFeatures;
		public ListItemCollection _us;
		public ListItemCollection _wps;
		public ListItemCollection _defects;
		public ListItemCollection _allocations;
		public List<ToolkitAllocationResponseModel> _toolkitAllocationsModel;
		public ListItemCollection _releases;
		public ListItemCollection _iterations;
		public List<ToolkitIterationModel> _toolkitIterations;

		private const string TitleField = "Title";
		private const string EstimateField = "Estimate";
		private const string DueDateField = "DueDate";
		private const string AssignedToField = "AssignedTo";
		private const string TeamField = "Team";
		private const string RelatedCaseField = "RelatedCase";
		private const string ReleaseField = "Release";
		private const string SprintField = "Sprint";
		private const string IterationField = "Iteration";
		private const string StatusField = "Status";
		private const string NoteField = "Noter";
		private const string FunctionalScenarioField = "FunctionalScenario";
		private const string WPTypeField = "WPType";
		private const string RemainingWorkField = "RemainingWork";
		private const string DepenOnField = "Depend_x0020_on";
		private const string StartDateField = "StartDate";

		public void Connect(string username, string password)
		{
			_context = new ClientContext("https://goto.netcompany.com/cases/GTE747/NCDPP");
			//GTO561/UIB0006
			//https://goto.netcompany.com/cases/GTO823/UIMDM
			ServicePointManager.ServerCertificateValidationCallback =
				((sender, certificate, chain, sslPolicyErrors) => true);

			string strUserName = username;
			string strPassword = password;
			NetworkCredential credentials = new NetworkCredential(strUserName, strPassword);
			_context.Credentials = credentials;
		}

		public void GetAllocationAdjustments()
		{
			//var userInfoList = _context.Web.SiteUserInfoList;

			var userInfoList = _context.Web.Lists.GetByTitle("Allocation Adjustments");
			var parameters = new string[] {

			};
			//CamlQuery _query =  CamlQuery.CreateAllItemsQuery(100, parameters);
			var query = new CamlQuery()
			{
				ViewXml = @"<View><Query><Where><Eq><FieldRef Name='Title' /><Value Type='Text'>NHAD - Off</Value></Eq></Where></Query><ViewFields><FieldRef Name='Title' /></ViewFields><QueryOptions /></View>"
			};
			//            query.ViewXml = @"<Query>
			//   <Where>
			//      <Eq>
			//         <FieldRef Name='Title' />
			//         <Value Type='Text'>NHAD - Off</Value>
			//      </Eq>
			//   </Where>
			//   <OrderBy>
			//      <FieldRef Name='Title' Ascending='True' />
			//   </OrderBy>
			//</Query>
			//<ViewFields>
			//   <FieldRef Name='Title' />
			//</ViewFields>
			//<RowLimit>10</RowLimit>
			//<QueryOptions />";


			_allocationadjustments = userInfoList.GetItems(query); //TODO:

			// Load users list
			_context.Load(_allocationadjustments, uitems => uitems.Include(
				item => item.Id,
				item => item["Title"],
				item => item.DisplayName));

			_context.ExecuteQuery();
		}


		public void GetAllocationAdjustments(string teamId)
		{
			var infoList = _context.Web.Lists.GetByTitle("Allocation Adjustments");
			//CamlQuery _query =  CamlQuery.CreateAllItemsQuery(100, parameters);
			var query = new CamlQuery()
			{
				ViewXml = $@"<View><Query>
<Where>
	<Eq>
		<FieldRef LookupId='TRUE' Name='Team' />
		<Value Type='Lookup'>{teamId}</Value>
	</Eq>
</Where>
</Query>
	<ViewFields>
		<FieldRef Name='Title' />
		<FieldRef Name='Team' />
		<FieldRef Name='DateFrom' />
		<FieldRef Name='DateTo' />
		<FieldRef Name='HoursCapacity' />
		<FieldRef Name='Resource' />
	</ViewFields>
<QueryOptions /></View>"
			};

			_allocationadjustments = infoList.GetItems(query); //TODO:

			_context.Load(_allocationadjustments, uitems => uitems.Include(
				item => item["Title"],
				item => item["Team"],
				item => item["DateFrom"],
				item => item["DateTo"],
				item => item["Resource"],
				item => item["HoursCapacity"]));

			_context.ExecuteQuery();

			_toolkitAllocationAdjustmentsModel = new List<ToolkitAllocationResponseModel>();
			foreach (var item in _allocationadjustments)
			{
				var teamLookupName = item.FieldValues["Team"] == null ? string.Empty : ((Microsoft.SharePoint.Client.FieldLookupValue)item.FieldValues["Team"]).LookupValue;
				var teamLookupId = item.FieldValues["Team"] == null ? 0 : ((Microsoft.SharePoint.Client.FieldLookupValue)item.FieldValues["Team"]).LookupId;

				var resourceLookupName = item.FieldValues["Resource"] == null ? string.Empty : ((Microsoft.SharePoint.Client.FieldLookupValue)item.FieldValues["Resource"]).LookupValue;
				var resourceLookupId = item.FieldValues["Resource"] == null ? 0 : ((Microsoft.SharePoint.Client.FieldLookupValue)item.FieldValues["Resource"]).LookupId;

				DateTime? startDate = null;
				if (item.FieldValues["DateFrom"] != null)
					startDate = DateTime.Parse(item.FieldValues["DateFrom"].ToString()).Date;
				DateTime? dueDate = null;
				if (item.FieldValues["DateTo"] != null)
					dueDate = DateTime.Parse(item.FieldValues["DateTo"].ToString()).Date;

				_toolkitAllocationAdjustmentsModel.Add(new ToolkitAllocationResponseModel
				{
					Title = item.FieldValues["Title"].ToString(),
					DateFrom = startDate,
					DateTo = dueDate,
					HoursCapacity = decimal.Parse(item.FieldValues["HoursCapacity"].ToString()),
					Team = new ToolkitLookupModel
					{
						LookupId = teamLookupId,
						LookupValue = teamLookupName
					},
					Resource = new ToolkitLookupModel
					{
						LookupId = resourceLookupId,
						LookupValue = resourceLookupName
					}
				});
			}
		}

		public void GetAllocation(string teamId)
		{
			var infoList = _context.Web.Lists.GetByTitle("Allocations");
			//CamlQuery _query =  CamlQuery.CreateAllItemsQuery(100, parameters);
			var query = new CamlQuery()
			{
				ViewXml = $@"<View><Query>
<Where>
	<Eq>
		<FieldRef LookupId='TRUE' Name='Team' />
		<Value Type='Lookup'>{teamId}</Value>
	</Eq>
</Where>
</Query>
	<ViewFields>
		<FieldRef Name='Title' />
		<FieldRef Name='Team' />
		<FieldRef Name='DateFrom' />
		<FieldRef Name='DateTo' />
		<FieldRef Name='HoursCapacity' />
		<FieldRef Name='Resource' />
	</ViewFields>
<QueryOptions /></View>"
			};

			_allocations = infoList.GetItems(query); //TODO:

			_context.Load(_allocations, uitems => uitems.Include(
				item => item["Title"],
				item => item["Team"],
				item => item["DateFrom"],
				item => item["DateTo"],
				item => item["Resource"],
				item => item["HoursCapacity"]));

			_context.ExecuteQuery();

			_toolkitAllocationsModel = new List<ToolkitAllocationResponseModel>();
			foreach (var item in _allocations)
			{
				var teamLookupName = item.FieldValues["Team"] == null ? string.Empty : ((Microsoft.SharePoint.Client.FieldLookupValue)item.FieldValues["Team"]).LookupValue;
				var teamLookupId = item.FieldValues["Team"] == null ? 0 : ((Microsoft.SharePoint.Client.FieldLookupValue)item.FieldValues["Team"]).LookupId;

				var resourceLookupName = item.FieldValues["Resource"] == null ? string.Empty : ((Microsoft.SharePoint.Client.FieldLookupValue)item.FieldValues["Resource"]).LookupValue;
				var resourceLookupId = item.FieldValues["Resource"] == null ? 0 : ((Microsoft.SharePoint.Client.FieldLookupValue)item.FieldValues["Resource"]).LookupId;

				DateTime? startDate = null;
				if (item.FieldValues["DateFrom"] != null)
					startDate = DateTime.Parse(item.FieldValues["DateFrom"].ToString()).Date;
				DateTime? dueDate = null;
				if (item.FieldValues["DateTo"] != null)
					dueDate = DateTime.Parse(item.FieldValues["DateTo"].ToString()).Date;

				_toolkitAllocationsModel.Add(new ToolkitAllocationResponseModel
				{
					Title = item.FieldValues["Title"].ToString(),
					DateFrom = startDate,
					DateTo = dueDate,
					HoursCapacity = decimal.Parse(item.FieldValues["HoursCapacity"].ToString()),
					Team = new ToolkitLookupModel
					{
						LookupId = teamLookupId,
						LookupValue = teamLookupName
					},
					Resource = new ToolkitLookupModel
					{
						LookupId = resourceLookupId,
						LookupValue = resourceLookupName
					}
				});
			}
		}

		private int GetUserId(string userDisplayName)
		{
			foreach (var u in _allocationadjustments)
			{
				if (u.DisplayName == userDisplayName)
				{
					return u.Id;
				}
			}
			return -1;
		}

		public bool CreateWorkPackages(List<WorkpackageModel> workpackageSelectedList)
		{
			// Assume that the web has a list named "Arbejdspakker".
			var workPackages = _context.Web.Lists.GetByTitle("Arbejdspakker");

			var titlesInCurrentWp = LoadExistingWorkpackages(workPackages);
			bool workPackagesCreated = false;

			//Create item workcase
			foreach (var wpSelected in workpackageSelectedList)
			{
				// If the workpackage exist already we do NOT want to create it. We check the existency by title.
				if (titlesInCurrentWp.Contains(wpSelected.Title) != false) continue;
				CreateWorkCaseListItem(workPackages, wpSelected);
				workPackagesCreated = true;
			}

			if (workPackagesCreated)
			{
				_context.ExecuteQuery();
			}

			return workPackagesCreated;
		}

		private void CreateWorkCaseListItem(List workPackages, WorkpackageModel wp)
		{
			ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
			ListItem newItem = workPackages.AddItem(itemCreateInfo);
			newItem[TitleField] = wp.Title;
			newItem[EstimateField] = wp.Estimate;
			DateTime dueDate = Convert.ToDateTime(wp.DueDate);
			newItem[DueDateField] = dueDate;

			// Add user
			FieldUserValue user = new FieldUserValue();
			var userId = GetUserId(wp.AssignedTo);
			if (userId != -1)
			{
				user.LookupId = userId; // Rikke Ottesen
				newItem[AssignedToField] = user;
			}

			if (wp.Team > 0)
			{
				var team = new FieldLookupValue { LookupId = wp.Team };
				newItem[TeamField] = team;
			}

			if (wp.RelatedCase > 0)
			{
				var relatedCase = new FieldLookupValue { LookupId = wp.RelatedCase };
				newItem[RelatedCaseField] = relatedCase;
			}

			// Always scrum for our workpackages
			var release = new FieldLookupValue { LookupId = 10 }; // Scrum = 10
			newItem[ReleaseField] = release;

			//var sprints = new[] { new FieldLookupValue { LookupId = 58 }, new FieldLookupValue { LookupId = 2 } };
			var sprints = new[] { new FieldLookupValue { LookupId = wp.Sprint } };
			newItem[SprintField] = sprints;

			//// https://goto.netcompany.com/cases/GTO561/UIB0006/Lists/Projektnumre/AllItems.aspx
			//if (wp.Application > 0)
			//{
			//	var application = new FieldLookupValue { LookupId = wp.Application };
			//	newItem[ProjektnummerField] = application;
			//}

			newItem.Update();
		}

		private List<string> LoadExistingWorkpackages(List workPackages)
		{
			List<string> titleList = new List<string>();
			ListItemCollection items = workPackages.GetItems(_query);
			_context.Load(items);
			_context.ExecuteQuery();
			foreach (var listItem in items)
			{
				titleList.Add(listItem["Title"].ToString());
			}

			return titleList;
		}

		public void GetReleases()
		{
			var query = new CamlQuery()
			{
				ViewXml = @"<View><Query><Where><Neq><FieldRef Name='status' /><Value Type='Text'>90 - Delivered</Value></Neq></Where></Query><ViewFields><FieldRef Name='Id' /><FieldRef Name='Title' /></ViewFields><QueryOptions /></View>"
			};

			_releases = _context.Web.Lists.GetByTitle("Releases").GetItems(query);

			_context.Load(_releases, uitems => uitems.Include(
				item => item.Id,
				item => item["Title"]));

			_context.ExecuteQuery();
		}

		public void GetIterations(string teamId)
		{
			var query = new CamlQuery()
			{
				ViewXml = $@"<View>
<Query>
   <Where>
      <Eq>
         <FieldRef Name='Teams' />
         <Value Type='LookupMulti'>TEAM READ</Value>
      </Eq>
   </Where>
   <OrderBy>
      <FieldRef Name='StartDate' Ascending='True' />
   </OrderBy>
</Query>
<ViewFields>
	<FieldRef Name='Id' />
	<FieldRef Name='Title' />
	<FieldRef Name='Teams' />
	<FieldRef Name='StartDate' />
	<FieldRef Name='EndDate' />
</ViewFields>
<QueryOptions />
</View>"
			};

			_iterations = _context.Web.Lists.GetByTitle("Iterations").GetItems(query);
			_context.Load(_iterations, uitems => uitems.Include(
				item => item.Id,
				item => item["Title"],
				item => item["Teams"],
				item => item["StartDate"],
				item => item["EndDate"]));

			_context.ExecuteQuery();

			_toolkitIterations = new List<ToolkitIterationModel>();
			foreach (var item in _iterations)
			{
				var teamLookupName = item.FieldValues["Teams"] == null ? string.Empty : ((Microsoft.SharePoint.Client.FieldLookupValue[])item.FieldValues["Teams"])[0].LookupValue;
				var teamLookupId = item.FieldValues["Teams"] == null ? 0 : ((Microsoft.SharePoint.Client.FieldLookupValue[])item.FieldValues["Teams"])[0].LookupId;

				DateTime startDate = DateTime.Parse(item.FieldValues["StartDate"].ToString()).Date;
				DateTime dueDate = DateTime.Parse(item.FieldValues["EndDate"].ToString()).Date;

				_toolkitIterations.Add(new ToolkitIterationModel
				{
					Id = item.Id,
					Title = item.FieldValues["Title"].ToString(),
					StartDate = startDate,
					EndDate = dueDate,
					Team = new ToolkitLookupModel
					{
						LookupId = teamLookupId,
						LookupValue = teamLookupName
					}
				});
			}
			_toolkitIterations = _toolkitIterations.OrderBy(t => t.StartDate).ToList();
		}
		public void GetFeatures(string releaseId)
		{
			var query = new CamlQuery()
			{
				ViewXml = $@"<View>
			<Query>
				<Where>
					<And>
						<And>
							<And>
								<Eq>
									<FieldRef LookupId='TRUE' Name='Release' />
									<Value Type='Lookup'>{releaseId}</Value>
								</Eq>
								<Neq>
									<FieldRef Name='Status' />
									<Value Type='Choice'>90 - Closed</Value>
								</Neq>
							</And>
							<And>
								<Neq>
									<FieldRef Name='Status' />
									<Value Type='Choice'>91 - Rejected</Value>
								</Neq>
								<Neq>
									<FieldRef Name='Status' />
									<Value Type='Choice'>92 - Duplicated</Value>
								</Neq>
							</And>
						</And>						
						<And>
							<Neq>
								<FieldRef Name='Status' />
								<Value Type='Choice'>93 - Cancelled</Value>
							</Neq>
							<Neq>
								<FieldRef Name='Status' />
								<Value Type='Choice'>20 - Not approved</Value>
							</Neq>
						</And>
					</And>
				</Where>
			</Query>
			<ViewFields>
				<FieldRef Name='Id' />
				<FieldRef Name='Title' />						
				<FieldRef Name='Team' />
				<FieldRef Name='Status' />
				<FieldRef Name='AssignedTo' />
				<FieldRef Name='DueDate' />
				<FieldRef Name='Release' />
<FieldRef Name='RemainingWork' />
			</ViewFields>					
			<QueryOptions />
			</View>"
			};

			//var title = "Cases";
			//_features = _context.Web.Lists.GetByTitle(title).GetItems(CamlQuery.CreateAllItemsQuery(10)); //TODO:
			//_context.Load(_features, uitems => uitems);
			//<RowLimit>10</RowLimit>

			_features = _context.Web.Lists.GetByTitle("Cases").GetItems(query); //TODO:
			_context.Load(_features, uitems => uitems.Include(
							item => item.Id,
							item => item["Title"],
							item => item["Team"],
							item => item["AssignedTo"],
							item => item["Status"],
							item => item["DueDate"],
							item => item["Release"],
							item => item["RemainingWork"]
							));

			_context.ExecuteQuery();
		}

		public void GetFeatures(string releaseId, string teamId)
		{
			var query = new CamlQuery()
			{
				ViewXml = $@"<View>
			<Query>
				<Where>
					<And>
						<And>
							<And>
								<Eq>
									<FieldRef LookupId='TRUE' Name='Release' />
									<Value Type='Lookup'>{releaseId}</Value>
								</Eq>
								<Neq>
									<FieldRef Name='Status' />
									<Value Type='Choice'>90 - Closed</Value>
								</Neq>
							</And>
							<And>
								<Neq>
									<FieldRef Name='Status' />
									<Value Type='Choice'>91 - Rejected</Value>
								</Neq>
								<Neq>
									<FieldRef Name='Status' />
									<Value Type='Choice'>92 - Duplicated</Value>
								</Neq>
							</And>
						</And>						
						<And>
							<And>
								<Neq>
									<FieldRef Name='Status' />
									<Value Type='Choice'>93 - Cancelled</Value>
								</Neq>
								<Eq>
									<FieldRef LookupId='TRUE' Name='Team' />
									<Value Type='Lookup'>{teamId}</Value>
								</Eq>
							</And>
							
								<Neq>
									<FieldRef Name='CaseType' />
									<Value Type='Choice'>Governance</Value>
								</Neq>
							
						</And>
					</And>
				</Where>
			</Query>
			<ViewFields>
				<FieldRef Name='Id' />
				<FieldRef Name='Title' />						
				<FieldRef Name='Team' />
				<FieldRef Name='Status' />
				<FieldRef Name='AssignedTo' />
				<FieldRef Name='DueDate' />
				<FieldRef Name='Release' />
<FieldRef Name='RemainingWork' />
			</ViewFields>					
			<QueryOptions />
			</View>"
			};

			_features = _context.Web.Lists.GetByTitle("Cases").GetItems(query); //TODO:
			_context.Load(_features, uitems => uitems.Include(
							item => item.Id,
							item => item["Title"],
							item => item["Team"],
							item => item["AssignedTo"],
							item => item["Status"],
							item => item["DueDate"],
							item => item["Release"],
							item => item["RemainingWork"]
							));

			_context.ExecuteQuery();

			_toolkitFeatures = new List<ToolkitFeatureResponseModel>();
			foreach (var item in _features)
			{
				var teamLookupName = item.FieldValues["Team"] == null ? string.Empty : ((Microsoft.SharePoint.Client.FieldLookupValue)item.FieldValues["Team"]).LookupValue;
				var teamLookupId = item.FieldValues["Team"] == null ? 0 : ((Microsoft.SharePoint.Client.FieldLookupValue)item.FieldValues["Team"]).LookupId;

				var releaseName = item.FieldValues["Release"] == null ? string.Empty : ((Microsoft.SharePoint.Client.FieldLookupValue)item.FieldValues["Release"]).LookupValue;
				var releaseLookupId = item.FieldValues["Release"] == null ? 0 : ((Microsoft.SharePoint.Client.FieldLookupValue)item.FieldValues["Release"]).LookupId;

				//var teamLookupName = item.FieldValues["Team"] == null ? string.Empty : ((Microsoft.SharePoint.Client.FieldLookupValue)item.FieldValues["Team"]).LookupValue;
				//var teamLookupId = item.FieldValues["Team"] == null ? 0 : ((Microsoft.SharePoint.Client.FieldLookupValue)item.FieldValues["Team"]).LookupId;

				DateTime? dueDate = null;
				if (item.FieldValues["DueDate"] != null)
					dueDate = DateTime.Parse(item.FieldValues["DueDate"].ToString()).Date;

				_toolkitFeatures.Add(new ToolkitFeatureResponseModel
				{
					Id = item.Id,
					Title = item.FieldValues["Title"].ToString(),
					Status = item.FieldValues["Status"].ToString(),
					RemainingWork = decimal.Parse(item.FieldValues["RemainingWork"].ToString()),
					DueDate = dueDate,
					Team = new ToolkitLookupModel
					{
						LookupId = teamLookupId,
						LookupValue = teamLookupName
					},
					Release = new ToolkitLookupModel
					{
						LookupId = releaseLookupId,
						LookupValue = releaseName
					}
				});
			}
			_toolkitIterations = _toolkitIterations.OrderBy(t => t.StartDate).ToList();
		}

		public void GetUserStories()
		{
			var featureIds = _features.Select(f => f.Id).ToList();
			var xmlText = new StringBuilder();
			xmlText.Append(@"<In><FieldRef LookupId='TRUE' Name='Case' /><Values>");

			foreach (var item in featureIds)
			{
				xmlText.AppendLine($"<Value Type='Lookup'>{item}</Value>");
			}

			xmlText.Append(@"</Values></In>");

			var query = new CamlQuery()
			{
				ViewXml = $@"<View>
							   <Query>
								  <Where>
										{xmlText}
								  </Where>
							   </Query>
							   <ViewFields>
								  <FieldRef Name='Id' />
								  <FieldRef Name='Title' />      
								  <FieldRef Name='Status' />
								  <FieldRef Name='Team' />
								  <FieldRef Name='Estimate' />
								  <FieldRef Name='Case' />
							   </ViewFields>
							   <QueryOptions />
							</View>"
			};

			var title = "User Stories";
			_us = _context.Web.Lists.GetByTitle(title).GetItems(query);
			_context.Load(_us, uitems => uitems.Include(
				item => item.Id,
				item => item["Title"],
				item => item["Team"],
				item => item["Estimate"],
				item => item["Case"],
				item => item["Status"]));

			_context.ExecuteQuery();
		}

		public void QuickTest()
		{
			try
			{
				var sharepointPage = _context.Web.Lists.GetByTitle("Work packages");
				ViewCollection collView = sharepointPage.Views;

				var view = sharepointPage.GetView(new Guid("BA1570B0-7778-4C08-923E-F0D9A5D0DFEC"));
				_context.Load(view, uitems => uitems);
				_context.ExecuteQuery();

				CamlQuery camlQuery = new CamlQuery();
				camlQuery.ViewXml = view.ViewQuery;
				var items = sharepointPage.GetItems(camlQuery);
				// Load data
				_context.Load(items, uitems => uitems);

				_context.ExecuteQuery();
			}
			catch (Exception ex)
			{

				throw;
			}
		}

		public void GetDataOfCustomView(string sharepointPageName, string viewGUID)
		{
			try
			{
				var sharepointPage = _context.Web.Lists.GetByTitle("Work packages");
				ViewCollection collView = sharepointPage.Views;

				//Load view
				var view = sharepointPage.GetView(new Guid("BA1570B0-7778-4C08-923E-F0D9A5D0DFEC"));
				_context.Load(view, uitems => uitems);
				_context.ExecuteQuery();

				//Load data of the view
				CamlQuery camlQuery = new CamlQuery();
				camlQuery.ViewXml = view.ViewQuery;
				var items = sharepointPage.GetItems(camlQuery);
				_context.Load(items, uitems => uitems);
				_context.ExecuteQuery();
			}
			catch (Exception ex)
			{

				throw;
			}
		}

		public void GetWorkpackages(string releaseId, string iterationId)
		{
			var usIds = _us.Select(f => f.Id).ToList();
			var xmlUSText = new StringBuilder();
			xmlUSText.Append(@"<In><FieldRef LookupId='TRUE' Name='FunctionalScenario' /><Values>");

			foreach (var item in usIds)
			{
				xmlUSText.AppendLine($"<Value Type='Lookup'>{item}</Value>");
			}

			xmlUSText.Append(@"</Values></In>");

			var xmlIterationText = new StringBuilder();
			if (!string.IsNullOrEmpty(iterationId) && iterationId != "0")
			{
				xmlIterationText.AppendLine($@"<AND>
										<Eq>
											<FieldRef LookupId='TRUE' Name='Iteration' />
											<Value Type='Lookup'>{iterationId}</Value>
										</Eq>
									 </AND>");
			}

			var query = new CamlQuery()
			{
				ViewXml = $@"<View>
							   <Query>
								  <Where>
									{xmlUSText}
									<AND>
									<Eq>
										<FieldRef LookupId='TRUE' Name='Release' />
										<Value Type='Lookup'>{releaseId}</Value>
									</Eq>
									</AND>
								    {xmlIterationText}
								  </Where>
							   </Query>
							   <ViewFields>
									<FieldRef Name='Id' />
									<FieldRef Name='Title' />      
									<FieldRef Name='Status' />
									<FieldRef Name='Team' />
									<FieldRef Name='Estimate' />
									<FieldRef Name='AssignedTo' />
									<FieldRef Name='StartDate' />									
									<FieldRef Name='DueDate' />
									<FieldRef Name='TimeSpent' />
									<FieldRef Name='WPType' />
									<FieldRef Name='RemainingWork' />
									<FieldRef Name='FunctionalScenario' />
									<FieldRef Name='Release' />
									<FieldRef Name='RelatedCase' />
									<FieldRef Name='Iteration' />
<FieldRef Name='Depend_x0020_on' />

							   </ViewFields>
							   <QueryOptions />
							</View>"
			};

			var title = "Work packages";
			_wps = _context.Web.Lists.GetByTitle(title).GetItems(query);
			_context.Load(_wps, uitems => uitems.Include(
				item => item.Id,
				item => item["Title"],
				item => item["Team"],
				item => item["Estimate"],
				item => item["AssignedTo"],
				item => item["Status"],
				item => item["TimeSpent"],
				item => item["WPType"],
				item => item["FunctionalScenario"],
				item => item["RemainingWork"],
				item => item["Iteration"],
				item => item["Release"],
				item => item["RelatedCase"],
				item => item["DueDate"],
				item => item["StartDate"],
				item => item["Depend_x0020_on"] //Depend on WP's IDs, split multiple values by ';'
				));

			_context.ExecuteQuery();
		}
		public void GetDefects() { }
		public void GetAllocations() { }

		public bool CreateUS(List<ToolkitUSModel> newUSs)
		{
			var title = "User Stories";
			var us = _context.Web.Lists.GetByTitle(title);
			var isSuccess = new UserStory.UserStory(_context).CreateUserStory(us, newUSs);
			return isSuccess;
		}

		public bool CreateWP(List<ToolkitWPModel> newWPs)
		{
			var title = "Work packages";
			var us = _context.Web.Lists.GetByTitle(title);
			var isSuccess = CreateWP(us, newWPs);
			return isSuccess;
		}

		public bool UpdateWP(List<ToolkitWPModel> updatedWPs)
		{
			var title = "Work packages";
			var us = _context.Web.Lists.GetByTitle(title);
			var isSuccess = UpdateWP(us, updatedWPs);
			return isSuccess;
		}

		public bool CreateWP(List toolkitItems, List<ToolkitWPModel> workpackageSelectedList)
		{
			try
			{
				foreach (var wpSelected in workpackageSelectedList)
				{
					CreateToolkitItemObject(toolkitItems, wpSelected);
				}
				_context.ExecuteQuery();

				return true;
			}
			catch (Exception ex)
			{

				throw;
			}
		}

		public bool UpdateWP(List toolkitItems, List<ToolkitWPModel> updatedWps)
		{
			try
			{
				foreach (var wpSelected in updatedWps)
				{
					UpdateToolkitWorkPackage(toolkitItems, wpSelected);
				}
				_context.ExecuteQuery();

				return true;
			}
			catch (Exception ex)
			{

				throw;
			}
		}

		private void UpdateToolkitWorkPackage(List toolkitItems, ToolkitWPModel wp)
		{
			try
			{
				var toolkitItem = toolkitItems.GetItemById(wp.Id);
				DateTime? dueDate = null;
				if (!string.IsNullOrEmpty(wp.DueDate))
					dueDate = Convert.ToDateTime(wp.DueDate).Date;

				DateTime? startDate = null;
				if (!string.IsNullOrEmpty(wp.StartDate))
					startDate = Convert.ToDateTime(wp.StartDate).Date;

				toolkitItem[StatusField] = wp.Status;
				toolkitItem[EstimateField] = wp.Estimate;
				toolkitItem[RemainingWorkField] = wp.RemainingWork.Contains(',')
					? wp.RemainingWork.Replace(',', '.')
					: wp.RemainingWork;
				//toolkitItem[WPTypeField] = wp.WPType;
				toolkitItem[DueDateField] = dueDate;
				//toolkitItem[ReleaseField] = new FieldLookupValue { LookupId = wp.Release };
				//toolkitItem[TeamField] = new FieldLookupValue { LookupId = wp.Team };
				toolkitItem[AssignedToField] = new FieldUserValue() { LookupId = wp.AssigneeId };
				toolkitItem[IterationField] = new FieldLookupValue() { LookupId = wp.IterationId ?? 0 };
				toolkitItem[StartDateField] = startDate;
				if (!string.IsNullOrEmpty(wp.DependOn))
					toolkitItem[DepenOnField] = new FieldLookupValue[] { new FieldLookupValue { LookupId = int.Parse(wp.DependOn) } };

				toolkitItem.Update();
			}
			catch (Exception ex)
			{

				throw;
			}
		}

		private void CreateToolkitItemObject(List toolkitItems, ToolkitWPModel wp)
		{
			ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
			ListItem newItem = toolkitItems.AddItem(itemCreateInfo);
			newItem[TitleField] = wp.Title;
			newItem[StatusField] = wp.Status;
			newItem[NoteField] = wp.Note;
			newItem[EstimateField] = wp.Estimate;
			newItem[RemainingWorkField] = wp.RemainingWork;
			newItem[WPTypeField] = wp.WPType;

			newItem[DueDateField] = Convert.ToDateTime(wp.DueDate);

			if (wp.Release > 0)
				newItem[ReleaseField] = new FieldLookupValue { LookupId = wp.Release };

			newItem[AssignedToField] = new FieldUserValue() { LookupId = 213 }; //213 is Viet Tran, 226 is Phat - https://goto.netcompany.com/cases/GTE747/NCDPP/_layouts/15/userdisp.aspx?ID=226

			if (wp.Team > 0)
			{
				newItem[TeamField] = new FieldLookupValue { LookupId = wp.Team };
			}

			if (wp.FunctionalScenario > 0)
			{
				newItem[FunctionalScenarioField] = new FieldLookupValue { LookupId = wp.FunctionalScenario };
			}

			newItem.Update();
		}

		public int GetUserStoryId(string usTitle, int featureId)
		{
			var xmlText = new StringBuilder();

			var query = new CamlQuery()
			{
				ViewXml = $@"<View>
							   <Query>
								   <Where>
									  <And>
										 <Eq>
											<FieldRef Name='Title' />
											<Value Type='Computed'>{usTitle}</Value>
										 </Eq>
										 <Eq>
											<FieldRef LookupId='TRUE' Name='Case' />
											<Value Type='Lookup'>{featureId}</Value>
										 </Eq>
									  </And>
								   </Where>
								   <OrderBy>
									  <FieldRef Name='Created' Ascending='False' />
								   </OrderBy>
								</Query>
							   <ViewFields>
								  <FieldRef Name='Id' />
								  <FieldRef Name='Title' />      
								  <FieldRef Name='Status' />
								  <FieldRef Name='Team' />
								  <FieldRef Name='Estimate' />
							   </ViewFields>
							   <RowLimit>1</RowLimit>
							   <QueryOptions />
							</View>"
			};

			var title = "User Stories";
			var selectedUs = _context.Web.Lists.GetByTitle(title).GetItems(query);
			_context.Load(selectedUs, uitems => uitems.Include(
				item => item.Id,
				item => item["Title"],
				item => item["Team"],
				item => item["Estimate"],
				item => item["Status"]));

			_context.ExecuteQuery();

			return selectedUs[0].Id;
		}

		public int GetFeatureReleaseIdById(int featureId)
		{
			var xmlText = new StringBuilder();

			var query = new CamlQuery()
			{
				ViewXml = $@"<View>
							   <Query>
								   <Where>
										<Eq>
											<FieldRef Name='ID' />
											<Value Type='Counter'>{featureId}</Value>
										</Eq>
									</Where>
								   <OrderBy>
									  <FieldRef Name='Created' Ascending='False' />
								   </OrderBy>
								</Query>
							   <ViewFields>
								  <FieldRef Name='Id' />
								  <FieldRef Name='Title' />      
								  <FieldRef Name='Status' />
								  <FieldRef Name='Team' />
								  <FieldRef Name='Estimate' />
									<FieldRef Name='Release' />
							   </ViewFields>
							   <RowLimit>1</RowLimit>
							   <QueryOptions />
							</View>"
			};

			var title = "Cases";
			var selectedUs = _context.Web.Lists.GetByTitle(title).GetItems(query);
			_context.Load(selectedUs, uitems => uitems.Include(
				item => item.Id,
				item => item["Title"],
				item => item["Team"],
				item => item["Estimate"],
				item => item["Release"],
				item => item["Status"]));

			_context.ExecuteQuery();

			return ((FieldLookupValue)(selectedUs[0].FieldValues["Release"])).LookupId;
		}
	}
}
