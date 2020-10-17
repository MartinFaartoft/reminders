using System;

namespace Reminders.Domain
{
    public interface IReminderService
    {
        ReminderReport GenerateReport(DateTime reportStartTime, DateTime reportEndTime);
    }
}