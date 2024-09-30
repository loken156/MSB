using Application.Dto.BoxType;
using FluentValidation;

// This class defines validation rules for BoxTypeDto objects using FluentValidation.
// Each property of the BoxTypeDto class is validated with specific rules:
// - BoxTypeId must not be empty.
// - Size must not be empty.
// - Stock must not be empty.
// Additional validation rules for Orders are commented out but can be uncommented if needed.

namespace Application.Validators.BoxTypeValidator
{
    public class BoxTypeValidator : AbstractValidator<BoxTypeDto>
    {
        public BoxTypeValidator()
        {
            RuleFor(command => command.BoxTypeId)
                .NotEmpty().WithMessage("BoxType ID can't be empty");

            RuleFor(box => box.Size)
                .NotEmpty().WithMessage("Box Size needs to be specified");
            
            RuleFor(box => box.Stock)
                .NotEmpty().WithMessage("BoxType Stock needs to be specified");
            
            RuleFor(box => box.Description)
                .NotEmpty().WithMessage("BoxType Description needs to be specified");

            
        }
    }
}