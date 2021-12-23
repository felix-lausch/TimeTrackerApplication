using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimeTrackerApplication.Services
{
    public interface IDataStore<T>
    {
        Task<T> AddItemAsync(T item);
        Task<T> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(Guid id);
        Task<T> GetItemAsync(Guid id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
