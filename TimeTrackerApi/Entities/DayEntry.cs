namespace TimeTrackerApi.Models;

using System.Globalization;

public class DayEntry
{
    public DayEntry()
    {
        TimeEntries = new HashSet<TimeEntry>();
    }

    public Guid Id { get; internal set; }

    public DateOnly Date { get; set; }

    public string Weekday => Date.ToString("dddd", CultureInfo.InvariantCulture);

    public ICollection<TimeEntry> TimeEntries { get; set; }
}
