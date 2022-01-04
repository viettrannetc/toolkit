using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BusinessLibrary.Ultilities
{
	public static class DateTimeExtension
	{
		// This presumes that weeks start with Monday.
		// Week 1 is the 1st week of the year with a Thursday in it.
		public static int GetIso8601WeekOfYear(this DateTime time)
		{
			// Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
			// be the same week# as whatever Thursday, Friday or Saturday are,
			// and we always get those right
			DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
			if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
			{
				time = time.AddDays(3);
			}

			// Return the week of our adjusted day
			return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
		}

		public static bool IsWeekend(this DateTime date)
		{
			return Constains.DayOff_InWeek.Contains(date.DayOfWeek.ToString());
		}

		public static bool IsInHolidayGlobal(this DateTime date)
		{
			return false;
		}

		public static bool IsInHolidayCountry(this DateTime date, string countryName)
		{
			return false;
		}

		public static void Move<T>(this List<T> list, int oldIndex, int newIndex)
		{
			var item = list[oldIndex];

			list.RemoveAt(oldIndex);

			if (newIndex > oldIndex) newIndex--;
			// the actual index could have shifted due to the removal

			list.Insert(newIndex, item);
		}


		public static bool IsAfterWorkingHours(this DateTime date, string timezone)
		{
			return true;
		}

		public static bool IsBeforeWorkingHours(this DateTime date, string timezone)
		{
			return true;
		}

		public static bool IsInWorkingHours(this DateTime date, string timezone)
		{
			return true;
		}
	}
}
