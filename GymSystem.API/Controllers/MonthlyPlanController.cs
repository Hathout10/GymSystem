using AutoMapper;
using GymSystem.BLL.Dtos.MonthlyPlan;
using GymSystem.BLL.Interfaces;
using GymSystem.BLL.Specifications;
using GymSystem.BLL.Errors;
using GymSystem.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymSystem.API.Controllers
{
    [Authorize]
    public class MonthlyPlanController : BaseApiController
    {
        private readonly IMonthlyPlanService _monthlyPlanService;

        public MonthlyPlanController(IMonthlyPlanService monthlyPlanService)
        {
            _monthlyPlanService = monthlyPlanService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MonthlyPlanDto>> GetMonthlyPlanById(int id)
        {
            var monthlyPlanDto = await _monthlyPlanService.GetMonthlyPlanByIdAsync(id);
            if (monthlyPlanDto == null) return NotFound(new ApiResponse(404, "Monthly plan not found."));
            return Ok(monthlyPlanDto);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pagination<MonthlyPlanDto>>> GetMonthlyPlans([FromQuery] SpecPrams specParams)
        {
            var monthlyPlansDto = await _monthlyPlanService.GetAllMonthlyPlansWithSpecAsync(specParams);
            if (monthlyPlansDto == null || !monthlyPlansDto.Any())
                return NotFound(new ApiResponse(404, "No monthly plans found."));

            var totalItems = await _monthlyPlanService.CountAsync(specParams);
            return Ok(new Pagination<MonthlyPlanDto>(specParams.PageIndex, specParams.PageSize, totalItems, monthlyPlansDto));
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Receptionist")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiValidationErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MonthlyPlanDto>> CreateMonthlyPlan(CreateMonthlyPlanDto createMonthlyPlanDto)
        {
            var createdMonthlyPlanDto = await _monthlyPlanService.CreateMonthlyPlanAsync(createMonthlyPlanDto);
            return CreatedAtAction(nameof(GetMonthlyPlanById), new { id = createdMonthlyPlanDto.Id }, createdMonthlyPlanDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Receptionist")] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiValidationErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MonthlyPlanDto>> UpdateMonthlyPlan(int id, UpdateMonthlyPlanDto updateMonthlyPlanDto)
        {
            if (id != updateMonthlyPlanDto.Id)
                return BadRequest(new ApiResponse(400, "ID mismatch."));

            var updatedMonthlyPlanDto = await _monthlyPlanService.UpdateMonthlyPlanAsync(id, updateMonthlyPlanDto);
            if (updatedMonthlyPlanDto == null) return NotFound(new ApiResponse(404, "Monthly plan not found."));
            return Ok(updatedMonthlyPlanDto);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteMonthlyPlan(int id)
        {
            var result = await _monthlyPlanService.DeleteMonthlyPlanAsync(id);
            if (!result) return NotFound(new ApiResponse(404, "Monthly plan not found."));
            return NoContent();
        }

        [HttpPatch("{id}/stop")]
        [Authorize(Roles = "Admin, Receptionist")] // Only Admin and Receptionist can stop plans
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MonthlyPlanDto>> StopMonthlyPlan(int id)
        {
            var stoppedMonthlyPlanDto = await _monthlyPlanService.StopMonthlyPlanAsync(id);
            if (stoppedMonthlyPlanDto == null) return NotFound(new ApiResponse(404, "Monthly plan not found."));
            return Ok(stoppedMonthlyPlanDto);
        }
    }
}