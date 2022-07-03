namespace TimeTrackerApi.Routes;

using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TimeTrackerApi.Dtos;
using TimeTrackerApi.Services;

public static class MonthRoutes
{
    public static WebApplication MapMonthRoutes(this WebApplication app)
    {
        app.MapGet("/month",
            async ([FromServices] MonthService monthService, IValidator<MonthRequest> validator, [FromQuery] int month, [FromQuery] int year) =>
            {
                var monthRequest = new MonthRequest(month, year);

                var validationResult = await validator.ValidateAsync(monthRequest);
                if (!validationResult.IsValid)
                {
                    var errors = new { errors = validationResult.Errors.Select(x => x.ErrorMessage) };
                    return Results.BadRequest(errors);
                }

                var monthResponse = await monthService.GetMonthByMonthAndYear(monthRequest.Month, monthRequest.Year);

                return Results.Ok(monthResponse);
            });

        app.MapGet("/month/current",
            async ([FromServices] MonthService monthService) =>
            {
                var dateTime = DateTime.UtcNow;

                var monthDto = await monthService.GetMonthByMonthAndYear(dateTime.Month, dateTime.Year);

                return Results.Ok(monthDto);
            });

        return app;
    }
}
