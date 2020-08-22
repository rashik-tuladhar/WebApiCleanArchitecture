using System.Threading.Tasks;
using Application.DTOs.Authentication;
using Application.Interfaces.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Route("api/v1/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger _log = Log.ForContext<AuthenticationController>();
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("token")]
        public async Task<IActionResult> AuthenticationUser(TokenRequest tokenRequest)
        {
            if (tokenRequest == null)
            {
                return BadRequest();
            }
            var tokenDetails = await _authenticationService.AuthenticateUser(tokenRequest);
            if (tokenDetails.Token != null)
            {
                return Ok(tokenDetails);
            }
            return Unauthorized();
        }

        [Authorize]
        [HttpGet]
        [Route("checktoken")]
        public string GetHello()
        {
            return "Hello";
        }
    }
}
