using LocationTracker.Application.Dtos.UserAuth;

namespace LocationTracker.Application.Interfaces.Auth
{
    public interface ITokenService
    {
        Task<string> GenerateToken(UserLoginRequest request);
    }
}
