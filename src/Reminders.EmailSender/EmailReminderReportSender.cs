using Microsoft.Extensions.Configuration;
using Reminders.Domain;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Linq;
using System.Threading.Tasks;

namespace Reminders.EmailSender
{
    public class EmailReminderReportSender : IReminderReportSender
    {
        private readonly SendGridClient _client;
        private readonly EmailAddress _sender, _receiver;

        public EmailReminderReportSender(IConfiguration configuration)
        {
            _client = new SendGridClient(configuration["SENDGRID_API_KEY"]);
            _sender = new EmailAddress(configuration["SENDGRID_SENDER_EMAIL"], configuration["SENDGRID_SENDER_NAME"]);
            _receiver = new EmailAddress(configuration["SENDGRID_RECEIVER_EMAIL"], configuration["SENDGRID_RECEIVER_NAME"]);
        }
        public async Task SendReminderReportAsync(ReminderReport reminderReport)
        {
            var subject = RenderSubject(reminderReport);
            var plainTextContent = RenderContent(reminderReport);
            var msg = MailHelper.CreateSingleEmail(_sender, _receiver, subject, plainTextContent, "");
            await _client.SendEmailAsync(msg);
        }

        private string RenderSubject(ReminderReport report)
        {
            int periodLength = (int)report.ReportEndDate.Subtract(report.ReportStartDate).TotalDays;
            return $"{report.Reminders.Count()} Reminders for the next {periodLength} days";
        }

        private string RenderContent(ReminderReport reminderReport)
        {
            var titles = reminderReport.Reminders.OrderBy(r => r.Date).Select(r => r.Title);

            return string.Join("\n", titles);
        }
    }
}
