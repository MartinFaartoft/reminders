using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reminders.Domain;
using Reminders.EmailSender;
using Reminders.Persistence;
using System;

[assembly: FunctionsStartup(typeof(Reminders.CronFunction.Startup))]
namespace Reminders.CronFunction
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
               .AddJsonFile("local.settings.json", optional: false, reloadOnChange: true)
               .AddEnvironmentVariables()
               .Build();

            builder.Services.AddSingleton<IConfiguration>(config);
            builder.Services.AddTransient<IReminderReportSender, EmailReminderReportSender>();
            builder.Services.AddSingleton<IReminderRepository>(new CsvFileReminderRepository(config["REMINDERS_CSV_URL"]));
            builder.Services.AddTransient<IReminderService, ReminderService>();
        }
    }
}
