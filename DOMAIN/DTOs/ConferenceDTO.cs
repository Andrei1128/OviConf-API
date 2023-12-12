using FluentValidation;

namespace DOMAIN.DTOs;

public class ConferenceDTO
{
    public string Name { get; set; } = string.Empty;
}
public class ConferenceDTOValidator : AbstractValidator<ConferenceDTO>
{
    public ConferenceDTOValidator()
    {
        RuleFor(x => x.Name).NotEmpty().Length(3, 255)
            .WithMessage("Name has to have at least 3 characters!");
    }
}
