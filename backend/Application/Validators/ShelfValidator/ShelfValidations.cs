using Application.Dto.AddShelf;
using FluentValidation;
using Infrastructure.Repositories.WarehouseRepo;

// This class defines validation rules for AddShelfDto objects using FluentValidation.
// Each property of the AddShelfDto class is validated with specific rules:
// - ShelfRow must be greater than or equal to 1.
// - ShelfColumn must be greater than or equal to 1.
// - Occupancy must be false, indicating the new shelf is not already occupied.
// Additional validations for other fields can be added as needed.
// The rule for WarehouseId is commented out, but it can be enabled to validate the existence of a warehouse with the given id.

namespace Application.Validators.ShelfValidator
{
    public class ShelfValidations : AbstractValidator<AddShelfDto>
    {

        private readonly IWarehouseRepository _warehouseRepository;


        public ShelfValidations(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;


            RuleFor(x => x.ShelfRow)
                .GreaterThanOrEqualTo(1).WithMessage("Shelf row must be greater than or equal to 1");

            RuleFor(x => x.ShelfColumn)
                .GreaterThanOrEqualTo(1).WithMessage("Shelf column must be greater than or equal to 1");

            RuleFor(x => x.Occupancy)
                  .Equal(false).WithMessage("New shelf cannot already be occupied");

            //RuleFor(x => x.WarehouseId)
            //.MustAsync(async (warehouseId, cancellationToken) =>
            //    await _warehouseRepository.ExistWarehouseAsync(warehouseId))
            //.WithMessage("Warehouse with the given id does not exist");

        }





    }
}