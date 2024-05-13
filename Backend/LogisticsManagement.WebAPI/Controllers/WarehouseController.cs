using Azure;
using LogisticsManagement.DataAccess.Models;
using LogisticsManagement.Service.DTOs;
using LogisticsManagement.Service.Services.IServices;
using LogisticsManagement.WebAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LogisticsManagement.WebAPI.Controllers
{
    [Route("api/warehouse")]
    [ApiController]

    [Authorize(Roles = "Admin")]
    public class WarehouseController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public WarehouseController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet(Name = "GetWarehouses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetWarehouses()
        {
            try
            {
                List<WarehouseDTO>? warehouses = await _adminService.GetAllWarehousesAsync();

                if (warehouses?.Count == 0)
                {
                    return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, data: Enumerable.Empty<WarehouseDTO>()));
                }

                return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, data: warehouses));


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, null, ex.Message));
            }
        }

        [HttpGet("{id}", Name = "GetWarehouse")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetWarehouse(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, error: "Please enter valid id"));
                }

                WarehouseDTO? warehouse = await _adminService.GetWarehouseByIdAsync(id);
                if (warehouse == null)
                {
                    return NotFound(ApiResponseHelper.Response(false, HttpStatusCode.NotFound, error: "Warehouse with given id not found"));
                }

                return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, data: warehouse));


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, null, ex.Message));
            }
        }

        [HttpPost(Name = "AddWarehouse")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public async Task<IActionResult> AddWarehouse([FromBody] WarehouseDTO warehouse)
        {
            try
            {


                if (warehouse == null || !ModelState.IsValid)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, null, "Please enter valid data"));
                }
                if (warehouse.Id > 0)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, null, "Please don't send Id while adding the warehouse"));
                }

                int addedWarehouseId = await _adminService.AddWarehouseAsync(warehouse);

                if (addedWarehouseId == 0 || addedWarehouseId == -1)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, error: "Failed to add warehouse. Please try again later"));
                }
                return CreatedAtAction("GetWarehouse", new { id = addedWarehouseId }, warehouse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, error: ex.Message));
            }
        }

        [HttpPut(Name = "UpdateWarehouse")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> UpdateWarehouse([FromBody] WarehouseDTO warehouse)
        {
            try
            {

                if (warehouse == null || !ModelState.IsValid)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, null, "Please enter valid data"));
                }
                if (warehouse.Id == 0)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, null, "Please provide a valid warehouse ID "));
                }

                int updatedWarehouseId = await _adminService.UpdateWarehouseAsync(warehouse);

                if (updatedWarehouseId == 0)
                {
                    return NotFound(ApiResponseHelper.Response(false, HttpStatusCode.NotFound, error: "Warehouse with given id not found"));
                }
                if (updatedWarehouseId == -1)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, error: "Failed to update warehouse. Please try again later"));
                }

                return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, data: "Warehouse details updated successfully"));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, error: ex.Message));
            }
        }

        //[HttpPatch("{id}", Name = "UpdateWarehousePartial")]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> UpdateWarehousePartial(int id, [FromBody] JsonPatchDocument<WarehouseDTO> warehouse)
        //{
        //    try
        //    {
        //        if (warehouse == null || !ModelState.IsValid)
        //        {
        //            return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, error: "Please enter valid data"));
        //        }
        //        if (id <= 0)
        //        {
        //            return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, error: "Invalid warehouse ID provided."));
        //        }

        //        WarehouseDTO? existingWarehouse = await _adminService.GetWarehouseByIdAsync(id);


        //        if (existingWarehouse == null)
        //        {
        //            return NotFound(ApiResponseHelper.Response(false, HttpStatusCode.NotFound,error: "Warehouse not found."));
        //        }

        //        warehouse.ApplyTo(existingWarehouse,ModelState);

        //        var result = await _adminService.UpdateWarehousePatchAsync(existingWarehouse);
               
        //        if (result == 0)
        //        {
        //            return NotFound(ApiResponseHelper.Response(false, HttpStatusCode.NotFound, error: "Warehouse with given id not found"));
        //        }

        //        if (result == -1)
        //        {
        //            return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError,error: "Failed to update warehouse."));
        //        }

        //        return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK,data: "Warehouse updated successfully."));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError,error: "Internal server error: " + ex.Message));
        //    }
        //}

        [HttpDelete("{id}", Name = "DeleteWarehouse")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteWarehouse(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, error: "Invalid warehouse ID provided."));
                }
                int response = await _adminService.RemoveWarehouseAsync(id);

                if (response == 0)
                {
                    return NotFound(ApiResponseHelper.Response(false, HttpStatusCode.NotFound, error: "Warehouse with given id not found"));
                }
                if (response == -1)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, error: "Failed to delete warehouse. Please try again later"));
                }
                return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, data: "Warehouse deleted successfully"));

            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, error: ex.Message));
            }

        }
    }
}
