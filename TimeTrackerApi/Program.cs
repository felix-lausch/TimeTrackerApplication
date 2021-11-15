using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TimeTrackerApi;
using TimeTrackerApi.Models;
using TimeTrackerApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<Program>();

builder.Services
    .AddTransient<TimeEntryRepository>()
    .AddDbContext<TimeTrackerContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("SqlDb"));
        options.EnableSensitiveDataLogging();
    },
    ServiceLifetime.Scoped,
    ServiceLifetime.Scoped)
    .AddEndpointsApiExplorer()
    .AddControllers()
    .AddFluentValidation(config =>
    {
        config.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        ValidatorOptions.Global.LanguageManager.Enabled = false;
    });

var app = builder.Build();

app.MapGet("/", () => "Hi :)");

app.MapGet("/timeEntries",
    async ([FromServices] TimeEntryRepository repo) =>
    {
        return await repo.GetAtllAsync();
    });

app.MapGet("/timeEntry/{id}",
    async ([FromServices] TimeEntryRepository repo, Guid id) =>
    {
        var timeEntry = await repo.GetById(id);
        return timeEntry is not null ? Results.Ok(timeEntry) : Results.NotFound();
    });

app.MapPost("/timeEntry",
    async ([FromServices] TimeEntryRepository repo, IValidator<TimeEntry> validator, TimeEntry entry) =>
    {
        var validationResult = validator.Validate(entry);
        if(!validationResult.IsValid)
        {
            //var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
            return Results.BadRequest(validationResult.ToString());
        }

        var createdEntry = await repo.CreateAsync(entry);

        return Results.Ok(createdEntry);
    });

app.Run();