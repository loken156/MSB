using Application.Commands.Address.AddAddress;
using Application.Commands.Address.DeleteAddress;
using Application.Commands.Address.UpdateAddress;
using Application.Dto.Adress;
using Application.Queries.Address.GetAll;
using Application.Queries.Address.GetByID;
using Application.Validators.AddressValidator;
using Domain.Models.Address;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace API.Controllers.Address
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        private readonly IAddressValidations _addressValidations;
        private readonly ILogger<AddressController> _logger;

        public AddressController(IMediator mediator, IConfiguration configuration, IAddressValidations validationRules, ILogger<AddressController> logger)
        {
            _mediator = mediator;
            _configuration = configuration;
            _addressValidations = validationRules;
            _logger = logger;
        }

        [HttpPost]
        [Route("AddAddress")]
        public async Task<ActionResult<AddressDto>> AddAddress([FromBody] AddAddressCommand command)
        {
            try
            {
                var validationResult = _addressValidations.Validate(command.NewAddress);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }
                var addressDto = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetAddressById), new { id = addressDto.AddressId }, addressDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding address");
                return StatusCode(500, "An error occurred while adding address");
            }
        }

        [HttpGet]
        [Route("GetAllAddresses")]
        public async Task<ActionResult<IEnumerable<AddressDto>>> GetAllAddresses()
        {
            try
            {
                var query = new GetAllAddressesQuery();
                var addresses = await _mediator.Send(query);
                return Ok(addresses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting all addresses");
                return StatusCode(500, "An error occurred while getting all addresses");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AddressDto>> GetAddressById(Guid id)
        {
            try
            {
                var query = new GetAddressByIdQuery(id);
                var address = await _mediator.Send(query);

                if (address == null)
                {
                    return NotFound();
                }

                return Ok(address);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting address by id");
                return StatusCode(500, "An error occurred while getting address by id");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress(Guid id, AddressModel address)
        {
            try
            {
                if (id != address.AddressId)
                {
                    return BadRequest();
                }

                var command = new UpdateAddressCommand(address);
                var updatedAddress = await _mediator.Send(command);

                return Ok("Update successful");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating address with id: {id}", id);
                return StatusCode(500, "An error occurred while updating the address");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(Guid id)
        {
            try
            {
                var command = new DeleteAddressCommand(id);
                await _mediator.Send(command);

                return Ok("Deletion Successful");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting address with id: {id}", id);
                return StatusCode(500, "An error occurred while deleting the address");
            }
        }
    }
}