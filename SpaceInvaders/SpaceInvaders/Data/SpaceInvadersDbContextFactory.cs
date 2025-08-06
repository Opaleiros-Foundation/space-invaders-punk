using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SpaceInvaders.Data
{
    public class SpaceInvadersDbContextFactory : IDesignTimeDbContextFactory<SpaceInvadersDbContext>
    {
        public SpaceInvadersDbContext CreateDbContext(string[] args)
        {
            // Build configuration
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Create DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<SpaceInvadersDbContext>();
            var connectionString = configuration.GetConnectionString("SpaceInvadersDb");
            builder.UseNpgsql(connectionString);

            return new SpaceInvadersDbContext(builder.Options);
        }
    }
}
