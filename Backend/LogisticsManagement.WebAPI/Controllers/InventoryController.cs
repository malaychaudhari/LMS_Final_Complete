using Azure;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using LogisticsManagement.Service.DTOs;
using LogisticsManagement.Service.Services.IServices;
using LogisticsManagement.WebAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using System.Net;

namespace LogisticsManagement.WebAPI.Controllers
{
    [Route("api/inventory")]
    [ApiController]

    public class InventoryController : ControllerBase
    {

        private readonly IManagerService _managerService;
        private readonly string _imagesFolderPath;
        public InventoryController(IManagerService managerService, IWebHostEnvironment webHostEnvironment)
        {
            _managerService = managerService;
            _imagesFolderPath = Path.Combine(webHostEnvironment.ContentRootPath, "Images");
        }



        // Get All Inventories
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<InventoryDTO>? inventories = await _managerService.GetInventories();
                if (inventories == null)
                {
                    return NotFound(ApiResponseHelper.Response(false, HttpStatusCode.NotFound, null, "No Inventories Available."));
                }
                return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, inventories, string.Empty));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, null, ex.Message));
            }
        }




        // Get Inventory by Id
        [HttpGet]
        [Route("{id:int}", Name = "GetById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, null, "Please enter valid id"));
                }

                InventoryDTO? inventory = await _managerService.GetInventory(id);

                if (inventory == null)
                {
                    return NotFound(ApiResponseHelper.Response(false, HttpStatusCode.NotFound, null, "No Inventory found with id : " + id));
                }
                return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, inventory, string.Empty));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, null, ex.Message));
            }
        }




        // Add Inventory
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(APIResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [Authorize(Roles ="Admin, Manager")]
        public async Task<IActionResult> Post([FromBody] InventoryDTO inventory)
        {
            try
            {
                if (inventory == null)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, null, "Invalid Inventory object provided."));
                }

                int? result = await _managerService.AddInventory(inventory);

                if (result == 0 || result == -1)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, null, "Internal Server Error"));
                }
                else if (result == -2)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, null, "Inventory Already Exists."));
                }
                else
                {
                    inventory.Id = (int)result;
                    return CreatedAtAction("Get", new { id = result }, inventory);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, null, ex.Message));
            }
        }




        // Remove Inventory
        [HttpDelete]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(APIResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, null, "Please enter valid id"));
                }
                int? result = await _managerService.RemoveInventory(id);
                if (result == 0 || result == -1)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, null, "Internal Server Error"));
                }
                else if (result == -2)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, null, "No Inventory Found with Id : " + id));
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




        // Update Inventory
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(APIResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Put([FromBody] InventoryDTO inventory)
        {
            try
            {
                if (inventory == null)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, null, "Invalid Inventory object provided."));
                }
                if (inventory.Stock <= 0)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, null, "Please Provide Valid Quantity."));
                }
                if (await _managerService.PutInventory(inventory) > 0)
                {
                    return NoContent();
                }
                return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, null, "Internal Server Error."));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, null, ex.Message));
            }
        }




        // Update Partial Inventory
        [HttpPatch]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(APIResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<InventoryDTO> patchInventory)
        {
            try
            {
                if (patchInventory == null)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, null, "Invalid Inventory object provided."));
                }
                if (id <= 0)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, null, "Please enter valid id"));
                }

                InventoryDTO? inv = await _managerService.GetInventory(id);

                if (inv == null)
                {
                    return NotFound(ApiResponseHelper.Response(false, HttpStatusCode.NotFound, null, "No Inventory found with id : " + id));
                }
                patchInventory.ApplyTo(inv);
                if (await _managerService.UpdateInventory(inv) > 0)
                {
                    return NoContent();
                }
                return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, null, "Internal Server Error."));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, null, ex.Message));
            }
        }




        [HttpPost("upload")]

        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Please select a file to upload.");
            }

            var filePath = Path.GetTempFileName();


            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            Account? account = new Account(
                    "drtbueipf",
                    "358663862986994",
                    "ebP9onw4YQHKpL3lcCihcUcdz90");

            Cloudinary cloudinary = new Cloudinary(account);

            var result = cloudinary.Upload(new ImageUploadParams()
            {
                File = new FileDescription(filePath),
                PublicId = Guid.NewGuid().ToString()
            });
            Console.WriteLine("Result " + result);
            return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, data: result.Url));
        }



        //[HttpPost("upload-local")]
        //public async Task<IActionResult> UploadImageLocal(IFormFile file)
        //{
        //    if (file == null || file.Length == 0)
        //        return BadRequest("No file uploaded.");

        //    try
        //    {
        //        if (!Directory.Exists(_imagesFolderPath))
        //            Directory.CreateDirectory(_imagesFolderPath);

        //        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        //        var filePath = Path.Combine(_imagesFolderPath, fileName);

        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await file.CopyToAsync(stream);
        //        }

        //        var imagePath = Path.GetFullPath(filePath);
        //        return Ok(new { imagePath });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}




    }

}
