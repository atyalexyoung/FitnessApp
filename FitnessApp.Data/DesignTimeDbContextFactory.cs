using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FitnessApp.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<FitnessAppDbContext>
    {
        public FitnessAppDbContext CreateDbContext(string[] args)
        {
            // Try to locate the appsettings.json either in DbMigrator or Data directory
            var basePath = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(basePath, "appsettings.json");
            if (!File.Exists(configPath))
            {
                // Fallback to DbMigrator’s folder
                configPath = Path.Combine(basePath, "../FitnessApp.DbMigrator/appsettings.json");
            }

            var configuration = new ConfigurationBuilder()
                .AddJsonFile(configPath, optional: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection") 
                ?? "Host=localhost;Database=fitnessapp;Username=postgres;Password=postgres";

            var optionsBuilder = new DbContextOptionsBuilder<FitnessAppDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new FitnessAppDbContext(optionsBuilder.Options);
        }
    }
}
