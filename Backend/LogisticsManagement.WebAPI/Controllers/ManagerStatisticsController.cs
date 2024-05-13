using LogisticsManagement.Service.DTOs;
using LogisticsManagement.Service.Services;
using LogisticsManagement.Service.Services.IServices;
using LogisticsManagement.WebAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LogisticsManagement.WebAPI.Controllers
{
    [Route("api/manager-statistics")]
    [ApiController]

    [Authorize(Roles = "Manager")]
    public class ManagerStatisticsController : ControllerBase
    {
        private readonly IManagerService _managerService;
        public ManagerStatisticsController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        [HttpGet("manager", Name = "GetManagerStatistics")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetStatistics()
        {
            try
            {
                ManagerSummaryStatisticsDTO? statistics = await _managerService.GetManagerStatistics();
                if (statistics == null)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, error: "Something went wrong"));
                }

                return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, data: statistics));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, error: ex.Message));
            }
        }
    }
}
