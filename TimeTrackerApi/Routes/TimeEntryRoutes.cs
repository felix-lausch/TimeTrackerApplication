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

        app.MapGet("/timeEntries2",
            async ([FromServices] IRepository<TimeEntry> repo, ILogger<Program> logger) =>
            {
                logger.LogInformation("Get all time entries requested.");
                //var spec = new TimeEntriesSpecification();
                //var timeEntries = await repo.ListAsync(spec);
                var timeEntries = await repo.ListAsync();
                        return Results.Ok(timeEntries);
                    })
            .AllowAnonymous();

        app.MapGet("/timeEntry/{id}",
            async ([FromServices] TimeEntryRepository repo, Guid id) =>
            {
                var timeEntry = await repo.GetByIdAsync(id);
                return timeEntry is not null ? Results.Ok(timeEntry) : Results.NotFound();
            });

        app.MapGet("/timeEntry2/{id}",
            async ([FromServices] IRepository<TimeEntry> repo, Guid id) =>
            {
                var timeEntry = await repo.GetByIdAsync(id);
                return timeEntry is not null ? Results.Ok(timeEntry) : Results.NotFound();
            })
            .AllowAnonymous();

        app.MapPost("/timeEntry",
            async ([FromServices] TimeEntryRepository repo, IValidator<TimeEntry> validator, HttpRequest req, TimeEntry entry) =>
            {
                var validationResult = validator.Validate(entry);
                if (!validationResult.IsValid)
                {
                    var errors = new { errors = validationResult.Errors.Select(x => x.ErrorMessage) };
                    return Results.BadRequest(errors);
                }

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

                var validationResult = validator.Validate(entry);
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
                    success => Results.NoContent(),
                    error => Results.BadRequest(error.Message));
            })
            .AllowAnonymous();

        return app;
    }
}
