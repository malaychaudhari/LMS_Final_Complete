using LogisticsManagement.Service.DTOs;
using LogisticsManagement.Service.Services.IServices;
using LogisticsManagement.WebAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LogisticsManagement.WebAPI.Controllers
{
    [Route("api/resource-mapping")]
    [ApiController]

    [Authorize(Roles = "Admin, Manager")]

    public class ResourceMappingController : ControllerBase
    {
        private readonly IManagerService _managerService;

        public ResourceMappingController(IManagerService managerService)
        {
            _managerService = managerService;
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(APIResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Post([FromBody] ResourceMappingDTO assignment)
        {
            try
            {
                if (assignment == null)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, null, "Invalid Inventory object provided."));
                }

                int? result = await _managerService.AssignOrder(assignment);

                if (result == 0 || result == -1)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, null, "Internal Server Error"));
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, null, ex.Message));
            }
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Get()
        {
            try
            {
                List<ResourceMappingDTO>? resourceMappings = await _managerService.getAssignedOrders();
                if (resourceMappings == null)
                {
                    return NotFound(ApiResponseHelper.Response(false, HttpStatusCode.NotFound, null, "No Inventories Available."));
                }
                return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, resourceMappings, string.Empty));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, null, ex.Message));
            }
        }

    }



}
