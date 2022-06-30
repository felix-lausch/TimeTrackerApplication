namespace TimeTrackerApi.Services;

using System.Globalization;
using TimeTrackerApi.Dtos;
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

    public async Task<IEnumerable<TimeEntriesByDayDto>> GetTimeEntriesForMonthAndYear(
        int month,
        int year)
    {
        var timeEntries = await timeEntryRepository.GetByMonthAndYearAsync(month, year);

        //var firstDayOfMonth = new DateOnly(year, month, 1);
        //var daysInMonth = firstDayOfMonth.AddMonths(1).AddDays(-1).Day;

        var daysInMonth = GetDaysInMonth(month, year);

        var results = new List<TimeEntriesByDayDto>();

        for (var i = 1; i <= daysInMonth; i++)
        {
            var date = new DateOnly(year, month, i);

            var dto = new TimeEntriesByDayDto(
                date,
                date.ToString("dddd", CultureInfo.InvariantCulture).ToUpper(),
                timeEntries.Where(timeEntry => timeEntry.Date == date));

            results.Add(dto);
        }

        return results;

        //TODO: use group by? use hashMap maybe?
        //var idk = timeEntries
        //    .GroupBy(day => day.Date);

        //var idk2 = idk.Select(grouping => new TimeEntriesByDayDto(
        //        grouping.Key,
        //        grouping.Key.ToString("dddd", CultureInfo.InvariantCulture),
        //        grouping.ToList()));
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
