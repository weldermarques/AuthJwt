using System.Security.Claims;

namespace AuthJwt.Api.Extensions;

public static class ClaimsPrincipalExtension
{
    public static string GetId(this ClaimsPrincipal user)
        => user.Claims.FirstOrDefault(c => c.Type == "id")?.Value 
           ??  string.Empty;
    
    public static string GetName(this ClaimsPrincipal user)
        => user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value 
           ??  string.Empty;
    
    public static string GetEmail(this ClaimsPrincipal user)
        => user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value 
           ??  string.Empty;
    
}
