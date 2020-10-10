using System;

namespace Reminders.Domain
{
    public class Reminder
    {
        public string Title { get; }
        public DateTime Date { get; }

        public Reminder(string title, DateTime date)
        {
            Title = title;
            Date = date;
        }
    }
}
