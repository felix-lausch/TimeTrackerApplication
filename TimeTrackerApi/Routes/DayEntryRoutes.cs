namespace TimeTrackerApi.Routes;

using Microsoft.AspNetCore.Mvc;
using TimeTrackerApi.Models;
using TimeTrackerApi.Repositories;

public static class DayEntryRoutes
{
    public static WebApplication MapDayEntryRoutes(this WebApplication app)
    {
        app.MapGet("/dayEntries",
            ([FromServices] DayEntryRepository repo, [FromQuery] int month, [FromQuery] int year) =>
            {
                if (month < 1 || month > 12)
                {
                    return Results.BadRequest("Month must be between 1 & 12.");
                }

                if (year < 1 || year > 9999)
                {
                    return Results.BadRequest("Year must be between 1 & 9999.");
                }

                var dayEntries = repo.GetByMonthAndYearAsync(month, year);

                return Results.Ok(dayEntries);
            });

        app.MapGet("/dayEntries/current",
            ([FromServices] DayEntryRepository repo) =>
            {
                var dateTime = DateTime.UtcNow;

                var dayEntries = repo.GetByMonthAndYearAsync(dateTime.Month, dateTime.Year);

                return Results.Ok(dayEntries);
            });

        app.MapPost("/dayEntry",
            async ([FromServices] DayEntryRepository repo, DayEntry entry) =>
            {
                var createdEntry = await repo.CreateAsync(entry);

                return Results.Ok(createdEntry);
            });

        return app;
    }
}
