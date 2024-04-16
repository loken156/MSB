using Application.Commands.Address.AddAddress;
using Application.Commands.Address.DeleteAddress;
using Application.Commands.Address.UpdateAddress;
using Application.Dto.Adress;
using Application.Queries.Address.GetAll;
using Application.Queries.Address.GetByID;
using Application.Validators.AddressValidator;
using Domain.Models.Address;
using FluentValidation;
using Infrastructure.Repositories.OrderRepo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Address
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        private readonly AddressRepository _addressRepository;
        private readonly AddressValidations _addressValidations;
        private readonly ILogger _logger;

        public AddressController(IMediator mediator, IConfiguration configuration, AddressRepository addressRepository, AddressValidations validationRules, ILogger logger)
        {
            _mediator = mediator;
            _configuration = configuration;
            _addressRepository = addressRepository;
            _addressValidations = validationRules;
            _logger = logger;
        }

        [HttpPost]
        [Route("Add Address")]
        public async Task<ActionResult<AddressDto>> AddAddress(AddAddressCommand command)
        {

            try
            {
                var adressDto = new AddressDto
                {
                    StreetName = command.NewAddress.StreetName,
                    StreetNumber = command.NewAddress.StreetNumber,
                    Apartment = command.NewAddress.Apartment,
                    ZipCode = command.NewAddress.ZipCode,
                    Floor = command.NewAddress.Floor,
                    City = command.NewAddress.City,
                    State = command.NewAddress.State,
                    Country = command.NewAddress.Country,
                };


                var validationResult = _addressValidations.Validate(adressDto);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }

                var address = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetAddressById), new { id = address.AddressId }, address);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding address");
                return StatusCode(500, "An error occurred while adding an Address");
            }

        }

        [HttpGet]
        [Route("Get All Addresses")]
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

        [HttpGet("Get Address By {id}")]
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

        [HttpPut("Update Address By {id}")]
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

        [HttpDelete("Delete Address By {id}")]
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