using GymSystem.DAL.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.DAL.Entities
{
    public class Attendance : BaseEntity
    {
        public bool IsAttended { get; set; }
        public DateTime AttendanceDate { get; set; } = DateTime.Now;

        // العلاقات
        public string UserId { get; set; } // تم تغييرها من int إلى string لأن AppUser يستخدم Id من نوع string
        public AppUser User { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }
    }
}
