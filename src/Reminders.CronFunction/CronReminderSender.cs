using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Reminders.Domain;

namespace Reminders.CronFunction
{
    public class CronReminderSender
    {
        private readonly IReminderService _reminderService;
        private readonly IReminderReportSender _sender;

        public CronReminderSender(IReminderService reminderService, IReminderReportSender sender)
        {
            _reminderService = reminderService;
            _sender = sender;
        }

        [FunctionName("CronReminderSender")] // Every Sunday at 20:00
        public void Run([TimerTrigger("0 20 * * 0")]TimerInfo timer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var report = _reminderService.GenerateReport(DateTime.Today, DateTime.Today.AddDays(90));
            _sender.SendReminderReportAsync(report);
        }
    }
}
