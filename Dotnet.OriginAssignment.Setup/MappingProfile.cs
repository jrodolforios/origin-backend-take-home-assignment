using AutoMapper;
using Dotnet.OriginAssignment.Domain.Models;
using Dotnet.OriginAssignment.Domain.Models.Response;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, GetUser>();
    }
}
