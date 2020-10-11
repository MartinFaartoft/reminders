using System.Threading.Tasks;

namespace Reminders.Domain
{
    public interface IReminderReportSender
    {
        public Task SendReminderReportAsync(ReminderReport reminderReport);
    }
}
