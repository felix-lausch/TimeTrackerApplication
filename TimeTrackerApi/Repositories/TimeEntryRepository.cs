using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OneOf;
using OneOf.Types;
using TimeTrackerApi.Models;

namespace TimeTrackerApi.Repositories;

public class TimeEntryRepository : RepositoryBase
{
    private readonly DbSet<TimeEntry> timeEntries;
    //private readonly IMemoryCache cache;

    //public TimeEntryRepository(TimeTrackerContext dbContext, IMemoryCache cache)
    public TimeEntryRepository(TimeTrackerContext dbContext)
        : base(dbContext)
    {
        timeEntries = dbContext.TimeEntries;
        //this.cache = cache;
    }

    public async Task<TimeEntry> CreateAsync(TimeEntry timeEntry)
    {
        timeEntries.Add(timeEntry);
        await SaveChangesAsync();

        return timeEntry;
    }

    //TODO: check if this does what i need it to do.
    public async Task<IEnumerable<TimeEntry>> GetByMonthAndYearAsync(int month, int year)
    {
        return await timeEntries
            .FromSqlRaw("SELECT * FROM TimeEntries " +
                "WHERE DATEPART(MONTH, Date) = {0} And DATEPART(YEAR, Date) = {1}",
                month,
                year)
            .ToListAsync();
    }

    public async Task<List<TimeEntry>> GetAllAsync()
    {
        return await timeEntries.ToListAsync();
    }

    public async Task<TimeEntry?> GetByIdAsync(Guid id)
    {
        return await timeEntries.FindAsync(id);
    }

    public async Task<OneOf<Success, Error>> DeleteByIdAsync(Guid id)
    {
        var timeEntry = await GetByIdAsync(id);

        if (timeEntry is null)
        {
            //throw new ArgumentException("TimeEntry to delete couldn't be found.");
            return new Error("TimeEntry to delete couldn't be found.");
        }

        timeEntries.Remove(timeEntry);
        await SaveChangesAsync();
        return new Success();
    }

    public async Task<OneOf<TimeEntry, Error>> UpdateAsync(TimeEntry entry)
    {
        var existingEntry = await GetByIdAsync(entry.Id);

        if (existingEntry is null)
        {
            //throw new ArgumentException("TimeEntry to update couldn't be found.");
            return new Error("TimeEntry to update couldn't be found.");
        }

        existingEntry.Date = entry.Date;
        existingEntry.StartHours = entry.StartHours;
        existingEntry.StartMinutes = entry.StartMinutes;
        existingEntry.EndHours = entry.EndHours;
        existingEntry.EndMinutes = entry.EndMinutes;
        existingEntry.PauseHours = entry.PauseHours;

        await SaveChangesAsync();

        return entry;
    }
}

public partial struct Error
{
    public string Message { get; set; }

    public Error(string messaage)
    {
        Message = messaage;
    }
}
