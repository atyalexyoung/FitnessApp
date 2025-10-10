using FitnessApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DotNetEnv;

namespace FitnessApp.DbMigrator;

class Program
{
    static async Task<int> Main(string[] args)
    {
        var searchDir = new DirectoryInfo(Directory.GetCurrentDirectory());
        string? envPath = null;

        while (searchDir != null)
        {
            var testPath = Path.Combine(searchDir.FullName, ".env");
            if (File.Exists(testPath))
            {
                envPath = testPath;
                break;
            }
            searchDir = searchDir.Parent;
        }

        if (envPath != null)
        {
            Console.WriteLine($"Loading environment variables from {envPath}");

            // Load and SET environment variables in the current process
            foreach (var line in File.ReadAllLines(envPath))
            {
                var trimmed = line.Trim();
                if (string.IsNullOrWhiteSpace(trimmed) || trimmed.StartsWith("#"))
                    continue;

                var parts = trimmed.Split('=', 2);
                if (parts.Length == 2)
                {
                    var key = parts[0].Trim();
                    var value = parts[1].Trim().Trim('"', '\'');
                    Environment.SetEnvironmentVariable(key, value);
                }
            }
        }
        else
        {
            Console.WriteLine($"No .env file found starting from {Directory.GetCurrentDirectory()}");
        }

        var configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .AddCommandLine(args)
            .Build();

        var connectionString = configuration["ConnectionStrings:LocalConnection"]
                            ?? configuration["ConnectionStrings:DefaultConnection"];

        if (string.IsNullOrEmpty(connectionString))
        {
            Console.Error.WriteLine("ERROR: Connection string not found!");
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