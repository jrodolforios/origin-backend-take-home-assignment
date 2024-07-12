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
    public class UserService(Configuration _config, IValidator<User> _userValidator) : IUserService
    {
        public async Task<User> GetUserByEmailAsync(string email)
        {
            var client = new RestClient(_config.ExternalApi.BaseUrl);
            var request = new RestRequest("users", Method.Get);
            request.AddParameter("email", email);

            var response = await client.ExecuteAsync<User>(request);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to retrieve user: {response.ErrorMessage}");
            }

            return response.Data;
        }

        public async Task<User> CreateUserAsync(User userRequest)
        {
            var validationResult = _userValidator.Validate(userRequest);

            if (!validationResult.IsValid)
            {
                var errorMessage = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new Exception(errorMessage);
            }

            var client = new RestClient(_config.ExternalApi.BaseUrl);
            var request = new RestRequest("users", Method.Post);
            request.AddJsonBody(userRequest);

            var response = await client.ExecuteAsync<User>(request);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to create user: {response.ErrorMessage}");
            }

            return response.Data;
        }

        public async Task<User> GetUserAsync(string userId)
        {
            var client = new RestClient(_config.ExternalApi.BaseUrl);
            var request = new RestRequest($"users/{userId}", Method.Get);

            var response = await client.ExecuteAsync<User>(request);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to get user {userId}: {response.ErrorMessage}");
            }

            return response.Data;
        }

        public async Task PatchUserAsync(string userId, Dictionary<string, string> fieldsToUpdate)
        {
            var client = new RestClient(_config.ExternalApi.BaseUrl);
            var request = new RestRequest($"/users/{userId}", Method.Patch)
                .AddJsonBody(fieldsToUpdate.Select(kvp => new { field = kvp.Key, value = kvp.Value }));

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Failed to update user fields: {response.Content}");
            }
        }
    }
}
