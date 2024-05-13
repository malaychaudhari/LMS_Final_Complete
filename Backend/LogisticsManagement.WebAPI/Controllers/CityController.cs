using LogisticsManagement.Service.DTOs;
using LogisticsManagement.Service.Services.IServices;
using LogisticsManagement.WebAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LogisticsManagement.WebAPI.Controllers
{
    [Route("api/city")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CityController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet(Name = "GetCities")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Authorize(Roles = "Customer")]

        public async Task<IActionResult> GetCities()
        {
            try
            {
                List<CityDTO>? cities = await _customerService.ViewAllCitiesAsync();

                if (cities?.Count == 0)
                {
                    return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, data: Enumerable.Empty<WarehouseDTO>()));
                }

                return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, data: cities));


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, null, ex.Message));
            }
        }

    }
}
