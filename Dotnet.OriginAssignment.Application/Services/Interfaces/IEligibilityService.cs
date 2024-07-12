using Dotnet.OriginAssignment.Domain.Models;
using Dotnet.OriginAssignment.Domain.Models.Requests;
using Dotnet.OriginAssignment.Domain.Models.Response;

namespace Dotnet.OriginAssignment.Application.Services.Interfaces
{
    public interface IEligibilityService
    {
        Task<List<ProcessedLineReport>> ProcessEligibilityFileAsync(EligibilityFileRequest eligibilityFileRequest);
    }
}
