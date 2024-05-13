using LogisticsManagement.Service.DTOs;
using LogisticsManagement.Service.Services.IServices;
using LogisticsManagement.WebAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LogisticsManagement.WebAPI.Controllers
{
    [Route("api/inventory-category")]
    [ApiController]


    [Authorize(Roles = "Admin,Manager")]

    public class InventoryCategoryController : ControllerBase
    {
        private readonly IManagerService _managerService;
        public InventoryCategoryController(IManagerService managerService) 
        {
            _managerService = managerService;
        }




        // Get All Inventories
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<InventoryCategoryDTO>? categories =  await _managerService.GetInventoryCategories();
                if(categories == null)
                {
                    return NotFound(ApiResponseHelper.Response(false, HttpStatusCode.NotFound, null, "No Categories Available."));
                }
                return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, categories, string.Empty));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, null, ex.Message));
            }
        }




        // Get Inventory Category by Id
        [HttpGet]
        [Route("{id:int}",Name = "GetInventoryCategotyById")]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(APIResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if(id <= 0)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, null, "Please enter valid id"));
                }

                InventoryCategoryDTO? category = await _managerService.GetInventoryCategory(id);

                if(category == null)
                {
                    return NotFound(ApiResponseHelper.Response(false, HttpStatusCode.NotFound, null, "No category found with id : " + id));
                }
                return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, category, string.Empty));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, null, ex.Message));
            }
        }




        // Add Inventory Category
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created,Type=typeof(APIResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Post([FromBody] InventoryCategoryDTO category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, null, "Invalid category object provided."));
                }

                int? result = await _managerService.AddInventoryCategory(category);

                if (result == 0 || result == -1)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, null, "Internal Server Error"));
                }
                else if(result == -2)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, null, "Category Already Exists."));
                }
                else
                {
                    category.Id = (int)result;
                    return CreatedAtAction("Get", new { id = result }, category);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, null, ex.Message));
            }
        }



        // Remove Inventory Category
        [HttpDelete]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(APIResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, null, "Please enter valid id"));
                }
                int? result = await _managerService.RemoveInventoryCategory(id);
                if (result == 0 || result == -1)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, null, "Internal Server Error"));
                }
                else if (result == -2)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, null, "No Category Found with Id : " + id));
                }
                else
                {
                    return NoContent();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, null, ex.Message));
            }
        }
    }
}
