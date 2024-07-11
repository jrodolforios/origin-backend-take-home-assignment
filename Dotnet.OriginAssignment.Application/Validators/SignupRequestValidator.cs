using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using FluentValidation.Results;
using ValidationFailure = FluentValidation.Results.ValidationFailure;
using Dotnet.OriginAssignment.Domain.Models.Requests;

namespace Dotnet.OriginAssignment.Application.Validators
{
    public class SignupRequestValidator : AbstractValidator<Signup>
    {
        public SignupRequestValidator()
        {
            RuleFor(request => request.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email must be a valid email address.");

            RuleFor(request => request.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"[0-9]").WithMessage("Password must contain at least one number.")
                .Matches(@"[\W]").WithMessage("Password must contain at least one special character.");

            RuleFor(request => request.Country)
                .NotEmpty().WithMessage("Country is required.")
                .Length(2).WithMessage("Country must be a valid ISO 3166-1 alpha-2 country code.");
        }
    }
}
