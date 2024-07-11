using Dotnet.OriginAssignment.Infra.Context;
using Dotnet.OriginAssignment.Infra.Repositories.ProcessedLineRepo;

namespace Dotnet.OriginAssignment.Infra.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        public IProcessedLineRepository ProcessedLines { get; }

        ModelContext getContext();
    }
}
