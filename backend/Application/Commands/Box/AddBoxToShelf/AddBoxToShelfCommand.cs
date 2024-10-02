using Application.Dto.Box;
using Application.Dto.Shelf;
using Domain.Models.Box;
using MediatR;

namespace Application.Commands.Box.AddBoxToShelf;

public class AddBoxToShelfCommand : IRequest<BoxDto>
{
    public Guid BoxId { get; set; }  // The ID of the box to be added
    public Guid ShelfId { get; set; }  // The ID of the shelf where the box will be added

    public AddBoxToShelfCommand(Guid boxId, Guid shelfId)
    {
        BoxId = boxId;
        ShelfId = shelfId;
    }
}

