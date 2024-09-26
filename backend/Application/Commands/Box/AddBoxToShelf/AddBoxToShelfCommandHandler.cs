using Domain.Models.Box;
using Infrastructure.Repositories.BoxRepo;
using Infrastructure.Repositories.ShelfRepo;
using MediatR;

namespace Application.Commands.Box.AddBoxToShelf;

public class AddBoxToShelfCommandHandler : IRequestHandler<AddBoxToShelfCommand, BoxModel>
{
    private readonly IShelfRepository _shelfRepository;
    private readonly IBoxRepository _boxRepository;

    public AddBoxToShelfCommandHandler(IShelfRepository shelfRepository, IBoxRepository boxRepository)
    {
        _shelfRepository = shelfRepository;
        _boxRepository = boxRepository;
    }

    public async Task<BoxModel> Handle(AddBoxToShelfCommand request, CancellationToken cancellationToken)
    {
        // Get the shelf from the repository
        var shelf = await _shelfRepository.GetShelfWithBoxesAsync(request.ShelfId);

        // Check for available slots
        if (request.Box.Size == BoxSize.Large && shelf.AvailableLargeSlots > 0)
        {
            shelf.AvailableLargeSlots--;
        }
        else if (request.Box.Size == BoxSize.Medium && shelf.AvailableMediumSlots > 0)
        {
            shelf.AvailableMediumSlots--;
        }
        else if (request.Box.Size == BoxSize.Small && shelf.AvailableSmallSlots > 0)
        {
            shelf.AvailableSmallSlots--;
        }
        else
        {
            throw new InvalidOperationException("No available slots for this box size.");
        }

        // Add the box to the shelf
        shelf.Boxes.Add(request.Box);

        // Update the shelf and save the box
        await _shelfRepository.UpdateShelfAsync(shelf);
        var createdBox = await _boxRepository.AddBoxAsync(request.Box);

        return createdBox;
    }
}