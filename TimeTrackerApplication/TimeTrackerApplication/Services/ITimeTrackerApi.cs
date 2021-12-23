namespace TimeTrackerApplication.Services
{
    using Refit;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TimeTrackerApplication.Models;

    public interface ITimeTrackerApi
    {
        [Get("/timeEntries")]
        Task<List<TimeEntry>> GetTimeEntries();

        [Get("/timeEntry/{id}")]
        Task<TimeEntry> GetTimeEntryById(Guid id);

        [Post("/timeEntry")]
        Task<TimeEntry> CreateTimeEntry(TimeEntry timeEntry);

        [Delete("/timeEntry/{id}")]
        Task DeleteTimeEntryById(Guid id);

        [Patch("/timeEntry")]
        Task UpdateTimeEntry(TimeEntry timeEntry);
    }
}
