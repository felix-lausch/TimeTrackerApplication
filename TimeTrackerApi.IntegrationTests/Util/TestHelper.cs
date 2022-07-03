namespace TimeTrackerApi.IntegrationTests.Util;

using System;
using TimeTrackerApi.Models;

public static class TestHelper
{
    public static TimeEntry GetTimeEntry()
    {
        return new TimeEntry
        {
            Id = Guid.NewGuid(),
            Date = new DateOnly(2020, 3, 4),
            StartHours = 7,
            StartMinutes = 30,
            EndHours = 17,
            EndMinutes = 0,
            PauseHours = 1.5,
        };
    }
}
