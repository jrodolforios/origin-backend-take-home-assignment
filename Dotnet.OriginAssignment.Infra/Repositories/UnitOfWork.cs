using Dotnet.OriginAssignment.Infra.Context;
using Dotnet.OriginAssignment.Infra.Repositories.ProcessedLineRepo;

namespace Dotnet.OriginAssignment.Infra.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ModelContext _context;

        public IProcessedLineRepository ProcessedLines { get; }

        public UnitOfWork(ModelContext modelContext)
        {
            _context = modelContext;

            ProcessedLines = new ProcessedLineRepository(this);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public ModelContext getContext()
        {
            return _context;
        }
    }
}
