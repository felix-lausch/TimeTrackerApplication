namespace TimeTrackerApplication.Services
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.ObjectModel;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class ApiService : IApiService
    {
        private static string url = "http://localhost:5000/timeEntry";

        private HttpClient client = new HttpClient();

        public async Task<ObservableCollection<T>> RefreshDataAsync<T>()
        {

            var uri = new Uri(url);
            try
            {
                var response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var entries = JsonConvert.DeserializeObject<ObservableCollection<T>>(content);
                    return entries;
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }
    }
}
