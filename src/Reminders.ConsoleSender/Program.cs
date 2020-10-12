using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Reminders.Domain;
using Reminders.EmailSender;
using Reminders.Persistence;
using System;
using System.Threading.Tasks;

namespace Reminders.ConsoleSender
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(builder => {
                    builder.AddUserSecrets<Program>();
                })
                .ConfigureServices((context, services) =>
            {
                services.AddTransient<IReminderReportSender, EmailReminderReportSender>();
                services.AddSingleton<IReminderRepository>(new CsvFileReminderRepository(context.Configuration["REMINDERS_CSV_URL"]));
                services.AddTransient<ReminderService>();
            }).Build();

            var reminderService = host.Services.GetService<ReminderService>();
            var sender = host.Services.GetService<IReminderReportSender>();
            var report = reminderService.GenerateReport(DateTime.Today, DateTime.Today.AddDays(90));
            await sender.SendReminderReportAsync(report);
        }
    }
}
