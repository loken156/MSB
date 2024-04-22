using Application.Commands.Order.AddOrder;
using Application.Commands.Order.DeleteOrder;
using Application.Commands.Order.UpdateOrder;
using Application.Commands.Shelf.AddShelf;
using Application.Dto.Notification;
using Application.Dto.Order;
using Application.Dto.Shelf;
using Application.Queries.Order.GetAll;
using Application.Queries.Order.GetByID;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;

namespace API.Controllers.Order
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly INotificationService _notificationService;

        public OrderController(IMediator mediator, INotificationService notificationService)
        {
            _mediator = mediator;
            _notificationService = notificationService;

        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("Add Order")]
        public async Task<ActionResult<OrderDto>> AddOrder(OrderDto orderdto, [FromQuery] Guid warehouseId)
        {
            var command = new AddOrderCommand(orderdto, warehouseId);
            var order = await _mediator.Send(command);
            var notification = new NotificationDto
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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("GetAllOrders")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrders()
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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("Get Order By {id}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(Guid id)
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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("Update Order By {id}")]
        public async Task<IActionResult> UpdateOrder(Guid id, OrderDto orderDto)
        {
            if (id != orderDto.OrderId)
            {
                return BadRequest();
            }

            var command = new UpdateOrderCommand(orderDto);
            var updatedOrder = await _mediator.Send(command);

            return NoContent();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("Delete Order By {id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var command = new DeleteOrderCommand(id);
            await _mediator.Send(command);

            return NoContent();
        }
    }
}