using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.BLL.Dtos.DailyPlan
{
    public class UpdateDailyPlanDto : CreateDailyPlanDto
    {
        public int Id { get; set; }
    }
}
