using Application.Dto.AddWarehouse;
using FluentValidation;

namespace Application.Validators.WarehouseValidator
{
    public class WareHouseValidations : AbstractValidator<AddWarehouseDto>
    {

        public WareHouseValidations()
        {
            RuleFor(warehouse => warehouse.WarehouseName)
                .NotEmpty().WithMessage("Warehouse name is required.")
                .MinimumLength(3).WithMessage("Minimum warehouse name length is 3 letters long")
                .MaximumLength(50).WithMessage("Maximum warehouse name length is 50 letters");

            RuleFor(warehouse => warehouse.AddressId)
                .NotEmpty().WithMessage("Address ID is required.");

            RuleFor(warehouse => warehouse.ShelfId)
                .NotEmpty().WithMessage("Shelf ID is required.");
        }

    }
}