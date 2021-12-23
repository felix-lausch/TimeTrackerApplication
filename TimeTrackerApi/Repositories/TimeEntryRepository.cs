using Microsoft.EntityFrameworkCore;
using TimeTrackerApi.Models;

namespace TimeTrackerApi.Repositories;

public class TimeEntryRepository : RepositoryBase
{
    private readonly DbSet<TimeEntry> timeEntries;

    public TimeEntryRepository(TimeTrackerContext dbContext)
        : base(dbContext)
    {
        timeEntries = dbContext.TimeEntries;
    }

    public async Task<TimeEntry> CreateAsync(TimeEntry timeEntry)
    {
        timeEntries.Add(timeEntry);
        await SaveChangesAsync();

        return timeEntry;
    }

    public async Task<List<TimeEntry>> GetAtllAsync()
    {
        return await timeEntries.ToListAsync();
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

    public async Task UpdateAsync(TimeEntry entry)
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
    }
}
