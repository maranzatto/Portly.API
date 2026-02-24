using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Portly.Application.Interfaces.Security;
using Portly.Application.Ports.Input;
using Portly.Application.Ports.Input.Resident;
using Portly.Application.Ports.Output;
using Portly.Infrastructure.Data;
using Portly.Infrastructure.Repositories;
using Portly.Infrastructure.Security;

namespace Portly.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString =
                configuration.GetConnectionString("DefaultConnection")
                ?? Environment.GetEnvironmentVariable("DATABASE_URL");

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("Database connection string not configured.");

            services.AddDbContext<PortlyDbContext>(options =>
                options.UseNpgsql(connectionString)
                       .UseSnakeCaseNamingConvention()
            );

            // Repositories
            services.AddScoped<IVisitorRepository, VisitorRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IResidentRepository, ResidentRepository>();

            // Security Services
            services.AddScoped<ITokenService, JwtTokenService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            return services;
        }
    }
}

