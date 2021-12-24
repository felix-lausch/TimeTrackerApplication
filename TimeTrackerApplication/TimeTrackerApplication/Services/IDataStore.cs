using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimeTrackerApplication.Services
{
    public interface IDataStore<T>
    {
        Task<T> AddItemAsync(T item);
        Task<OneOf<T, string>> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(Guid id);
        Task<T> GetItemAsync(Guid id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
