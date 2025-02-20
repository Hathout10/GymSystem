using GymSystem.DAL.Entities.Enums.Business;
using GymSystem.DAL.Entities.Identity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.DAL.Entities
{
    public class Membership : BaseEntity
    {
        public string? ImageUrl { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }

        // العلاقات
        public string? UserId { get; set; } // تم تغييرها من int إلى string لأن AppUser يستخدم Id من نوع string
        public AppUser? User { get; set; }
        public int? SessionId { get; set; }
        public Session? Session { get; set; }
		public int? SubscriptionPlanId { get; set; }

		public SubscriptionPlan? SubscriptionPlan { get; set; }

	}
}
