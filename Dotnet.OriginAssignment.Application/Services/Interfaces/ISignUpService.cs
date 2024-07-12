using Dotnet.OriginAssignment.Domain.Models;
using Dotnet.OriginAssignment.Domain.Models.Requests;
using Dotnet.OriginAssignment.Domain.Models.Response;

namespace Dotnet.OriginAssignment.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<User> CreateUserAsync(User userRequest);
        Task<User> GetUserAsync(string userId);
        Task PatchUserAsync(string userId, Dictionary<string, string> fieldsToUpdate);
    }
}
