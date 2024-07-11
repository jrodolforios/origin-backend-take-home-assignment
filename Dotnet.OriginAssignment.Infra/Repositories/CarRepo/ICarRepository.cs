using Dotnet.OriginAssignment.Infra.Models;

namespace Dotnet.OriginAssignment.Infra.Repositories.CarRepo
{
    public interface ICarRepository
    {
        Task<List<User>> GetCars();
        Task SaveCar(User carEntity);
    }
}
