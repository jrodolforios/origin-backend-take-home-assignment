using Dotnet.OriginAssignment.Infra.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Dotnet.OriginAssignment.Infra.Repositories.CarRepo
{
    public class CarRepository : ICarRepository
    {
        private IUnitOfWork _unitOfWork;
        public CarRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<User>> GetCars()
        {
            var context = _unitOfWork.getContext();

            var result = await context.Cars.ToListAsync();

            return result;
        }

        public async Task SaveCar(User carEntity)
        {
            var context = _unitOfWork.getContext();

            context.Cars.Add(carEntity);
            await context.SaveChangesAsync();
        }
    }
}
