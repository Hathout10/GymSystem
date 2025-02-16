using GymSystem.BLL.Dtos.MonthlyPlan;
using GymSystem.BLL.Specifications;
using GymSystem.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymSystem.BLL.Interfaces
{
    public interface IMonthlyPlanService
    {
        Task<MonthlyPlanDto> GetMonthlyPlanByIdAsync(int id);
        Task<IReadOnlyList<MonthlyPlanDto>> GetAllMonthlyPlansWithSpecAsync(SpecPrams specParams);
        Task<MonthlyPlanDto> CreateMonthlyPlanAsync(CreateMonthlyPlanDto createMonthlyPlanDto);
        Task<MonthlyPlanDto> UpdateMonthlyPlanAsync(int id, UpdateMonthlyPlanDto updateMonthlyPlanDto);
        Task<bool> DeleteMonthlyPlanAsync(int id);
        Task<MonthlyPlanDto> StopMonthlyPlanAsync(int id);
        Task<int> CountAsync(SpecPrams specParams);
    }
}