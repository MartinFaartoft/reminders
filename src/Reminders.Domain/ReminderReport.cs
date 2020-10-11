using System;
using System.Collections.Generic;
using System.Text;

namespace Reminders.Domain
{
    public class ReminderReport
    {
        public IEnumerable<Reminder> Reminders { get; }
        public DateTime ReportStartDate { get; }
        public DateTime ReportEndDate { get; }

        public ReminderReport(IEnumerable<Reminder> reminders, DateTime reportStartDate, DateTime reportEndDate)
        {
            Reminders = reminders;
            ReportStartDate = reportStartDate;
            ReportEndDate = reportEndDate;
        }
    }
}
