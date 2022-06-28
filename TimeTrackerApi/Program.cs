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
using TimeTrackerApi.Routes;
using TimeTrackerApi.Specifications;

const string CorsPolicyName = "AllowLocalHost";

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<Program>();

//Services
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
//builder.Services.AddAuthorization(options =>
//{
//    options.FallbackPolicy = new AuthorizationPolicyBuilder()
//        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
//        .RequireAuthenticatedUser()
//        .Build();
//});

builder.Services
    .AddCors(options =>
    {
        options.AddDefaultPolicy(policy =>
        {
            //policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            policy.WithOrigins("http://localhost:3000").WithMethods("POST", "PUT", "DELETE").WithHeaders("content-type");
        });

    })
    .AddTransient<TimeEntryRepository>()
    //.AddScoped(typeof(EfRepository<>))
    //.AddScoped(typeof(IRepository<>), typeof(CachedRepository<>))
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
app.UseRouting();
app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();

//app.UseAuthentication();
//app.UseAuthorization();

app.InitTimeEntryRoutes();

//app.MapGet("/", () => "Hi :)");

//app.MapGet("/timeEntries",
//    async ([FromServices] TimeEntryRepository repo) =>
//    {
//        var timeEntries = await repo.GetAllAsync();
//        return Results.Ok(timeEntries);
//    });

//app.MapGet("/timeEntries2",
//    async ([FromServices] IRepository<TimeEntry> repo, ILogger<Program> logger) =>
//    {
//        logger.LogInformation("Get all time entries requested.");
//        //var spec = new TimeEntriesSpecification();
//        //var timeEntries = await repo.ListAsync(spec);
//        var timeEntries = await repo.ListAsync();
//        return Results.Ok(timeEntries);
//    })
//    .AllowAnonymous();

//app.MapGet("/timeEntry/{id}",
//    async ([FromServices] TimeEntryRepository repo, Guid id) =>
//    {
//        var timeEntry = await repo.GetById(id);
//        return timeEntry is not null ? Results.Ok(timeEntry) : Results.NotFound();
//    });

//app.MapGet("/timeEntry2/{id}",
//    async ([FromServices] IRepository<TimeEntry> repo, Guid id) =>
//    {
//        var timeEntry = await repo.GetByIdAsync(id);
//        return timeEntry is not null ? Results.Ok(timeEntry) : Results.NotFound();
//    })
//    .AllowAnonymous();

//app.MapPost("/timeEntry",
//    async ([FromServices] TimeEntryRepository repo, IValidator<TimeEntry> validator, HttpRequest req, TimeEntry entry) =>
//    {
//        var validationResult = validator.Validate(entry);
//        if (!validationResult.IsValid)
//        {
//            var errors = new { errors = validationResult.Errors.Select(x => x.ErrorMessage) };
//            return Results.BadRequest(errors);
//        }

//        var createdEntry = await repo.CreateAsync(entry);

//        return Results.Ok(createdEntry);
//    });

//app.MapPut("/timeEntry/{id}",
//    async ([FromServices] TimeEntryRepository repo, IValidator<TimeEntry> validator, TimeEntry entry, Guid id) =>
//    {
//        if (id == Guid.Empty)
//        {
//            return Results.BadRequest("No valid id provided.");
//        }
//        entry.Id = id;

//        var validationResult = validator.Validate(entry);
//        if (!validationResult.IsValid)
//        {
//            var errors = new { errors = validationResult.Errors.Select(x => x.ErrorMessage) };
//            return Results.BadRequest(errors);
//        }

//        var result = await repo.UpdateAsync(entry);

//        return result.Match(
//            updatedEntry => Results.Ok(updatedEntry),
//            error => Results.BadRequest(error.Message));
//    })
//    .AllowAnonymous();

//app.MapDelete("/timeEntry/{id}",
//    async ([FromServices] TimeEntryRepository repo, Guid id) =>
//    {
//        var result = await repo.DeleteById(id);

//        return result.Match(
//            success => Results.NoContent(),
//            error => Results.BadRequest(error.Message));
//    })
//    .AllowAnonymous();

app.Run();