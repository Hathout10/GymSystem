using GymSystem.DAL.Entities.Identity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.DAL.Entities
{
    //نشاطًا اللي بيحصل في الجيم، زي اليوجا، التدريب الرياضي
    public class Session : BaseEntity
    {
        public string? ImageUrl { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public string SessionName { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsDeleted { get; set; }

        // العلاقات
        public string TrainerId { get; set; } // تم تغييرها من int إلى string لأن AppUser يستخدم Id من نوع string
        public AppUser Trainer { get; set; }
        public ICollection<Membership> Memberships { get; set; } = new List<Membership>();
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    }
}
