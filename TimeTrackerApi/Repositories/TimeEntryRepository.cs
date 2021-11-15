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
}
