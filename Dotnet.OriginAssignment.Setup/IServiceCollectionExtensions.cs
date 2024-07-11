using Dotnet.OriginAssignment.Application.Services;
using Dotnet.OriginAssignment.Application.Services.Interfaces;
using Dotnet.OriginAssignment.Application.Validators;
using Dotnet.OriginAssignment.Domain.Configuration;
using Dotnet.OriginAssignment.Domain.Models;
using Dotnet.OriginAssignment.Domain.Models.Requests;
using Dotnet.OriginAssignment.Infra.Context;
using Dotnet.OriginAssignment.Infra.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Dotnet.OriginAssignment.Setup
{
    [ExcludeFromCodeCoverage]
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection Bootstrap(this IServiceCollection services, IConfiguration configuration)
        {
            var configApp = configuration.Get<Configuration>();

            try
            {
                services.AddDbContext<ModelContext>(options => options.UseInMemoryDatabase("CarDatabase"));
            }
            catch (Exception e)
            {
                throw e;
            }

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddTransient<IValidator<User>, UserValidator>();
            services.AddTransient<IValidator<Signup>, SignupRequestValidator>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<ISignUpService, SignUpService>();

            services.AddSingleton(configuration.Get<Configuration>());

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;

        }
    }
}