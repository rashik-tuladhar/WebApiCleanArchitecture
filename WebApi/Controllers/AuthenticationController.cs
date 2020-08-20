using System.Threading.Tasks;
using Application.DTOs.Authentication;
using Application.Interfaces.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger _log = Log.ForContext<AuthenticationController>();
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet]
        public async Task<TokenResponse> AuthenticationUser()
        {
            var tokenDetails = await _authenticationService.AuthenticateUser(new TokenRequest {Username = "rashik", Password = "rashik"});
            return tokenDetails;
        }

        [Authorize]
        [HttpGet]
        [Route("CheckToken")]
        public string GetHello()
        {
            return "Hello";
        }
    }
}
