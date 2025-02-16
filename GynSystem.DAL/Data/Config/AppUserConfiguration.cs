using GymSystem.DAL.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymSystem.DAL.Data.Config
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            // 1. تكوين الفهرس الفريد لـ DisplayName
            builder.HasIndex(u => u.DisplayName).IsUnique();

            // 2. تكوين العلاقة مع Address (واحد إلى واحد)
            builder.HasOne(u => u.Address)
                   .WithOne(a => a.User)
                   .HasForeignKey<Address>(a => a.AppUserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // 3. تكوين العلاقة مع DailyPlan (واحد إلى واحد اختياري)
            builder.HasOne(u => u.DailyPlan)
                   .WithMany()
                   .HasForeignKey(u => u.DailyPlanId)
                   .OnDelete(DeleteBehavior.SetNull);

            // 4. تكوين العلاقة مع MonthlyPlan (واحد إلى واحد اختياري)
            builder.HasOne(u => u.MonthlyPlan)
                   .WithMany()
                   .HasForeignKey(u => u.MonthlyPlanId)
                   .OnDelete(DeleteBehavior.SetNull);

            // 5. تكوين العلاقة مع NutritionPlan (واحد إلى كثير)
            builder.HasOne(u => u.NutritionPlan)
                   .WithMany(np => np.Users)
                   .HasForeignKey(u => u.NutritionPlanId)
                   .OnDelete(DeleteBehavior.SetNull);

            // 6. تكوين العلاقة مع Memberships (واحد إلى كثير)
            builder.HasMany(u => u.Memberships)
                   .WithOne(m => m.User)
                   .HasForeignKey(m => m.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // 7. تكوين العلاقة مع Attendances (واحد إلى كثير)
            builder.HasMany(u => u.Attendances)
                   .WithOne(a => a.User)
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // 8. تكوين العلاقة مع RefreshTokens (واحد إلى كثير)
            builder.HasMany(u => u.RefreshTokens)
                   .WithOne(rt => rt.User)
                   .HasForeignKey(rt => rt.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // 15. تكوين فلتر للاستعلامات لاستبعاد المستخدمين المحذوفين
            builder.HasQueryFilter(u => !u.IsDeleted);

            // 16. تكوين الخصائص الإضافية
            builder.Property(u => u.UserCode).IsRequired();
            builder.Property(u => u.Gender).HasMaxLength(10); // مثال: Male, Female, Other
            builder.Property(u => u.City).HasMaxLength(100);
            builder.Property(u => u.ProfileImageName).HasMaxLength(255);
        }
    }
}