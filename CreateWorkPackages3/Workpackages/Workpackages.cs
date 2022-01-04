using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using CreateWorkPackages3.Workpackages.Model;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Client;

namespace CreateWorkPackages3.Workpackages
{
	class Workpackages
	{
		private ClientContext _context;
		CamlQuery _query = CamlQuery.CreateAllItemsQuery(10000);
		public ListItemCollection _allocationadjustments;
		public ListItemCollection _features;
		public ListItemCollection _us;
		public ListItemCollection _wps;
		public ListItemCollection _defects;
		public ListItemCollection _allocations;
		public ListItemCollection _releases;
		public ListItemCollection _iterations;

		private const string TitleField = "Title";
		private const string EstimateField = "Estimate";
		private const string DueDateField = "DueDate";
		private const string AssignedToField = "AssignedTo";
		private const string TeamField = "Team";
		private const string RelatedCaseField = "RelatedCase";
		private const string ReleaseField = "Release";
		private const string SprintField = "Sprint";
		private const string ProjektnummerField = "Projektnummer";

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

			// https://goto.netcompany.com/cases/GTO561/UIB0006/Lists/Projektnumre/AllItems.aspx
			if (wp.Application > 0)
			{
				var application = new FieldLookupValue { LookupId = wp.Application };
				newItem[ProjektnummerField] = application;
			}

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

		public void GetIterations()
		{
			var query = new CamlQuery()
			{
				ViewXml = @"<View><ViewFields><FieldRef Name='Title' /></ViewFields><QueryOptions /></View>"
			};

			_iterations = _context.Web.Lists.GetByTitle("Iterations").GetItems(query);

			_context.Load(_iterations, uitems => uitems.Include(
				item => item.Id,
				item => item["Title"],
				item => item.DisplayName));

			_context.ExecuteQuery();
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
							item => item["Release"]));

			_context.ExecuteQuery();
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
				item => item["Status"]));

			_context.ExecuteQuery();
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
									<FieldRef Name='DueDate' />
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
				item => item["DueDate"]));

			_context.ExecuteQuery();
		}
		public void GetDefects() { }
		public void GetAllocations() { }

		public void GetDailyTasks(string releaseId, string iterationId)
		{
			GetFeatures(releaseId);
		}

		public bool CreateUS(List<ToolkitUSModel> newUSs)
		{
			var title = "User Stories";
			var us = _context.Web.Lists.GetByTitle(title);
			var isSuccess = new UserStory.UserStory(_context).CreateUserStory(us, newUSs);
			return isSuccess;
		}
	}
}
