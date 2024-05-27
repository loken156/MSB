using Application.Commands.Order.AddOrder;
using Application.Commands.Order.DeleteOrder;
using Application.Commands.Order.UpdateOrder;
using Application.Dto.Box;
using Application.Dto.Order;
using Application.Queries.Order.GetAll;
using Application.Queries.Order.GetByID;
using AutoMapper;
using Domain.Models.Box;
using Domain.Models.Notification;
using Domain.Models.Order;
using Infrastructure.Repositories.BoxRepo;
using Infrastructure.Repositories.OrderRepo;
using Infrastructure.Services.Caching;
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
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private readonly IOrderRepository _orderRepository;
        private readonly IBoxRepository _boxRepository;

        public OrderController(IMediator mediator, INotificationService notificationService, ILogger<OrderController> logger, IMapper mapper, ICacheService cacheService, IOrderRepository repository, IBoxRepository boxRepository)
        {
            _mediator = mediator;
            _notificationService = notificationService;
            _logger = logger;
            _mapper = mapper;
            _cacheService = cacheService;
            _orderRepository = repository;
            _boxRepository = boxRepository;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("Add Order")]
        public async Task<ActionResult<OrderDto>> AddOrder(AddOrderDto orderDto, [FromQuery] Guid warehouseId)
        {
            try
            {
                var command = new AddOrderCommand(orderDto, warehouseId);
                var order = await _mediator.Send(command);

                await _cacheService.SetAsync($"OrderForUser_{order.UserId}", order, TimeSpan.FromMinutes(30));

                var notification = new NotificationModel
                {
                    UserId = order.UserId,
                    Message = "Your order has been accepted."
                };

                await _notificationService.SendNotification(notification);

                // Assuming you want to map the result back to OrderDto before returning
                var resultDto = _mapper.Map<OrderDto>(order); // Use AutoMapper to map OrderModel back to OrderDto
                return CreatedAtAction(nameof(GetOrderById), new { id = resultDto.OrderId }, resultDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding order with command: {Command}", orderDto);
                return StatusCode(500, "An error occurred while adding the order");
            }
        }

        public async Task<IActionResult> SaveOrderAndBox(string userId)
        {
            try
            {
                // Retrieve the order and box from the cache
                var order = await _cacheService.GetAsync<OrderModel>($"OrderForUser_{userId}");
                var boxDto = await _cacheService.GetAsync<BoxDto>($"BoxForUser_{userId}");

                if (order == null || boxDto == null)
                {
                    _logger.LogWarning("Order or box not found in cache for user: {UserId}", userId);
                    return NotFound("Order or box not found in cache");
                }

                // Convert BoxDto to BoxModel
                var box = _mapper.Map<BoxModel>(boxDto);

                // Save the order and box to the database
                await _orderRepository.CreateOrderAsync(order);
                await _boxRepository.AddBoxAsync(box);

                // Remove the order and box from the cache
                await _cacheService.RemoveAsync($"OrderForUser_{userId}");
                await _cacheService.RemoveAsync($"BoxForUser_{userId}");

                _logger.LogInformation("Successfully saved order and box for user: {UserId}", userId);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving order and box for user: {UserId}", userId);
                return StatusCode(500, "An error occurred while saving the order and box");
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