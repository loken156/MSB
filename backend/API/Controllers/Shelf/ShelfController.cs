using Application.Commands.Box.AddBoxToShelf;
using Application.Commands.Shelf.AddShelf;
using Application.Commands.Shelf.DeleteShelf;
using Application.Commands.Shelf.UpdateShelf;
using Application.Dto.Box;
using Application.Dto.Shelf;
using Application.Queries.Shelf.GetAll;
using Application.Queries.Shelf.GetByID;
using Application.Validators.ShelfValidator;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers.Shelf
{
    [AllowAnonymous]
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

        // Endpoint to add a new shelf
        [HttpPost("AddShelf")]
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

        // Endpoint to add a box to a shelf
        [HttpPost("AddBoxToShelf")]
        public async Task<IActionResult> AddBoxToShelf([FromBody] AddBoxToShelfDto dto)
        {
            try
            {
                // Create the command from the DTO
                var command = new AddBoxToShelfCommand(dto.BoxId, dto.ShelfId);

                // Send the command to the handler via MediatR
                var updatedBox = await _mediator.Send(command);

                // Optionally map the returned box to a BoxDto if necessary
                return Ok(updatedBox);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Validation or logic error while adding box to shelf");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the box to the shelf");
                return StatusCode(500, "An error occurred while adding the box to the shelf");
            }
        }


        // Endpoint to get all shelves
        [HttpGet("GetAllShelves")]
        public async Task<ActionResult<IEnumerable<ShelfDto>>> GetAllShelves()
        {
            var query = new GetAllShelvesQuery();
            var shelves = await _mediator.Send(query);

            var shelfDtos = shelves.Select(shelf => new ShelfDto
            {
                ShelfId = shelf.ShelfId,
                ShelfRows = shelf.ShelfRows,
                ShelfColumn = shelf.ShelfColumn,
                Section = shelf.Section,
                LargeBoxCapacity = shelf.LargeBoxCapacity,
                MediumBoxCapacity = shelf.MediumBoxCapacity,
                SmallBoxCapacity = shelf.SmallBoxCapacity,
                AvailableLargeSlots = shelf.AvailableLargeSlots,
                AvailableMediumSlots = shelf.AvailableMediumSlots,
                AvailableSmallSlots = shelf.AvailableSmallSlots,
                Occupancy = shelf.Occupancy,
                WarehouseId = shelf.WarehouseId
            }).ToList();

            return Ok(shelfDtos);
        }

        // Endpoint to get a shelf by its ID
        [HttpGet("GetShelfBy{id}")]
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
                ShelfRows = shelf.ShelfRows,
                ShelfColumn = shelf.ShelfColumn,
                Section = shelf.Section,
                LargeBoxCapacity = shelf.LargeBoxCapacity,
                MediumBoxCapacity = shelf.MediumBoxCapacity,
                SmallBoxCapacity = shelf.SmallBoxCapacity,
                AvailableLargeSlots = shelf.AvailableLargeSlots,
                AvailableMediumSlots = shelf.AvailableMediumSlots,
                AvailableSmallSlots = shelf.AvailableSmallSlots,
                Occupancy = shelf.Occupancy,
                WarehouseId = shelf.WarehouseId,
                Boxes = shelf.Boxes.Select(b => new BoxDto
                {
                    BoxId = b.BoxId,
                    Type = b.Type,   // Assuming 'Type' represents the box type
                    Size = b.BoxType?.Size ?? "Unknown" // Use the BoxType's size or fallback to "Unknown" if null
                }).ToList()
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

            var command = new UpdateShelfCommand(shelfDto);
            var updatedShelf = await _mediator.Send(command);

            return NoContent();
        }

        // Endpoint to delete a shelf by its ID
        [HttpDelete("DeleteShelfBy{id}")]
        public async Task<IActionResult> DeleteShelf(Guid id)
        {
            var command = new DeleteShelfCommand(id);
            await _mediator.Send(command);

            return NoContent();
        }
    }
}