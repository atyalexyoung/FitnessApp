using FitnessApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FitnessApp.DbMigrator;

class Program
{
    static async Task<int> Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddEnvironmentVariables()
            .AddCommandLine(args)
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        if (string.IsNullOrEmpty(connectionString))
        {
            Console.Error.WriteLine("ERROR: Connection string not found!");
            Console.Error.WriteLine("Set it via:");
            Console.Error.WriteLine("  - appsettings.json");
            Console.Error.WriteLine("  - Environment variable: ConnectionStrings__DefaultConnection");
            Console.Error.WriteLine("  - Command line: --ConnectionStrings:DefaultConnection=\"...\"");
            return 1;
        }

        var optionsBuilder = new DbContextOptionsBuilder<FitnessAppDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        await using var dbContext = new FitnessAppDbContext(optionsBuilder.Options);

        try
        {
            var command = args.Length > 0 ? args[0].ToLower() : "all";

            switch (command)
            {
                case "migrate":
                    Console.WriteLine("Running migrations...");
                    await dbContext.Database.MigrateAsync();
                    Console.WriteLine("✓ Migrations applied successfully");
                    break;

                case "seed":
                    Console.WriteLine("Seeding exercises...");
                    await DatabaseSeeder.SeedDatabaseAsync(dbContext);
                    Console.WriteLine("✓ Exercises seeded successfully");
                    break;

                case "all":
                    Console.WriteLine("Running migrations...");
                    await dbContext.Database.MigrateAsync();
                    Console.WriteLine("✓ Migrations applied");

                    Console.WriteLine("Seeding exercises...");
                    await DatabaseSeeder.SeedDatabaseAsync(dbContext);
                    Console.WriteLine("✓ Exercises seeded");
                    break;

                case "reset":
                    Console.WriteLine("WARNING: This will delete all data!");
                    Console.Write("Are you sure? (yes/no): ");
                    var confirm = Console.ReadLine();
                    if (confirm?.ToLower() == "yes")
                    {
                        Console.WriteLine("Dropping database...");
                        await dbContext.Database.EnsureDeletedAsync();
                        Console.WriteLine("Creating database...");
                        await dbContext.Database.MigrateAsync();
                        Console.WriteLine("Seeding exercises...");
                        await DatabaseSeeder.SeedDatabaseAsync(dbContext);
                        Console.WriteLine("✓ Database reset complete");
                    }
                    else
                    {
                        Console.WriteLine("Reset cancelled");
                    }
                    break;

                case "revert":
                    if (args.Length < 2)
                    {
                        Console.WriteLine("Usage: revert <MigrationName>");
                        return 1;
                    }

                    var targetMigration = args[1];
                    Console.WriteLine($"Reverting to migration: {targetMigration}");
                    await dbContext.Database.MigrateAsync(targetMigration);
                    Console.WriteLine($"✓ Database reverted to {targetMigration}");
                    break;
                default:
                    Console.WriteLine("Unknown command. Available commands:");
                    Console.WriteLine("  migrate - Run pending migrations");
                    Console.WriteLine("  seed    - Seed/update exercises");
                    Console.WriteLine("  all     - Run migrations and seed (default)");
                    Console.WriteLine("  reset   - Drop, recreate, and seed database");
                    return 1;
            }

            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"ERROR: {ex.Message}");
            Console.Error.WriteLine(ex.StackTrace);
            return 1;
        }
    }
}