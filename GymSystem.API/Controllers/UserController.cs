using GymSystem.API.Controllers;
using GymSystem.BLL.Dtos;
using GymSystem.BLL.Dtos.Role;
using GymSystem.BLL.Errors;
using GymSystem.DAL.Entities.Enums.Auth;
using GymSystem.DAL.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymSystem.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : BaseApiController
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public UserController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet("users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> GetUsers()
        {
            try
            {
                var users = await _userManager.Users.ToListAsync();
                var userDtos = new List<UserDto>();

                foreach (var user in users)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var userRoles = roles.Select(role => Enum.Parse<UserRoleEnum>(role)).ToList();

                    userDtos.Add(new UserDto
                    {
                        Id = user.Id,
                        DisplayName = user.DisplayName,
                        UserName = user.UserName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Roles = userRoles
                    });
                }

                return new ApiResponse(200, "Users retrieved successfully", userDtos);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

       
        [HttpGet("users/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> GetUser(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);

                if (user == null)
                {
                    return new ApiResponse(404, "User not found!");
                }

                var allRoles = await _roleManager.Roles.ToListAsync();
                var rolesWithSelection = new List<RoleDTO>();

                foreach (var role in allRoles)
                {
                    var isAssigned = await _userManager.IsInRoleAsync(user, role.Name);
                    rolesWithSelection.Add(new RoleDTO
                    {
                        //Name = Enum.Parse<UserRoleEnum>(role.Name),
                        IsSelected = isAssigned
                    });
                }

                var userRoleDto = new UserRoleDTO
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Roles = rolesWithSelection
                };

                return new ApiResponse(200, "User retrieved successfully", userRoleDto);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPut("users/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> UpdateUserRoles(string id, UserRoleDTO model)
        {
            if (!ModelState.IsValid)
            {
                return new ApiValidationErrorResponse
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage))
                };
            }

            try
            {
                var user = await _userManager.FindByIdAsync(model.UserId);

                if (user == null)
                {
                    return new ApiResponse(404, "User not found!");
                }

                var currentRoles = await _userManager.GetRolesAsync(user);

                // Convert roles to string names
                var updatedRoles = model.Roles
                    .Where(r => r.IsSelected)
                    .Select(r => r.Name.ToString())
                    .ToList();

                // Add new roles
                var rolesToAdd = updatedRoles.Except(currentRoles).ToList();
                if (rolesToAdd.Any())
                {
                    await _userManager.AddToRolesAsync(user, rolesToAdd);
                }

                // Remove old roles
                var rolesToRemove = currentRoles.Except(updatedRoles).ToList();
                if (rolesToRemove.Any())
                {
                    await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
                }

                return new ApiResponse(200, "User roles updated successfully!");
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        private ActionResult<ApiResponse> HandleException(Exception ex)
        {
            return new ApiExceptionResponse(500, "An unexpected error occurred", ex.Message);
        }
    }
}