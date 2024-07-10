
using Dotnet.OriginAssignment.Application.Services.Interfaces;
using Dotnet.OriginAssignment.Domain.Models;
using Dotnet.OriginAssignment.Domain.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.OriginAssignment.Api.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class SignUpController(ISignUpService _signUpService) : Controller
    {
        [HttpPost(Name = "SignUp")]
        [ProducesResponseType(typeof(List<GetUser>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SignUp(Signup signupRequest)
        {
            try
            {
                var result = await _signUpService.SignUp(signupRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
