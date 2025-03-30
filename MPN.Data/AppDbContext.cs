using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MPN.Data;

internal class AppDbContext : IdentityDbContext<AppUser>
{
    public DbSet<Entities.Item> Items { get; set; }
    public DbSet<Entities.Vendor> Vendors { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
}

internal class DesignTimeAppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile($"appsettings.{envName}.json", optional: true)
            .AddEnvironmentVariables()
            .AddCommandLine(args)
            .Build();

        var connString = config.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Unable to get connection string.");

        var optionBuilder = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(connString);

        return new AppDbContext(optionBuilder.Options);
    }
}
