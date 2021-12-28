namespace TimeTrackerApi.Specifications;

using Ardalis.Specification;
using TimeTrackerApi.Models;

public class TimeEntriesSpecification : Specification<TimeEntry>
{
    public TimeEntriesSpecification()
    {
        Query
            .Where(_ => true)
            .EnableCache(nameof(TimeEntriesSpecification), "ListAll");
    }
}
