using Application.Commands.Box.AddBox;
using Application.Commands.Box.DeleteBox;
using Application.Commands.Box.UpdateBox;
using Application.Dto.Box;
using Application.Dto.BoxType;
using Application.Dto.Order;
using Application.Queries.Box.GetAll;
using Application.Queries.Box.GetByID;
using Application.Queries.BoxType.GetByID;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.BoxController
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoxController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BoxController> _logger;
        private readonly IValidator<BoxDto> _boxValidator;
        private readonly IMapper _mapper;

        public BoxController(IMediator mediator, ILogger<BoxController> logger, IValidator<BoxDto> boxValidator, IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _boxValidator = boxValidator;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("AddBox")]
        public async Task<ActionResult<BoxDto>> AddBox([FromBody] BoxDto newBoxDto)
        {
            _logger.LogInformation("Attempting to add a new box");

            if (newBoxDto.BoxId == Guid.Empty)
            {
                newBoxDto.BoxId = Guid.NewGuid(); // Generate a new GUID for BoxId
            }

            // Validate the DTO
            ValidationResult validationResult = _boxValidator.Validate(newBoxDto);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation failed for adding box: {Errors}", validationResult.Errors.Select(e => e.ErrorMessage));
                return BadRequest(validationResult.Errors);
            }

            try
            {
                // Create the command with the DTO from the request
                var command = new AddBoxCommand(newBoxDto);

                // Send the command to the handler through MediatR
                var boxDto = await _mediator.Send(command);

                // If successful, return the created box DTO
                _logger.LogInformation("Box added successfully with ID {BoxId}", boxDto.BoxId);
                return CreatedAtAction(nameof(GetBoxById), new { id = boxDto.BoxId }, boxDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new box");
                return StatusCode(500, "An internal error occurred. Please try again later.");
            }
        }

        [HttpGet]
        [Route("GetAllBoxes")]
        public async Task<ActionResult<IEnumerable<BoxDto>>> GetAllBoxes()
        {
            _logger.LogInformation("Attempting to retrieve all boxes");

            try
            {
                var query = new GetAllBoxesQuery();
                var boxes = await _mediator.Send(query);

                if (boxes == null || !boxes.Any())
                {
                    _logger.LogWarning("No boxes available in the database");
                    return NotFound("No boxes available.");
                }

                var boxDtos = boxes.Select(box => new BoxDto
                {
                    BoxId = box.BoxId,
                    Type = box.Type,
                    TimesUsed = box.TimesUsed,
                    Stock = box.Stock,
                    ImageUrl = box.ImageUrl,
                    UserNotes = box.UserNotes,
                    OrderId = box.OrderId,
                    BoxTypeId = box.BoxTypeId,  // Only return the BoxTypeId
                    Size = box.BoxType?.Size  // Optionally return the size based on BoxType
                }).ToList();

                _logger.LogInformation("Retrieved {Count} boxes successfully", boxDtos.Count);
                return Ok(boxDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all boxes");
                return StatusCode(500, "An error occurred while retrieving the boxes. Please try again later.");
            }
        }

        [HttpGet("GetBoxBy{id}")]
        public async Task<ActionResult<BoxDto>> GetBoxById(Guid id)
        {
            _logger.LogInformation("Attempting to retrieve box with ID {BoxId}", id);

            try
            {
                var query = new GetBoxByIdQuery(id);
                var box = await _mediator.Send(query);

                if (box == null)
                {
                    _logger.LogWarning("No box found with ID {BoxId}", id);
                    return NotFound($"No box found with ID {id}.");
                }

                // Fetch the BoxType using the BoxTypeId
                var boxType = await _mediator.Send(new GetBoxTypeByIdQuery(box.BoxTypeId));
                if (boxType == null)
                {
                    _logger.LogWarning("No BoxType found with ID {BoxTypeId}", box.BoxTypeId);
                    return NotFound($"No BoxType found with ID {box.BoxTypeId}.");
                }

                // Map the box to BoxDto and include the size from BoxType
                var boxDto = new BoxDto
                {
                    BoxId = box.BoxId,
                    Type = box.Type,
                    TimesUsed = box.TimesUsed,
                    Stock = box.Stock,
                    ImageUrl = box.ImageUrl,
                    UserNotes = box.UserNotes,
                    OrderId = box.OrderId,
                    BoxTypeId = box.BoxTypeId,  // Use the BoxTypeId
                    Size = boxType.Size          // Get the Size from the BoxType
                };

                _logger.LogInformation("Box with ID {BoxId} retrieved successfully", id);
                return Ok(boxDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving box with ID {BoxId}", id);
                return StatusCode(500, "An error occurred while retrieving the box. Please try again later.");
            }
        }


        [HttpPut("UpdateBoxBy{id}")]
        public async Task<IActionResult> UpdateBox(Guid id, BoxDto boxDto)
        {
            _logger.LogInformation("Starting update of box with ID {BoxId}", id);

            try
            {
                if (id != boxDto.BoxId)
                {
                    _logger.LogWarning("Mismatch between path ID {PathId} and box DTO ID {DtoId}", id, boxDto.BoxId);
                    return BadRequest("The ID in the URL does not match the ID in the body.");
                }

                var command = new UpdateBoxCommand(boxDto);
                _logger.LogInformation("Sending UpdateBoxCommand for box ID {BoxId}", boxDto.BoxId);

                var updatedBox = await _mediator.Send(command);

                _logger.LogInformation("Box with ID {BoxId} updated successfully", boxDto.BoxId);
                return NoContent();
            }
            catch (KeyNotFoundException knfEx)
            {
                _logger.LogError(knfEx, "Box with ID {BoxId} not found", id);
                return NotFound($"No box found with ID {id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating box with ID {BoxId}", id);
                return StatusCode(500, "An error occurred while updating the box. Please try again later.");
            }
        }

        [HttpDelete("DeleteBoxBy{id}")]
        public async Task<IActionResult> DeleteBox(Guid id)
        {
            _logger.LogInformation("Starting deletion of box with Id {BoxId}", id);
            try
            {
                var command = new DeleteBoxCommand(id);
                await _mediator.Send(command);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting the box with ID {BoxId}", id);
                return StatusCode(500, "An error occurred while deleting the box. Please try again later.");
            }
        }
    }
}