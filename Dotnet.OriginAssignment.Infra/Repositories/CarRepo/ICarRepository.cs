using Dotnet.OriginAssignment.Infra.Models;

namespace Dotnet.OriginAssignment.Infra.Repositories.CarRepo
{
    public interface ICarRepository
    {
        Task<List<Car>> GetCars();
        Task SaveCar(Car carEntity);
    }
}
