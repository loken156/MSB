
using Application.Commands.Box.DeleteBoxType;
using Application.Commands.BoxType.AddBoxType;
using Application.Dto.BoxType;
using Application.Queries.BoxType.GetAll;
using Application.Queries.BoxType.GetByID;
using AutoMapper;
using Domain.Models.BoxType;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.BoxTypeController
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoxTypeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BoxTypeController> _logger;
        private readonly IValidator<BoxTypeDto> _boxtypeValidator;
        private readonly IMapper _mapper;

        public BoxTypeController(IMediator mediator, ILogger<BoxTypeController> logger, IValidator<BoxTypeDto> boxtypeValidator, IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _boxtypeValidator = boxtypeValidator;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("AddBoxType")]
        public async Task<ActionResult<BoxTypeDto>> AddBoxType([FromBody] BoxTypeDto newBoxTypeDto)
        {
            _logger.LogInformation("Attempting to add a new box type");

            // Validate the DTO before proceeding
            ValidationResult validationResult = _boxtypeValidator.Validate(newBoxTypeDto);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation failed for adding box type: {Errors}", validationResult.Errors.Select(e => e.ErrorMessage));
                return BadRequest(validationResult.Errors);
            }

            try
            {
                // Create the command with the DTO from the request
                var command = new AddBoxTypeCommand(newBoxTypeDto);

                // Send the command to the handler through MediatR
                var boxtypeDto = await _mediator.Send(command);

                // If successful, return the created box DTO
                _logger.LogInformation("Box type added successfully with ID {BoxTypeId}", boxtypeDto.BoxTypeId);
                return CreatedAtAction(nameof(GetBoxTypeById), new { id = boxtypeDto.BoxTypeId }, boxtypeDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new box type");
                return StatusCode(500, "An internal error occurred. Please try again later.");
            }
        }

        [HttpGet]
        [Route("GetAllBoxTypes")]
        public async Task<ActionResult<IEnumerable<BoxTypeDto>>> GetAllBoxTypes()
        {
            _logger.LogInformation("Attempting to retrieve all boxes");

            try
            {
                var query = new GetAllBoxTypesQuery();
                var boxtypes = await _mediator.Send(query);

                if (boxtypes == null || !boxtypes.Any())
                {
                    _logger.LogWarning("No box types available in the database");
                    return NotFound("No box types available.");
                }

                var boxtypeDtos = boxtypes.Select(boxtype => new BoxTypeDto
                {
                    BoxTypeId = boxtype.BoxTypeId,
                    Size = boxtype.Size,
                    Stock = boxtype.Stock,
                    Description = boxtype.Description
                }).ToList();

                _logger.LogInformation("Retrieved {Count} box types successfully", boxtypeDtos.Count);
                return Ok(boxtypeDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all boxes");
                return StatusCode(500, "An error occurred while retrieving the box types. Please try again later.");
            }
        }

        [HttpGet("GetBoxTypeBy{id}")]
        public async Task<ActionResult<BoxTypeDto>> GetBoxTypeById(int id)
        {
            _logger.LogInformation("Attempting to retrieve box type with ID {BoxTypeId}", id);

            try
            {
                var query = new GetBoxTypeByIdQuery(id);
                var boxtype = await _mediator.Send(query);

                if (boxtype == null)
                {
                    _logger.LogWarning("No box type found with ID {BoxTypeId}", id);
                    return NotFound($"No box type found with ID {id}.");
                }

                var boxtypeDto = new BoxTypeDto
                {
                    BoxTypeId = boxtype.BoxTypeId,
                    Size = boxtype.Size,
                    Stock = boxtype.Stock,
                    Description = boxtype.Description
                };

                _logger.LogInformation("Box type with ID {BoxTypeId} retrieved successfully", id);
                return Ok(boxtypeDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving box with ID {BoxTypeId}", id);
                return StatusCode(500, "An error occurred while retrieving the box. Please try again later.");
            }
        }



        [HttpPut("UpdateBoxTypeBy{id}")]
        public async Task<IActionResult> UpdateBoxType(int id, BoxTypeDto boxtypeDto)
        {
            _logger.LogInformation("Starting update of box type with ID {BoxTypeId}", id);

            try
            {
                if (id != boxtypeDto.BoxTypeId)
                {
                    _logger.LogWarning("Mismatch between path ID {PathId} and box type DTO ID {DtoId}", id, boxtypeDto.BoxTypeId);
                    return BadRequest("The ID in the URL does not match the ID in the body.");
                }

                var command = new UpdateBoxTypeCommand(boxtypeDto);
                _logger.LogInformation("Sending UpdateBoxTypeCommand for box ID {BoxTypeId}", boxtypeDto.BoxTypeId);

                var updatedBoxType = await _mediator.Send(command);

                _logger.LogInformation("Box type with ID {BoxTypeId} updated successfully", boxtypeDto.BoxTypeId);
                return NoContent();
            }
            catch (KeyNotFoundException knfEx)
            {
                _logger.LogError(knfEx, "Box type with ID {BoxTypeId} not found", id);
                return NotFound($"No box type found with ID {id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating box type with ID {BoxTypeId}", id);
                return StatusCode(500, "An error occurred while updating the box type. Please try again later.");
            }
        }

        [HttpDelete("DeleteBoxTypeBy{id}")]
        public async Task<IActionResult> DeleteBoxType(int id)
        {
            _logger.LogInformation("Starting deletion of box type with Id {BoxTypeId}", id);
            try
            {
                var command = new DeleteBoxTypeCommand(id);
                await _mediator.Send(command);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting the box type with ID {BoxTypeId}", id);
                return StatusCode(500, "An error occurred whíle deleting the box type. Please try again later.");
            }
        }
    }
}