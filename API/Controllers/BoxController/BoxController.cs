using Application.Commands.Box.AddBox;
using Application.Commands.Box.DeleteBox;
using Application.Commands.Box.UpdateBox;
using Application.Dto.Box;
using Application.Queries.Box.GetAll;
using Application.Queries.Box.GetByID;
using Application.Validators.BoxValidator;
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
        private readonly BoxValidator _boxValidator;

        public BoxController(IMediator mediator, ILogger<BoxController> logger, BoxValidator boxValidator)
        {
            _mediator = mediator;
            _logger = logger;
            _boxValidator = boxValidator;
        }

        [HttpPost]
        [Route("Add Box")]
        public async Task<ActionResult<BoxDto>> AddBox(AddBoxCommand command)
        {
            _logger.LogInformation("Attempting to add a new box");


            ValidationResult validationResult = _boxValidator.Validate(command.NewBox);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation failed for adding box: {Errors}", validationResult.Errors.Select(e => e.ErrorMessage));
                return BadRequest(validationResult.Errors);
            }

            try
            {
                var box = await _mediator.Send(command);

                if (box == null)
                {
                    _logger.LogError("Failed to create a new box, mediator returned null");
                    return BadRequest("Could not create box, data may be incorrect.");
                }

                _logger.LogInformation("Box added successfully with ID {BoxId}", box.BoxId);
                return CreatedAtAction(nameof(GetBoxById), new { id = box.BoxId }, box);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new box");
                return StatusCode(500, "An internal error occurred. Please try again later.");
            }
        }



        [HttpGet]
        [Route("Get All Boxes")]
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
                    Order = box.Order,
                    Size = box.Size,
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

        [HttpGet("Get Box By {id}")]
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

                var boxDto = new BoxDto
                {
                    BoxId = box.BoxId,
                    Type = box.Type,
                    TimesUsed = box.TimesUsed,
                    Stock = box.Stock,
                    ImageUrl = box.ImageUrl,
                    UserNotes = box.UserNotes,
                    Size = box.Size,
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
        [HttpPut("Update Box By {id}")]
        public async Task<IActionResult> UpdateBox(Guid id, BoxDto boxDto)
        {

            // Inject ILogger via constructor or method injection to use it here
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

        [HttpDelete("Delete Box By {id}")]
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
                return StatusCode(500, "An error occurred whíle deleting the box. Please try again later.");
            }


        }

    }
}