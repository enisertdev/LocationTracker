using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using LocationTracker.Application.Dtos.SystemUser;
using LocationTracker.Application.Dtos.UserAuth;
using LocationTracker.Application.Interfaces.Auth;
using Serilog;
using TaxiLocation.Domain.Entities.Identity;

namespace LocationTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAuthService _authService;

        public AuthController(UserManager<AppUser> userManager, IAuthService authService)
        {
            _userManager = userManager;
            _authService = authService;
        }

        [Authorize]
        [HttpGet("validate")]
        public async Task<IActionResult> ValidateSystemUser()
        {
            try
            {
                var getIdFromToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                if (string.IsNullOrEmpty(getIdFromToken))
                    return Unauthorized();
                var user = await _userManager.FindByIdAsync(getIdFromToken);
                return Ok(new { message = "Access granted." });
            }
            catch (Exception ex)
            {
                Log.Information(ex.Message);
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequest model)
        {
            var findUser = await _userManager.FindByEmailAsync(model.Email);
            if (findUser == null)
                return BadRequest(new { message = "Email or password is wrong." });

            var token = await _authService.LoginUserAsync(model);
            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Email or password wrong." });

            return Ok(new { token = token, message = "Login successful" });

        }
    }
}
