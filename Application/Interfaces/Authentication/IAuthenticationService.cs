using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Authentication;

namespace Application.Interfaces.Authentication
{
    public interface IAuthenticationService
    {
        Task<TokenResponse> AuthenticateUser(TokenRequest tokenRequest);
    }
}
