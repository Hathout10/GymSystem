using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymSystem.DAL.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        // الخصائص الأساسية
        public string DisplayName { get; set; }
        public Address Address { get; set; }
        public int UserRole { get; set; } // يمكن استخدام الأدوار (Roles) بدلاً من هذه الخاصية
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? ProfileImageName { get; set; }
        public string UserCode { get; set; }

        [NotMapped]
        public IFormFile? Image { get; set; }

        // الخصائص المضافة من Subscriber
        public string Gender { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string City { get; set; }
        public bool IsStopped { get; set; } = false;
        public DateTime? StopDate { get; set; }
        public int HaveDays { get; set; }
        public string AddBy { get; set; }
        public int RemainingDays { get; set; }

        // العلاقات
        public int? DailyPlanId { get; set; }
        public DailyPlan DailyPlan { get; set; }

        public int? MonthlyPlanId { get; set; }
        public MonthlyPlan MonthlyPlan { get; set; }

        public int? NutritionPlanId { get; set; }
        public NutritionPlan NutritionPlan { get; set; }

        public ICollection<Membership> Memberships { get; set; } = new List<Membership>();
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}