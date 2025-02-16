using GymSystem.BLL.Errors;
using GymSystem.BLL.Dtos.NutritionPlan;

using GymSystem.BLL.Interfaces.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymSystem.API.Controllers
{
        public class NutritionPlanController : BaseApiController
        {
            private readonly INutritionPlanRepo _nutritionPlanRepo;

            public NutritionPlanController(INutritionPlanRepo nutritionPlanRepo)
            {
                _nutritionPlanRepo = nutritionPlanRepo;
            }

            /// <summary>
            /// Get all active Nutrition Plans.
            /// </summary>
            /// <response code="200">Returns the list of Nutrition Plans.</response>
            /// <response code="500">If an error occurs while fetching the data.</response>
            [HttpGet("GetNutritionPlans")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public async Task<IActionResult> GetNutritionPlans()
            {
                try
                {
                    var nutritionPlans = await _nutritionPlanRepo.GetNutritionPlans();
                    return Ok(nutritionPlans);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse(500, "An error occurred: " + ex.Message));
                }
            }

            /// <summary>
            /// Get a specific Nutrition Plan by ID.
            /// </summary>
            /// <param name="nutritionPlanId">The ID of the Nutrition Plan.</param>
            /// <response code="200">Returns the Nutrition Plan details.</response>
            /// <response code="404">If the Nutrition Plan is not found.</response>
            /// <response code="500">If an error occurs while fetching the data.</response>
            [HttpGet("GetNutritionPlan/{nutritionPlanId}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public async Task<IActionResult> GetNutritionPlan(int nutritionPlanId)
            {
                try
                {
                    var nutritionPlan = await _nutritionPlanRepo.GetNutritionPlan(nutritionPlanId);

                    if (nutritionPlan == null)
                    {
                        return NotFound(new ApiResponse(404, "Nutrition Plan not found"));
                    }

                    return Ok(nutritionPlan);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse(500, "An error occurred: " + ex.Message));
                }
            }

            /// <summary>
            /// Create a new Nutrition Plan.
            /// </summary>
            /// <param name="nutritionPlanDto">The Nutrition Plan data.</param>
            /// <response code="200">If the Nutrition Plan is created successfully.</response>
            /// <response code="400">If the Nutrition Plan already exists.</response>
            /// <response code="500">If an error occurs while creating the Nutrition Plan.</response>
            [Authorize(Roles = "Admin,Trainer")]
            [HttpPost("CreateNutritionPlan")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public async Task<IActionResult> CreateNutritionPlan([FromForm] NutritionPlanDto nutritionPlanDto)
            {
                try
                {
                    var response = await _nutritionPlanRepo.CreateNutritionPlan(nutritionPlanDto);

                    if (response.StatusCode == 400)
                    {
                        return BadRequest(response);
                    }

                    return Ok(response);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse(500, "An error occurred: " + ex.Message));
                }
            }

            /// <summary>
            /// Update an existing Nutrition Plan.
            /// </summary>
            /// <param name="nutritionPlanId">The ID of the Nutrition Plan to update.</param>
            /// <param name="nutritionPlanDto">The updated Nutrition Plan data.</param>
            /// <response code="200">If the Nutrition Plan is updated successfully.</response>
            /// <response code="404">If the Nutrition Plan is not found.</response>
            /// <response code="500">If an error occurs while updating the Nutrition Plan.</response>
            [Authorize(Roles = "Admin,Trainer")]
            [HttpPut("UpdateNutritionPlan/{nutritionPlanId}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public async Task<IActionResult> UpdateNutritionPlan(int nutritionPlanId, [FromForm] NutritionPlanDto nutritionPlanDto)
            {
                try
                {
                    var response = await _nutritionPlanRepo.UpdateNutritionPlan(nutritionPlanId, nutritionPlanDto);

                    if (response.StatusCode == 404)
                    {
                        return NotFound(response);
                    }

                    return Ok(response);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse(500, "An error occurred: " + ex.Message));
                }
            }

            /// <summary>
            /// Delete a Nutrition Plan by marking it as deleted.
            /// </summary>
            /// <param name="nutritionPlanId">The ID of the Nutrition Plan to delete.</param>
            /// <response code="200">If the Nutrition Plan is deleted successfully.</response>
            /// <response code="404">If the Nutrition Plan is not found.</response>
            /// <response code="500">If an error occurs while deleting the Nutrition Plan.</response>
            [Authorize(Roles = "Admin,Trainer")]
            [HttpDelete("DeleteNutritionPlan/{nutritionPlanId}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public async Task<IActionResult> DeleteNutritionPlan(int nutritionPlanId)
            {
                try
                {
                    var response = await _nutritionPlanRepo.DeleteNutritionPlan(nutritionPlanId);

                    if (response.StatusCode == 404)
                    {
                        return NotFound(response);
                    }

                    return Ok(response);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse(500, "An error occurred: " + ex.Message));
                }
            }
        }
}
