
using Azure.Core;
using Dotnet.OriginAssignment.Application.Services.Interfaces;
using Dotnet.OriginAssignment.Domain.Models;
using Dotnet.OriginAssignment.Domain.Models.Requests;
using Dotnet.OriginAssignment.Domain.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.OriginAssignment.Api.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class EligibilityController(IEligibilityService _eligibilityService) : Controller
    {
        [HttpPost(Name = "Process")]
        [ProducesResponseType(typeof(List<ProcessedLineReport>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SignUp(EligibilityFileRequest eligibilityRequest)
        {
            try
            {
                var result = await _eligibilityService.ProcessEligibilityFileAsync(eligibilityRequest); ;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
