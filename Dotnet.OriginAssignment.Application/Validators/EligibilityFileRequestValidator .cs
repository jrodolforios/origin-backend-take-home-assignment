using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using FluentValidation.Results;
using ValidationFailure = FluentValidation.Results.ValidationFailure;
using Dotnet.OriginAssignment.Domain.Models;
using Dotnet.OriginAssignment.Domain.Models.Requests;

namespace Dotnet.OriginAssignment.Application.Validators
{
    public class EligibilityFileRequestValidator : AbstractValidator<EligibilityFileRequest>
    {
        public EligibilityFileRequestValidator()
        {
            RuleFor(x => x.FileUrl)
                .NotEmpty().WithMessage("File URL is required.")
                .Must(IsValidUrl).WithMessage("Invalid URL format.");

            RuleFor(x => x.EmployerName)
                .NotEmpty().WithMessage("Employer name is required.")
                .MaximumLength(255).WithMessage("Employer name must be less than 255 characters.");
        }

        private bool IsValidUrl(string fileUrl)
        {
            return Uri.TryCreate(fileUrl, UriKind.Absolute, out var uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
