using Application.Commands.Shelf.AddShelf;
using Application.Commands.Shelf.DeleteShelf;
using Application.Commands.Shelf.UpdateShelf;
using Application.Dto.Shelf;
using Application.Queries.Shelf.GetAll;
using Application.Queries.Shelf.GetByID;
using Application.Validators.ShelfValidator;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Shelf
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShelfController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ShelfController> _logger;
        private readonly ShelfValidations _shelfValidations;
        private readonly IMapper _mapper;

        public ShelfController(IMediator mediator, ILogger<ShelfController> logger, ShelfValidations shelfValidations, IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _shelfValidations = shelfValidations;
            _mapper = mapper;
        }

        [HttpPost("Add Shelf")]
        public async Task<IActionResult> AddShelf([FromBody] AddShelfCommand command)
        {
            _logger.LogInformation("Adding a new shelf with command: {Command}", command);
            var validationResult = await _shelfValidations.ValidateAsync(command.NewShelf);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation failed for AddShelfCommand: {ValidationErrors}", validationResult.Errors);
                return BadRequest(validationResult.Errors);
            }
            try
            {
                var shelf = await _mediator.Send(command);
                // Use AutoMapper to map ShelfModel to ShelfDto
                var shelfDto = _mapper.Map<ShelfDto>(shelf);

                _logger.LogInformation("Shelf added successfully: {ShelfId}", shelfDto.ShelfId);
                return CreatedAtAction(nameof(GetShelfById), new { id = shelfDto.ShelfId }, shelfDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding shelf with command: {Command}", command);
                return StatusCode(500, "An error occurred while adding the shelf");
            }
        }


        [HttpGet]
        [Route("Get All Shelves")]
        public async Task<ActionResult<IEnumerable<ShelfDto>>> GetAllShelves()
        {
            var query = new GetAllShelvesQuery();
            var shelves = await _mediator.Send(query);
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

        [HttpGet("Get Shelf By {id}")]
        public async Task<ActionResult<ShelfDto>> GetShelfById(Guid id)
        {
            var query = new GetShelfByIdQuery(id);
            var shelf = await _mediator.Send(query);

            if (shelf == null)
            {
                return NotFound();
            }

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

        [HttpPut("Update Shelf By {id}")]
        public async Task<IActionResult> UpdateShelf(Guid id, ShelfDto shelfDto)
        {
            if (id != shelfDto.ShelfId)
            {
                return BadRequest();
            }

            var command = new UpdateShelfCommand(shelfDto);
            var updatedShelf = await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("Delete Shelf By {id}")]
        public async Task<IActionResult> DeleteShelf(Guid id)
        {
            var command = new DeleteShelfCommand(id);
            await _mediator.Send(command);

            return NoContent();
        }
    }
}