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
    [Route("api/order")]
    [ApiController]

    //[Authorize()]
    public class OrderController : ControllerBase
    {
        private readonly IDriverService _driverService;
        private readonly ICustomerService _customerService;
        private readonly IManagerService _managerService;

        public OrderController(ICustomerService customerService, IManagerService managerService, IDriverService driverService)
        {
            _driverService = driverService;
            _customerService = customerService;
            _managerService = managerService;
        }


        // Get All Orders
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [Authorize(Roles = "Admin, Manager")]

        public async Task<IActionResult> Get()
        {
            try
            {
                List<OrderDTO>? orders = await _managerService.getOrders();
                if (orders == null)
                {
                    return NotFound(ApiResponseHelper.Response(false, HttpStatusCode.NotFound, null, "No Orders Available."));
                }
                return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, orders, string.Empty));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, null, ex.Message));
            }
        }

        [HttpGet("order-details/{orderId:int}", Name = "GetOrderDetails")]
        //[Authorize(Roles = "Customer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrderDetails(int orderId)
        {
            try
            {
                if (orderId <= 0)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, null, "Please enter valid id"));
                }

                var orderDetails = await _customerService.ViewOrderDetails(orderId);

                if (orderDetails == null)
                {
                    return NotFound(ApiResponseHelper.Response(false, HttpStatusCode.NotFound, null, "Order with given id not found"));
                }

                return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, orderDetails, string.Empty));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, null, ex.Message));
            }
        }

        [HttpGet("all-orders/{customerId:int}")]
        //[Authorize(Roles = "Customer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> GetAllOrders(int customerId)
        {
            try
            {
                if (customerId <= 0)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, null, "Please enter valid customer Id"));
                }

                var allOrders = await _customerService.ViewAllOrdersAsync(customerId);

                if (allOrders == null)
                {
                    return NotFound(ApiResponseHelper.Response(false, HttpStatusCode.NotFound, null, "Customer with given id not found"));
                }

                return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, allOrders, string.Empty));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, null, ex.Message));
            }
        }

        [HttpPost(Name = "AddOrder")]
        //[Authorize(Roles = "Customer")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]

        [Authorize(Roles = "Customer")]

        public async Task<IActionResult> AddOrder([FromBody] OrderDTO order)
        {
            try
            {
                if (order == null || !ModelState.IsValid)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, null, "Please enter valid data"));
                }
                if (order.Id > 0)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, null, "Please don't send Id while adding order"));
                }

                int addedOrderId = await _customerService.AddOrderAsync(order);

                if (addedOrderId == 0 || addedOrderId == -1)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, error: "Failed to add order. Please try again later"));
                }

                if (addedOrderId == -2)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, error: "User with given id not found"));
                }

                order.Id = addedOrderId;

                return CreatedAtAction("GetOrderDetails", new { orderId = addedOrderId }, null);
                //return Redirect($"api/order-details/{addedOrderId}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, error: ex.Message));
            }
        }

        [Authorize(Roles = "Admin, Manager, Driver")]

        // Get Assigned Orders
        [HttpGet("assigned-orders/{driverId:int}")]
        [Authorize(Roles = "Driver")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAssignedOrders(int driverId)
        {
            try
            {
                if (driverId <= 0)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, null, "Please enter valid id"));
                }

                var assignedOrders = await _driverService.GetAllAssignedOrdersAsync(driverId);

                if (assignedOrders == null)
                {
                    return NotFound(ApiResponseHelper.Response(false, HttpStatusCode.NotFound, null, "Driver with given id not found"));
                }

                return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, assignedOrders, string.Empty));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, null, ex.Message));
            }
        }

        // Get Assigned Orders
        [HttpGet("completed-orders/{driverId:int}")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [Authorize(Roles = "Admin, Manager, Driver")]

        public async Task<IActionResult> GetCompletedOrders(int driverId)
        {
            try
            {
                if (driverId <= 0)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, null, "Please enter valid id"));
                }

                var completedOrders = await _driverService.GetAllCompletedOrdersAsync(driverId);

                if (completedOrders == null)
                {
                    return NotFound(ApiResponseHelper.Response(false, HttpStatusCode.NotFound, null, "Driver with given id not found"));
                }

                return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, completedOrders, string.Empty));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, null, ex.Message));
            }
        }

        [HttpPut("update-status", Name = "UpdateStatus")]
        [Authorize(Roles = "Driver")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(APIResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [Authorize(Roles = "Admin, Manager, Driver")]

        public async Task<IActionResult> UpdateStatusRequest([FromBody] UpdateStatusDTO changeStatus)
        {
            try
            {
                if (changeStatus == null || changeStatus.orderStatusId==0 || changeStatus.orderId==0)
                {
                    return BadRequest(ApiResponseHelper.Response(false, HttpStatusCode.BadRequest, null, "Invalid Status object provided."));
                }

                int? result = await _driverService.UpdateStatusAsync(changeStatus);

                if (result == 0 || result == -1)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, null, "Internal Server Error"));
                }
                else
                {
                    return Ok(ApiResponseHelper.Response(true, HttpStatusCode.OK, data: "Order status updated successfully"));
                    //return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.Response(false, HttpStatusCode.InternalServerError, ex.Message));
            }

        }
    }
}
