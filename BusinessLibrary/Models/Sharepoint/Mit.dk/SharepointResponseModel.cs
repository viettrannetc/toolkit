using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLibrary.Models.Sharepoint.Mit.dk
{
	public class SharepointResponseModel<T> where T : class
	{
		public SharepointResponseModel()
		{
			D = new SharepointResultModel<T>();
		}
		public SharepointResultModel<T> D { get; set; }
	}

	public class SharepointResultModel<T>
	{
		public SharepointResultModel()
		{
			Results = new List<T>();
		}
		public List<T> Results { get; set; }
		public string __next { get; set; }
	}

	public class SharepointResultObjectModel
	{
		public SharepointResultObjectMetadataModel __metadata { get; set; }
		/// <summary>
		/// Used by Feature, Defects
		/// </summary>
		public string Title { get; set; }

		public string Id { get; set; }
		/// <summary>
		/// can use this field to do the filter to looking for changes
		/// </summary>
		public string Modified { get; set; }
		/// <summary>
		/// can use this field to do the filter to looking for changes
		/// </summary>
		public string Created { get; set; }
		public string TeamId { get; set; }
		public string Status { get; set; }
		public string ReleaseId { get; set; }
	}

	public class SharepointResultTimeRegistrationModel : SharepointResultObjectModel
	{
		public string WorkPackageStatus { get; set; }
		public decimal Hours { get; set; }
		public string DoneById { get; set; }
		public string DoneByStringId { get; set; }
		public DateTime? DoneDate { get; set; }
		public string Week { get; set; }
		public string WorkPackageId { get; set; }
		public string CaseId { get; set; }
	}

	public class SharepointResultFeatureModel : SharepointResultObjectModel
	{
		public string Priority { get; set; }
		public string AssignedToStringId { get; set; }
		public string AssignedToId { get; set; }
		public string ApplicationId { get; set; }
		public decimal RemainingWork { get; set; }
		public decimal WorkPackageEstimate { get; set; }
		/// <summary>
		/// Budget
		/// </summary>
		public decimal Estimate { get; set; }
		public DateTime? DueDate { get; set; }
	}

	public class SharepointResultUserStoryModel : SharepointResultObjectModel
	{
		public string Priority { get; set; }
		public string AssignedToStringId { get; set; }
		public string AssignedToId { get; set; }
		public string ApplicationId { get; set; }
		/// <summary>
		/// This is Feature Id from US's view
		/// </summary>
		public string CaseId { get; set; }
	}

	public class SharepointResultWorkPackageModel : SharepointResultObjectModel
	{
		public string Priority { get; set; }
		public string AssignedToStringId { get; set; }
		public string AssignedToId { get; set; }
		public string ApplicationId { get; set; }
		public decimal RemainingWork { get; set; }
		public decimal Estimate { get; set; }
		public decimal TimeSpent { get; set; }
		/// <summary>
		/// This is Feature Id from WP's view
		/// </summary>
		public string FunctionalScenarioId { get; set; }
		/// <summary>
		/// This is US Id from WP's view
		/// </summary>
		public string RelatedCaseId { get; set; }
		public DateTime? DueDate { get; set; }
		public string WPType { get; set; }
		public SharepointResultDependingOnModel Depend_x0020_onId { get; set; }
		public SharepointResultDependingOnModel Depend_x0020_on_x0020_Defect_x00Id { get; set; }		
	}



	public class SharepointResultDefectsModel : SharepointResultObjectModel
	{
		public string Priority { get; set; }
		public string AssignedToStringId { get; set; }
		public string AssignedToId { get; set; }
		public string ApplicationId { get; set; }
		public DateTime? DueDate { get; set; }
	}

	public class SharepointResultApplicationModel : SharepointResultObjectModel
	{
	}

	public class SharepointResultReleaseModel : SharepointResultObjectModel
	{
	}

	public class SharepointResultTeamModel : SharepointResultObjectModel
	{
	}

	public class SharepointResultAllocationModel : SharepointResultObjectModel
	{
		public string ResourceId { get; set; }
		public string ResourceStringId { get; set; }
		public DateTime DateFrom { get; set; }
		public DateTime DateTo { get; set; }
		public decimal HoursCapacity { get; set; }
	}

	public class SharepointResultAllocationAdjustmentModel : SharepointResultAllocationModel
	{
	}

	public class SharepointResultObjectMetadataModel
	{
		public string Id { get; set; }
		public string Uri { get; set; }
		public string Etag { get; set; }
		public string Type { get; set; }
	}

	public class SharepointResultDependingOnModel
	{
		public SharepointResultObjectMetadataModel __metadata { get; set; }
		public dynamic[] Results { get; set; }
	}

	public class SharepointResultDependingOnDefectModel : SharepointResultDependingOnModel
	{ }
}
