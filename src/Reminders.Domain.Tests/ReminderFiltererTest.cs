using System;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;

namespace Reminders.Domain.Tests
{
    public class ReminderFiltererTest
    {
        ReminderFilterer sut = new ReminderFilterer();
        
        [Fact]
        public void ShouldIncludeReminderForTomorrow()
        {
            var reminderForTomorrow = CreateReminder(DateTime.Today.AddDays(1));
            sut.Filter(reminderForTomorrow, DateTime.Today, DateTime.Today.AddDays(7)).Should().BeEquivalentTo(reminderForTomorrow);
        }

        [Fact]
        public void ShouldNotIncludeReminderForYesterday()
        {
            var reminderForYesterday = CreateReminder(DateTime.Today.AddDays(-1));
            sut.Filter(reminderForYesterday, DateTime.Today, DateTime.Today.AddDays(7)).Should().BeEmpty();
        }

        private IEnumerable<Reminder> CreateReminder(DateTime date)
        {
            return new[] { new Reminder("My Test Reminder", date) };
        }
    }
}
