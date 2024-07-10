using Dotnet.OriginAssignment.Infra.Context;
using Dotnet.OriginAssignment.Infra.Repositories.CarRepo;

namespace Dotnet.OriginAssignment.Infra.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ModelContext _context;

        public ICarRepository Cars { get; }

        public UnitOfWork(ModelContext modelContext)
        {
            _context = modelContext;

            Cars = new CarRepository(this);
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
