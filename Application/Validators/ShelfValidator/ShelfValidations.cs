using Application.Dto.AddShelf;
using FluentValidation;
using Infrastructure.Repositories.WarehouseRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            RuleFor(x => x.WarehouseId)
            .MustAsync(async (warehouseId, cancellationToken) =>
                await _warehouseRepository.ExistWarehouseAsync(warehouseId))
            .WithMessage("Warehouse with the given id does not exist");

        }





    }
}
