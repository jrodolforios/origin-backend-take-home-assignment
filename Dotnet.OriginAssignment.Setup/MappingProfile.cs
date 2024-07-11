using AutoMapper;
using CarDto = Dotnet.OriginAssignment.Infra.Models.User;
using EngineDto = Dotnet.OriginAssignment.Infra.Models.Engine;
using WheelDto = Dotnet.OriginAssignment.Infra.Models.Wheel;
using InteriorDto = Dotnet.OriginAssignment.Infra.Models.Interior;
using Dotnet.OriginAssignment.Domain.Models.Models;
using Dotnet.OriginAssignment.Domain.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Mapping from EF models to DTOs
        CreateMap<User, Dotnet.OriginAssignment.Infra.Models.Car>();
        CreateMap<Engine, EngineDto>();
        CreateMap<Wheel, WheelDto>();
        CreateMap<Interior, InteriorDto>();

        // Mapping from DTOs to EF models (if needed)
        CreateMap<Dotnet.OriginAssignment.Infra.Models.Car, User>();
        CreateMap<EngineDto, Engine>();
        CreateMap<WheelDto, Wheel>();
        CreateMap<InteriorDto, Interior>();
    }
}
