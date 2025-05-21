using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientManagementSystem.Data.DataContext;
using PatientManagementSystem.Repository;
using PatientManagementSystem.Repository.Interfaces;
using PatientManagementSystem.Services;
using PatientManagementSystem.Services.Interfaces;

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
                           .AllowAnyMethod();
                });
            });

            // Register Repositories
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            // Register Services
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            
            var app = builder.Build();

            app.UseCors("AllowFrontend");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            // OPTIONAL: Map default route to UsersController
            app.MapGet("/", ([FromServices] IUserService userService) =>
                Results.Redirect("/api/users/roles"));

            app.Run();
        }
    }
}
