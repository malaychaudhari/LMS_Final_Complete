using LogisticsManagement.Service.DTOs;
using LogisticsManagement.Service.Services.IServices;
using LogisticsManagement.WebAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LogisticsManagement.WebAPI.Controllers
{
    [Route("api/statistics")]
    [ApiController]

    [Authorize(Roles = "Admin")]

    public class StatisticsController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public StatisticsController(IAdminService adminService)
        {
            _adminService = adminService;

        }

        [HttpGet("admin", Name = "GetStatistics")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetStatistics()
        {
            try
            {
                AdminSummaryStatisticsDTO? statistics = await _adminService.GetAdminSummaryStatisticsAsync();
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
