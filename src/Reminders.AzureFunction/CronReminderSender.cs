using System;
using System.Linq;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Reminders.Domain;

namespace Reminders.AzureFunction
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

        [FunctionName("CronReminderSender")] // Sundays at 20:00
        public void Run([TimerTrigger("0 0 20 * * 0")]TimerInfo reminderTimer, ILogger log)
        {
            log.LogInformation($"C# CronReminderSender function executed at: {DateTime.Now}");

            var report = _reminderService.GenerateReport(DateTime.Today, DateTime.Today.AddDays(30));
            _sender.SendReminderReportAsync(report);

            log.LogInformation($"Done executing, found {report.Reminders.Count()} reminders");
        }
    }
}
