//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using BusinessLibrary.Common;
//using BusinessLibrary.Models.Sharepoint.Mit.dk;
//using BusinessLibrary.Ultilities;

//namespace BusinessLibrary.Services
//{
//	public class SharepointService
//	{

//		private async Task<List<T>> PullNestyData<T>(string url) where T : class
//		{
//			var restService = new RestServiceSharepointHelper();
//			var result = new List<T>();
//			var allSharePointsObjects = await restService.Get<SharepointResponseModel<T>>(url);
//			result.AddRange(allSharePointsObjects.D.Results);

//			while (!string.IsNullOrWhiteSpace(allSharePointsObjects.D.__next))
//			{
//				allSharePointsObjects = await restService.Get<SharepointResponseModel<T>>(url);
//				result.AddRange(allSharePointsObjects.D.Results);
//			}

//			result = result.Distinct().ToList();

//			return result;
//		}

//		public async Task FetchData(DateTime? latestPulled)
//		{
//			var result = await PullData(latestPulled);

//			var excel = new Excel();
//			var tempFile = excel.CreateExcelFile().CreateWorksheets(result);

//			//foreach (var prop in result.GetType().GetProperties())
//			//{
//			//	var values = (IEnumerable<object>)prop.GetValue(result);
//			//	if (values == null || !values.Any()) continue;

//			//	var value = values.Cast<object>().ToList();
//			//	tempFile.AddWorkSheet(value);
//			//}

//			tempFile.Dispose();


//		}


//		public async Task UpdateData(SharepointExcelModel model)
//		{
//			//TODO: change new data to the current ones
//		}

//		public async Task ExportData(SharepointExcelModel model)
//		{
//			//TODO: Take data from database (excel file/ PostgreSQL) to the View
//		}





//		public async Task<SharepointExcelModel> PullData(DateTime? latestPulled)
//		{
//			if (latestPulled == null) latestPulled = new DateTime(2017, 1, 1);
//			var latestUpdated = latestPulled.Value.ToString("yyyy-MM-ddTHH:mm:ssZ");

//			var restService = new RestServiceSharepointHelper();
//			string allSharePointsObjectsUrl = $"https://goto.netcompany.com/cases/GTE747/NCDPP/_api/web/lists?$select=Title";

//			var allSharePointsObjects = await PullNestyData<SharepointResultObjectModel>(allSharePointsObjectsUrl);
//			var result = new SharepointExcelModel();

//			foreach (var sharepointObject in Configuration.SharepointObjects)
//			{
//				var selectedSharePointObject = allSharePointsObjects.FirstOrDefault(r => r.Title == sharepointObject);

//				if (selectedSharePointObject == null) continue;

//				var url = string.Empty;
//				switch (sharepointObject)
//				{
//					//url = string.Format(Configuration.SharepointObject_Query_Format,
//					//						Configuration.SharepointObject_Feature,
//					//						latestUpdated,
//					//						string.Join(",", (new SharepointResultFeatureModel()).GetType().GetProperties().Select(p => p.Name).ToList()));

//					case Configuration.SharepointObject_Feature:
//						url = $"https://goto.netcompany.com/cases/GTE747/NCDPP/_api/Web/Lists/GetByTitle('Functional Scenarios')/Items?$filter=Modified gt datetime'{latestUpdated}'&$select=Title,Id,Modified,Created,Status,WorkPackageEstimate,RemainingWork,ApplicationId,Estimate,DueDate,Priority,AssignedToStringId,AssignedToId,TeamId";
//						result.SharepointResultFeatureModels = await PullNestyData<SharepointResultFeatureModel>(url);
//						break;
//					case Configuration.SharepointObject_UserStory:
//						url = $"https://goto.netcompany.com/cases/GTE747/NCDPP/_api/Web/Lists/GetByTitle('User Stories')/Items?$filter=Modified gt datetime'{latestUpdated}'&$select=Title,Id,Modified,Created,Status,WorkPackageEstimate,RemainingWork,CaseId,Estimate,Priority,AssignedToStringId,TeamId";
//						result.SharepointResultUserStoryModels = await PullNestyData<SharepointResultUserStoryModel>(url);
//						break;
//					case Configuration.SharepointObject_WorkPackage:
//						url = $"https://goto.netcompany.com/cases/GTE747/NCDPP/_api/Web/Lists/GetByTitle('Work Packages')/Items?$filter=Modified gt datetime'{latestUpdated}'&$select=Title,Id,Modified,Created,Status,RemainingWork,FunctionalScenarioId,RelatedCaseId,Estimate,Priority,AssignedToStringId,TeamId,DueDate,TimeSpent,ReleaseId,WPType";
//						result.SharepointResultWorkpackageModels = await PullNestyData<SharepointResultWorkPackageModel>(url);
//						break;
//					case Configuration.SharepointObject_Defects:
//						url = $"https://goto.netcompany.com/cases/GTE747/NCDPP/_api/Web/Lists/GetByTitle('defects')/Items?$filter=Modified gt datetime'{latestUpdated}'&$select=Title,Id,Modified,Created,Status,Priority,AssignedToStringId,TeamId,DueDate,ReleaseId";
//						result.SharepointResultDefectsModels = await PullNestyData<SharepointResultDefectsModel>(url);
//						break;
//					case Configuration.SharepointObject_TimeRegistration:
//						url = $"https://goto.netcompany.com/cases/GTE747/NCDPP/_api/Web/Lists/GetByTitle('Time registration')/Items?$filter=Modified gt datetime'{latestUpdated}'&$select=Title,Id,Modified,Created,WorkPackageStatus,Hours,DoneById,DoneByStringId,DoneDate,Week,WorkPackageId,CaseId,Total";
//						result.SharepointResultTimeRegistrationModels = await PullNestyData<SharepointResultTimeRegistrationModel>(url);
//						break;
//					case Configuration.SharepointObject_Application:
//						url = $"https://goto.netcompany.com/cases/GTE747/NCDPP/_api/Web/Lists/GetByTitle('Applications')/Items?$filter=Modified gt datetime'{latestUpdated}'&$select=Title,Id,Modified,Created,Status";
//						result.SharepointResultApplicationModels = await PullNestyData<SharepointResultApplicationModel>(url);
//						break;
//					case Configuration.SharepointObject_Release:
//						url = $"https://goto.netcompany.com/cases/GTE747/NCDPP/_api/Web/Lists/GetByTitle('Releases')/Items?$filter=Modified gt datetime'{latestUpdated}'&$select=Title,Id,Modified,Created";
//						result.SharepointResultReleaseModels = await PullNestyData<SharepointResultReleaseModel>(url);
//						break;
//					case Configuration.SharepointObject_Team:
//						url = $"https://goto.netcompany.com/cases/GTE747/NCDPP/_api/Web/Lists/GetByTitle('Teams')/Items?$filter=Modified gt datetime'{latestUpdated}'&$select=Title,Id,Modified,Created";
//						result.SharepointResultTeamModels = await PullNestyData<SharepointResultTeamModel>(url);
//						break;
//					case Configuration.SharepointObject_Allocation:
//						url = $"https://goto.netcompany.com/cases/GTE747/NCDPP/_api/Web/Lists/GetByTitle('Allocations')/Items?$filter=Modified gt datetime'{latestUpdated}'&$select=Title,Id,Modified,Created,ResourceId,ResourceStringId,DateFrom,DateTo,HoursCapacity";
//						result.SharepointResultAllocationModels = await PullNestyData<SharepointResultAllocationModel>(url);
//						break;
//					case Configuration.SharepointObject_AllocationAdjustment:
//						url = $"https://goto.netcompany.com/cases/GTE747/NCDPP/_api/Web/Lists/GetByTitle('Allocation Adjustments')/Items?$filter=Modified gt datetime'{latestUpdated}'&$select=Title,Id,Modified,Created,ResourceId,ResourceStringId,DateFrom,DateTo,HoursCapacity";
//						result.SharepointResultAllocationAdjustmentModels = await PullNestyData<SharepointResultAllocationAdjustmentModel>(url);
//						break;
//				}

//			}

//			return result;
//		}
//	}
//}
