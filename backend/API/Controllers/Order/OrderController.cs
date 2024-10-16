﻿using Application.Commands.Order.AddOrder;
using Application.Commands.Order.DeleteOrder;
using Application.Commands.Order.UpdateOrder;
using Application.Dto.Box;
using Application.Dto.Order;
using Application.Queries.Order.GetAll;
using Application.Queries.Order.GetByID;
using Application.Services;
using Application.Services.Detrack;
using AutoMapper;
using Domain.Models.Box;
using Domain.Models.Notification;
using Domain.Models.Order;
using Infrastructure.Entities;
using Infrastructure.Repositories.BoxRepo;
using Infrastructure.Repositories.OrderRepo;
using Infrastructure.Services.Caching;
using Infrastructure.Services.Notification;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Order
{
    // Define the route and make this a controller for handling API requests
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        // Dependencies injected via constructor
        private readonly IMediator _mediator;
        private readonly INotificationService _notificationService;
        private readonly ILogger<OrderController> _logger;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private readonly IOrderRepository _orderRepository;
        private readonly IBoxRepository _boxRepository;
        private readonly DeliveryService _deliveryService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly OrderService _orderService;  // Added OrderService
        private readonly IDetrackService _detrackService;  // Inject DetrackService

        // Constructor to initialize the dependencies
        public OrderController(IMediator mediator, INotificationService notificationService, ILogger<OrderController> logger, IMapper mapper, ICacheService cacheService, IOrderRepository repository, IBoxRepository boxRepository, UserManager<ApplicationUser> userManager, OrderService orderService, IDetrackService detrackService)

        {
            _mediator = mediator;
            _notificationService = notificationService;
            _logger = logger;
            _mapper = mapper;
            _cacheService = cacheService;
            _orderRepository = repository;
            _boxRepository = boxRepository;
            _userManager = userManager;
            _orderService = orderService;
            _detrackService = detrackService;

        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("AddOrder")]
        public async Task<ActionResult<OrderDto>> AddOrder(AddOrderDto orderDto)
        {
            try
            {
                // Map the DTO to OrderModel
                var newOrder = _mapper.Map<OrderModel>(orderDto);

                // Create the order in the database
                var createdOrder = await _orderRepository.AddOrderAsync(newOrder);

                // Attempt to create a Detrack job
                var detrackSuccess = await _detrackService.CreateDetrackJobAsync(_mapper.Map<OrderDto>(createdOrder));

                if (!detrackSuccess)
                {
                    return StatusCode(500, "Order created but failed to create Detrack job.");
                }

                // Map the result back to OrderDto
                var resultDto = _mapper.Map<OrderDto>(createdOrder);

                return CreatedAtAction(nameof(GetOrderById), new { id = resultDto.OrderId }, resultDto);
            }
            catch (Exception ex)
            {
                // Log the error and return 500
                return StatusCode(500, $"An error occurred while adding the order: {ex.Message}");
            }
        }


        /*
                // Endpoint to add a new order
                [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
                [HttpPost]
                [Route("AddOrder")]
                public async Task<ActionResult<OrderDto>> AddOrder(AddOrderDto orderDto, [FromQuery] Guid warehouseId)
                {
                    try
                    {
                        // Create and send the AddOrderCommand using MediatR
                        var command = new AddOrderCommand(orderDto, warehouseId);
                        var order = await _mediator.Send(command);

                        // Cache the order for the user
                        await _cacheService.SetAsync($"OrderForUser_{order.UserId}", order, TimeSpan.FromMinutes(30));

                        // Send a notification to the user
                        var notification = new NotificationModel
                        {
                            UserId = order.UserId,
                            Message = "Your order has been accepted."
                        };
                        await _notificationService.SendNotification(notification);

                        // Schedule deliveries
                        await _deliveryService.ScheduleDeliveries();

                        // Map the result to OrderDto before returning
                        var resultDto = _mapper.Map<OrderDto>(order);
                        return CreatedAtAction(nameof(GetOrderById), new { id = resultDto.OrderId }, resultDto);
                    }
                    catch (Exception ex)
                    {
                        // Log error and return a server error status
                        _logger.LogError(ex, "Error adding order with command: {Command}", orderDto);
                        return StatusCode(500, "An error occurred while adding the order");
                    }
                }
        */
        // Endpoint to save order and box details
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("Saveorder")]
        public async Task<IActionResult> SaveOrderAndBox(string userId)
        {
            try
            {
                // Retrieve order and box from the cache
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
                await _orderRepository.AddOrderAsync(order);
                await _boxRepository.AddBoxAsync(box);

                // Remove the order and box from the cache
                await _cacheService.RemoveAsync($"OrderForUser_{userId}");
                await _cacheService.RemoveAsync($"BoxForUser_{userId}");

                _logger.LogInformation("Successfully saved order and box for user: {UserId}", userId);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log error and return a server error status
                _logger.LogError(ex, "Error saving order and box for user: {UserId}", userId);
                return StatusCode(500, "An error occurred while saving the order and box");
            }
        }

        // Endpoint to get all orders
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("GetAllOrders")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrders()
        {
            try
            {
                // Create and send the GetAllOrdersQuery using MediatR
                var query = new GetAllOrdersQuery();
                var orders = await _mediator.Send(query);

                // Map each OrderModel to OrderDto
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
                // Log error and return a server error status
                _logger.LogError(ex, "Error getting all orders");
                return StatusCode(500, "Internal server error");
            }
        }

        // Endpoint to get an order by its ID
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetOrderBy{id}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(Guid id)
        {
            try
            {
                // Create and send the GetOrderByIdQuery using MediatR
                var query = new GetOrderByIdQuery(id);
                var order = await _mediator.Send(query);

                if (order == null)
                {
                    return NotFound();
                }

                // Map the OrderModel to OrderDto
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
                // Log error and return a server error status
                _logger.LogError(ex, "Error getting order by id: {id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        // Endpoint to update an order by its ID
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("UpdateOrderBy{id}")]
        public async Task<IActionResult> UpdateOrder(Guid id, OrderDto orderDto)
        {
            try
            {
                if (id != orderDto.OrderId)
                {
                    return BadRequest();
                }

                // Create and send the UpdateOrderCommand using MediatR
                var command = new UpdateOrderCommand(orderDto);
                var updatedOrder = await _mediator.Send(command);

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log error and return a server error status
                _logger.LogError(ex, "Error updating order with id: {id}", id);
                return StatusCode(500, "An error occurred while updating the order");
            }
        }

        // Endpoint to delete an order by its ID
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("DeleteOrderBy{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            try
            {
                // Create and send the DeleteOrderCommand using MediatR
                var command = new DeleteOrderCommand(id);
                await _mediator.Send(command);

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log error and return a server error status
                _logger.LogError(ex, "Error deleting order with id: {id}", id);
                return StatusCode(500, "An error occurred while deleting the order");
            }
        }
    }
}