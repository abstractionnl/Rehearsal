using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using LanguageExt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Rehearsal.Messages.Authorization;
using Rehearsal.WebApi.Infrastructure;

namespace Rehearsal.WebApi.Authorization
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class JwtController : Controller
    {
        public JwtController(IOptions<JwtOptions> jwtIssuerOptions, IUserRepository userRepository)
        {
            UserRepository = userRepository;
            JwtOptions = jwtIssuerOptions.Value;
        }

        private JwtOptions JwtOptions { get; }
        private IUserRepository UserRepository { get; }

        [HttpGet, Route("token")]
        public IActionResult ShowClaims()
        {
            return Ok(
                new
                {
                    Identity = new
                    {
                        User.Identity.Name
                    },
                    Claims = User.Claims.Select(claim => new { claim.Type, claim.Value })
                }
            );
        }
        
        [HttpPost, Route("token")]
        public IActionResult GenerateJwtToken([FromBody] TokenRequestModel request) => 
            GetClaim(request.UserName).Some<IActionResult>(
                claimsIdentity =>
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(),
                            ClaimValueTypes.Integer64)
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
                        expires_in = (int) JwtOptions.ValidFor.TotalSeconds
                    });
                }
            )
            .None(Forbid);

        private Option<ClaimsIdentity> GetClaim(string userName)
        {
            return UserRepository.GetByUsername(userName).HeadOrNone()
                .Map(user => new ClaimsIdentity(
                    new GenericIdentity(user.UserName), 
                    new []
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), 
                    }));
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - 
                                 new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);
    }
}