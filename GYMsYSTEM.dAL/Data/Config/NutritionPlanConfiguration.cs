﻿using GymSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.DAL.Data.Config
{
    public class NutritionPlanConfiguration : IEntityTypeConfiguration<NutritionPlan>
    {
        public void Configure(EntityTypeBuilder<NutritionPlan> builder)
        {
            builder.HasKey(np => np.Id);

            builder.HasMany(np => np.Users)
                   .WithOne(u => u.NutritionPlan)
                   .HasForeignKey(u => u.NutritionPlanId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(np => np.Meals)
                   .WithOne(m => m.NutritionPlan)
                   .HasForeignKey(m => m.NutritionPlanId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasQueryFilter(np => !np.IsDeleted);
        }
    }
}
