using System;
using Microsoft.IdentityModel.Tokens;

namespace Rehearsal.WebApi.Infrastructure
{
    public class JwtOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public TimeSpan ValidFor { get; set; } = TimeSpan.FromMinutes(5);
        public SigningCredentials SigningCredentials { get; set; }
    }
}