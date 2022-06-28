namespace TimeTrackerApi.Repositories;

using Microsoft.EntityFrameworkCore;
using TimeTrackerApi.Models;

public class DayEntryRepository : RepositoryBase
{
    private readonly DbSet<DayEntry> dayEntries;

    public DayEntryRepository(TimeTrackerContext dbContext)
        : base(dbContext)
    {
        dayEntries = dbContext.DayEntries;
    }

    public async Task<DayEntry> CreateAsync(DayEntry dayEntry)
    {
        dayEntries.Add(dayEntry);
        await SaveChangesAsync();

        return dayEntry;
    }
}
