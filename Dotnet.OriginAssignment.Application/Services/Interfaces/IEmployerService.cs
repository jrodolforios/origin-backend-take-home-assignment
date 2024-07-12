using Dotnet.OriginAssignment.Domain.Models;
using Dotnet.OriginAssignment.Domain.Models.Requests;
using Dotnet.OriginAssignment.Domain.Models.Response;

namespace Dotnet.OriginAssignment.Application.Services.Interfaces
{
    public interface IEmployerService
    {
        Task<string> GetEmployerIdByNameAsync(string employerName);
    }
}
