using GymSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.DAL.Data
{
	public class GymSystemContext : DbContext
	{
		public GymSystemContext(DbContextOptions<GymSystemContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Membership>()
	                    .HasOne(m => m.Session)
	                    .WithMany()
	                    .HasForeignKey(m => m.SessionId)
	                    .OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Membership>()
	                    .HasOne(m => m.SubscriptionPlan)
	                    .WithMany()
	                    .HasForeignKey(m => m.SubscriptionPlanId)
	                    .OnDelete(DeleteBehavior.NoAction);
		}

		// تعريف الجداول الخاصة بالكيانات الأخرى
		public DbSet<DailyPlan> DailyPlans { get; set; }
		public DbSet<MonthlyPlan> MonthlyPlans { get; set; }
		public DbSet<NutritionPlan> NutritionPlans { get; set; }
		public DbSet<Membership> Memberships { get; set; }
		public DbSet<Session> Sessions { get; set; }
		public DbSet<Attendance> Attendances { get; set; }
		public DbSet<Meal> Meals { get; set; }
		public DbSet<MealsCategory> MealsCategories { get; set; }


	}
}
