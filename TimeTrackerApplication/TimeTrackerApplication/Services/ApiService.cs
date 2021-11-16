namespace TimeTrackerApplication.Services
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.ObjectModel;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    public class ApiService : IApiService
    {
        private static string baseUrl = "http://localhost:5000/timeEntry";

        private HttpClient client = new HttpClient(); //TODO: use HttpClientFactory & then DI

        //TODO: look into refit

        public async Task<ObservableCollection<T>> RefreshDataAsync<T>()
        {
            //var uri = new Uri(baseUrl);
            try
            {
                var response = await client.GetAsync("http://localhost:5000/timeEntries");

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
            return new ObservableCollection<T>();
        }

        public async Task<T> PostDataAsync<T>(T input)
        {
            //TODO: use refit for this shit
            var uri = new Uri(baseUrl);

            string json = JsonConvert.SerializeObject(input);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            response = await client.PostAsync(uri, content);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                //var responseContent = await response.Content.ReadAsStringAsync();
                var created = JsonConvert.DeserializeObject<T>(responseContent);
                return created;
            }

            throw new Exception("Answer not as expected.");
        }
    }
}
