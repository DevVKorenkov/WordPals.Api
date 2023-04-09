using System.Security.Claims;

namespace WordPals.Api.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal user) => user.GetUserClaim(ClaimTypes.NameIdentifier);
        
        public static string GetUserName(this ClaimsPrincipal user) => user.GetUserClaim(ClaimTypes.Name);

        public static string GetUserEmail(this ClaimsPrincipal user) => user.GetUserClaim(ClaimTypes.Email);

        public static string GetUserClaim(this ClaimsPrincipal user, string claimType) =>
            user.FindFirst(u => u.Type.Equals(claimType, StringComparison.OrdinalIgnoreCase)).Value;
    }
}
