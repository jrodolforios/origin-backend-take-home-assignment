using Dotnet.OriginAssignment.Application.Services.Interfaces;
using Dotnet.OriginAssignment.Infra.Repositories;
using AutoMapper;
using Dotnet.OriginAssignment.Infra.Models;
using FluentValidation;
using Dotnet.OriginAssignment.Domain.Models.Requests;
using Dotnet.OriginAssignment.Domain.Models.Response;
using Dotnet.OriginAssignment.Domain.Models;
using RestSharp;
using Dotnet.OriginAssignment.Domain.Configuration;

namespace Dotnet.OriginAssignment.Application.Services
{
    public class EmployerService(Configuration _config) : IEmployerService
    {
        public async Task<string> GetEmployerIdByNameAsync(string employerName)
        {
            var client = new RestClient(_config.ExternalApi.BaseUrl);
            var request = new RestRequest("/employers", Method.Get);
            request.AddParameter("name", employerName);

            var response = await client.ExecuteAsync<GetEmployerResponse>(request);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to get employer ID: {response.Content}");
            }

            return response.Data?.Id;
        }
    }
}
