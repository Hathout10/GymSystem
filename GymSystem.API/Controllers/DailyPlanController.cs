using AutoMapper;
using GymSystem.BLL.Errors;
using GymSystem.API.Helpers;
using GymSystem.BLL.Interfaces;
using GymSystem.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GymSystem.BLL.Dtos.DailyPlan;

namespace GymSystem.API.Controllers
{

    public class DailyPlanController : BaseApiController
    {
        private readonly IDailyPlanService _dailyPlanService;
        private readonly IMapper _mapper;

        public DailyPlanController(IDailyPlanService dailyPlanService, IMapper mapper)
        {
            _dailyPlanService = dailyPlanService;
            _mapper = mapper;
        }

        [CachedResponse(700)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<DailyPlanDto>>> GetAllDailyPlans()
        {
            var plans = await _dailyPlanService.GetAllDailyPlansAsync();
            var activePlans = plans.Where(p => !p.IsStopped).ToList();
            return Ok(_mapper.Map<IReadOnlyList<DailyPlanDto>>(activePlans));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DailyPlanDto>> GetDailyPlanById(int id)
        {
            var dailyPlan = await _dailyPlanService.GetDailyPlanByIdAsync(id);
            if (dailyPlan == null)
            {
                return NotFound(new ApiResponse(404, "Daily plan not found"));
            }
            return Ok(_mapper.Map<DailyPlanDto>(dailyPlan));
        }

        [HttpPost]
        public async Task<ActionResult<DailyPlanDto>> CreateDailyPlan(CreateDailyPlanDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiValidationErrorResponse { Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList() });
            }

            var dailyPlan = _mapper.Map<DailyPlan>(dto);
            var createdPlan = await _dailyPlanService.CreateDailyPlanAsync(dailyPlan);
            return Ok(_mapper.Map<DailyPlanDto>(createdPlan));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DailyPlanDto>> UpdateDailyPlan(int id, UpdateDailyPlanDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiValidationErrorResponse { Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList() });
            }

            var dailyPlan = _mapper.Map<DailyPlan>(dto);
            dailyPlan.Id = id;

            var updatedPlan = await _dailyPlanService.UpdateDailyPlanAsync(dailyPlan);
            return Ok(_mapper.Map<DailyPlanDto>(updatedPlan));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDailyPlan(int id)
        {
            await _dailyPlanService.DeleteDailyPlanAsync(id);
            return Ok(new ApiResponse(200, $"DailyPlan with ID {id} has been deleted successfully."));
        }

        [HttpPost("{id}/stop")]
        public async Task<ActionResult<DailyPlanDto>> StopDailyPlan(int id)
        {
            var updatedPlan = await _dailyPlanService.StopDailyPlanAsync(id);
            return Ok(_mapper.Map<DailyPlanDto>(updatedPlan));
        }
    }
}
