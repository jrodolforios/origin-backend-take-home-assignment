using Dotnet.OriginAssignment.Infra.Models;

namespace Dotnet.OriginAssignment.Infra.Repositories.ProcessedLineRepo
{
    public interface IProcessedLineRepository
    {
        Task<ProcessedLine> GetValidProcessedLineByEmailAsync(string email);
    }
}
