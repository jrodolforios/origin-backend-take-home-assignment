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
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;
using System.Globalization;
using System.Net.Http;
using CsvHelper;
using CsvHelper.Configuration;
using Dotnet.OriginAssignment.Application.Mappers;
using Azure.Core;
using Dotnet.OriginAssignment.Application.Validators;

namespace Dotnet.OriginAssignment.Application.Services
{
    public class EligibilityService(Configuration _config, IUserService _userService, IEmployerService _employerService, IUnitOfWork _unitOfWork, IMapper _mapper, IHttpClientFactory _httpClientFactory, IValidator<EligibilityFileRequest> _eligibilityFileRequestValidator, IValidator<EligibilityFileEntry> _eligibilityRecordValidator) : IEligibilityService
    {
        public async Task<List<ProcessedLineReport>> ProcessEligibilityFileAsync(EligibilityFileRequest eligibilityFileRequest)
        {
            var processedLines = new List<ProcessedLineReport>();

            var validationResult = _eligibilityFileRequestValidator.Validate(eligibilityFileRequest);
            if (!validationResult.IsValid)
            {
                var errorMessage = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new Exception(errorMessage);
            }
            var employer = await _employerService.GetEmployerIdByNameAsync(eligibilityFileRequest.EmployerName);

            var eligibilityFile = await _unitOfWork.EligibilityFiles.CreateEligibilityFile(new EligibilityFile() { EmployerId = employer, FileUrl = eligibilityFileRequest.FileUrl});

            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(eligibilityFileRequest.FileUrl);
            response.EnsureSuccessStatusCode();

            using (var stream = await response.Content.ReadAsStreamAsync())
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.Context.RegisterClassMap<EligibilityFileMap>();
                var records = csv.GetRecords<EligibilityFileEntry>();

                foreach (var record in records)
                {
                    var errorMessage = string.Empty;
                    ProcessedLine processedLine = null;

                    var validationResultEligibilityRecord = _eligibilityRecordValidator.Validate(record);
                    if (!validationResultEligibilityRecord.IsValid)
                    {
                        errorMessage = string.Join("; ", validationResultEligibilityRecord.Errors.Select(e => e.ErrorMessage));
                    }

                    var user = await _userService.GetUserByEmailAsync(record.Email);
                    if (user != null)
                    {
                        var valuesToChange = new Dictionary<string, string>
                        {
                            { "Country", record.Country },
                            { "Salary", record.Salary.ToString() },
                            { "FullName", record.FullName },
                            { "BirthDate", record.BirthDate.ToString() },
                            { "Salary", record.Salary.ToString() }
                        };

                        await _userService.PatchUserAsync(user.Id, valuesToChange);
                    }
                    else
                    {
                        processedLine = new ProcessedLine
                        {
                            Email = record.Email,
                            Country = record.Country,                            
                            FullName = record.FullName,
                            EmployerId = employer,
                            BirthDate = record.BirthDate,
                            Salary = record.Salary,
                            EligibilityFileId = eligibilityFile.Id,
                            Success = validationResultEligibilityRecord.IsValid,
                            ErrorMessage = errorMessage

                        };
                        processedLine = await _unitOfWork.ProcessedLines.CreateProcessedLine(processedLine);

                        processedLines.Add(new ProcessedLineReport()
                        {
                            Email = processedLine.Email,
                            ErrorMessage = processedLine.ErrorMessage,
                            Success = processedLine.Success
                        });
                    }
                }

                await _unitOfWork.getContext().SaveChangesAsync();
            }

            await _unitOfWork.getContext().SaveChangesAsync();
            return processedLines;
        }
    }
}
