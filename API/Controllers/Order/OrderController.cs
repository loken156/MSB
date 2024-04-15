using Application.Commands.Order.AddOrder;
using Application.Commands.Order.DeleteOrder;
using Application.Commands.Order.UpdateOrder;
using Application.Dto.Order;
using Application.Queries.Order.GetAll;
using Application.Queries.Order.GetByID;
using Application.Validators.OrderValidator;
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
        private readonly ILogger<OrderController> _logger;
        private readonly OrderValidator _orderValidator;


        public OrderController(IMediator mediator, ILogger<OrderController> logger, OrderValidator orderValidator)
        {
            _mediator = mediator;
            _logger = logger;
            _orderValidator = orderValidator;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("Add Order")]
        public async Task<IActionResult> AddOrder(AddOrderCommand command, [FromQuery] Guid warehouseId)
        {

            var validationResult = _orderValidator.Validate(command.NewOrder);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation failed for AddOrderCommand: {ValidationErrors}", validationResult.Errors);
                return BadRequest(validationResult.Errors);
            }


            try
            {
                var order = await _mediator.Send(command);
                var orderDto = new OrderDto
                {
                    OrderId = order.OrderId,
                    OrderDate = order.OrderDate,
                    TotalCost = order.TotalCost,
                    OrderStatus = order.OrderStatus,
                    UserId = order.UserId,
                    RepairNotes = order.RepairNotes
                };

                _logger.LogInformation("Order added successfully: {OrderId}", orderDto.OrderId);
                return CreatedAtAction(nameof(GetOrderById), new { id = orderDto.OrderId }, orderDto);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding order with command: {Command}", command);
                return StatusCode(500, "An error occurred while adding the order");
            }



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