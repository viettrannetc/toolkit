using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLibrary.Ultilities;

namespace BusinessLibrary.Models.Planning
{
	public static class PlanningModelExtension
	{
		public static void ReleaseDate(this PlanningFeatureModel progresses)
		{
			try
			{
				DateTime today = DateTime.Today;
				int daysUntilMonday = ((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7) % 7;
				DateTime nextMonday = today.AddDays(daysUntilMonday);

				if (Constains.FE_Status_In_Phase3_Implementated.Contains(progresses.Status))
					progresses.ReleaseDate = nextMonday;
				else if (!progresses.UserStories.Any() || progresses.UserStories.Any(m => m.ReleaseDate == null))
					progresses.ReleaseDate = null;
				else
				{
					var implementedDate = progresses.UserStories.OrderByDescending(m => m.ReleaseDate).Select(m => m.ReleaseDate).First().Value;
					var releasedDate = implementedDate.Date.AddDays(1);

					progresses.ReleaseDate = Constains.FE_Status_In_Phase3_Implementated.Contains(progresses.Status) || releasedDate.Date.DayOfWeek == DayOfWeek.Saturday || releasedDate.Date.DayOfWeek == DayOfWeek.Sunday
						? nextMonday
						: releasedDate;
				}
			}
			catch (Exception ex)
			{

				throw;
			}
		}

		public static void ReleaseDate(this PlanningUserStoryModel progresses)
		{
			if (progresses.Wps.Any(m => m.ReleaseDate == null))
				progresses.ReleaseDate = null;
			else
				progresses.ReleaseDate = progresses.Wps.OrderByDescending(m => m.ReleaseDate).Select(m => m.ReleaseDate).FirstOrDefault();
		}

		public static void ReleaseDate(this PlanningWpModel progresses, PeopleAllocation memberResource)
		{
			if (progresses.Status == Constains.WP_Status_Blocked || memberResource == null)
				progresses.ReleaseDate = null;
			else
			{
				if (progresses.Status == Constains.WP_Status_ReadyForReview)
					progresses.ReleaseDate = DateTime.UtcNow;
				else
					progresses.ReleaseDate = memberResource.ScheduleForWorking.Where(s => s.WpId == progresses.Id).OrderByDescending(s => s.Date).FirstOrDefault()?.Date;
			}
		}

		public static T DeepCopy<T>(this T value)
		{
			string json = JsonConvert.SerializeObject(value);

			return JsonConvert.DeserializeObject<T>(json);
		}

		public static void Order(this List<PlanningFeatureModel> features)
		{
			for (int i = 0; i <= Constains.FE_Priority.Count() - 1; i++)
			{
				var selectedFeature = features.FirstOrDefault(f => f.Name == Constains.FE_Priority[i]);
				if (selectedFeature != null)
					selectedFeature.Order = i;
				else
					selectedFeature.Order = Constains.FE_Priority.Count() + 1;
			}
			features = features.OrderBy(f => f.Order).ThenByDescending(f => f.Status).ToList();
		}

		
	}

}