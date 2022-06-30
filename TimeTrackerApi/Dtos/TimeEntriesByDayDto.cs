namespace TimeTrackerApi.Dtos;

using TimeTrackerApi.Models;

public record TimeEntriesByDayDto(
    DateOnly Date,
    string Weekday,
    IEnumerable<TimeEntry> TimeEntries);
