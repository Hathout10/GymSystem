using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.DAL.Entities.Identity
{
    //  (Owned Entity Type)  الغرض: تُستخدم هذه السمة في Entity Framework Core للإشارة إلى أن الكلاس RefreshToken هو نوع مملوك

    // لا يوجد بشكل مستقل في قاعدة البيانات، بل يكون جزءًا من كيان آخر (Entity) النوع المملوك هو كيان

    // ويتم تخزينه في نفس الجدول الخاص بالمستخدم في قاعدة البيانات User جزءًا من كلاس RefreshToken على سبيل المثال، يمكن أن يكون.

    // (User) كأعمدة إضافية في جدول الكلاس الرئيسي RefreshToken  يتم تخزين خصائص 
    [Owned]
    public class RefreshToken
    {
		public int Id { get; set; }
		public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? Revoked { get; set; }
        public bool IsActive => Revoked == null && !IsExpired;

        // إضافة UserId كـ Foreign Key
        public string? UserId { get; set; }
        public AppUser? User { get; set; }
    }
}
