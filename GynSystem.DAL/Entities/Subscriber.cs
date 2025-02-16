using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.DAL.Entities
{
    public class Subscriber : BaseEntity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Gmail { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string City { get; set; }
        public bool IsStopped { get; set; } = false;
        public DateTime? StopDate { get; set; }
        public int HaveDays { get; set; }
        public string AddBy { get; set; }
        public int RemainingDays { get; set; }
        public int? DailyPlanId { get; set; }
        public DailyPlan DailyPlan { get; set; }
        public int? MonthlyPlanId { get; set; }
        public MonthlyPlan MonthlyPlan { get; set; }
    }
}
