using LogisticsManagement.DataAccess.Models;
using LogisticsManagement.Service.DTOs;
using LogisticsManagement.Service.Enums;
using LogisticsManagement.Service.Services.IServices;
using LogisticsManagement.WebAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LogisticsManagement.WebAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IAdminService _adminService;
        private APIResponseDTO _response;
        public AuthController(IAuthService authService, IAdminService adminService)
        {
            _authService = authService;
            _response = new();
            _adminService = adminService;
        }


        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            try
            {

                //check if login model is not null and model state is valid
                if (login == null || !ModelState.IsValid)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, null, "Please enter valid data"));
                }

                var response = await _authService.LoginAsync(login);

                if (response.Token == null)
                {
                    //return unauthorized with appropriate error message
                    return Unauthorized(ApiResponseHelper.Response(false, HttpStatusCode.Unauthorized, null, response.Message));
                }

                //return response with token if response is not null
                return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, response.Token, string.Empty));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, null, ex.Message));
            }
        }

        [HttpPost("signup")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignUp([FromBody] UserDTO user)
        {

            try
            {

                //check if userDTO model is not null and model state is valid
                if (user == null || !ModelState.IsValid)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, null, "Please enter valid data"));
                }
                if (user.Id > 0)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, null, "Please don't send Id while creating the user"));
                }

                //call signup service method
                int result = await _authService.SignUpAsync(user);

                //if result is -1 then user already exists
                if (result == -1)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, null, "User with email already exists"));
                }
                //if result > 0 then user created
                else if (result > 0)
                {
                    return CreatedAtAction("GetUser", new { id = result }, null);
                    //return CreatedAtRoute("GetUser", new { id = userInfo.Id }, userInfo);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, null, "Internal server error while signing up"));
        }


        [HttpGet("user/{id}", Name = "GetUserAuth")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, null, "Please enter valid id"));
                }
                var user = await _authService.GetUserByIdAsync(id);
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


    }
}
