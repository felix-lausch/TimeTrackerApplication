using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TimeTrackerApi;
using TimeTrackerApi.Repositories;
using TimeTrackerApi.Routes;
using TimeTrackerApi.Util;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Configuration.AddUserSecrets<Program>();

    builder.Services
        .Configure<JsonOptions>(options =>
        {
            options.SerializerOptions.Converters.Add(new DateOnlyJsonConverter());
        })
        .AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy
                    .WithOrigins("http://localhost:3000")
                    .WithMethods("POST", "PUT", "DELETE")
                    .WithHeaders("content-type");
            });

        })
        .AddScoped<TimeEntryRepository>()
        .AddScoped<DayEntryRepository>()
        .AddDbContext<TimeTrackerContext>(options =>
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
}

var app = builder.Build();
{
    app.UseRouting();
    app.UseCors();

    app.MapTimeEntryRoutes();
    app.MapDayEntryRoutes();

    app.MapGet("/", () => "TimeTracker API is reachable.");

    app.Run();
}
