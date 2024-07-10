using Dotnet.OriginAssignment.Application.Services.Interfaces;
using Dotnet.OriginAssignment.Domain.Models.Models;
using Dotnet.OriginAssignment.Infra.Repositories;
using AutoMapper;
using Dotnet.OriginAssignment.Domain.Models;

namespace Dotnet.OriginAssignment.Application.Services
{
    public class SignUpService(IUnitOfWork _unitOfWork, IMapper _mapper) : ISignUpService
    {

        public Task<GetUser> SignUp(Signup signupRequest)
        {
            throw new NotImplementedException();
        }
    }
}
