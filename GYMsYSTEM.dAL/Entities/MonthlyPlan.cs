using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.DAL.Entities
{
    public class MonthlyPlan : SubscriptionPlan
    {
        public int DurationInDays { get; set; }
    }
}
