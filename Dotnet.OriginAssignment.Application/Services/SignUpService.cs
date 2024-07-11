using Dotnet.OriginAssignment.Application.Services.Interfaces;
using Dotnet.OriginAssignment.Infra.Repositories;
using AutoMapper;
using Dotnet.OriginAssignment.Domain.Models;
using FluentValidation;
using Dotnet.OriginAssignment.Domain.Models.Requests;
using Dotnet.OriginAssignment.Domain.Models.Response;

namespace Dotnet.OriginAssignment.Application.Services
{
    public class SignUpService(IUnitOfWork _unitOfWork, IMapper _mapper, IValidator<Signup> _signupRequestValidator, UserRepository) : ISignUpService
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
            var employer = await _employerRepository.GetEmployerByEmailAsync(signupRequest.Email);
            User user;

            if (employer != null)
            {
                // Use employer-provided data to create the user
                user = new User
                {
                    Email = signupRequest.Email,
                    Password = signupRequest.Password,
                    Country = signupRequest.Country,
                    AccessType = Domain.Models.Enums.AccessType.Employer,
                    FullName = employer.FullName,
                    EmployerId = employer.EmployerId,
                    BirthDate = employer.BirthDate,
                    Salary = employer.Salary
                };
            }
            else
            {
                // Validate if the email already exists
                var existingUser = await _userRepository.GetUserByEmailAsync(signupRequest.Email);
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
            var createdUser = await _userRepository.CreateUserAsync(user);

            // Return the created user details
            return _mapper.Map<User, GetUser>(createdUser);
        }
    }
}
