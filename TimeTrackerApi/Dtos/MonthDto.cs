namespace TimeTrackerApi.Dtos;

public record MonthDto(
    IEnumerable<TimeEntriesByDayDto> Days,
    string MonthString,
    int Month,
    int Year);