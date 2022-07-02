namespace TimeTrackerApi.Services;

using System.Globalization;
using TimeTrackerApi.Dtos;
using TimeTrackerApi.Models;
using TimeTrackerApi.Repositories;

public class MonthService
{
    private readonly TimeEntryRepository timeEntryRepository;
    private readonly Dictionary<int, int> DaysInMonthByMonth = new()
    {
        { 1, 31 },
        { 2, 28 },
        { 3, 31 },
        { 4, 30 },
        { 5, 31 },
        { 6, 30 },
        { 7, 31 },
        { 8, 31 },
        { 9, 30 },
        { 10, 31 },
        { 11, 30 },
        { 12, 31 },
    };

    public MonthService(TimeEntryRepository timeEntryRepository)
    {
        this.timeEntryRepository = timeEntryRepository;
    }

    public async Task<MonthDto> GetMonthByMonthAndYear(
        int month,
        int year)
    {
        var timeEntries = await timeEntryRepository.GetByMonthAndYearAsync(month, year);

        var timeEntriesGroupings = timeEntries.GroupBy(t => t.Date); //TODO: do this query in repo somehow

        var daysInMonth = GetDaysInMonth(month, year);

        var days = Enumerable.Range(1, daysInMonth)
            .Select(day => new DateOnly(year, month, day))
            .Select(date =>
            {
                return new TimeEntriesByDayDto(
                    date,
                    date.ToString("dddd", CultureInfo.InvariantCulture).ToUpper(),
                    timeEntriesGroupings.FirstOrDefault(g => g.Key == date) ??  Enumerable.Empty<TimeEntry>());
            });

        return new MonthDto(
                    days,
                    new DateOnly(year, month, 1).ToString("MMMM", CultureInfo.InvariantCulture),
                    month,
                    year);
    }

    private int GetDaysInMonth(int month, int year)
    {
        var daysInMonth = DaysInMonthByMonth[month];

        var isFebruaryInLeapYear = month == 2 && year % 2 == 0;
        if (isFebruaryInLeapYear)
        {
            return 29;
        }

        return daysInMonth;
    }
}
