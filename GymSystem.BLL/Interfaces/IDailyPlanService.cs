using GymSystem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.BLL.Interfaces
{
    public interface IDailyPlanService
    {
        Task<DailyPlan> CreateDailyPlanAsync(DailyPlan dailyPlan);
        Task<DailyPlan> UpdateDailyPlanAsync(DailyPlan dailyPlan);
        Task DeleteDailyPlanAsync(int id);
        Task<DailyPlan> GetDailyPlanByIdAsync(int id);
        Task<IReadOnlyList<DailyPlan>> GetAllDailyPlansAsync();
        Task<DailyPlan> StopDailyPlanAsync(int id);
    }
}
