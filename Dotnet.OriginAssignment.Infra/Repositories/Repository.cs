using Dotnet.OriginAssignment.Infra.Context;

namespace Dotnet.OriginAssignment.Infra.Repositories
{
    public class Repository : IRepository
    {
        private readonly ModelContext _context;

        public Repository(ModelContext context)
        {
            _context = context;
        }

        public ModelContext getContext()
        {
            return _context;
        }
    }
}
