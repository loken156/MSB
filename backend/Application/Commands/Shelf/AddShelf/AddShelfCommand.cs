using Application.Dto.AddShelf;
using Domain.Models.Shelf;
using MediatR;

namespace Application.Commands.Shelf.AddShelf
{
    public class AddShelfCommand : IRequest<ShelfModel>
    {
        public AddShelfCommand(AddShelfDto newShelf, string warehouseName)
        {
            NewShelf = newShelf;
            WarehouseName = warehouseName;
        }

        public AddShelfDto NewShelf { get; }
        public string WarehouseName { get; }


    }
}