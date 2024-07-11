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
    public class SignUpService(Configuration _config, IUnitOfWork _unitOfWork, IMapper _mapper, IValidator<Signup> _signupRequestValidator, IValidator<User> _userValidator) : ISignUpService
    {
        public async Task<GetUser> SignUp(Signup signupRequest)
        {
            var validationResult = _signupRequestValidator.Validate(signupRequest);

            if (!validationResult.IsValid)
            {
                var errorMessage = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new Exception(errorMessage);
            }

            // Check if the email is associated with some employer via the eligibility file
            var processedLine = await _unitOfWork.ProcessedLines.GetValidProcessedLineByEmailAsync(signupRequest.Email);
            User user;

            if (processedLine != null)
            {
                // Use employer-provided data to create the user
                user = new User
                {
                    Email = signupRequest.Email,
                    Password = signupRequest.Password,
                    Country = signupRequest.Country,
                    AccessType = Domain.Models.Enums.AccessType.Employer,
                    FullName = processedLine.FullName,
                    EmployerId = processedLine.EmployerId,
                    BirthDate = processedLine.BirthDate,
                    Salary = processedLine.Salary
                };
            }
            else
            {
                // Validate if the email already exists
                var existingUser = await GetUserByEmailAsync(signupRequest.Email);
                if (existingUser != null)
                {
                    throw new Exception("Email already exists.");
                }

                // Create the user with DTC access type
                user = new User
                {
                    Email = signupRequest.Email,
                    Password = signupRequest.Password,
                    Country = signupRequest.Country,
                    AccessType = Domain.Models.Enums.AccessType.DTC
                };
            }

            // Save the user to the User Service
            var createdUser = await CreateUserAsync(user);

            // Return the created user details
            return _mapper.Map<User, GetUser>(createdUser);
        }

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
    }
}
