using GymSystem.BLL.Interfaces;
using GymSystem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.BLL.Services
{
    public class DailyPlanService : IDailyPlanService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DailyPlanService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DailyPlan> CreateDailyPlanAsync(DailyPlan dailyPlan)
        {
            await _unitOfWork.Repository<DailyPlan>().Add(dailyPlan);
            await _unitOfWork.Complete();
            return dailyPlan;
        }

        public async Task<DailyPlan> UpdateDailyPlanAsync(DailyPlan dailyPlan)
        {
            var existingPlan = await _unitOfWork.Repository<DailyPlan>().GetByIdAsync(dailyPlan.Id);
            if (existingPlan is null)
            {
                throw new KeyNotFoundException("Daily Plan not found");
            }

            existingPlan.Name = dailyPlan.Name;
            existingPlan.Price = dailyPlan.Price;

            _unitOfWork.Repository<DailyPlan>().Update(existingPlan);
            await _unitOfWork.Complete();
            return existingPlan;
        }

        public async Task DeleteDailyPlanAsync(int id)
        {
            var dailyPlan = await _unitOfWork.Repository<DailyPlan>().GetByIdAsync(id);
            if (dailyPlan is null)
            {
                throw new KeyNotFoundException("Daily Plan not found");
            }

            _unitOfWork.Repository<DailyPlan>().Delete(dailyPlan);
            await _unitOfWork.Complete();
        }

        public async Task<DailyPlan> GetDailyPlanByIdAsync(int id)
        {
            var dailyPlan = await _unitOfWork.Repository<DailyPlan>().GetByIdAsync(id);
            if (dailyPlan is null)
            {
                throw new KeyNotFoundException("Daily Plan not found");
            }
            return dailyPlan;
        }

        public async Task<IReadOnlyList<DailyPlan>> GetAllDailyPlansAsync()
        {
            return await _unitOfWork.Repository<DailyPlan>().GetAllAsync();
        }

        public async Task<DailyPlan> StopDailyPlanAsync(int id)
        {
            var dailyPlan = await _unitOfWork.Repository<DailyPlan>().GetByIdAsync(id);
            if (dailyPlan is null)
            {
                throw new KeyNotFoundException("Daily Plan not found");
            }

            dailyPlan.IsStopped = !dailyPlan.IsStopped;
            _unitOfWork.Repository<DailyPlan>().Update(dailyPlan);
            await _unitOfWork.Complete();
            return dailyPlan;
        }
    }
}

