namespace TimeTrackerApi.IntegrationTests;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using TimeTrackerApi.IntegrationTests.Util;
using TimeTrackerApi.Models;
using TimeTrackerApi.Util;

public class IntegrationTest
{
    protected readonly HttpClient httpClient;
    protected readonly JsonSerializerOptions jsonSerializerOptions = new ();

    protected IntegrationTest()
    {
        var appFactory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Replace sql database with inMemoryDb
                    var dbContextDescriptor = services.Single(s => s.ServiceType == typeof(DbContextOptions<TimeTrackerContext>));
                    services.Remove(dbContextDescriptor);
                    
                    services.AddDbContext<TimeTrackerContext>(
                        options => options.UseInMemoryDatabase("TestDb"),
                        ServiceLifetime.Scoped,
                        ServiceLifetime.Scoped);
                    
                    // Ensure clean db for every test
                    using (var scope = services.BuildServiceProvider().CreateScope())
                    {
                        var scopedServices = scope.ServiceProvider;
                        var db = scopedServices.GetRequiredService<TimeTrackerContext>();
                        db.Database.EnsureDeleted();
                        db.Database.EnsureCreated();
                    }
                });

            });

        httpClient = appFactory.CreateClient();

        jsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
        jsonSerializerOptions.PropertyNameCaseInsensitive = true;
    }

    protected async Task<TimeEntry> SetupTimeEntry()
    {
        var response = await httpClient.PostAsJsonAsync(
            "/timeEntry",
            TestHelper.GetTimeEntry(),
            jsonSerializerOptions);

        var timeEntry = await response.Content.ReadFromJsonAsync<TimeEntry>(jsonSerializerOptions);

        return timeEntry!;
    }
}
