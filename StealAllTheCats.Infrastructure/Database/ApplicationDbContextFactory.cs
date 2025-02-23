using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace StealAllTheCats.Infrastructure.Database;


public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        // Go up one level to reach the solution directory
        var basePath = Directory.GetParent(Directory.GetCurrentDirectory())?.FullName
                       ?? throw new Exception("Failed to determine base path.");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)  // Root of the solution
            .AddJsonFile("StealAllTheCats.API/appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // Configure DbContext options
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
