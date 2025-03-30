using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MPN.Data;

public static class DBContextConfig
{
    public static IServiceCollection AddDbServices(this IServiceCollection services, string connectionString)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

        services.AddDbContext<AppDbContext>(o => o.UseSqlServer(connectionString))
            .AddDefaultIdentity<AppUser>()
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<AppDbContext>();

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddMediatR(c => c.RegisterServicesFromAssembly(System.Reflection.Assembly.GetExecutingAssembly()));

        return services;
    }
}
