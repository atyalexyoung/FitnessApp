using FitnessApp.Data;
using FitnessApp.Interfaces.Repositories;
using FitnessApp.Interfaces.Services;
using FitnessApp.Repositories.InMemoryRepos;
using FitnessApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace FitnessApp
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            AddServices(builder);
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                // do swagger stuff if in development.
                app.UseSwagger();
                app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });

                // seed database with test data if in local development.
                using var scope = app.Services.CreateScope();
                var services = scope.ServiceProvider;

                var dbContext = services.GetRequiredService<FitnessAppDbContext>();
                await DatabaseSeeder.SeedDatabaseAsync(dbContext);
            }

            // setup and run app.
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }

        /// <summary>
        /// Adds all the services to the builder.
        /// </summary>
        /// <param name="builder">The builder to add the services to.</param>
        private static void AddServices(WebApplicationBuilder builder)
        {
            // ---------------------------
            // AUTHENTICATION SETUP
            // ---------------------------
            // We're currently using JWT Bearer Authentication with tokens that are issued
            // by our own custom login endpoint. These tokens are signed with a local secret key.
            // This setup is useful for development and testing without an external identity provider.
            //
            // TODO: In production, switch to using an OpenID Connect (OIDC) provider (e.g., Azure AD B2C, Auth0).
            // This will involve:
            //   - Moving token issuance to the provider (users log in through the provider)
            //   - Using the Authorization Code Flow with PKCE
            //   - Validating tokens signed by the provider (via metadata endpoint)
            //   - Configuring Angular/.NET MAUI to obtain and send the access token
            //
            // For more info, see:
            // https://learn.microsoft.com/aspnet/core/security/authentication/jwtbearer
            // https://learn.microsoft.com/aspnet/core/security/authentication/social/oidc

            // UPDATE:
            // NOW WILL BE CONSIDERING AND MOST LIKELY USING FIREBASE AUTH FOR AUTHENTICATION

            // authentication setup
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var key = builder.Configuration["Jwt:Key"];

                if (string.IsNullOrWhiteSpace(key))
                    throw new InvalidOperationException("JWT key not configured. Make sure it's set via user-secrets or environment variables.");

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false, // Optional: set to true when using a real issuer
                    ValidateAudience = false, // Optional: set to true if you specify an audience
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };
            });

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new() { Title = "FitnessApp", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Enter JWT token like: Bearer {token}",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                            },
                            new string[] {}
                        }
                    });
            });

            // database setup.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<FitnessAppDbContext>(options =>
                options.UseNpgsql(connectionString));

            // services
            builder.Services.AddScoped<IWorkoutsService, WorkoutsService>();
            builder.Services.AddScoped<IExercisesService, ExercisesService>();
            builder.Services.AddScoped<IUsersService, UsersService>();

            // repositories
            // TODO: Remove Singleton usage and replace with scoped once the InMemory test data isn't being used.
            builder.Services.AddSingleton<IWorkoutsRepository, InMemoryWorkoutsRepository>();
            builder.Services.AddSingleton<IWorkoutExerciseRepository, InMemoryWorkoutExerciseRepository>();
            builder.Services.AddSingleton<IUsersRepository, InMemoryUsersRepository>();
            builder.Services.AddSingleton<IExercisesRepository, InMemoryExercisesRepository>();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            // adding swagger support stuff
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }
    }
}
