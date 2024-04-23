using Application.Dto.AddShelf;
using Application.Dto.Box;
using Domain.Models.Shelf;
using MediatR;

namespace Application.Commands.Shelf.AddShelf
{
    public class AddShelfCommand : IRequest<ShelfModel>
    {
        public AddShelfCommand(AddShelfDto newShelf, Guid warehouseId, IEnumerable<BoxDto> boxes)
        {
            NewShelf = newShelf;
            WarehouseId = warehouseId;
            Boxes = boxes;
        }

        public AddShelfDto NewShelf { get; }
        public Guid WarehouseId { get; }
        public IEnumerable<BoxDto> Boxes { get; }
    }
}