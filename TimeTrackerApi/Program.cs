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

app.MapGet("/", () => "TimeTracker API is running.");

app.Run();