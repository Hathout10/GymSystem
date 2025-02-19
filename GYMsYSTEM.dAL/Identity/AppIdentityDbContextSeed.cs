﻿using GymSystem.DAL.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GymSystem.DAL.Identity
{
	public class AppIdentityDbContextSeed
	{
		public static async Task SeedUsersAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			// Seed Roles
			await SeedRolesAsync(roleManager);

			// Seed Admin Users
			await SeedAdminUsersAsync(userManager);

			// Seed Trainer Users
			await SeedTrainerUsersAsync(userManager);

			// Seed Member Users
			await SeedMemberUsersAsync(userManager);

			// Seed Receptionist Users
			await SeedReceptionistUsersAsync(userManager);
		}

		private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
		{
			var roles = new List<IdentityRole>
			{
				new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
				new IdentityRole { Id = "2", Name = "Trainer", NormalizedName = "TRAINER" },
				new IdentityRole { Id = "3", Name = "Member", NormalizedName = "MEMBER" },
				new IdentityRole { Id = "5", Name = "Receptionist", NormalizedName = "RECEPTIONIST" }
			};

			foreach (var role in roles)
			{
				if (!await roleManager.RoleExistsAsync(role.Name))
				{
					await roleManager.CreateAsync(role);
				}
			}
		}

		//     private static async Task SeedAdminUsersAsync(UserManager<AppUser> userManager)
		//     {
		//         var adminUsers = new List<AppUser>
		//         {
		//             new AppUser
		//             {
		//                 DisplayName = "Ahmed",
		//                 UserName = "ahmedelfayoumi97",
		//                 Email = "ahmedelfayoumi203@gmail.com",
		//                 PhoneNumber = "01093422098",
		//                 Address = new Address
		//                 {
		//                     FristName = "Ahmed",
		//                     LastName = "Elfayoumi",
		//                     Country = "Egypt",
		//                     City = "El-bagour",
		//                     Street = "10 Tahrir St."
		//                 }
		//             },
		//	 new AppUser
		//	{
		//		DisplayName = "mohamed",
		//		UserName = "hathout10",
		//		Email = "mohamedhathout263@gmail.com",
		//		PhoneNumber = "01096596731",
		//		Address = new Address
		//		{
		//			FristName = "Mohamed",
		//			LastName = "Hathout",
		//			Country = "Egypt",
		//			City = "Menouf",
		//			Street = "Tarek Barhim"
		//		}
		//	}
		//};

		//         foreach (var user in adminUsers)
		//         {
		//             if (await userManager.FindByEmailAsync(user.Email) == null)
		//             {
		//                 var result = await userManager.CreateAsync(user, "Pa$$w0rd");

		//                 if (result.Succeeded)
		//                 {
		//                     await userManager.AddToRoleAsync(user, "Admin");
		//                 }
		//             }
		//         }
		//     }

		private static async Task SeedAdminUsersAsync(UserManager<AppUser> userManager)
		{
			var adminUsers = new List<(AppUser User, string Password)>
	{
		(
			new AppUser
			{
				DisplayName = "Ahmed Admin",
				UserName = "ahmedadmin",
				Email = "ahmedelfayoumi203@gmail.com",
				PhoneNumber = "01093422098",
				Address = new Address
				{
					FristName = "Ahmed",
					LastName = "Elfayoumi",
					Country = "Egypt",
					City = "El-bagour",
					Street = "10 Tahrir St."
				},
				UserCode = GenerateUserCode("Admin", 1),
				City = string.Empty,
				Gender = string.Empty,
				
			},
			"Pa$$w0rd"
		),
		(
			new AppUser
			{
				DisplayName = "Mohamed",
				UserName = "hathout10",
				Email = "mohamedhathout263@gmail.com",
				PhoneNumber = "01096596731",
				Address = new Address
				{
					FristName = "Mohamed",
					LastName = "Hathout",
					Country = "Egypt",
					City = "Menouf",
					Street = "Tarek Barhim"
				},
				UserCode = GenerateUserCode("Admin", 2),
				City = string.Empty,
				Gender = string.Empty


			},
			"Pa$$w0rd"
		)
	};

			await SeedUsersWithRole(userManager, adminUsers, "Admin");
		}

		private static async Task SeedTrainerUsersAsync(UserManager<AppUser> userManager)
		{
			var trainerUsers = new List<(AppUser User, string Password)>
	 {
		 (
			 new AppUser
			 {
				 DisplayName = "Mohamed Trainer",
				 UserName = "mohamedtrainer",
				 Email = "mohamedtrainer@example.com",
				 PhoneNumber = "0123456789",
				 Address = new Address
				 {
					 FristName = "Mohamed",
					 LastName = "Trainer",
					 Country = "Egypt",
					 City = "Cairo",
					 Street = "15 Nile St."
				 },
				 UserCode = GenerateUserCode("Trainer", 1),
				City = string.Empty,
				Gender = string.Empty,
			 },
			 "Trainer@123"
		 )
	 };

			await SeedUsersWithRole(userManager, trainerUsers, "Trainer");
		}


		//private static async Task SeedMemberUsersAsync(UserManager<AppUser> userManager)
		//{
		//    var memberUsers = new List<AppUser>
		//    {
		//        new AppUser
		//        {
		//            DisplayName = "Ali Member",
		//            UserName = "alimember",
		//            Email = "alimember@example.com",
		//            PhoneNumber = "0112233445",
		//            Address = new Address
		//            {
		//                FristName = "Ali",
		//                LastName = "Member",
		//                Country = "Egypt",
		//                City = "Alexandria",
		//                Street = "20 Corniche St."
		//            }
		//        }
		//    };

		//    foreach (var user in memberUsers)
		//    {
		//        if (await userManager.FindByEmailAsync(user.Email) == null)
		//        {
		//            var result = await userManager.CreateAsync(user, "Member@123");

		//            if (result.Succeeded)
		//            {
		//                await userManager.AddToRoleAsync(user, "Member");
		//            }
		//        }
		//    }
		//}

		private static async Task SeedMemberUsersAsync(UserManager<AppUser> userManager)
		{
			var memberUsers = new List<(AppUser User, string Password)>
		  {
			  (
				  new AppUser
				  {
					  DisplayName = "Ali Member",
					  UserName = "alimember",
					  Email = "alimember@example.com",
					  PhoneNumber = "0112233445",
					  Address = new Address
					  {
						  FristName = "Ali",
						  LastName = "Member",
						  Country = "Egypt",
					 City = "Alexandria",
						  Street = "20 Corniche St."
					  },
					  UserCode = GenerateUserCode("Member", 1), // توليد UserCode للمستخدم
					City = string.Empty,
					Gender = string.Empty,
				  },
				  "Member@123"


			  )
		  };
			await SeedUsersWithRole(userManager, memberUsers, "Member");
		}


		private static async Task SeedReceptionistUsersAsync(UserManager<AppUser> userManager)
		{
			var receptionistUsers = new List<(AppUser User, string Password)>
	  {
		  (
			  new AppUser
			  {
				  DisplayName = "Sara Receptionist",
				  UserName = "sarareceptionist",
				  Email = "sarareceptionist@example.com",
				  PhoneNumber = "0109876543",
				  Address = new Address
				  {
					  FristName = "Sara",
					  LastName = "Receptionist",
					  Country = "Egypt",
					  City = "Giza",
					  Street = "25 Pyramid St."
				  },
				  UserCode = GenerateUserCode("Receptionist", 1) ,// توليد UserCode للمستخدم
				  	City = string.Empty,
				Gender = string.Empty,
			  },
			  "Receptionist@123"
		  )
	  };

			await SeedUsersWithRole(userManager, receptionistUsers, "Receptionist");
		}
		private static async Task SeedUsersWithRole(UserManager<AppUser> userManager, List<(AppUser User, string Password)> users, string role)
		{
			foreach (var (user, password) in users)
			{
				if (await userManager.FindByEmailAsync(user.Email) == null)
				{
					var result = await userManager.CreateAsync(user, password);
					if (result.Succeeded)
					{
						await userManager.AddToRoleAsync(user, role);
					}
				}
			}
		}

		private static string GenerateUserCode(string role, int userCount)
		{
			return $"{role.Substring(0, 2).ToUpper()}-{DateTime.UtcNow.ToString("yyMMdd")}-{userCount}";
		}

	}
}