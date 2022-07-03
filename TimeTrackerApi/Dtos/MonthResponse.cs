namespace TimeTrackerApi.Dtos;

public record MonthResponse(
    IEnumerable<TimeEntriesByDayDto> Days,
    string MonthString,
    int Month,
    int Year);