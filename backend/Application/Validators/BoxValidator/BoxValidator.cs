using Application.Dto.Box;
using FluentValidation;

namespace Application.Validators.BoxValidator
{
    public class BoxValidator : AbstractValidator<BoxDto>
    {
        public BoxValidator()
        {
            RuleFor(box => box.BoxId)
                .NotEmpty().WithMessage("Box ID can't be empty");

            RuleFor(box => box.Type)
                .NotEmpty().WithMessage("Box Type needs to be specified");

            RuleFor(box => box.BoxTypeId)
                .NotEmpty().WithMessage("BoxTypeId is required");
        }
    }
}