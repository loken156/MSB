using Application.Dto.TimeSlot;
using FluentValidation;

// This class defines validation rules for TimeSlotDto objects using FluentValidation.
// Each property of the TimeSlotDto class is validated with specific rules:
// - Id must not be empty.
// - Date must not be empty.
// - TimeSlot must not be empty.
// - Occupancy must not be empty.

namespace Application.Validators.TimeSlotValidator
{
    public class TimeSlotValidator : AbstractValidator<TimeSlotDto>
    {
        public TimeSlotValidator()
        {
            RuleFor(command => command.Id)
                .NotEmpty().WithMessage("TimeSlot ID can't be empty");

            RuleFor(slot => slot.Date)
                .NotEmpty().WithMessage("TimeSlot Date needs to be specified");

            RuleFor(slot => slot.TimeSlot)
                .NotEmpty().WithMessage("TimeSlot description needs to be specified");

            RuleFor(slot => slot.Occupancy)
                .NotEmpty().WithMessage("TimeSlot Occupancy needs to be specified");
        }
    }
}