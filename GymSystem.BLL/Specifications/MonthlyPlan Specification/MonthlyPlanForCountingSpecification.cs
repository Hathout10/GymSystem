using GymSystem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.BLL.Specifications.MonthlyPlan_Specification
{
    public class MonthlyPlanForCountingSpecification : BaseSpecification<MonthlyPlan>
    {
        public MonthlyPlanForCountingSpecification(SpecPrams specParams)
            : base(x =>
                (string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search))
            )
        {
        }
    }
}
