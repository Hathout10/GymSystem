using GymSystem.DAL.Data;
using GymSystem.DAL.Entities.Identity;
using GymSystem.DAL.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GymSystem.API.Extentions
{
    public static class IdentityServicesExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
            })
                  .AddEntityFrameworkStores<GymSystemContext>()
                  .AddDefaultTokenProviders();


            services.AddAuthentication(/*JwtBearerDefaults.AuthenticationScheme*/
                 options =>
                 {
                     //Generate With the Same Jwt Schema
                     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                     //Validate With the Same Jwt Schema
                     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                 })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    //options.RequireHttpsMetadata = true;

                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidAudience = configuration["JWT:ValidAudience"],
                        ValidateIssuer = true,
                        ValidIssuer = configuration["JWT:ValidIssuer"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
                        ValidateLifetime = true,

                    };
                });
            //services.AddAuthorization();

            return services;
        }
    }
}
