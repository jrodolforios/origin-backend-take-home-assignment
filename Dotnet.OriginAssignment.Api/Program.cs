using Dotnet.OriginAssignment.Api.Middlewares;
using Dotnet.OriginAssignment.Setup;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;

namespace Dotnet.OriginAssignment.Api
{
    [ExcludeFromCodeCoverage]
    internal static class Program
    {
        [ExcludeFromCodeCoverage]
        private static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            var configBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
               .AddEnvironmentVariables();

            var _config = configBuilder.Build();

            builder.Services.AddControllers(options =>
            {
                options.Conventions.Add(new RoutePrefixConvention());
            }).AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = $"UseOrigin Services | Enviroment : {builder.Environment.EnvironmentName.ToUpper()} | Branch: {Environment.GetEnvironmentVariable("BRANCH_NAME")?.ToString() ?? "Local"} | {System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription}" });

            });

            builder.Services.Bootstrap(_config);
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseCors("corsapp");
            }

            app.UseSwagger(x =>
            {
                x.RouteTemplate = "/cars/swagger/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/cars/swagger/v1/swagger.json", "Your API V1");
                c.RoutePrefix = "swagger";
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}