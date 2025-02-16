using GymSystem.DAL.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.BLL.Specifications.MonthlyPlan_Specification
{
    public class MonthlyPlanSpecification : BaseSpecification<MonthlyPlan>
    {
        public MonthlyPlanSpecification(SpecPrams specParams)
            : base(x =>
                (string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search.ToLower())) &&
                !x.IsStopped
            )
        {
            AddOrderBy(x => x.Name);

            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case "nameAsc":
                        AddOrderBy(x => x.Name);
                        break;
                    case "nameDesc":
                        AddOrderByDescending(x => x.Name);
                        break;
                    default:
                        AddOrderBy(x => x.Name);
                        break;
                }
            }

        }
    }
}
