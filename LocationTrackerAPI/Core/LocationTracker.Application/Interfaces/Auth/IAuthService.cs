using LocationTracker.Application.Dtos.UserAuth;

namespace LocationTracker.Application.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<string> LoginUserAsync(UserLoginRequest request);
    }
}
