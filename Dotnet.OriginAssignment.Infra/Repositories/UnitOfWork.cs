using Dotnet.OriginAssignment.Infra.Context;
using Dotnet.OriginAssignment.Infra.Repositories.ProcessedLineRepo;

namespace Dotnet.OriginAssignment.Infra.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ModelContext _context;

        public IProcessedLineRepository ProcessedLines { get; }
        public IEligibilityFileRepository EligibilityFiles { get; }

        public UnitOfWork(ModelContext modelContext)
        {
            _context = modelContext;

            ProcessedLines = new ProcessedLineRepository(this);
            EligibilityFiles = new EligibilityFileRepository(this);
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
