using System;
using System.Collections.Generic;
using System.Linq;

namespace Reminders.Domain
{
    public class ReminderFilterer
    {
        public IEnumerable<Reminder> Filter(IEnumerable<Reminder> reminders, DateTime start, DateTime end)
        {
            return reminders
                .Where(r => HasOccurrenceInPeriod(r, start, end))
                .OrderBy(r => r.Date.Month)
                .ThenBy(r => r.Date.Day);
        }

        private bool HasOccurrenceInPeriod(Reminder reminder, DateTime start, DateTime end)
        {
            // only works for annually recurring reminders with a period less than 1 year
            var adjustedDate = reminder.Date.AddYears(start.Year - reminder.Date.Year);

            return adjustedDate >= start && adjustedDate < end;
        }
    }
}
