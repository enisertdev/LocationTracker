using System;
using System.Collections.Generic;
using System.Linq;
using LocationTracker.Application.Interfaces.Auth;
using LocationTracker.Application.Interfaces.LocationInterface;
using LocationTracker.Application.Interfaces.PasswordHasher;
using LocationTracker.Application.Interfaces.UserInterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaxiLocation.Domain.Entities.Identity;
using TaxiLocation.Persistance.Configuration;
using TaxiLocation.Persistance.Context;

using TaxiLocation.Persistance.Repositories.LocationRepository;
using TaxiLocation.Persistance.Repositories.UserRepository;
using TaxiLocation.Persistance.Services.Auth;

namespace TaxiLocation.Persistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceServices(this IServiceCollection services)
        {
            services.AddDbContext<LocationTrackerDbContext>(options =>
                options.UseSqlServer(ConnectionConfiguration.ConnectionString));
            services.AddScoped<IUserReadRepository, UserReadRepository>();
            services.AddScoped<IUserWriteRepository, UserWriteRepository>();
            services.AddScoped<ILocationReadRepository, LocationReadRepository>();
            services.AddScoped<ILocationWriteRepository, LocationWriteRepository>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddIdentityCore<AppUser>().AddEntityFrameworkStores<LocationTrackerDbContext>();  
        }
    }
}
