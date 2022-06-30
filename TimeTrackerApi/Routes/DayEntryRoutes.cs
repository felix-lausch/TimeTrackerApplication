﻿namespace TimeTrackerApi.Routes;

using Microsoft.AspNetCore.Mvc;
using TimeTrackerApi.Models;
using TimeTrackerApi.Repositories;
using TimeTrackerApi.Services;

public static class DayEntryRoutes
{
    public static WebApplication MapDayEntryRoutes(this WebApplication app)
    {
        app.MapGet("/dayEntries",
            async ([FromServices] MonthService monthService, [FromQuery] int month, [FromQuery] int year) =>
            {
                if (month < 1 || month > 12)
                {
                    return Results.BadRequest("Month must be between 1 & 12.");
                }

                if (year < 1 || year > 9999)
                {
                    return Results.BadRequest("Year must be between 1 & 9999.");
                }

                var dtos = await monthService.GetTimeEntriesForMonthAndYear(month, year);

                return Results.Ok(dtos);
            });

        app.MapGet("/dayEntries/current",
            async ([FromServices] MonthService monthService) =>
            {
                var dateTime = DateTime.UtcNow;

                var timeEntriesByDayDtos = await monthService.GetTimeEntriesForMonthAndYear(dateTime.Month, dateTime.Year);

                return Results.Ok(timeEntriesByDayDtos);
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
