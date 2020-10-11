using System;

namespace Reminders.Domain
{
    public class ReminderService
    {
        private readonly IReminderRepository _reminderRepository;
        private readonly ReminderFilterer _reminderFilterer = new ReminderFilterer();

        public ReminderService(IReminderRepository reminderRepository)
        {
            _reminderRepository = reminderRepository;
        }

        public ReminderReport GenerateReport(DateTime reportStartTime, DateTime reportEndTime)
        {
            var reminders = _reminderRepository.GetAllReminders();
            var filteredReminders = _reminderFilterer.Filter(reminders, reportStartTime, reportEndTime);
            return new ReminderReport(filteredReminders, reportStartTime, reportEndTime);
        }
    }
}
