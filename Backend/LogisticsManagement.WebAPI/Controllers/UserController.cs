using Azure;
using LogisticsManagement.DataAccess.Models;
using LogisticsManagement.Service.DTOs;
using LogisticsManagement.Service.Enums;
using LogisticsManagement.Service.Services;
using LogisticsManagement.Service.Services.IServices;
using LogisticsManagement.WebAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LogisticsManagement.WebAPI.Controllers
{
    [Route("api/user")]
    [ApiController]

    [Authorize()]
    public class UserController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public UserController(IAdminService adminService)
        {
            _adminService = adminService;

        }

        // Get Users by role
        [HttpGet("role/{id}", Name = "GetUsersByRole")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponseDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> GetUsersByRole(int id)
        {

            try
            {
                if (id <= 0 || !Enum.IsDefined(typeof(UserRoles), id))
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, null, error:"Please enter valid role"));
                }

                var users = await _adminService.GetUsersByRoleAsync(id);
                if (users.Count > 0)
                {
                    return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, users, string.Empty));
                }
                return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, Enumerable.Empty<UserDTO>(), string.Empty));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, null, ex.Message));
            }
        }

        // Get User by id
        [HttpGet("{id}", Name = "GetUser")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]



        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, null, error:"Please enter valid id"));
                }
                var user = await _adminService.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound(ApiResponseHelper.Response(false, HttpStatusCode.NotFound, null, "User with given id not found"));
                }

                return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, user, string.Empty));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, null, ex.Message));
            }

        }


        // Get Pending Users by role
        [HttpGet("pendingusers/role/{id}", Name = "GetPendingUsersByRole")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> GetPendingUsersByRole(int id)
        {

            try
            {
                if (id <= 0 || (id != (int)UserRoles.Manager && id != (int)UserRoles.Driver))
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, error: "Please enter valid role"));
                }

                var users = await _adminService.GetPendingUsersByRoleAsync(id);
                if (users.Count > 0)
                {
                    return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, data:users));
                }
                return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, Enumerable.Empty<UserDTO>(), string.Empty));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, error: ex.Message));
            }
        }


        // Approve or reject manager/driver signup request
        [HttpPut("signup-request", Name = "UpdateSignupRequest")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateSignupRequest([FromBody] SignUpRequestDTO signUpRequest)
        {
            try
            {

                if (signUpRequest == null || !ModelState.IsValid)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, error: "Please enter valid data"));
                }
                if (signUpRequest.UserId <= 0 || (signUpRequest.Status != (int)SignUpStatus.Approved && signUpRequest.Status != (int)SignUpStatus.Rejected))
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, error: "Please enter valid status"));
                }

                int response = await _adminService.UpdateUserSignUpRequestAsync(signUpRequest.UserId, signUpRequest.Status);

                if (response == 0)
                {
                    return NotFound(ApiResponseHelper.Response(false, HttpStatusCode.NotFound, error: "Admin/Driver with given id not found"));
                }

                if (response == -1)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError,error: "Failed to update user's sign-up request. Please try again later"));
                }
                return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, data: "User's status updated successfully"));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, error:ex.Message));
            }

        }

        //Delete user by making i inactive
        [HttpDelete("deactivate/{userId}", Name = "DeactivateUser")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            try
            {
                if (userId <= 0)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, error: "Please enter valid id"));
                }
                int response = await _adminService.DeleteUserAsync(userId);
                if (response == 0)
                {
                    return NotFound(ApiResponseHelper.Response(false, HttpStatusCode.NotFound, error: "User with given id not found"));
                }
                if (response == -1)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, error: "Failed to delete user. Please try again later"));
                }
                return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, data: "User blocked successfully"));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, error: ex.Message));
            }
        }

        [HttpPut("activate/{userId}", Name = "UnblockUser")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> UnblockUser(int userId)
        {
            try
            {
                if (userId <= 0)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, error: "Please enter valid id"));
                }
                int response = await _adminService.UnBlockUserById(userId);
                if (response == 0)
                {
                    return NotFound(ApiResponseHelper.Response(false, HttpStatusCode.NotFound, error: "User with given id not found"));
                }
                if (response == -1)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, error: "Failed to unblock user. Please try again later"));
                }
                return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, data: "User unblocked successfully"));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, error: ex.Message));
            }
        }


        //Assign Manager to Warehouse
        [HttpPut("assign-warehouse", Name = "AssignManagerToWarehouse")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> AssignManagerToWarehouse([FromBody] AssignManagerToWarehouseDTO assignManager)
        {
            try
            {
                if (assignManager == null || !ModelState.IsValid)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, error: "Please enter valid data"));
                }

                int response = await _adminService.AssignManagerToWarehouseAsync(assignManager.ManagerId, assignManager.WarehouseId);

                if (response == 0)
                    return NotFound(ApiResponseHelper.Response(false, HttpStatusCode.NotFound, error: "Manager or Warehouse with given id not found"));
                if (response == -1)

                    return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, error: "Failed to Assign Manager to warehouse. Please try again later"));

                return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, data: "Manager assigned to warehouse successfully"));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, error: ex.Message));
            }
        }

    }
}
