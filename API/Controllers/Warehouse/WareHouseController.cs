using Application.Commands.Warehouse.AddWarehouse;
using Application.Commands.Warehouse.DeleteWarehouse;
using Application.Commands.Warehouse.UpdateWarehouse;
using Application.Dto.AddWarehouse;
using Application.Dto.Warehouse;
using Application.Queries.Warehouse.GetAll;
using Application.Queries.Warehouse.GetByID;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Warehouse
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<WarehouseController> _logger;
        private readonly IValidator<AddWarehouseDto> _wareHouseValidations;
        private readonly IMapper _mapper;



        public WarehouseController(IMediator mediator, ILogger<WarehouseController> logger, IValidator<AddWarehouseDto> validations, IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _wareHouseValidations = validations;
            _mapper = mapper;
        }

        [HttpPost("Add Warehouse")]
        public async Task<IActionResult> AddWarehouse([FromBody] AddWarehouseCommand command)
        {
            var validationResult = _wareHouseValidations.Validate(command.NewWarehouse); // Validate the DTO inside the command
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation failed for AddWarehouseCommand: {ValidationErrors}", validationResult.Errors);
                return BadRequest(validationResult.Errors);
            }

            try
            {
                // This sends the command and should return a WarehouseModel, which you will then map to a WarehouseDto.
                var warehouseModel = await _mediator.Send(command);
                var warehouseDto = _mapper.Map<WarehouseDto>(warehouseModel); // Use AutoMapper to map WarehouseModel to WarehouseDto

                _logger.LogInformation("Warehouse added successfully: {WarehouseName}", warehouseDto.WarehouseName);
                return CreatedAtAction(nameof(AddWarehouse), warehouseDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding warehouse with command: {Command}", command);
                return StatusCode(500, "An error occurred while adding the warehouse");
            }
        }





        [HttpDelete("Delete Warehouse by {id}")]
        public async Task<IActionResult> DeleteWarehouse(Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteWarehouseCommand(id));
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting warehouse with id: {Id}", id);
                return StatusCode(500, "An error occurred while deleting the warehouse");
            }

        }

        [HttpPut("Update Warehouse")]
        public async Task<ActionResult<WarehouseDto>> UpdateWarehouse([FromBody] UpdateWarehouseCommand command)
        {
            try
            {
                var warehouse = await _mediator.Send(command);
                var warehouseDto = _mapper.Map<WarehouseDto>(warehouse); // Use AutoMapper to map WarehouseModel to WarehouseDto
                return Ok(warehouseDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating warehouse with command: {Command}", command);
                return StatusCode(500, "An error occurred while updating the warehouse");
            }
        }

        [HttpGet("Get All WareHouses")]
        public async Task<ActionResult<IEnumerable<WarehouseDto>>> GetAllWarehouses()
        {
            try
            {
                var query = new GetAllWarehousesQuery();
                var warehouses = await _mediator.Send(query);
                var warehouseDtos = warehouses.Select(warehouse => new WarehouseDto
                {
                    WarehouseId = warehouse.WarehouseId,
                    WarehouseName = warehouse.WarehouseName
                });
                return Ok(warehouseDtos);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting all warehouses");
                return StatusCode(500, "An error occurred while getting all warehouses");
            }

        }

        [HttpGet("Get Warehouse By {id}")]
        public async Task<ActionResult<WarehouseDto>> GetWarehouseById(Guid id)
        {
            try
            {
                var query = new GetWarehouseByIdQuery(id);
                var warehouse = await _mediator.Send(query);

                if (warehouse == null)
                {
                    return NotFound();
                }

                var warehouseDto = _mapper.Map<WarehouseDto>(warehouse); // Use AutoMapper to map WarehouseModel to WarehouseDto
                return Ok(warehouseDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting warehouse by id");
                return StatusCode(500, "An error occurred while getting warehouse by id");
            }
        }

    }
}