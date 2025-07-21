
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientManagementSystem.Data.DataContext;
using PatientManagementSystem.Data.Repositories;
using PatientManagementSystem.Repository; 
using PatientManagementSystem.Repository.Interfaces; 
using PatientManagementSystem.Services;
using PatientManagementSystem.Common.Services;
using PatientManagementSystem.Services.Interfaces; 
using Microsoft.AspNetCore.Authentication.JwtBearer; 
using Microsoft.IdentityModel.Tokens; 
using System.Text; 

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
            builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            builder.Services.AddScoped<IPatientRepository, PatientRepository>();
            builder.Services.AddScoped<IVitalRepository, VitalRepository>();
            // Register Services
            builder.Services.AddScoped<IAuthService, AuthService>(); 
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IPatientService, PatientService>();
            builder.Services.AddScoped<IVitalService, VitalService>();
            builder.Services.AddScoped<IAppointmentService, AppointmentService>();
            builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            builder.Services.AddScoped<IDoctorService, DoctorService>();
            builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();

            builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
            builder.Services.AddScoped<IProfileService, ProfileService>();
            builder.Services.AddControllers();
    
            builder.Services.AddHttpContextAccessor(); 


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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            builder.Services.AddAuthorization(); 

            var app = builder.Build();


       
            app.UseCors("AllowFrontend");

            app.UseHttpsRedirection(); 

            app.UseRouting(); 

            app.UseAuthentication(); 
            app.UseAuthorization(); 

            app.MapControllers();

           
            app.MapGet("/", ([FromServices] IUserService userService) =>
                Results.Redirect("/api/users/roles")); 

            app.Run();
        }
    }
}
