using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Authentication
{
    public class TokenClaims
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Roles { get; set; }
    }
}
