using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Rehearsal.WebApi
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class JwtController : Controller
    {
        public JwtController(IOptions<JwtOptions> jwtIssuerOptions)
        {
            JwtOptions = jwtIssuerOptions.Value;
        }

        public JwtOptions JwtOptions { get; }
        
        [HttpPost, Route("token")]
        public IActionResult GenerateJwtToken()
        {
            var claimsIdentity = new ClaimsIdentity(
                new GenericIdentity("DefaultUser")
            );
            
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, claimsIdentity.Name), 
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64)
            }.Union(claimsIdentity.Claims);
            
            var token = new JwtSecurityToken(
                issuer: JwtOptions.Issuer,
                audience: JwtOptions.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.Add(JwtOptions.ValidFor),
                signingCredentials: JwtOptions.SigningCredentials);
            
            return Ok(new
            {
                access_token = new JwtSecurityTokenHandler().WriteToken(token),
                expires_in = (int)JwtOptions.ValidFor.TotalSeconds
            });
        }
        
        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - 
                                 new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);
    }

    public class JwtOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public TimeSpan ValidFor { get; set; } = TimeSpan.FromMinutes(5);
        public SigningCredentials SigningCredentials { get; set; }
    }
}