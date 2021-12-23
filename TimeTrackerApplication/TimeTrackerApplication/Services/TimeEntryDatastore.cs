namespace TimeTrackerApplication.Services
{
    using Refit;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TimeTrackerApplication.Models;

    public class TimeEntryDatastore : IDataStore<TimeEntry>
    {
        private const string timeTrackerUrl = "http://localhost:5000";
        private readonly ITimeTrackerApi api;

        public TimeEntryDatastore()
        {
            api = RestService.For<ITimeTrackerApi>(timeTrackerUrl);
        }

        public async Task<TimeEntry> AddItemAsync(TimeEntry item)
        {
            return await api.CreateTimeEntry(item);
        }

        public async Task<bool> DeleteItemAsync(Guid id)
        {
            await api.DeleteTimeEntryById(id);
            return true; //TODO: make better
        }

        public async Task<TimeEntry> GetItemAsync(Guid id)
        {
            return await api.GetTimeEntryById(id);
        }

        public async Task<IEnumerable<TimeEntry>> GetItemsAsync(bool forceRefresh = false)
        {
            return await api.GetTimeEntries();
        }

        //public async Task<bool> UpdateItemAsync(TimeEntry item)
        //{
        //    await api.UpdateTimeEntry(item);
        //    return true; //TODO: remove bool?
        //}

        public async Task<TimeEntry> UpdateItemAsync(TimeEntry item)
        {
            await api.UpdateTimeEntry(item);
            return item;

        }
    }
}
