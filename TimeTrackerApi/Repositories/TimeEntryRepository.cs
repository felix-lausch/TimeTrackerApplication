using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TimeTrackerApi.Models;

namespace TimeTrackerApi.Repositories;

public class TimeEntryRepository : RepositoryBase
{
    private readonly DbSet<TimeEntry> timeEntries;
    private readonly IMemoryCache cache;

    public TimeEntryRepository(TimeTrackerContext dbContext, IMemoryCache cache)
        : base(dbContext)
    {
        timeEntries = dbContext.TimeEntries;
        this.cache = cache;
    }

    public async Task<TimeEntry> CreateAsync(TimeEntry timeEntry)
    {
        timeEntries.Add(timeEntry);
        await SaveChangesAsync();

        return timeEntry;
    }

    public async Task<List<TimeEntry>> GetAtllAsync()
    {
        var cacheKey = "timeEntriesList";

        var entries = cache.Get<List<TimeEntry>>(cacheKey);

        if (entries is null)
        {
            entries = await timeEntries.ToListAsync();

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(20));

            cache.Set(cacheKey, entries, cacheEntryOptions);
        }

        return entries;
    }

    public async Task<TimeEntry?> GetById(Guid id)
    {
        return await timeEntries.FindAsync(id);
    }

    public async Task DeleteById(Guid id)
    {
        var timeEntry = await GetById(id);

        if (timeEntry is null)
        {
            throw new ArgumentException("TimeEntry to delete couldn't be found.");
        }

        timeEntries.Remove(timeEntry);
        await SaveChangesAsync();
    }

    public async Task<TimeEntry> UpdateAsync(TimeEntry entry)
    {
        var existingEntry = await GetById(entry.Id);

        if (existingEntry is null)
        {
            throw new ArgumentException("TimeEntry to update couldn't be found.");
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
