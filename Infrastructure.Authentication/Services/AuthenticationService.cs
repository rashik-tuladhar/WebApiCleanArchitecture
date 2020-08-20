using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Authentication;
using Application.Interfaces.Authentication;
using Application.Interfaces.Repositories;
using Domain.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Authentication.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly IGenericRepositoryDapper _genericRepositoryDapper;
        private readonly JwtSettings _jwtSettings;

        public AuthenticationService(IConfiguration configuration, IGenericRepositoryDapper genericRepositoryDapper, IOptions<JwtSettings> jwtSettings)
        {
            _configuration = configuration;
            _genericRepositoryDapper = genericRepositoryDapper;
            _jwtSettings = jwtSettings.Value;
        }

        /// <summary>
        /// Authentication user and return the token response
        /// </summary>
        /// <param name="tokenRequest"></param>
        /// <returns></returns>
        public async Task<TokenResponse> AuthenticateUser(TokenRequest tokenRequest)
        {
            TokenResponse tokenResponse = new TokenResponse();
            var user =await AuthenticateUserDetails(tokenRequest);
            if (user.Name != null)
            {
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim("Roles",user.Roles)
                };
                tokenResponse = BuildToken(claims);
            }
            return tokenResponse;
        }

        /// <summary>
        /// Build token after successful credentials validation
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        private TokenResponse BuildToken(Claim[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiryTime = DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings.DurationInMinutes));
            var token = new JwtSecurityToken(_jwtSettings.Issuer,
                _jwtSettings.Audience,
                expires: expiryTime,
                claims: claims,
                signingCredentials: signingCredentials);
            var details = new TokenResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresOn = expiryTime.ToString("MM/dd/yyyy HH:mm:ss")
            };
            return details;
        }


        /// <summary>
        /// Validate user credentials from database
        /// </summary>
        /// <param name="tokenRequest"></param>
        /// <returns></returns>
        private async Task<TokenClaims> AuthenticateUserDetails(TokenRequest tokenRequest)
        {
            //var userDetails = await _genericRepositoryDapper.ManageDataWithSingleObjectAsync<TokenClaims>("", tokenRequest);
            //return userDetails;

            var tokenDetails = new TokenClaims();
            if (tokenRequest.Username == "rashik" && tokenRequest.Password == "rashik")
            {
                tokenDetails.Email = "hello@rashik.com.np";
                tokenDetails.Name = "Rashik Tuladhar";
                tokenDetails.Roles =
                    "auth.weather"; //roles are separated by comma(,) which is also mentioned in constant class named AuthorizationConstants
            }
            return tokenDetails;
        }
    }
}
