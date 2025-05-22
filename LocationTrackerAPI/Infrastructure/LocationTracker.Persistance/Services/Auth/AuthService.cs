using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocationTracker.Application.Dtos.UserAuth;
using LocationTracker.Application.Interfaces.Auth;
using Microsoft.AspNetCore.Identity;
using TaxiLocation.Domain.Entities.Identity;

namespace TaxiLocation.Persistance.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;

        public AuthService(ITokenService tokenService, UserManager<AppUser> userManager)
        {
            _tokenService = tokenService;
            _userManager = userManager;
        }

        public async Task<string> LoginUserAsync(UserLoginRequest request)
        {

            var user = await _userManager.FindByEmailAsync(request.Email);
     

            bool loginCorrect = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!loginCorrect)
                return string.Empty;

            return await _tokenService.GenerateToken(request);




        }
    }
}
