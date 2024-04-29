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

            RuleFor(warehouse => warehouse.ShelfIds)
                .NotEmpty().WithMessage("At least one Shelf ID is required.")
                .Must(ids => ids.All(id => id != Guid.Empty)).WithMessage("Shelf IDs must not be empty.");
        }
    }
}
