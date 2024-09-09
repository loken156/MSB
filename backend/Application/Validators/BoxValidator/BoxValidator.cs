using Application.Dto.Box;
using FluentValidation;

// This class defines validation rules for BoxDto objects using FluentValidation.
// Each property of the BoxDto class is validated with specific rules:
// - BoxId must not be empty.
// - Type must not be empty.
// - Size must not be empty.
// Additional validation rules for Orders are commented out but can be uncommented if needed.

namespace Application.Validators.BoxValidator
{
    public class BoxValidator : AbstractValidator<BoxDto>
    {
        public BoxValidator()
        {
            RuleFor(command => command.BoxId)
                .NotEmpty().WithMessage("Box ID can't be empty");

            RuleFor(box => box.Type)
                .NotEmpty().WithMessage("Box Type needs specificaton");

            RuleFor(box => box.Size)
                .NotEmpty().WithMessage("Box Size needs to be specified");

            //RuleFor(box => box.Orders)
            //    .NotEmpty().WithMessage("Box needs to be attached to an ID");


        }
    }
}