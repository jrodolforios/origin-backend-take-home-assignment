using Dotnet.OriginAssignment.Infra.Context;
using Dotnet.OriginAssignment.Infra.Repositories.CarRepo;

namespace Dotnet.OriginAssignment.Infra.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        public ICarRepository Cars { get; }

        ModelContext getContext();
    }
}
