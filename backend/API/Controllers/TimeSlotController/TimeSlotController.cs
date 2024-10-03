using Application.Commands.TimeSlot.AddTimeSlot;
using Application.Commands.TimeSlot.DeleteTimeSlot;
using Application.Commands.TimeSlot.UpdateTimeSlot;
using Application.Dto.TimeSlot;
using Application.Queries.TimeSlot.GetAll;
using Application.Queries.TimeSlot.GetByID;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers.TimeSlotController
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimeSlotController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TimeSlotController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<TimeSlotDto>> AddTimeSlot([FromBody] TimeSlotDto timeSlotDto)
        {
            var command = new AddTimeSlotCommand(timeSlotDto);
            var createdTimeSlot = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetTimeSlotById), new { id = createdTimeSlot.Id }, createdTimeSlot);
        }

        [HttpGet]
        [Route("GetAllTimeSlot")]
        public async Task<ActionResult<IEnumerable<TimeSlotDto>>> GetAllTimeSlots()
        {
            var query = new GetAllTimeSlotsQuery();
            var timeSlots = await _mediator.Send(query);
            return Ok(timeSlots);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TimeSlotDto>> GetTimeSlotById(Guid id)
        {
            var query = new GetTimeSlotByIdQuery(id);
            var timeSlot = await _mediator.Send(query);

            if (timeSlot == null)
            {
                return NotFound();
            }

            return Ok(timeSlot);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTimeSlot(Guid id, [FromBody] TimeSlotDto timeSlotDto)
        {
            if (id != timeSlotDto.Id)
            {
                return BadRequest();
            }

            var command = new UpdateTimeSlotCommand(timeSlotDto);
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimeSlot(Guid id)
        {
            var command = new DeleteTimeSlotCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}