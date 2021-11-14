#nullable disable
namespace TimeTrackerApi;

using Microsoft.EntityFrameworkCore;
using TimeTrackerApi.Models;

public class TimeTrackerContext : DbContext
{
    public TimeTrackerContext()
    {
    }

    public TimeTrackerContext(DbContextOptions<TimeTrackerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TimeEntry> TimeEntries { get; set; }
}
