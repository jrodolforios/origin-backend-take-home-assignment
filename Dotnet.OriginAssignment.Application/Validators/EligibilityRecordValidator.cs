using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using FluentValidation.Results;
using ValidationFailure = FluentValidation.Results.ValidationFailure;
using Dotnet.OriginAssignment.Domain.Models;

namespace Dotnet.OriginAssignment.Application.Validators
{
    public class EligibilityRecordValidator : AbstractValidator<EligibilityFileEntry>
    {
        public EligibilityRecordValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country is required.")
                .Length(2).WithMessage("Country code must be 2 characters long.");

            RuleFor(x => x.FullName)
                .MaximumLength(255).WithMessage("Full name must be less than 255 characters.");

            RuleFor(x => x.BirthDate)
                .Must(BeAValidDate).WithMessage("Invalid birth date format.")
                .When(x => x.BirthDate.HasValue);

            RuleFor(x => x.Salary)
                .GreaterThanOrEqualTo(0).WithMessage("Salary must be greater than or equal to 0.")
                .When(x => x.Salary.HasValue);
        }

        private bool BeAValidDate(DateTime? date)
        {
            return date.HasValue && date.Value <= DateTime.Now;
        }
    }
}
