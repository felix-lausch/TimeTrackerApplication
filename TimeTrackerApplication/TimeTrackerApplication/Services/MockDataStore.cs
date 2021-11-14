using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTrackerApplication.Models;

namespace TimeTrackerApplication.Services
{
    public class MockDataStore : IDataStore<TimeEntry>
    {
        readonly List<TimeEntry> entries;

        public MockDataStore()
        {
            entries = new List<TimeEntry>()
            {
                new TimeEntry
                {
                    DisplayEndTime = "13:45",
                    DisplayStartTime = "16:45",
                    PauseHours = 1.75,
                    TotalHours = "12:50:00"
                },
            };
        }

        public Task<bool> AddItemAsync(TimeEntry item)
        {
            throw new NotImplementedException();
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
            return await Task.FromResult(entries);
        }

        public Task<bool> UpdateItemAsync(TimeEntry item)
        {
            throw new NotImplementedException();
        }
    }
}