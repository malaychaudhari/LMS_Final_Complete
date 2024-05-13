using LogisticsManagement.Service.DTOs;
using LogisticsManagement.Service.Services.IServices;
using LogisticsManagement.WebAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LogisticsManagement.WebAPI.Controllers
{
    [Route("api/country")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CountryController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet(Name = "GetCountries")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Authorize(Roles = "Customer")]

        public async Task<IActionResult> GetCountries()
        {
            try
            {
                List<CountryDTO>? countries = await _customerService.ViewAllCountriesAsync();

                if (countries?.Count == 0)
                {
                    return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, data: Enumerable.Empty<WarehouseDTO>()));
                }

                return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, data: countries));


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, null, ex.Message));
            }
        }
    }
}
