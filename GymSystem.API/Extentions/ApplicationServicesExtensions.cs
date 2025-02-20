using GymSystem.BLL.Errors;
using GymSystem.API.Helpers;
using GymSystem.BLL.Interfaces;
using GymSystem.BLL.Interfaces.Business;
using GymSystem.BLL.Services;
using GymSystem.DAL.Data;
using GymSystem.DAL.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GymSystem.DAL.Identity;
using GymSystem.BLL.Services.Trainer;

namespace GymSystem.API.Extentions
{
	public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddSingleton<IResponceCacheService, ResponceCacheService>();
            services.AddScoped<ITrainerService, TrainerService>();
            services.AddDataProtection();

            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
            })
        .AddEntityFrameworkStores<AppIdentityDbContext>()
		.AddSignInManager()
		.AddDefaultTokenProviders();

	         services.Configure<ApiBehaviorOptions>(Options =>
            {
                Options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var Errors = actionContext.ModelState
                        .Where(P => P.Value.Errors.Count() > 0)
                        .SelectMany(P => P.Value.Errors)
                        .Select(E => E.ErrorMessage)
                        .ToArray();

                    var ValidationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = Errors
                    };

                    return new BadRequestObjectResult(ValidationErrorResponse);
                };
            });


            return services; // Return the modified IServiceCollection
        }

    }
}
