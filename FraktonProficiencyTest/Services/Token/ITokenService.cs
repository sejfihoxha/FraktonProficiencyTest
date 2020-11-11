
using FraktonProficiencyTest.Data.Entities;
using System.Security.Claims;

namespace FraktonProficiencyTest.Services.Token
{
    public interface ITokenService:IService
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
