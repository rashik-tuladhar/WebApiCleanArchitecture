using System;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Infrastructure.Authentication.Models
{
    [Serializable]
    [Table("TokenAuthenticationDetails", Schema = "Authentication")]
    public class TokenAuthenticationDetails : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }
    }
}
