using GymSystem.API.Controllers;
using GymSystem.BLL.Dtos;
using GymSystem.BLL.Dtos.Role;
using GymSystem.BLL.Errors;
using GymSystem.DAL.Entities.Enums.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymSystem.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : BaseApiController
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        /// <summary>
        /// Get all roles.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> GetRoles()
        {
            try
            {
                var roles = await _roleManager.Roles.ToListAsync();
                var roleDTOs = roles.Select(r => new RoleDTO
                {
                    Id = r.Id,
                    Name = r.Name // هنا نستخدم اسم الدور كسلسلة نصية
                }).ToList();

                return new ApiResponse(200, "Roles retrieved successfully", roleDTOs);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Create a new role.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> CreateRole([FromBody] RoleFormDTO model)
        {
            if (!ModelState.IsValid)
            {
                return new ApiValidationErrorResponse { Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList() };
            }

            try
            {
                // التحقق من وجود الدور بالفعل
                if (await _roleManager.RoleExistsAsync(model.Name))
                {
                    return new ApiResponse(409, "Role already exists!");
                }

                var result = await _roleManager.CreateAsync(new IdentityRole(model.Name.Trim()));
                if (result.Succeeded)
                {
                    return new ApiResponse(200, "Role created successfully!");
                }

                return new ApiExceptionResponse(400, "Failed to create role", string.Join(", ", result.Errors.Select(e => e.Description)));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Delete a role by ID.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> DeleteRole(string id)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role == null)
                {
                    return new ApiResponse(404, "Role not found!");
                }

                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return new ApiResponse(200, "Role deleted successfully!");
                }

                return new ApiExceptionResponse(400, "Failed to delete role", string.Join(", ", result.Errors.Select(e => e.Description)));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Update an existing role.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> UpdateRole(string id, [FromBody] RoleDTO model)
        {
            if (!ModelState.IsValid)
            {
                return new ApiValidationErrorResponse { Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList() };
            }

            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role == null)
                {
                    return new ApiResponse(404, "Role not found!");
                }

                // التحقق من وجود دور بنفس الاسم
                if (await _roleManager.RoleExistsAsync(model.Name) && role.Name != model.Name)
                {
                    return new ApiResponse(409, "Role already exists!");
                }

                // تحديث اسم الدور
                role.Name = model.Name;
                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return new ApiResponse(200, "Role updated successfully!");
                }

                return new ApiExceptionResponse(400, "Failed to update role", string.Join(", ", result.Errors.Select(e => e.Description)));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Handle exceptions and return an appropriate response.
        /// </summary>
        private ActionResult<ApiResponse> HandleException(Exception ex)
        {
            return new ApiExceptionResponse(500, "An unexpected error occurred", ex.Message);
        }

        private static string UserRoleEnumToString(UserRoleEnum role)
        {
            return role.ToString(); 
        }

        private static UserRoleEnum StringToUserRoleEnum(string roleName)
        {
            return (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), roleName); // تحويل سلسلة نصية إلى Enum
        }
    }
}