namespace TimeTrackerApi.Repositories;

using Microsoft.EntityFrameworkCore;
using System;
using TimeTrackerApi.Models;

public class DayEntryRepository : RepositoryBase
{
    private readonly DbSet<DayEntry> dayEntries;

    public DayEntryRepository(TimeTrackerContext dbContext)
        : base(dbContext)
    {
        dayEntries = dbContext.DayEntries;
    }

    public IAsyncEnumerable<DayEntry> GetAsync()
    {
        return dayEntries.AsAsyncEnumerable();
    }

    public IEnumerable<DayEntry> GetByMonthAndYearAsync(int month, int year)
    {
        return dayEntries.FromSqlRaw(
            "SELECT * FROM DayEntries " +
            "WHERE DATEPART(MONTH, Date) = {0} And DATEPART(YEAR, Date) = {1}",
            month,
            year);
    }

    public async Task<DayEntry> CreateAsync(DayEntry dayEntry)
    {
        dayEntries.Add(dayEntry);
        await SaveChangesAsync();

        return dayEntry;
    }
}
