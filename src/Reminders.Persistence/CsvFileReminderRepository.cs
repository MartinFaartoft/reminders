using Reminders.Domain;
using System;
using System.Collections.Generic;

namespace Reminders.Persistence
{
    public class CsvFileReminderRepository : IReminderRepository
    {
        public CsvFileReminderRepository()
        {

        }
        public IEnumerable<Reminder> GetAllReminders()
        {
            return new[] { new Reminder(title: "A test reminder", DateTime.Today.AddDays(1)) };
        }
    }
}
