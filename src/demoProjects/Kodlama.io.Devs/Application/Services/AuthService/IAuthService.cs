using Core.Security.Entities;
using Core.Security.JWT;

namespace Application.Services.AuthService;

public interface IAuthService
{
    public Task<AccessToken> GenerateAccessToken(User user);
    public Task<RefreshToken> GenerateRefreshToken(User user, string ipAddress);
}