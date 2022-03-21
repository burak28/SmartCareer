using FluentValidation;

namespace SmartCareer.Models;

public class UserRequest
{
    public string MailAddress { get; set; }
    public string Password { get; set; }
}

public class UserRequestValidator : AbstractValidator<UserRequest>
{
    public UserRequestValidator()
    {
        RuleFor(x => x.MailAddress).NotNull().EmailAddress();
        RuleFor(x => x.Password).NotNull().MaximumLength(20);
    }
}