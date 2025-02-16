using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.BLL.Dtos.NutritionPlan
{
    public class NutritionPlanDto
    {
        public int Id { get; set; }
        public string PlanName { get; set; }
        public string Description { get; set; }
    }
}
