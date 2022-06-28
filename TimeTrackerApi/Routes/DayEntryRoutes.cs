namespace TimeTrackerApi.Routes;

using Microsoft.AspNetCore.Mvc;
using TimeTrackerApi.Models;
using TimeTrackerApi.Repositories;

public static class DayEntryRoutes
{
    public static WebApplication MapDayEntryRoutes(this WebApplication app)
    {
        app.MapPost("/dayEntry",
            async ([FromServices] DayEntryRepository repo, DayEntry entry) =>
            {
                var createdEntry = await repo.CreateAsync(entry);

                return Results.Ok(createdEntry);
            });

        return app;
    }
}
