using System.Collections.Generic;

namespace Reminders.Domain
{
    public interface IReminderRepository
    {
        IEnumerable<Reminder> GetAllReminders();
    }
}