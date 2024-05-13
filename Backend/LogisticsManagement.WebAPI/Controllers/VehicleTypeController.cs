using LogisticsManagement.Service.DTOs;
using LogisticsManagement.Service.Services.IServices;
using LogisticsManagement.WebAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LogisticsManagement.WebAPI.Controllers
{
    [Route("api/vehicle-type")]
    [ApiController]

    [Authorize(Roles = "Admin,Manager")]
    public class VehicleTypeController : ControllerBase
    {
        private readonly IManagerService _managerService;
        public VehicleTypeController(IManagerService managerService)
        {
            _managerService = managerService;
        }


        // Get All Vehicle Types
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<VehicleTypeDTO>? vehicleTypes = await _managerService.GetVehicleType();
                if (vehicleTypes == null)
                {
                    return NotFound(ApiResponseHelper.Response(false, HttpStatusCode.NotFound, null, "No Vehicle Types Available."));
                }
                return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, vehicleTypes, string.Empty));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, null, ex.Message));
            }
        }
    }
}
