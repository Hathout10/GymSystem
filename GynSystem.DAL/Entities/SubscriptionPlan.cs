using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.DAL.Entities
{
    public abstract class SubscriptionPlan : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsStopped { get; set; } = false;
        public DateTime? StopDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public ICollection<Subscriber> Subscribers { get; set; } = new List<Subscriber>();
    }
}
