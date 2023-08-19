using ManageEmployees.Application.Contracts.Identity;
using ManageEmployees.Application.Models.Identity;
using ManageEmployees.Identity.DBContext;
using ManageEmployees.Identity.Models;
using ManageEmployees.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ManageEmployees.Identity
{
    public static class IdentityServiceRegistration
    {
        public static IServiceCollection ConfigureIdentityServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.AddDbContext<IdentityDBContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ManageEmployeesDB")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDBContext>()
                .AddDefaultTokenProviders();


            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserService, UserService>();

            return services;
        }
    }
}
