namespace TimeTrackerApi.Routes;

using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TimeTrackerApi.Models;
using TimeTrackerApi.Repositories;

public static class TimeEntryRoutes
{
    public static WebApplication MapTimeEntryRoutes(this WebApplication app)
    {
        app.MapGet("/timeEntries",
            async ([FromServices] TimeEntryRepository repo, HttpRequest req) =>
            {
                var timeEntries = await repo.GetAllAsync();
                return Results.Ok(timeEntries);
            });

        app.MapGet("/timeEntry/{id}",
            async ([FromServices] TimeEntryRepository repo, Guid id) =>
            {
                var timeEntry = await repo.GetByIdAsync(id);
                return timeEntry is not null ? Results.Ok(timeEntry) : Results.NotFound();
            });

        app.MapPost("/timeEntry",
            async ([FromServices] TimeEntryRepository repo, IValidator<TimeEntry> validator, HttpRequest req, TimeEntry entry) =>
            {
                var validationResult = await validator.ValidateAsync(entry);
                if (!validationResult.IsValid)
                {
                    var errors = new { errors = validationResult.Errors.Select(x => x.ErrorMessage) };
                    return Results.BadRequest(errors);
                }

                entry.Id = Guid.NewGuid();

                var createdEntry = await repo.CreateAsync(entry);

                return Results.Ok(createdEntry);
            });

        app.MapPut("/timeEntry/{id}",
            async ([FromServices] TimeEntryRepository repo, IValidator<TimeEntry> validator, TimeEntry entry, Guid id) =>
            {
                if (id == Guid.Empty)
                {
                    return Results.BadRequest("No valid id provided.");
                }
                entry.Id = id;

                var validationResult = await validator.ValidateAsync(entry);
                if (!validationResult.IsValid)
                {
                    var errors = new { errors = validationResult.Errors.Select(x => x.ErrorMessage) };
                    return Results.BadRequest(errors);
                }

                var result = await repo.UpdateAsync(entry);

                return result.Match(
                    updatedEntry => Results.Ok(updatedEntry),
                    error => Results.BadRequest(error.Message));
            })
            .AllowAnonymous();

        app.MapDelete("/timeEntry/{id}",
            async ([FromServices] TimeEntryRepository repo, Guid id) =>
            {
                var result = await repo.DeleteByIdAsync(id);

                return result.Match(
                    timeEntry => Results.Ok(timeEntry),
                    error => Results.BadRequest(error.Message));
            })
            .AllowAnonymous();

        return app;
    }
}
