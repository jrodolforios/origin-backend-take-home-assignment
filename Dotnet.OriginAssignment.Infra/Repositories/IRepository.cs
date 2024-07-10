using Dotnet.OriginAssignment.Infra.Context;

namespace Dotnet.OriginAssignment.Infra.Repositories
{
    public interface IRepository
    {
        ModelContext getContext();
    }
}
