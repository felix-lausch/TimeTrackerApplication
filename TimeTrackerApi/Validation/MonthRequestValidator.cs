namespace TimeTrackerApi.Validation;

using FluentValidation;
using TimeTrackerApi.Dtos;

public class MonthRequestValidator : AbstractValidator<MonthRequest>
{
    public MonthRequestValidator()
    {
        RuleFor(x => x.Month).InclusiveBetween(1, 12);
        RuleFor(x => x.Year).InclusiveBetween(1, 9999);
    }
}
