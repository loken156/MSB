using Application.Dto.Shelf;
using Domain.Models.Shelf;
using MediatR;

namespace Application.Commands.Shelf.AddShelf
{
    public class AddShelfCommand : IRequest<ShelfModel>
    {
        public AddShelfCommand(ShelfDto newShelf, string warehouseName)
        {
            NewShelf = newShelf;
            WarehouseName = warehouseName;
        }

        public ShelfDto NewShelf { get; }
        public string WarehouseName { get; }


    }
}