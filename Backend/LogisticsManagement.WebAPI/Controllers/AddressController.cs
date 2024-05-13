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
    [Route("api/address")]
    [ApiController]

    [Authorize()]

    public class AddressController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public AddressController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet(Name = "GetAddresses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        [Authorize(Roles = "Admin")]
       
        public async Task<IActionResult> GetAddresses()
        {
            try
            {
                List<AddressDTO>? address = await _customerService.GetAllAddressAsync();

                if (address?.Count == 0)
                {
                    return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, data: Enumerable.Empty<WarehouseDTO>()));
                }

                return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, data: address));


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, null, ex.Message));
            }
        }


        [HttpGet("{id}", Name = "GetAddress")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [AllowAnonymous]
        public async Task<IActionResult> GetAddress(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, error: "Please enter valid id"));
                }

                AddressDTO? address = await _customerService.GetAddressByIdAsync(id);
                if (address == null)
                {
                    return NotFound(ApiResponseHelper.Response(false, HttpStatusCode.NotFound, error: "Address with given id not found"));
                }

                return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, data: address));


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, null, ex.Message));
            }
        }

        [HttpPost(Name = "AddAddress")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]

        [Authorize(Roles = "Admin, Customer")]

        public async Task<IActionResult> AddAddress([FromBody] AddressDTO address)
        {
            try
            {


                if (address == null || !ModelState.IsValid)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, null, "Please enter valid data"));
                }
                if (address.Id > 0)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, null, "Please don't send Id while addin address"));
                }

                int addedAddressId = await _customerService.AddAddressAsync(address);

                if (addedAddressId == 0 || addedAddressId == -1)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, error: "Failed to add address. Please try again later"));
                }

                if (addedAddressId == -2)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, error: "User with given id not found"));
                }

                address.Id = addedAddressId;
;                return CreatedAtAction("GetAddress", new { id = addedAddressId }, address);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, error: ex.Message));
            }
        }


        [HttpPut(Name = "UpdateAddress")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [Authorize(Roles = "Admin, Customer")]

        public async Task<IActionResult> UpdateAddress([FromBody] AddressDTO address)
        {
            try
            {

                if (address == null || !ModelState.IsValid)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, null, "Please enter valid data"));
                }
                if (address.Id == 0)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, null, "Please provide a valid address ID "));
                }

                int updatedAddressId = await _customerService.UpdateAddressAsync(address);

                if (updatedAddressId == 0)
                {
                    return NotFound(ApiResponseHelper.Response(false, HttpStatusCode.NotFound, error: "Address with given id not found"));
                }
                if (updatedAddressId == -1)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, error: "Failed to update address. Please try again later"));
                }

                return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, data: "Address details updated successfully"));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, error: ex.Message));
            }
        }


        [HttpDelete("{id}", Name = "DeleteAddress")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [Authorize(Roles = "Admin, Customer")]

        public async Task<IActionResult> DeleteAddress(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, error: "Invalid address ID provided."));
                }
                int response = await _customerService.RemoveAddressAsync(id);

                if (response == 0)
                {
                    return NotFound(ApiResponseHelper.Response(false, HttpStatusCode.NotFound, error: "Address with given id not found"));
                }
                if (response == -1)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, error: "Failed to delete address. Please try again later"));
                }
                return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, data: "Address deleted successfully"));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, error: ex.Message));
            }

        }


    }
}
