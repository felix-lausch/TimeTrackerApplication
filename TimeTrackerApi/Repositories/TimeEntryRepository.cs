using TimeTrackerApi.Models;

namespace TimeTrackerApi.Repositories;

public class TimeEntryRepository
{
    private readonly Dictionary<Guid, TimeEntry> _timeEntries = new();

    public void Create(TimeEntry timeEntry)
    {
        if (timeEntry is null)
        {
            return;
        }
        
        _timeEntries[timeEntry.Id] = timeEntry;
    }

    public List<TimeEntry> GetAtll()
    {
        return _timeEntries.Values.ToList();
    }

    public TimeEntry? GetById(Guid id)
    {
        return _timeEntries.TryGetValue(id, out var entry) switch
        {
            true => entry,
            false => null
        };
    }

}
