using System.Security.Claims;
using LocationTracker.Application.Dtos.ClientUser;
using LocationTracker.Application.Dtos.SystemUser;
using LocationTracker.Application.Hubs;
using LocationTracker.Application.Interfaces.UserInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TaxiLocation.Domain.Entities;
using TaxiLocation.Domain.Entities.Identity;

namespace TaxiLocation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly IUserReadRepository _userReadRepository;
        private readonly IHubContext<LocationHub> _hubContext;
        private readonly UserManager<AppUser> _userManager;


        public UsersController(IUserWriteRepository userWriteRepository, IUserReadRepository userReadRepository, IHubContext<LocationHub> hubContext, UserManager<AppUser> userManager)
        {
            _userWriteRepository = userWriteRepository;
            _userReadRepository = userReadRepository;
            _hubContext = hubContext;
            _userManager = userManager;
        }

        [Authorize]
        [HttpPost("ClientUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO model)
        {
            try
            {
                Location location = new();
                User user = new()
                {
                    Name = model.Name,
                    PhoneNumber = model.PhoneNumber,
                    Location = location
                };
                await _userWriteRepository.AddAsync(user);
                await _userWriteRepository.SaveChangesAsync();
                await _hubContext.Clients.All.SendAsync("ReceiveNewUser", location.Latitude, location.Longitude,
                    user.Name, user.Id, user.PhoneNumber);
                return Ok(new { message = "User Created" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [Authorize]
        [HttpPost("SystemUser")]
        public async Task<IActionResult> CreateSystemUser([FromBody] CreateSystemUserDTO model)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Email = model.Email,
                UserName = model.Email
            },
                password: model.Password);
            if (!result.Succeeded)
                return BadRequest(new { message = result.Errors });
            return Ok(new { message = "System User Created" });
        }

        [Authorize]
        [HttpGet("User")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userReadRepository.GetOneByIdAsync(id);
            return Ok(user);
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userReadRepository.GetAll().ToList();
            return Ok(users);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _userReadRepository.GetOneByIdAsync(id);
            if (user == null)
                return BadRequest(new { message = "user could not found." });
            await _hubContext.Clients.All.SendAsync("ReceiveUserRemoval", user.Id);
            _userWriteRepository.Remove(user);
            await _userWriteRepository.SaveChangesAsync();
            return Ok(new { message = "user removal was succesful." });
        }



    }
}
