using System.Collections.Generic;
namespace BusinessLibrary.Models.Sharepoint.Mit.dk
{
	public class SharepointExcelModel
	{
		public SharepointExcelModel()
		{
			SharepointResultTimeRegistrationModels = new List<SharepointResultTimeRegistrationModel>();
			SharepointResultFeatureModels = new List<SharepointResultFeatureModel>();
			SharepointResultUserStoryModels = new List<SharepointResultUserStoryModel>();
			SharepointResultDefectsModels = new List<SharepointResultDefectsModel>();
			SharepointResultApplicationModels = new List<SharepointResultApplicationModel>();
			SharepointResultReleaseModels = new List<SharepointResultReleaseModel>();
			SharepointResultAllocationModels = new List<SharepointResultAllocationModel>();
			SharepointResultAllocationAdjustmentModels = new List<SharepointResultAllocationAdjustmentModel>();
			SharepointResultWorkpackageModels = new List<SharepointResultWorkPackageModel>();
			SharepointResultTeamModels = new List<SharepointResultTeamModel>();
		}
		public List<SharepointResultTimeRegistrationModel> SharepointResultTimeRegistrationModels { get; set; }
		public List<SharepointResultFeatureModel> SharepointResultFeatureModels { get; set; }
		public List<SharepointResultUserStoryModel> SharepointResultUserStoryModels { get; set; }
		public List<SharepointResultWorkPackageModel> SharepointResultWorkpackageModels { get; set; }
		public List<SharepointResultDefectsModel> SharepointResultDefectsModels { get; set; }
		public List<SharepointResultApplicationModel> SharepointResultApplicationModels { get; set; }
		public List<SharepointResultReleaseModel> SharepointResultReleaseModels { get; set; }
		public List<SharepointResultAllocationModel> SharepointResultAllocationModels { get; set; }
		public List<SharepointResultAllocationAdjustmentModel> SharepointResultAllocationAdjustmentModels { get; set; }
		public List<SharepointResultTeamModel> SharepointResultTeamModels { get; set; }
		
	}
}
