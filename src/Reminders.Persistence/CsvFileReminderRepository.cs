using Reminders.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;

namespace Reminders.Persistence
{
    /// <summary>
    /// Reads and parses CSV files with rows of the format: yyyy-MM-dd;Reminder title
    /// </summary>
    public class CsvFileReminderRepository : IReminderRepository
    {
        private readonly string _csvFileUrl;
        private IEnumerable<Reminder> _remindersFromFile = Array.Empty<Reminder>();

        public CsvFileReminderRepository(string csvFileUrl)
        {
            _csvFileUrl = csvFileUrl;
        }
        public IEnumerable<Reminder> GetAllReminders()
        {
            if (!_remindersFromFile.Any())
            {
                string csvContents = ReadCsvFile(_csvFileUrl);
                _remindersFromFile = ParseReminders(csvContents);
            }

            return _remindersFromFile;
        }

        private IEnumerable<Reminder> ParseReminders(string csvContents)
        {
            return csvContents.Split("\n")
                .Select(line => ParseReminderLine(line));
        }

        private Reminder ParseReminderLine(string line)
        {
            var split = line.Split(';');
            var date = DateTime.Parse(split[0], null, DateTimeStyles.RoundtripKind);
            var title = split[1];

            return new Reminder(title, date);
        }

        private string ReadCsvFile(string csvFileUrl)
        {
            WebClient client = new WebClient();
            return client.DownloadString(csvFileUrl);
        }
    }
}
