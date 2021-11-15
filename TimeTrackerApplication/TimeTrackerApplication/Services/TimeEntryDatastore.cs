namespace TimeTrackerApplication.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TimeTrackerApplication.Models;

    public class TimeEntryDatastore : IDataStore<TimeEntry>
    {
        private readonly ApiService api;

        public TimeEntryDatastore()
        {
            api = new ApiService();
        }

        public async Task<bool> AddItemAsync(TimeEntry item)
        {
            var entry = await api.PostDataAsync(item);
            return entry != null;
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<TimeEntry> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        //public async Task<bool> AddItemAsync(TimeEntry entry)
        //{
        //    entries.Add(entry);

        //    return await Task.FromResult(true);
        //}

        //public async Task<bool> UpdateItemAsync(TimeEntry entry)
        //{
        //    var oldItem = entries.Where((TimeEntry arg) => arg.Id == entry.Id).FirstOrDefault();
        //    entries.Remove(oldItem);
        //    entries.Add(entry);

        //    return await Task.FromResult(true);
        //}

        //public async Task<bool> DeleteItemAsync(string id)
        //{
        //    var oldItem = entries.Where((Item arg) => arg.Id == id).FirstOrDefault();
        //    entries.Remove(oldItem);

        //    return await Task.FromResult(true);
        //}

        //public async Task<Item> GetItemAsync(string id)
        //{
        //    return await Task.FromResult(entries.FirstOrDefault(s => s.Id == id));
        //}

        public async Task<IEnumerable<TimeEntry>> GetItemsAsync(bool forceRefresh = false)
        {
            return await api.RefreshDataAsync<TimeEntry>();
        }

        public Task<bool> UpdateItemAsync(TimeEntry item)
        {
            throw new NotImplementedException();
        }
    }
}
