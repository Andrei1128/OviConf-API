using FluentValidation;

namespace DOMAIN.DTOs;

public class ConferenceDTO
{
    public string Name { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
public class ConferenceDTOValidator : AbstractValidator<ConferenceDTO>
{
    public ConferenceDTOValidator()
    {
        RuleFor(x => x.Name).NotEmpty().Length(3, 255)
            .WithMessage("Name has to have at least 3 characters!");
        RuleFor(x => x.StartDate).NotEmpty()
            .WithMessage("Start date is required!");
        RuleFor(x => x.EndDate).NotEmpty()
            .WithMessage("Start end is required!");
        RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate)
            .WithMessage("End date has to be greater than start date!");
    }
}
