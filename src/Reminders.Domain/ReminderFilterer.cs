using System;
using System.Collections.Generic;
using System.Linq;

namespace Reminders.Domain
{
    public class ReminderFilterer
    {
        public IEnumerable<Reminder> Filter(IEnumerable<Reminder> reminders, DateTime start, DateTime end)
        {
            return reminders.Where(r => start <= r.Date && r.Date < end);
        }
    }
}
