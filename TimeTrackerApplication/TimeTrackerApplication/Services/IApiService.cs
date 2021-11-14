namespace TimeTrackerApplication.Services
{
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    public interface IApiService
    {
        Task<ObservableCollection<T>> RefreshDataAsync<T>();
    }
}