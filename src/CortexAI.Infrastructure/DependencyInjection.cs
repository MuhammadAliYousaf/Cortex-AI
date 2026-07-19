using CortexAI.Domain.Repositories;
using CortexAI.Infrastructure.Identity;
using CortexAI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CortexAI.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("CortexAI")
            ?? throw new InvalidOperationException("Connection string 'CortexAI' is not configured.");

        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
        services.Configure<DefaultAdminOptions>(configuration.GetSection(DefaultAdminOptions.SectionName));

        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        return services;
    }
}
