using AuthJwt.Core;
using AuthJwt.Core.Contexts.AccountContext.UseCases.Authenticate;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthJwt.Api.Extensions;

public static class JwtExtension
{
    public static string Generate(ResponseData data)
    {
        var handler = new JwtSecurityTokenHandler();
        
        var key = Encoding.UTF8.GetBytes(Configuration.Secrets.JwtPrivateKey);
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaimsIdentity(data),
            Expires = DateTime.Now.AddHours(8),
            SigningCredentials = credentials
        };
        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

    private static ClaimsIdentity GenerateClaimsIdentity(ResponseData user)
    {
        var ci = new ClaimsIdentity();
        ci.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
        ci.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));
        ci.AddClaim(new Claim(ClaimTypes.Email, user.Email));
        foreach (var role in user.Roles)
            ci.AddClaim(new Claim(ClaimTypes.Role, role));
        
        return ci;
    }
}
