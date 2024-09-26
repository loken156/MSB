using Domain.Models.Box;
using MediatR;

namespace Application.Commands.Box.AddBoxToShelf;

public class AddBoxToShelfCommand : IRequest<BoxModel>
{
    public AddBoxToShelfCommand(BoxModel box, Guid shelfId)
    {
        Box = box;
        ShelfId = shelfId;
    }

    public BoxModel Box { get; }
    public Guid ShelfId { get; }
}