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
    .AddSingleton<TimeEntryRepository>()
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

app.MapGet("/timeEntry", ([FromServices] TimeEntryRepository repo) =>
{
    return repo.GetAtll();
});

app.MapGet("/timeEntry/{id}", ([FromServices] TimeEntryRepository repo, Guid id) =>
{
    var timeEntry = repo.GetById(id);
    return timeEntry is not null ? Results.Ok(timeEntry) : Results.NotFound();
});

app.MapPost("/timeEntry",
    ([FromServices] TimeEntryRepository repo, IValidator<TimeEntry> validator, TimeEntry entry) =>
{
    var validationResult = validator.Validate(entry);
    if(!validationResult.IsValid)
    {
        var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
        return Results.BadRequest(errors);
    }

    repo.Create(entry);

    return Results.Ok();
});

app.Run();