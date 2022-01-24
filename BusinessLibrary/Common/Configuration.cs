using System.Collections.Generic;

namespace BusinessLibrary.Common
{
	public static class Configuration
	{
		public static string Username = @"ncdmz\vitra";
		public static string Password = "XuanViet11";
		public static string Domain = @"https://goto.netcompany.com/cases/GTE747/NCDPP";

		public static string URL_Sharepoint_GetAllObjects = @"https://goto.netcompany.com/cases/GTE747/NCDPP/_api/web/lists?$select=Title";

		//https://goto.netcompany.com/cases/GTE747/NCDPP/_api/Web/Lists/GetByTitle('User Stories')/Items
		//https://goto.netcompany.com/cases/GTE747/NCDPP/_api/Web/Lists/GetByTitle('Functional Scenarios')/Items
		//https://social.technet.microsoft.com/wiki/contents/articles/35796.sharepoint-2013-using-rest-api-for-selecting-filtering-sorting-and-pagination-in-sharepoint-list.aspx
		//https://goto.netcompany.com/cases/GTE747/NCDPP/_api/Web/Lists/GetByTitle('defects')/Items?$filter=Modified gt datetime'2021-11-02T00:00:00Z'&$select=ID,Title,Priority,AssignedToId,Modified

		public const string SharepointObject_Feature = "Functional Scenarios";
		public const string SharepointObject_UserStory = "User Stories";
		public const string SharepointObject_WorkPackage = "Work packages";
		public const string SharepointObject_Defects = "Defects";
		public const string SharepointObject_TimeRegistration = "Time registration";
		public const string SharepointObject_Application = "Applications";
		public const string SharepointObject_Release = "Releases";
		public const string SharepointObject_Team = "Teams";
		public const string SharepointObject_Allocation = "Allocations";
		public const string SharepointObject_AllocationAdjustment = "Allocation adjustments";

		public static List<string> SharepointObjects = new List<string>() { SharepointObject_Feature,
																			SharepointObject_UserStory,
																			SharepointObject_WorkPackage,
																			SharepointObject_Defects,
																			SharepointObject_TimeRegistration,
																			SharepointObject_Application,
																			SharepointObject_Release,
																			SharepointObject_Team ,
																			SharepointObject_Allocation,
																			SharepointObject_AllocationAdjustment   };

		/// <summary>
		/// {0}: object name
		/// {1}: last time updated: 2021-11-02T00:00:00Z
		/// {2}: selected fields: ID,Title,Priority,AssignedToId,Modified
		/// </summary>
		public static string SharepointObject_Query_Format = @"https://goto.netcompany.com/cases/GTE747/NCDPP/_api/Web/Lists/GetByTitle('{0}')/Items?$filter=Modified gt datetime'{1}'&$select={2}";

		public static int _defaultReleaseId = 13;
		public static int _defaultTeamId = 32;
	}
}