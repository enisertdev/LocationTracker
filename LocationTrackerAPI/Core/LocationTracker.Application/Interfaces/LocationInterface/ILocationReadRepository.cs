using TaxiLocation.Domain.Entities;

namespace LocationTracker.Application.Interfaces.LocationInterface
{
    public interface ILocationReadRepository : IReadRepository<Location>
    {
        IEnumerable<Location> GetAllLocationsWithUser();
        Task<Location> GetLocationWithUserById(Guid id);
    }
}
