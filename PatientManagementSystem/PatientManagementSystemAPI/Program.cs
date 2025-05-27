//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using PatientManagementSystem.Data.DataContext;
//using PatientManagementSystem.Data.Repositories;
//using PatientManagementSystem.Repository;
//using PatientManagementSystem.Repository.Interfaces;
//using PatientManagementSystem.Services;
//using PatientManagementSystem.Services.Interfaces;

//namespace PatientManagementSystemAPI
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            var builder = WebApplication.CreateBuilder(args);

//            // Add DbContext
//            builder.Services.AddDbContext<ApplicationDbContext>(options =>
//                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//            builder.Services.AddCors(options =>
//            {
//                options.AddPolicy("AllowFrontend", builder =>
//                {
//                    builder.WithOrigins("http://localhost:3000")
//                           .AllowAnyHeader()
//                           .AllowAnyMethod();
//                });
//            });

//            // Register Repositories
//            builder.Services.AddScoped<IUserRepository, UserRepository>();
//            builder.Services.AddScoped<IAuthService, AuthService>();
//            // Register Services
//            builder.Services.AddScoped<IUserService, UserService>();
//            builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
//            builder.Services.AddScoped<ITokenService, TokenService>();
//            builder.Services.AddControllers();
//            builder.Services.AddEndpointsApiExplorer();
//            builder.Services.AddHttpContextAccessor();

//            // Add authentication services
//            builder.Services.AddAuthentication(options =>
//            {
//                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//            })
//            .AddJwtBearer(options =>
//            {
//                options.TokenValidationParameters = new TokenValidationParameters
//                {
//                    ValidateIssuerSigningKey = true, // Validate the signature key
//                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"])), // Your secret key
//                    ValidateIssuer = false, // Set to true if you have a specific issuer (e.g., "your-api-issuer")
//                    ValidateAudience = false, // Set to true if you have a specific audience (e.g., "your-client-app")
//                    ValidateLifetime = true, // Validate token expiry
//                    ClockSkew = TimeSpan.Zero // No leeway for expiry date
//                };
//            });

//            builder.Services.AddAuthorization();
//            var app = builder.Build();

//            app.UseCors("AllowFrontend");

//            app.UseHttpsRedirection();

//            app.UseAuthorization();

//            app.MapControllers();

//            // OPTIONAL: Map default route to UsersController
//            app.MapGet("/", ([FromServices] IUserService userService) =>
//                Results.Redirect("/api/users/roles"));

//            app.Run();
//        }
//    }
//}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientManagementSystem.Data.DataContext;
using PatientManagementSystem.Data.Repositories; // Check this, usually you just need the interface namespace for injection
using PatientManagementSystem.Repository; // Check this, often just for concrete implementations
using PatientManagementSystem.Repository.Interfaces; // Good, for IUserRepository etc.
using PatientManagementSystem.Services; // Check this, often just for concrete implementations
using PatientManagementSystem.Services.Interfaces; // Good, for IAuthService, IUserService etc.
using Microsoft.AspNetCore.Authentication.JwtBearer; // Essential for JwtBearerDefaults
using Microsoft.IdentityModel.Tokens; // Essential for SymmetricSecurityKey, TokenValidationParameters
using System.Text; // Essential for Encoding.UTF8

namespace PatientManagementSystemAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add DbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                });
            });

            // Register Repositories
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            // -------------------------------------------------------------
            // IMPORTANT CORRECTION/CLARIFICATION HERE:
            // IAuthService is a service, not a repository.
            // Move it to the "Register Services" section for better organization.
            // builder.Services.AddScoped<IAuthService, AuthService>(); // Remove from here
            // -------------------------------------------------------------

            // Register Services
            builder.Services.AddScoped<IAuthService, AuthService>(); // Moved here for clarity
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>(); // Corrected this in our last chat, good.
            builder.Services.AddScoped<ITokenService, TokenService>(); // Correct, assuming ITokenService is the interface for TokenService

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer(); // For Swagger/OpenAPI
            builder.Services.AddHttpContextAccessor(); // Good for accessing HttpContext in services

            

            // Add authentication services
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    // Check your appsettings.json: is it "JwtSettings:Key" or "JwtSettings:Secret"?
                    // Previous examples used "JwtSettings:Secret". Be consistent.
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            builder.Services.AddAuthorization(); // Good

            var app = builder.Build();

            // --- Middleware Configuration (Order Matters!) ---
       
            app.UseCors("AllowFrontend"); // CORS must be before UseRouting, UseAuthentication, UseAuthorization

            app.UseHttpsRedirection(); // Good

            // You need UseRouting() before UseAuthentication() and UseAuthorization()
            // for attribute-based routing to work correctly with auth.
            app.UseRouting(); // ADD THIS LINE

            app.UseAuthentication(); // Must be after UseRouting() and before UseAuthorization()
            app.UseAuthorization();  // Must be after UseAuthentication()

            app.MapControllers(); // Maps your controller routes

            // OPTIONAL: Map default route to UsersController
            app.MapGet("/", ([FromServices] IUserService userService) =>
                Results.Redirect("/api/users/roles")); // This will redirect GET requests to the root URL.

            app.Run();
        }
    }
}
