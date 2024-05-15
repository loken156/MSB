using Application.Commands.Order.AddOrder;
using Application.Commands.Order.DeleteOrder;
using Application.Commands.Order.UpdateOrder;
using Application.Dto.Order;
using Application.Queries.Order.GetAll;
using Application.Queries.Order.GetByID;
using Domain.Models.Notification;
using Infrastructure.Services.Notification;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Order
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly INotificationService _notificationService;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IMediator mediator, INotificationService notificationService, ILogger<OrderController> logger)
        {
            _mediator = mediator;
            _notificationService = notificationService;
            _logger = logger;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("Add Order")]
        public async Task<ActionResult<OrderDto>> AddOrder(OrderDto orderdto, [FromQuery] Guid warehouseId)
        {
            try
            {
                var command = new AddOrderCommand(orderdto, warehouseId);
                var order = await _mediator.Send(command);

                var notification = new NotificationModel
                {
                    UserId = order.UserId,
                    Message = "Your order has been accepted."
                };

                await _notificationService.SendNotification(notification);

                var orderDto = new OrderDto
                {
                    OrderId = order.OrderId,
                    OrderDate = order.OrderDate,
                    TotalCost = order.TotalCost,
                    OrderStatus = order.OrderStatus,
                    UserId = order.UserId,
                    WarehouseId = order.WarehouseId,
                    RepairNotes = order.RepairNotes
                };
                return CreatedAtAction(nameof(GetOrderById), new { id = orderDto.OrderId }, orderDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding order with command: {Command}", orderdto);
                return StatusCode(500, "An error occurred while adding the order");
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("GetAllOrders")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrders()
        {
            try
            {
                var query = new GetAllOrdersQuery();
                var orders = await _mediator.Send(query);
                var orderDtos = orders.Select(order => new OrderDto
                {
                    OrderId = order.OrderId,
                    OrderDate = order.OrderDate,
                    TotalCost = order.TotalCost,
                    OrderStatus = order.OrderStatus,
                    UserId = order.UserId,
                    RepairNotes = order.RepairNotes
                });
                return Ok(orderDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all orders");
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("Get Order By {id}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(Guid id)
        {
            try
            {
                var query = new GetOrderByIdQuery(id);
                var order = await _mediator.Send(query);

                if (order == null)
                {
                    return NotFound();
                }

                var orderDto = new OrderDto
                {
                    OrderId = order.OrderId,
                    OrderDate = order.OrderDate,
                    TotalCost = order.TotalCost,
                    OrderStatus = order.OrderStatus,
                    UserId = order.UserId,
                    RepairNotes = order.RepairNotes
                };
                return Ok(orderDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting order by id: {id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("Update Order By {id}")]
        public async Task<IActionResult> UpdateOrder(Guid id, OrderDto orderDto)
        {
            try
            {
                if (id != orderDto.OrderId)
                {
                    return BadRequest();
                }

                var command = new UpdateOrderCommand(orderDto);
                var updatedOrder = await _mediator.Send(command);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating order with id: {id}", id);
                return StatusCode(500, "An error occurred while updating the order");
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("Delete Order By {id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            try
            {
                var command = new DeleteOrderCommand(id);
                await _mediator.Send(command);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting order with id: {id}", id);
                return StatusCode(500, "An error occurred while deleting the order");
            }
        }
    }
}