namespace TimeTrackerApi.IntegrationTests;

using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
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
                    var dbContextDescriptor = services.Single(s => s.ServiceType == typeof(DbContextOptions<TimeTrackerContext>));
                    var removedSuccessful = services.Remove(dbContextDescriptor);
                    
                    services.AddDbContext<TimeTrackerContext>(
                        options => options.UseInMemoryDatabase("TestDb"),
                        ServiceLifetime.Scoped,
                        ServiceLifetime.Scoped);
                });
            });

        httpClient = appFactory.CreateClient();

        jsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    }

    protected async Task CreateTimeEntry()
    {
        var timeEntry = new TimeEntry
        {
            Date = new DateOnly(2020, 3, 4),
            StartHours = 7,
            StartMinutes = 30,
            EndHours = 17,
            EndMinutes = 0,
            PauseHours = 1.5,
        };

        await httpClient.PostAsJsonAsync(
            "/timeEntry",
            timeEntry,
            jsonSerializerOptions);
    }
}
