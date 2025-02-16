using GymSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.DAL.Data
{
    public class GymSystemContext :DbContext
    {
        public GymSystemContext(DbContextOptions<GymSystemContext> options) :base(options) { }

        // تعريف الجداول الخاصة بالكيانات الأخرى
        public DbSet<DailyPlan> DailyPlans { get; set; }
        public DbSet<MonthlyPlan> MonthlyPlans { get; set; }
        public DbSet<NutritionPlan> NutritionPlans { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<MealsCategory> MealsCategories { get; set; }


    }
}
