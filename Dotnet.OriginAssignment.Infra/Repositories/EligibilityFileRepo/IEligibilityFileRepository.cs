using Dotnet.OriginAssignment.Domain.Models.Requests;
using Dotnet.OriginAssignment.Infra.Models;

namespace Dotnet.OriginAssignment.Infra.Repositories.ProcessedLineRepo
{
    public interface IEligibilityFileRepository
    {
        Task<EligibilityFile> CreateEligibilityFile(EligibilityFile eligibilityFile);
    }
}
