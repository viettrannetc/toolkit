using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using CreateWorkPackages3.Workpackages.Model;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Client;

namespace CreateWorkPackages3.UserStory
{
	class UserStory
	{
		private ClientContext _context;
		//public ListItemCollection _allocationadjustments;
		//public ListItemCollection _features;
		//public ListItemCollection _us;
		//public ListItemCollection _wps;
		//public ListItemCollection _defects;
		//public ListItemCollection _allocations;
		//public ListItemCollection _releases;
		//public ListItemCollection _iterations;

		private const string TitleField = "Title";
		private const string EstimateField = "Estimate";
		private const string DueDateField = "DueDate";
		private const string AssignedToField = "AssignedTo";
		private const string TeamField = "Team";
		private const string CaseField = "Case";
		private const string StatusField = "Status";
		private const string ReleaseField = "Release";
		private const string SprintField = "Sprint";
		private const string DescriptionField = "Description";

		public UserStory(ClientContext context)
		{
			_context = context;

		}

		//public void Connect(string username, string password)
		//{
		//	_context = new ClientContext("https://goto.netcompany.com/cases/GTE747/NCDPP");
		//	//GTO561/UIB0006
		//	//https://goto.netcompany.com/cases/GTO823/UIMDM
		//	ServicePointManager.ServerCertificateValidationCallback =
		//		((sender, certificate, chain, sslPolicyErrors) => true);

		//	string strUserName = username;
		//	string strPassword = password;
		//	NetworkCredential credentials = new NetworkCredential(strUserName, strPassword);
		//	_context.Credentials = credentials;
		//}

		//private int GetUserId(string userDisplayName)
		//{
		//	foreach (var u in _allocationadjustments)
		//	{
		//		if (u.DisplayName == userDisplayName)
		//		{
		//			return u.Id;
		//		}
		//	}
		//	return -1;
		//}

		public bool CreateUserStory(List toolkitItems, List<ToolkitUSModel> workpackageSelectedList)
		{
			// Assume that the web has a list named "Arbejdspakker".
			//var toolkitItems = _context.Web.Lists.GetByTitle("User Stories");
			//var titlesInCurrentWp = LoadExistingWorkpackages(toolkitItems);


			//Create item workcase
			try
			{
				foreach (var wpSelected in workpackageSelectedList)
				{
					// If the workpackage exist already we do NOT want to create it. We check the existency by title.
					// if (titlesInCurrentWp.Contains(wpSelected.Title) != false) continue;
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

		private void CreateToolkitItemObject(List toolkitItems, ToolkitUSModel wp)
		{
			ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
			ListItem newItem = toolkitItems.AddItem(itemCreateInfo);
			newItem[TitleField] = wp.Title;
			newItem[StatusField] = wp.Status;
			newItem[DescriptionField] = wp.Note;
			//newItem[DueDateField] = Convert.ToDateTime(wp.DueDate);

			//if (wp.Release > 0)
			//	newItem[ReleaseField] = new FieldLookupValue { LookupId = wp.Release };

			// Add user
			//var userId = GetUserId(wp.AssignedTo);
			//if (userId != -1)
			//{
			//	newItem[AssignedToField] = new FieldUserValue() { LookupId = userId };
			//}

			newItem[AssignedToField] = new FieldUserValue() { LookupId = 213 }; //213 is Viet Tran, 226 is Phat - https://goto.netcompany.com/cases/GTE747/NCDPP/_layouts/15/userdisp.aspx?ID=226

			if (wp.Team > 0)
			{
				newItem[TeamField] = new FieldLookupValue { LookupId = wp.Team };
			}

			//if (wp.RelatedCase > 0)
			//{
			//	var relatedCase = new FieldLookupValue { LookupId = wp.RelatedCase };
			//	newItem[RelatedCaseField] = relatedCase;
			//}

			if (wp.Case > 0)
			{
				newItem[CaseField] = new FieldLookupValue { LookupId = wp.Case };
			}

			//// Always scrum for our workpackages
			//var release = new FieldLookupValue { LookupId = 10 }; // Scrum = 10
			//newItem[ReleaseField] = release;

			////var sprints = new[] { new FieldLookupValue { LookupId = 58 }, new FieldLookupValue { LookupId = 2 } };
			//var sprints = new[] { new FieldLookupValue { LookupId = wp.Sprint } };
			//newItem[SprintField] = sprints;

			//// https://goto.netcompany.com/cases/GTO561/UIB0006/Lists/Projektnumre/AllItems.aspx
			//if (wp.Application > 0)
			//{
			//	var application = new FieldLookupValue { LookupId = wp.Application };
			//	newItem[ProjektnummerField] = application;
			//}

			newItem.Update();
		}

		//private List<string> LoadExistingWorkpackages(List toolkitItems)
		//{
		//	List<string> titleList = new List<string>();
		//	ListItemCollection items = toolkitItems.GetItems(_query);
		//	_context.Load(items);
		//	_context.ExecuteQuery();
		//	foreach (var listItem in items)
		//	{
		//		titleList.Add(listItem["Title"].ToString());
		//	}

		//	return titleList;
		//}
	}
}
