using Application.Commands.Shelf.AddShelf;
using Application.Commands.Shelf.DeleteShelf;
using Application.Commands.Shelf.UpdateShelf;
using Application.Dto.Shelf;
using Application.Queries.Shelf.GetAll;
using Application.Queries.Shelf.GetByID;
using Application.Validators.ShelfValidator;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Shelf
{
    // Define the route and make this a controller for handling API requests related to shelves
    [Route("api/[controller]")]
    [ApiController]
    public class ShelfController : ControllerBase
    {
        // Dependencies injected via constructor
        private readonly IMediator _mediator;
        private readonly ILogger<ShelfController> _logger;
        private readonly ShelfValidations _shelfValidations;
        private readonly IMapper _mapper;

        // Constructor to initialize the dependencies
        public ShelfController(IMediator mediator, ILogger<ShelfController> logger, ShelfValidations shelfValidations, IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _shelfValidations = shelfValidations;
            _mapper = mapper;
        }

        // Endpoint to add a new shelf
        [HttpPost("AddShelf")]
        public async Task<IActionResult> AddShelf([FromBody] AddShelfCommand command)
        {
            _logger.LogInformation("Adding a new shelf with command: {Command}", command);

            // Validate the command using FluentValidation
            var validationResult = await _shelfValidations.ValidateAsync(command.NewShelf);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation failed for AddShelfCommand: {ValidationErrors}", validationResult.Errors);
                return BadRequest(validationResult.Errors);
            }

            try
            {
                // Send the AddShelfCommand using MediatR
                var shelf = await _mediator.Send(command);

                // Use AutoMapper to map ShelfModel to ShelfDto
                var shelfDto = _mapper.Map<ShelfDto>(shelf);

                _logger.LogInformation("Shelf added successfully: {ShelfId}", shelfDto.ShelfId);
                return CreatedAtAction(nameof(GetShelfById), new { id = shelfDto.ShelfId }, shelfDto);
            }
            catch (Exception ex)
            {
                // Log error and return a server error status
                _logger.LogError(ex, "Error adding shelf with command: {Command}", command);
                return StatusCode(500, "An error occurred while adding the shelf");
            }
        }

        // Endpoint to get all shelves
        [HttpGet]
        [Route("GetAllShelves")]
        public async Task<ActionResult<IEnumerable<ShelfDto>>> GetAllShelves()
        {
            // Create and send the GetAllShelvesQuery using MediatR
            var query = new GetAllShelvesQuery();
            var shelves = await _mediator.Send(query);

            // Map each ShelfModel to ShelfDto
            var shelfDtos = shelves.Select(shelf => new ShelfDto
            {
                ShelfId = shelf.ShelfId,
                ShelfRow = shelf.ShelfRow,
                ShelfColumn = shelf.ShelfColumn,
                Occupancy = shelf.Occupancy,
                WarehouseId = shelf.WarehouseId
            });
            return Ok(shelfDtos);
        }

        // Endpoint to get a shelf by its ID
        [HttpGet("GetShelfBy{id}")]
        public async Task<ActionResult<ShelfDto>> GetShelfById(Guid id)
        {
            // Create and send the GetShelfByIdQuery using MediatR
            var query = new GetShelfByIdQuery(id);
            var shelf = await _mediator.Send(query);

            if (shelf == null)
            {
                return NotFound();
            }

            // Map the ShelfModel to ShelfDto
            var shelfDto = new ShelfDto
            {
                ShelfId = shelf.ShelfId,
                ShelfRow = shelf.ShelfRow,
                ShelfColumn = shelf.ShelfColumn,
                Occupancy = shelf.Occupancy,
                WarehouseId = shelf.WarehouseId
            };
            return Ok(shelfDto);
        }

        // Endpoint to update a shelf by its ID
        [HttpPut("UpdateShelfBy{id}")]
        public async Task<IActionResult> UpdateShelf(Guid id, ShelfDto shelfDto)
        {
            if (id != shelfDto.ShelfId)
            {
                return BadRequest();
            }

            // Create and send the UpdateShelfCommand using MediatR
            var command = new UpdateShelfCommand(shelfDto);
            var updatedShelf = await _mediator.Send(command);

            return NoContent();
        }

        // Endpoint to delete a shelf by its ID
        [HttpDelete("DeleteShelfBy{id}")]
        public async Task<IActionResult> DeleteShelf(Guid id)
        {
            // Create and send the DeleteShelfCommand using MediatR
            var command = new DeleteShelfCommand(id);
            await _mediator.Send(command);

            return NoContent();
        }
    }
}