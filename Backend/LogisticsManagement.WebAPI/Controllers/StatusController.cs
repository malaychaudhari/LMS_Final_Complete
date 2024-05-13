using LogisticsManagement.Service.DTOs;
using LogisticsManagement.Service.Services;
using LogisticsManagement.Service.Services.IServices;
using LogisticsManagement.WebAPI.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LogisticsManagement.WebAPI.Controllers
{
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IDriverService _driverService;
       

        public StatusController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpGet("order", Name = "GetOrderStatuses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        //[Authorize(Roles = "Admin")]

        public async Task<IActionResult> GetOrderStatuses()
        {
            try
            {
                List<OrderStatusDTO>? orderStatuses = await _driverService.GetOrderStatusAsync();

                if (orderStatuses?.Count == 0)
                {
                    return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, data: Enumerable.Empty<OrderStatusDTO>()));
                }

                return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, data: orderStatuses));


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, null, ex.Message));
            }
        }
    }
}
