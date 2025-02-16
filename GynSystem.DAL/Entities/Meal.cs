using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.DAL.Entities
{
    public class Meal : BaseEntity
    {
        public string? ImageUrl { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public string MealName { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public int? NutritionPlanId { get; set; }
        public NutritionPlan? NutritionPlan { get; set; }
        public int MealsCategoryId { get; set; }
        public MealsCategory MealsCategory { get; set; }
    }
}
