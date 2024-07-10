using Dotnet.OriginAssignment.Domain.Models;
using Dotnet.OriginAssignment.Domain.Models.Models;

namespace Dotnet.OriginAssignment.Application.Services.Interfaces
{
    public interface ISignUpService
    {
        Task<GetUser> SignUp(Signup signupRequest);
    }
}
