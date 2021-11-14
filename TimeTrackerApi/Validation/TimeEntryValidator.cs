namespace TimeTrackerApi.Validation;

using FluentValidation;
using TimeTrackerApi.Models;

public class TimeEntryValidator : AbstractValidator<TimeEntry>
{
    public TimeEntryValidator()
    {
        RuleFor(x => x.StartHours).InclusiveBetween(0, 23);
        RuleFor(x => x.StartMinutes).InclusiveBetween(0, 59);

        RuleFor(x => x.EndHours).InclusiveBetween(0, 23);
        RuleFor(x => x.EndHours).GreaterThanOrEqualTo(x => x.StartHours);
        RuleFor(x => x.EndMinutes).InclusiveBetween(0, 59);

        RuleFor(x => x).Must(x =>
        {
            if (x.EndHours == x.StartHours)
            {
                return x.EndMinutes > x.StartMinutes;
            }

            return true;
        }).WithMessage("EndMinutes must be greater than startMinutes when end- & startHours are equal.");

        RuleFor(x => x.PauseHours).InclusiveBetween(0, 23.75);
        RuleFor(x => x.PauseHours).Must(x =>
        {
            var rest = x - Convert.ToInt32(x);
            return rest switch
            {
                0 => true,
                _ => rest % 0.25 == 0
            };
        }).WithMessage("Minutes can only be entered in quarters of an hour => [.0, .25, .50, .75]");


        RuleFor(x => x.TotalHours.Ticks).GreaterThan(0).WithMessage("PauseTime must fit between start- &  endTime.");
    }
}