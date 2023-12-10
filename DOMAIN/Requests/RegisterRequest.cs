using FluentValidation;

namespace DOMAIN.Requests;

public class RegisterRequest
{
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string RePassword { get; set; } = string.Empty;
}
public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().Length(3, 255)
            .WithMessage("Name has to have at least 3 characters!");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().Length(3, 255)
            .WithMessage("Invalid email address!");
        RuleFor(x => x.Password).NotEmpty().Length(8, 50).Matches(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).+$")
            .WithMessage("Password has to have at least 1 lowercase, 1 uppercase and 1 digit!");
        RuleFor(x => x.RePassword).NotEmpty().Equal(x => x.Password)
            .WithMessage("Passwords does not match!");
    }
}
