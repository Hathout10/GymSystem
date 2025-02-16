using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.BLL.Dtos.DailyPlan
{
    public class CreateDailyPlanDto
    {
        [Required(ErrorMessage = "The plan name is required.")]
        [StringLength(100, ErrorMessage = "The plan name must be between 1 and 100 characters.", MinimumLength = 1)]
        public string Name { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "The price must be greater than zero.")]
        public decimal Price { get; set; }
    }
}
