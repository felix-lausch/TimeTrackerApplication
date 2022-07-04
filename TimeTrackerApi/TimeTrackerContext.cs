namespace TimeTrackerApi;

using Microsoft.EntityFrameworkCore;
using TimeTrackerApi.Models;
using TimeTrackerApi.Util;

public class TimeTrackerContext : DbContext
{
    public TimeTrackerContext()
    {
    }

    public TimeTrackerContext(DbContextOptions<TimeTrackerContext> options)
        : base(options)
    {
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder.Properties<DateOnly>()
            .HaveConversion<DateOnlyValueConverter>()
            .HaveColumnType("date");
    }

    public virtual DbSet<TimeEntry> TimeEntries => Set<TimeEntry>();
}
