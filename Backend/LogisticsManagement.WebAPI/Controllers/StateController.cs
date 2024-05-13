using LogisticsManagement.Service.DTOs;
using LogisticsManagement.Service.Services.IServices;
using LogisticsManagement.WebAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LogisticsManagement.WebAPI.Controllers
{
    [Route("api/state")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public StateController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet(Name = "GetStates")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Authorize(Roles = "Customer")]

        public async Task<IActionResult> GetStates()
        {
            try
            {
                List<StateDTO>? states = await _customerService.ViewAllStatesAsync();

                if (states?.Count == 0)
                {
                    return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, data: Enumerable.Empty<WarehouseDTO>()));
                }

                return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, data: states));


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, null, ex.Message));
            }
        }
    }
}
