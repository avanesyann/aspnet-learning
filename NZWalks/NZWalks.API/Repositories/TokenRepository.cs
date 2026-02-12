using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace NZWalks.API.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        public string CreateJWTToken(IdentityUser user, List<string> roles)
        {
            // Create claims
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            throw new NotImplementedException();
        }
    }
}
