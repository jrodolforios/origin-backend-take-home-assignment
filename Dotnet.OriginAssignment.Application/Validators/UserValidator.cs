using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using FluentValidation.Results;
using ValidationFailure = FluentValidation.Results.ValidationFailure;
using Dotnet.OriginAssignment.Domain.Models;

namespace Dotnet.OriginAssignment.Application.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email must be a valid email address.");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"[0-9]").WithMessage("Password must contain at least one number.")
                .Matches(@"[\W]").WithMessage("Password must contain at least one special character.");

            RuleFor(user => user.Country)
                .NotEmpty().WithMessage("Country is required.")
                .Length(2).WithMessage("Country must be a valid ISO 3166-1 alpha-2 country code.");

            RuleFor(user => user.AccessType)
                .NotEmpty().WithMessage("Access type is required.")
                .Must(value => value == "dtc" || value == "employer")
                .WithMessage("Access type must be either 'dtc' or 'employer'.");

            RuleFor(user => user.FullName)
                .MaximumLength(255).WithMessage("Full name must not exceed 255 characters.")
                .When(user => !string.IsNullOrEmpty(user.FullName));

            RuleFor(user => user.EmployerId)
                .MaximumLength(255).WithMessage("Employer ID must not exceed 255 characters.")
                .When(user => !string.IsNullOrEmpty(user.EmployerId));

            RuleFor(user => user.BirthDate)
                .LessThan(DateTime.Now).WithMessage("Birth date must be in the past.")
                .When(user => user.BirthDate.HasValue);

            RuleFor(user => user.Salary)
                .GreaterThan(0).WithMessage("Salary must be a positive value.")
                .When(user => user.Salary.HasValue);
        }

    }
}
