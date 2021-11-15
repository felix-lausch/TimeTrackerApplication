using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TimeTrackerApi;
using TimeTrackerApi.Models;
using TimeTrackerApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<Program>();

//Services
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build();
});

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
    .AddSwaggerGen()
    .AddControllers()
    .AddFluentValidation(config =>
    {
        config.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        ValidatorOptions.Global.LanguageManager.Enabled = false;
    });

var app = builder.Build();

//Uses
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Hi :)").AllowAnonymous();

app.MapGet("/timeEntries",
    async ([FromServices] TimeEntryRepository repo) =>
    {
        var timeEntries = await repo.GetAtllAsync();
        return Results.Ok(timeEntries);
    })
    .AllowAnonymous();

app.MapGet("/timeEntry/{id}",
    async ([FromServices] TimeEntryRepository repo, Guid id) =>
    {
        var timeEntry = await repo.GetById(id);
        return timeEntry is not null ? Results.Ok(timeEntry) : Results.NotFound();
    })
    .AllowAnonymous();

app.MapPost("/timeEntry",
    async ([FromServices] TimeEntryRepository repo, IValidator<TimeEntry> validator, TimeEntry entry) =>
    {
        var validationResult = validator.Validate(entry);
        if(!validationResult.IsValid)
        {
            var errors = new { errors = validationResult.Errors.Select(x => x.ErrorMessage) };
            return Results.BadRequest(errors);
        }

        var createdEntry = await repo.CreateAsync(entry);

        return Results.Ok(createdEntry);
    })
    .AllowAnonymous();

app.Run();