using Application.Dto.Box;
using FluentValidation;
using Infrastructure.Repositories.BoxTypeRepo; // Assuming you have a BoxType repository
using System.Linq;

namespace Application.Validators.BoxValidator
{
    public class BoxValidator : AbstractValidator<BoxDto>
    {
        private readonly IBoxTypeRepository _boxTypeRepository;

        // Inject the repository into the validator
        public BoxValidator(IBoxTypeRepository boxTypeRepository)
        {
            _boxTypeRepository = boxTypeRepository;

            RuleFor(command => command.BoxId)
                .NotEmpty().WithMessage("Box ID can't be empty");

            RuleFor(box => box.Type)
                .NotEmpty().WithMessage("Box Type needs to be specified");

            RuleFor(box => box.BoxType.Size)  // Accessing Size through BoxType
                .NotEmpty().WithMessage("Box Size needs to be specified")
                .MustAsync(async (size, cancellation) => await BeAValidSize(size))
                .WithMessage("Box Size is invalid. Must be a valid size from the BoxType table.");

        }

        // Method to check if the size is valid based on the BoxType table
        private async Task<bool> BeAValidSize(string size)
        {
            var validSizes = await _boxTypeRepository.GetAllBoxSizesAsync(); // Get valid sizes from the database
            return validSizes.Contains(size);
        }
    }
}