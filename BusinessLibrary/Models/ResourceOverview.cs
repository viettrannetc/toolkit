using BusinessLibrary.Ultilities;
using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLibrary.Models.Planning;
using System.Runtime.Caching;
using MemoryCache = System.Runtime.Caching.MemoryCache;

namespace BusinessLibrary.Models
{
	public class PeopleAllocation
	{
		ObjectCache _cache = MemoryCache.Default;

		public PeopleAllocation()
		{

		}

		private DateTime _endDate;

		/// <summary>
		/// Please filter out data by specific team/member/remaining days from now on BEFORE passing the parameter
		/// </summary>
		/// <param name="endDate"></param>
		/// <param name="assignedWPs">all WPs (blocked and non-blocked)</param>
		/// <param name="allAllocationRecords"></param>
		/// <param name="allDayOffRecords"></param>
		public PeopleAllocation(string peopleName, DateTime endDate, List<ToolKitWorkPackageModel> assignedWPs, List<ToolkitAllocationModel> allAllocationRecords, List<ToolkitAllocationAdjustmentModel> allDayOffRecords, List<ToolKitWorkPackageModel> allToolkitWPs)
		{
			Name = peopleName;
			Team = allAllocationRecords.FirstOrDefault(a => a.Name == peopleName)?.Team;
			_endDate = endDate;
			ScheduleForAvailable = new List<ResourceFree>();
			var cloneAssignedWPs = new List<ToolKitWorkPackageModel>();
			assignedWPs.ForEach(a => cloneAssignedWPs.Add(a.DeepCopy()));

			Dictionary<DateTime, decimal> availableAllocatedDays = new Dictionary<DateTime, decimal>();
			var memberAllocation = allAllocationRecords.Where(a => a.Name == peopleName).ToList();
			foreach (var allocationRecord in memberAllocation)
			{
				var startDate = allocationRecord.From;
				while (startDate <= allocationRecord.To)
				{
					if (!availableAllocatedDays.ContainsKey(startDate))
					{
						availableAllocatedDays.Add(startDate, allocationRecord.Hours);
					}
					startDate = startDate.AddDays(1);
				}
			}

			Dictionary<DateTime, decimal> _unavailableWorkingDays = new Dictionary<DateTime, decimal>();
			var memberAllocationAdjustments = allDayOffRecords.Where(a => a.Name == peopleName).ToList();
			var today = DateTime.UtcNow.Date;
			foreach (var dayOffRecord in memberAllocationAdjustments)
			{
				var firstDayOff = dayOffRecord.From;
				while (firstDayOff.Date <= dayOffRecord.To.Date)
				{
					if (firstDayOff.Date >= today.Date && !_unavailableWorkingDays.ContainsKey(firstDayOff))
					{
						_unavailableWorkingDays.Add(firstDayOff, dayOffRecord.Hours);
					}

					firstDayOff = firstDayOff.AddDays(1);
				}
			}

			var availableWorkingDays = new Dictionary<DateTime, decimal>();
			while (today < _endDate)
			{
				if (!today.IsWeekend() && availableAllocatedDays.Keys.Contains(today.Date))
				{
					var remainingAvailableHours = _unavailableWorkingDays.ContainsKey(today.Date)
						? availableAllocatedDays[today.Date] - _unavailableWorkingDays[today]
						: availableAllocatedDays[today.Date];
					if (remainingAvailableHours > 0)
					{
						availableWorkingDays.Add(today, remainingAvailableHours);
					}
				}

				today = today.AddDays(1);
			}

			HoursAvailable = availableWorkingDays.Values.Sum();
			HoursAllocatedBlocked = cloneAssignedWPs.Where(wp => wp.CurrentStatus == Constains.WP_Status_Blocked).Sum(wp => wp.RemainingHours);
			HoursAllocatedInTotal = cloneAssignedWPs.Sum(wp => wp.RemainingHours);
			HoursAllocatedUnBlocked = cloneAssignedWPs.Where(wp => wp.CurrentStatus != Constains.WP_Status_Blocked).Sum(wp => wp.RemainingHours);
			AvailableFrom = GetAvailableFrom(HoursAllocatedUnBlocked, availableWorkingDays);

			ScheduleForWorking = GenerateSchedule(peopleName, endDate, allAllocationRecords, allDayOffRecords, allToolkitWPs);
		}

		public DateTime GetAvailableFrom(decimal hoursAllocatedUnBlocked, Dictionary<DateTime, decimal> availableWorkingDays)
		{
			var allocatedUnblocked = hoursAllocatedUnBlocked;

			foreach (var availableWorkingDay in availableWorkingDays)
			{
				if (allocatedUnblocked > 0)
					allocatedUnblocked = allocatedUnblocked - Constains.WorkingHours_PerDay;
				else
					return availableWorkingDay.Key;
			}

			//allocated > 0 and there is no available working days
			try
			{
				var lastDate = availableWorkingDays.Last().Key;

				while (allocatedUnblocked > 0)
				{
					lastDate = lastDate.AddDays(1);
					allocatedUnblocked = allocatedUnblocked - Constains.WorkingHours_PerDay;
				}
				return lastDate;
			}
			catch (Exception ex)
			{

				throw;
			}
		}
		public string Team { get; set; }
		public string Name { get; set; }
		public decimal HoursAvailable { get; set; }
		public decimal HoursAllocatedBlocked { get; set; }
		public decimal HoursAllocatedInTotal { get; set; }
		public decimal HoursAllocatedUnBlocked { get; set; }
		public decimal HoursNonWork
		{
			get
			{
				return HoursAvailable - HoursAllocatedUnBlocked;
			}
		}
		public DateTime AvailableFrom { get; set; }

		//TODO: skills? experience? which task will fit with the resource?

		public List<ResourceOnAllocatedWps> ScheduleForWorking { get; set; }
		public List<ResourceFree> ScheduleForAvailable { get; set; }

		public List<ResourceOnAllocatedWps> GenerateSchedule(string peopleName, DateTime endDate, List<ToolkitAllocationModel> allAllocationRecords, List<ToolkitAllocationAdjustmentModel> allDayOffRecords, List<ToolKitWorkPackageModel> allToolkitWPs)
		{
			var cacheData = _cache.Get(peopleName) as List<ResourceOnAllocatedWps>;
			if (cacheData != null)
				return cacheData;

			_endDate = endDate;
			var openWorkPackages = allToolkitWPs.Where(f => Constains.Release_1st.Contains(f.Release) && !Constains.Status_Un_Expected.Contains(f.CurrentStatus)).ToList();

			var assignedWPs = openWorkPackages
					.Where(owp =>
							owp.Assignee == peopleName
							&& owp.RemainingHours > 0
							&& Constains.WP_TYPE_BuildingPhase.Contains(owp.WpType))
					.ToList();
			var cloneAssignedWPs = new List<ToolKitWorkPackageModel>();
			assignedWPs.ForEach(a => cloneAssignedWPs.Add(a.DeepCopy()));

			Dictionary<DateTime, decimal> availableAllocatedDays = new Dictionary<DateTime, decimal>();
			var memberAllocation = allAllocationRecords.Where(a => a.Name == peopleName).ToList();
			foreach (var allocationRecord in memberAllocation)
			{
				var startDate = allocationRecord.From;
				while (startDate <= allocationRecord.To)
				{
					if (!availableAllocatedDays.ContainsKey(startDate))
					{
						availableAllocatedDays.Add(startDate, allocationRecord.Hours);
					}
					startDate = startDate.AddDays(1);
				}
			}

			var unavailableWorkingDays = new Dictionary<DateTime, decimal>();
			var today = DateTime.UtcNow.Date;
			var memberAllocationAdjustments = allDayOffRecords.Where(a => a.Name == peopleName).ToList();
			foreach (var dayOffRecord in memberAllocationAdjustments)
			{
				var firstDayOff = dayOffRecord.From;
				while (firstDayOff <= dayOffRecord.To)
				{
					if (firstDayOff >= today && !unavailableWorkingDays.ContainsKey(firstDayOff))
					{
						unavailableWorkingDays.Add(firstDayOff, dayOffRecord.Hours);
					}

					firstDayOff = firstDayOff.AddDays(1);
				}
			}

			var availableWorkingDays = new Dictionary<DateTime, decimal>();
			var clonedAvailableWorkingDays = new Dictionary<DateTime, decimal>();
			while (today < _endDate)
			{
				if (!today.IsWeekend() && availableAllocatedDays.Keys.Contains(today.Date))
				{
					var remainingAvailableHours = unavailableWorkingDays.ContainsKey(today)
						? availableAllocatedDays[today.Date] - unavailableWorkingDays[today]
						: availableAllocatedDays[today.Date];
					if (remainingAvailableHours > 0)
					{
						availableWorkingDays.Add(today, remainingAvailableHours);
						clonedAvailableWorkingDays.Add(today, remainingAvailableHours);
					}
				}

				today = today.AddDays(1);
			}

			var result = new List<ResourceOnAllocatedWps>();
			ScheduleForAvailable = new List<ResourceFree>();

			foreach (var day in clonedAvailableWorkingDays)
			{
				var remainingAvailableHoursInDay = day.Value;

				//foreach (var featurePriority in Constains.FE_Priority)  //TODO: DONE - implement based on the priority from Feature level?
				for (int i = 0; i <= Constains.FE_Priority.Count() - 1; i++)
				{
					var featurePriority = Constains.FE_Priority[i];
					if (remainingAvailableHoursInDay <= 0) break;
					var assignedWpsByFeature = cloneAssignedWPs.Where(wp => wp.FeatureName == featurePriority && wp.RemainingHours > 0).OrderBy(w => w.CurrentStatus).ToList();
					if (!assignedWpsByFeature.Any()) continue;

					//Order by priority
					for (int j = Constains.WP_Priority.Count - 1; j >= 0; j--)
					{
						var selectedOne = assignedWpsByFeature.FirstOrDefault(w => w.Id == Constains.WP_Priority[j]);
						if (selectedOne != null)
						{
							assignedWpsByFeature.Move(assignedWpsByFeature.IndexOf(selectedOne), 0);
						}
					}

					foreach (var wp in assignedWpsByFeature)
					{
						if (wp.RemainingHours <= 0) continue;
						if (remainingAvailableHoursInDay <= 0) break;

						if (wp.CurrentStatus == Constains.WP_Status_Blocked)
						{
							//need to move this workload after the blocker is done
							var wpBlocker = allToolkitWPs.FirstOrDefault(w => w.Id == wp.DependOnWPId);
							if (wpBlocker == null) //TODO: no root cause, no plan
							{
								continue;
							}
							else
							{
								//find the end date of the blockers, so we can know when we can start the WPs
								//generate whole plan of the blocker
								var blockerPlan = GenerateSchedule(wpBlocker.Assignee, endDate, allAllocationRecords, allDayOffRecords, allToolkitWPs);
								var releasedBlockerDate = blockerPlan.Where(pl => pl.WpId == wpBlocker.Id).OrderByDescending(p => p.Date).FirstOrDefault()?.Date;

								if (releasedBlockerDate == null) //TODO: no root cause, no plan
								{
									continue;
								}
								else if (releasedBlockerDate.Value.AddDays(1).Date > day.Key.Date)
								{
									continue;
									//if it <=, then we process as usual
									//if it >, then we need to wait to the future time and then process it as usual
								}
							}
						}

						var originalWp = allToolkitWPs.First(w => w.Id == wp.Id);
						originalWp.HasPlan = true;

						var remainingHours = wp.RemainingHours;
						if (remainingHours - remainingAvailableHoursInDay > 0) //this WP must move to next day
						{
							var specificWpInDay = new ResourceOnAllocatedWps(peopleName, wp.Id, wp.Name, day.Key, remainingAvailableHoursInDay);
							result.Add(specificWpInDay);

							wp.RemainingHours = wp.RemainingHours - remainingAvailableHoursInDay; //reduce remaining hours in WP
							remainingAvailableHoursInDay = 0;
							break;
						}
						else //This WP should be done in this day
						{
							var specificWpInDay = new ResourceOnAllocatedWps(peopleName, wp.Id, wp.Name, day.Key, wp.RemainingHours);
							result.Add(specificWpInDay);
							remainingAvailableHoursInDay = remainingAvailableHoursInDay - wp.RemainingHours;
							wp.RemainingHours = 0;
						}
					} //---end of WPs loop in a day
				} //---end of WPs loop of feature priority in a day
				if (remainingAvailableHoursInDay > 0)
					ScheduleForAvailable.Add(new ResourceFree(day.Key.Date, remainingAvailableHoursInDay, Team, peopleName));
			}

			_cache.Set(peopleName, result, new DateTimeOffset(new DateTime(2022, 1, 1)));
			ScheduleForAvailable = ScheduleForAvailable.Distinct().ToList();



			return result;
		}
	}


	public class ResourceOnAllocatedWps
	{
		public ResourceOnAllocatedWps(string worker, string wpId, string wpName, DateTime date, decimal spentHours)
		{
			Name = worker;
			WpId = wpId;
			WpName = wpName;
			Date = date;
			WorkingHours = spentHours;
		}
		public DateTime Date { get; set; }
		public string WpId { get; set; }
		public string WpName { get; set; }
		public decimal WorkingHours { get; set; }
		public string Name { get; set; }
	}

	public class ResourceFree
	{
		public ResourceFree(DateTime date, decimal freeHours, string team, string name)
		{
			Date = date;
			FreeHours = freeHours;
			Team = team;
			Name = name;
		}
		public DateTime Date { get; set; }
		public decimal FreeHours { get; set; }
		public string Team { get; set; }
		public string Name { get; set; }
	}
}
