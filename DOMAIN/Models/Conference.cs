using FluentValidation;

namespace DOMAIN.Models;

public class Conference
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Place { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime RegistrationTill { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
}

public class ConferenceValidator : AbstractValidator<Conference>
{
    public ConferenceValidator()
    {
        RuleFor(x => x.Name).NotEmpty().Length(3, 255)
            .WithMessage("Name has to have at least 3 characters!");
        RuleFor(x => x.Place).NotEmpty().Length(3, 255)
            .WithMessage("Place has to have at least 3 characters!");
        RuleFor(x => x.RegistrationTill).NotEmpty()
            .WithMessage("Registration till date is required!");
        RuleFor(x => x.StartDate).NotEmpty()
            .WithMessage("Start date is required!");
        RuleFor(x => x.EndDate).NotEmpty()
            .WithMessage("End date is required!");
        RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate)
            .WithMessage("End date has to be greater than start date!");
    }
}
