using FluentValidation;
using TechLibrary.Communication.Requests;

namespace TechLibrary.Api.UseCases.Users.Register;

public class RegisterUserValidator : AbstractValidator<RequestUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(request => request.Name).NotEmpty().WithMessage("Name must not be empty");
        RuleFor(request => request.Email).EmailAddress().WithMessage("Email is not valid.");
        RuleFor(request => request.Password).NotEmpty().WithMessage("Password must not be empty");
        When(request => string.IsNullOrEmpty(request.Password) == false, () =>
        {
            RuleFor(request => request.Password.Length).GreaterThanOrEqualTo(6).WithMessage("Password " +
                "must contain more than 6 characters");
        });
    }
}