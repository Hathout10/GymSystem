using GymSystem.API.Extentions;
using GymSystem.DAL.Data;
using GymSystem.DAL.Entities.Identity;
using GymSystem.DAL.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

public class Program
{
	public async static Task Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.AddControllers();
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();
		

		// Configure Database Contexts
		builder.Services.AddDbContext<GymSystemContext>(options =>
		{
			options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
		});

		builder.Services.AddDbContext<AppIdentityDbContext>(options =>
		{
			options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
		});

		// Configure Redis
		builder.Services.AddSingleton<IConnectionMultiplexer>(provider =>
			ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis")));

		// Add Custom Services
		builder.Services.AddApplicationServices();

		builder.Services.AddAuthentication();
		// Configure Identity Services
		//builder.Services.AddIdentity<AppUser, IdentityRole>()
		//		.AddEntityFrameworkStores<AppIdentityDbContext>()
		//		.AddDefaultTokenProviders()
		//		.AddSignInManager();
		builder.Services.AddScoped<UserManager<AppUser>>();

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseHttpsRedirection();

		app.UseAuthentication(); // Ensure Authentication Middleware is added
		app.UseAuthorization();

		app.MapControllers();

		using var scope = app.Services.CreateScope();
		var services = scope.ServiceProvider;
		var loggerFactory = services.GetRequiredService<ILoggerFactory>();

		try
		{
			// Apply migrations
			var identityContext = services.GetRequiredService<AppIdentityDbContext>();
			await identityContext.Database.MigrateAsync();

			var userManager = services.GetRequiredService<UserManager<AppUser>>();
			var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
			await AppIdentityDbContextSeed.SeedUsersAsync(userManager, roleManager);
		}
		catch (Exception ex)
		{
			var logger = loggerFactory.CreateLogger<Program>();
			logger.LogError(ex, "An error occurred during migration or seeding.");
		}

		
		await app.RunAsync();
	}
}
