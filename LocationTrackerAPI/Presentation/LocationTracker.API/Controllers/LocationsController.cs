using System.Text.Json.Serialization;
using LocationTracker.Application.Hubs;
using LocationTracker.Application.Interfaces.LocationInterface;
using LocationTracker.Application.Interfaces.UserInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Serilog;

namespace TaxiLocation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly IUserReadRepository _userReadRepository;
        private readonly ILocationWriteRepository _locationWriteRepository;
        private readonly ILocationReadRepository _locationReadRepository;
        private readonly IHubContext<LocationHub> _hubContext;

        public LocationsController(IUserReadRepository userReadRepository, ILocationWriteRepository locationWriteRepository, ILocationReadRepository locationReadRepository, IHubContext<LocationHub> hubContext)
        {
            _userReadRepository = userReadRepository;
            _locationWriteRepository = locationWriteRepository;
            _locationReadRepository = locationReadRepository;
            _hubContext = hubContext;
        }

        public class LocationDTO
        {
            public Guid UserId { get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public string Name { get; set; }
            [JsonPropertyName("phonenumber")]
            public string PhoneNumber { get; set; }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLocation(Guid id, double longitude, double latitude)
        {
            var location = await _locationReadRepository.GetLocationWithUserById(id);
            if (location == null)
                return BadRequest(new { message = "invalid location." });

            location.Latitude = latitude;
            location.Longitude = longitude;
            location.TimeStamp = DateTime.UtcNow;

            _locationWriteRepository.Update(location);
            await _locationWriteRepository.SaveChangesAsync();

            return Ok(new
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                Name = location.User.Name,
                UserId = location.UserId,
                Phonenumber = location.User.PhoneNumber
            });
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocation(Guid id)
        {
            var location = await _locationReadRepository.GetSingleAsync(user => user.UserId == id, false);
            return Ok(location);
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllLocations()
        {
            var locations = _locationReadRepository.GetAllLocationsWithUser();
            var locs = locations.Select(p => new LocationDTO
            {
                Latitude = p.Latitude,
                Longitude = p.Longitude,
                Name = p.User.Name,
                UserId = p.UserId,
                PhoneNumber = p.User.PhoneNumber

            }).ToList();

            return Ok(locs);
        }


        [HttpPost("traccar")]
        public async Task<IActionResult> TraccarUpdate([FromQuery] Guid id, [FromQuery] double lat, [FromQuery] double lon, [FromQuery] double speed)
        {

            try
            {
                var location = await _locationReadRepository.GetLocationWithUserById(id);
                if (location == null)
                {
                    Log.Warning("Location bulunamadı. ID: {Id}", id);
                    return NotFound();
                }

                location.Latitude = lat;
                location.Longitude = lon;
                location.TimeStamp = DateTime.UtcNow;

                await _hubContext.Clients.All.SendAsync("ReceiveLocation", location.Latitude, location.Longitude, location.User.Name, location.UserId, location.User.PhoneNumber, speed);

                _locationWriteRepository.Update(location);
                await _locationWriteRepository.SaveChangesAsync();

                return Ok("Konum güncellendi.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
