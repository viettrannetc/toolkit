using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using BusinessLibrary.Models;
using BusinessLibrary.Models.Planning;
using BusinessLibrary.Services.Store;
using CreateWorkPackages3.Utilities;
using CreateWorkPackages3.Workpackages.Model;
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
		public Dictionary<int, List<WPItemModel>> _wpHistories = new Dictionary<int, List<WPItemModel>>();
		public ListItemCollection _defects;
		public ListItemCollection _allocations;
		public List<ToolkitAllocationResponseModel> _toolkitAllocationsModel;
		public ListItemCollection _releases;
		public ListItemCollection _iterations;
		public List<ToolkitIterationModel> _toolkitIterations;

		/// <summary>
		/// latest data from toolkit
		/// </summary>
		public List<WPItemModel> _currentWPLocalData = new List<WPItemModel>();

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

		private const string _column_Feature = "Feature";
		private const string _column_WpType = "WPType";
		private const string _column_Status = "Status";
		private const string _column_Start = "Start";
		private const string _column_Assignee = "Assignee";
		private const string _column_Estimate = "Estimate";
		private const string _column_Remaining = "Remaining";
		private const string _column_DependOn = "DependOn";
		private const string _column_Spent = "Spent";
		private const string _column_DueDate = "DueDate";
		private const string _column_Iteration = "Iteration";

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


		public void GetAllocationAdjustmentsByTeam(string teamId)
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
				var teamLookupName = item.FieldValues["Team"] == null ? string.Empty : ((FieldLookupValue)item.FieldValues["Team"]).LookupValue;
				var teamLookupId = item.FieldValues["Team"] == null ? 0 : ((FieldLookupValue)item.FieldValues["Team"]).LookupId;

				var resourceLookupName = item.FieldValues["Resource"] == null ? string.Empty : ((FieldLookupValue)item.FieldValues["Resource"]).LookupValue;
				var resourceLookupId = item.FieldValues["Resource"] == null ? 0 : ((FieldLookupValue)item.FieldValues["Resource"]).LookupId;

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

		public void GetAllocationByTeam(string teamId)
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
				var teamLookupName = item.FieldValues["Team"] == null ? string.Empty : ((FieldLookupValue)item.FieldValues["Team"]).LookupValue;
				var teamLookupId = item.FieldValues["Team"] == null ? 0 : ((FieldLookupValue)item.FieldValues["Team"]).LookupId;

				var resourceLookupName = item.FieldValues["Resource"] == null ? string.Empty : ((FieldLookupValue)item.FieldValues["Resource"]).LookupValue;
				var resourceLookupId = item.FieldValues["Resource"] == null ? 0 : ((FieldLookupValue)item.FieldValues["Resource"]).LookupId;

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
				var teamLookupName = item.FieldValues["Teams"] == null ? string.Empty : ((FieldLookupValue[])item.FieldValues["Teams"])[0].LookupValue;
				var teamLookupId = item.FieldValues["Teams"] == null ? 0 : ((FieldLookupValue[])item.FieldValues["Teams"])[0].LookupId;

				DateTime startDate = DateTime.Parse(item.FieldValues["StartDate"].ToString()).Date;
				DateTime dueDate = DateTime.Parse(item.FieldValues["EndDate"].ToString()).Date;
				if (teamLookupId != 32) continue;
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

		public void GetFeaturesByRelease(string releaseId)
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

		public void GetFeaturesByTeam(string teamId)
		{
			var query = new CamlQuery()
			{
				ViewXml = $@"<View>
			<Query>
				<Where>
					<And>
						<And>
							<Neq>
								<FieldRef Name='Status' />
								<Value Type='Choice'>90 - Closed</Value>
							</Neq>															
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
				var teamLookupName = item.FieldValues["Team"] == null ? string.Empty : ((FieldLookupValue)item.FieldValues["Team"]).LookupValue;
				var teamLookupId = item.FieldValues["Team"] == null ? 0 : ((FieldLookupValue)item.FieldValues["Team"]).LookupId;

				var releaseName = item.FieldValues["Release"] == null ? string.Empty : ((FieldLookupValue)item.FieldValues["Release"]).LookupValue;
				var releaseLookupId = item.FieldValues["Release"] == null ? 0 : ((FieldLookupValue)item.FieldValues["Release"]).LookupId;

				//var teamLookupName = item.FieldValues["Team"] == null ? string.Empty : ((FieldLookupValue)item.FieldValues["Team"]).LookupValue;
				//var teamLookupId = item.FieldValues["Team"] == null ? 0 : ((FieldLookupValue)item.FieldValues["Team"]).LookupId;

				DateTime? dueDate = null;
				if (item.FieldValues["DueDate"] != null)
					dueDate = DateTime.Parse(item.FieldValues["DueDate"].ToString()).Date;

				_toolkitFeatures.Add(new ToolkitFeatureResponseModel
				{
					Id = item.Id,
					Title = item.FieldValues["Title"].ToString(),
					Status = item.FieldValues["Status"].ToString(),
					RemainingWork = item.FieldValues["RemainingWork"] == null ? 0 : decimal.Parse(item.FieldValues["RemainingWork"].ToString()),
					DueDate = dueDate ?? null,
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
			_toolkitFeatures = _toolkitFeatures.OrderBy(t => t.Release.LookupId).ThenBy(t => t.Title).ToList();
		}

		public void GetUserStoriesByFeatureIds()
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

		public void GetWorkpackagesByUsIds()
		{
			var usIds = _us.Select(f => f.Id).ToList();
			var featureIds = _features.Select(f => f.Id).ToList();

			var xmlUSText = new StringBuilder();
			//xmlUSText.Append(@"<In><FieldRef LookupId='TRUE' Name='RelatedCase' /><Values>");
			//foreach (var item in featureIds)
			//{
			//	xmlUSText.AppendLine($"<Value Type='Lookup'>{item}</Value>");
			//}

			//xmlUSText.Append(@"<In><FieldRef LookupId='TRUE' Name='FunctionalScenario' /><Values>");
			xmlUSText.Append(@"<In><FieldRef Name='FunctionalScenario' /><Values>");
			foreach (var item in usIds)
			{
				xmlUSText.AppendLine($"<Value Type='Lookup'>{item}</Value>");
			}

			xmlUSText.Append(@"</Values></In>");

			//var xmlReleaseText = new StringBuilder();
			//if (!string.IsNullOrEmpty(releaseId) && releaseId != "0")
			//{
			//	xmlReleaseText.AppendLine($@"<AND>
			//						<Eq>
			//							<FieldRef LookupId='TRUE' Name='Release' />
			//							<Value Type='Lookup'>{releaseId}</Value>
			//						</Eq>
			//						</AND>");
			//	//xmlReleaseText.AppendLine($@"<Eq>
			//	//						<FieldRef LookupId='TRUE' Name='Release' />
			//	//						<Value Type='Lookup'>{releaseId}</Value>
			//	//					</Eq>");
			//}

			//xmlUSText.Append(@"</Values></In>");


			//var xmlWhereCondition = new StringBuilder();
			//xmlWhereCondition.AppendLine("<Where>");
			//if (xmlReleaseText != null)
			//{
			//	if (xmlReleaseText != null)
			//	{
			//		xmlWhereCondition.AppendLine("<AND>");
			//		xmlWhereCondition.AppendLine(xmlUSText.ToString());
			//		xmlWhereCondition.AppendLine(xmlReleaseText.ToString());
			//		xmlWhereCondition.AppendLine("</AND>");
			//	}
			//	else
			//	{
			//		xmlWhereCondition.AppendLine(xmlReleaseText.ToString());
			//	}
			//}
			//xmlWhereCondition.AppendLine("</Where>");

			var query = new CamlQuery()
			{
				ViewXml = $@"<View>
							   <Query><Where>
								  {xmlUSText}
							   </Where></Query>
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
									<FieldRef Name='_UIVersionString' />
							   </ViewFields>
							   <QueryOptions />
							</View>"
			};

			var title = "Work packages";

			//_wps = _context.Web.Lists.GetByTitle(title).GetItems(_query);
			//_context.Load(_wps, uitems => uitems);
			//_context.ExecuteQuery();

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
				item => item["Depend_x0020_on"], //Depend on WP's IDs, split multiple values by ';'
				item => item["_UIVersionString"]
				));

			_context.ExecuteQuery();

			SyncWorkpackageHistoryToLocal();

		}

		private void SyncWorkpackageHistoryToLocal()
		{
			try
			{
				_wpHistories = WorkpackageGetBackupHistory();
				var newChanges = new List<WPItemModel>();
				foreach (var wp in _wps)
				{
					if (_wpHistories.Keys.Contains(wp.Id)) //perhaps only add newer versions
					{
						var latestVersionFromDatabase = _wpHistories[wp.Id].OrderByDescending(v => v.Version).First().Version;
						if (latestVersionFromDatabase < int.Parse(wp.FieldValues["_UIVersionString"].ToString().Split('.')[0])) //add newer versions
						{
							var newHistories = GetWorkpackagesByWorkpackageVersionId(wp, latestVersionFromDatabase);
							if (!_wpHistories.Keys.Any(k => k == wp.Id))
							{
								_wpHistories.Add(wp.Id, newHistories);
								newChanges.AddRange(newHistories);
							}
							else
							{
								var existingVersions = _wpHistories[wp.Id].Select(v => v.Version).ToList();
								var newVersions = newHistories.Where(nh => !existingVersions.Contains(nh.Version)).ToList();
								_wpHistories[wp.Id].AddRange(newVersions);
								newChanges.AddRange(newVersions);
							}
						}
					}
					else //add to database 
					{
						var newHistories = GetWorkpackagesByWorkpackageVersionId(wp, 0);
						if (!_wpHistories.Keys.Any(k => k == wp.Id))
						{
							_wpHistories.Add(wp.Id, newHistories);
							newChanges.AddRange(newHistories);
						}
					}
				}

				if (newChanges.Any())
					WorkpackageSaveBackupHistory(newChanges);
			}
			catch (Exception ex)
			{

				throw;
			}
		}

		public List<WPItemModel> GetWorkpackagesByWorkpackageVersionId(ListItem history, int latestVersionId)
		{
			var historyItems = history.Versions;
			_context.Load(historyItems, uitems => uitems.Include(
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
				item => item["Depend_x0020_on"], //Depend on WP's IDs, split multiple values by ';'
				item => item["_UIVersionString"],
				item => item["Modified"]
				));
			_context.ExecuteQuery();

			var result = ConverWorkpackpageModel(null, null, history.Id, historyItems);
			return result;
		}

		public void WorkpackageSaveBackupHistory(List<WPItemModel> newChanges)
		{
			var csv = new CSV();
			string filename = $@"{Environment.CurrentDirectory}\..\..\Workpackages\History\workpackage.csv";
			csv.Write(filename, newChanges);
		}

		public Dictionary<int, List<WPItemModel>> WorkpackageGetBackupHistory()
		{
			var csv = new CSV();
			string filename = $@"{Environment.CurrentDirectory}\..\..\Workpackages\History\workpackage.csv";
			var result = csv.Get<WPItemModel>(filename);

			var t1 = result.GroupBy(u => u.WPId).ToDictionary(g => g.Key, g => g.ToList());

			return t1;
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

				if (!string.IsNullOrEmpty(wp.Status))
					toolkitItem[StatusField] = wp.Status;

				if (!string.IsNullOrEmpty(wp.Estimate))
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

		public List<WPItemModel> BuildWorkpackpageModel(ListItemCollection features, ListItemCollection userStories, ListItemCollection workpackages)
		{
			var result = new List<WPItemModel>();
			foreach (var fe in features)
			{
				var selectedUSs = userStories.Where(u => u.FieldValues["Case"] != null && ((FieldLookupValue)u.FieldValues["Case"]).LookupId == fe.Id).ToList();
				foreach (var us in selectedUSs)
				{
					var selectedWPs = workpackages.Where(u => u.FieldValues["FunctionalScenario"] != null && ((FieldLookupValue)u.FieldValues["FunctionalScenario"]).LookupId == us.Id).ToList();
					foreach (var wp in selectedWPs)
					{
						var assignee = wp.FieldValues["AssignedTo"] == null ? string.Empty : ((FieldLookupValue)wp.FieldValues["AssignedTo"]).LookupValue;
						var team = wp.FieldValues["Team"] == null ? string.Empty : ((FieldLookupValue)wp.FieldValues["Team"]).LookupValue;

						DateTime? startDate = null;
						if (wp.FieldValues["StartDate"] != null)
							startDate = DateTime.Parse(wp.FieldValues["StartDate"].ToString()).Date;
						DateTime? dueDate = null;
						if (wp.FieldValues["DueDate"] != null)
							dueDate = DateTime.Parse(wp.FieldValues["DueDate"].ToString()).Date;

						var iterationId = wp.FieldValues["Iteration"] == null ? string.Empty : ((FieldLookupValue)wp.FieldValues["Iteration"]).LookupId.ToString();
						var iterationName = wp.FieldValues["Iteration"] == null ? string.Empty : ((FieldLookupValue)wp.FieldValues["Iteration"]).LookupValue;

						var dependOnWPIds = wp.FieldValues["Depend_x0020_on"] != null && (wp.FieldValues["Depend_x0020_on"] as FieldLookupValue[]).Count() > 0
					? (wp.FieldValues["Depend_x0020_on"] as FieldLookupValue[])[0].LookupValue.Contains(";")
						? (wp.FieldValues["Depend_x0020_on"] as FieldLookupValue[])[0].LookupValue.Split(';')[0]
						: (wp.FieldValues["Depend_x0020_on"] as FieldLookupValue[])[0].LookupValue
					: string.Empty;

						result.Add(new WPItemModel()
						{
							FeatureId = fe.Id,
							Feature = fe.FieldValues["Title"].ToString(),
							USId = us.Id,
							USTitle = us.FieldValues["Title"].ToString(),
							WPAssignee = assignee,
							WPStart = startDate,
							WPDueDate = dueDate,
							WPEstimate = wp.FieldValues[_column_Estimate] == null ? string.Empty : wp.FieldValues[_column_Estimate].ToString(),
							WPId = wp.Id,
							WPRemainingHour = wp.FieldValues["RemainingWork"] == null ? string.Empty : wp.FieldValues["RemainingWork"].ToString(),
							WPSpentHour = wp.FieldValues["TimeSpent"] == null ? string.Empty : wp.FieldValues["TimeSpent"].ToString(),
							WPStatus = wp.FieldValues[_column_Status] == null ? string.Empty : wp.FieldValues[_column_Status].ToString(),
							WPTeam = team,
							WPTitle = wp.FieldValues["Title"] == null ? string.Empty : wp.FieldValues["Title"].ToString(),
							WPType = wp.FieldValues[_column_WpType] == null ? string.Empty : wp.FieldValues[_column_WpType].ToString(),
							WPIterationId = iterationId,
							WPIterationName = iterationName,
							WPDependOn = dependOnWPIds,
							Version = wp.FieldValues["_UIVersionString"] == null ? 0 : int.Parse(wp.FieldValues["_UIVersionString"].ToString().Split('.')[0]),
							VersionDate = DateTime.Parse(wp.FieldValues["Modified"].ToString()).Date
						});
					}
				}
			}

			result.SortPriority();
			result = result
				.OrderBy(d => d.FeatureShow)
				.ThenBy(d => d.WPPriority)
				.ToList();

			_currentWPLocalData = result;
			return result;
		}

		public List<WPItemModel> ConverWorkpackpageModel(ListItem fe, ListItem us, int workPackageId, ListItemVersionCollection workpackages)
		{
			var result = new List<WPItemModel>();
			foreach (var wp in workpackages)
			{
				var assignee = wp.FieldValues["AssignedTo"] == null ? string.Empty : ((FieldLookupValue)wp.FieldValues["AssignedTo"]).LookupValue;
				var team = wp.FieldValues["Team"] == null ? string.Empty : ((FieldLookupValue)wp.FieldValues["Team"]).LookupValue;

				DateTime? startDate = null;
				if (wp.FieldValues["StartDate"] != null)
					startDate = DateTime.Parse(wp.FieldValues["StartDate"].ToString()).Date;
				DateTime? dueDate = null;
				if (wp.FieldValues["DueDate"] != null)
					dueDate = DateTime.Parse(wp.FieldValues["DueDate"].ToString()).Date;

				var iterationId = wp.FieldValues["Iteration"] == null ? string.Empty : ((FieldLookupValue)wp.FieldValues["Iteration"]).LookupId.ToString();
				var iterationName = wp.FieldValues["Iteration"] == null ? string.Empty : ((FieldLookupValue)wp.FieldValues["Iteration"]).LookupValue;

				var dependOnWPIds = wp.FieldValues["Depend_x0020_on"] != null
									&& (wp.FieldValues["Depend_x0020_on"] as FieldLookupValue[]).Count() > 0
										? (wp.FieldValues["Depend_x0020_on"] as FieldLookupValue[])[0].LookupValue.Contains(";")
											? (wp.FieldValues["Depend_x0020_on"] as FieldLookupValue[])[0].LookupValue.Split(';')[0]
											: (wp.FieldValues["Depend_x0020_on"] as FieldLookupValue[])[0].LookupValue
										: string.Empty;

				result.Add(new WPItemModel()
				{
					FeatureId = fe == null
						? ((FieldLookupValue)wp.FieldValues["RelatedCase"]).LookupId
						: fe.Id,
					Feature = fe == null
						? ((FieldLookupValue)wp.FieldValues["RelatedCase"]).LookupValue
						: fe.FieldValues["Title"].ToString(),
					USId = us == null
						? wp.FieldValues["FunctionalScenario"] == null
							? 0
							: ((FieldLookupValue)wp.FieldValues["FunctionalScenario"]).LookupId
						: us.Id,
					USTitle = us == null
						? wp.FieldValues["FunctionalScenario"] == null
							? ""
							: ((FieldLookupValue)wp.FieldValues["FunctionalScenario"]).LookupValue
						: us.FieldValues["Title"].ToString(),
					WPAssignee = assignee,
					WPStart = startDate,
					WPDueDate = dueDate,
					WPEstimate = wp.FieldValues[_column_Estimate] == null ? string.Empty : wp.FieldValues[_column_Estimate].ToString(),
					WPId = workPackageId,
					WPRemainingHour = wp.FieldValues["RemainingWork"] == null ? string.Empty : wp.FieldValues["RemainingWork"].ToString(),
					WPSpentHour = wp.FieldValues["TimeSpent"] == null ? string.Empty : wp.FieldValues["TimeSpent"].ToString(),
					WPStatus = wp.FieldValues[_column_Status] == null ? string.Empty : wp.FieldValues[_column_Status].ToString(),
					WPTeam = team,
					WPTitle = wp.FieldValues["Title"] == null ? string.Empty : wp.FieldValues["Title"].ToString(),
					WPType = wp.FieldValues[_column_WpType] == null ? string.Empty : wp.FieldValues[_column_WpType].ToString(),
					WPIterationId = iterationId,
					WPIterationName = iterationName,
					WPDependOn = dependOnWPIds,
					Version = wp.FieldValues["_UIVersionString"] == null ? 0 : int.Parse(wp.FieldValues["_UIVersionString"].ToString().Split('.')[0]),
					VersionDate = DateTime.Parse(wp.FieldValues["Modified"].ToString()).Date
				});
			}

			result = result
				.OrderBy(d => d.FeatureShow)
				.ToList();

			return result;
		}
	}
}
