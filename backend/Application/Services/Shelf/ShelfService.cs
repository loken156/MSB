using Domain.Models.Box;
using Infrastructure.Repositories.BoxRepo;
using Infrastructure.Repositories.ShelfRepo;

public class ShelfService
{
    private readonly IShelfRepository _shelfRepository;
    private readonly IBoxRepository _boxRepository;

    public ShelfService(IShelfRepository shelfRepository, IBoxRepository boxRepository)
    {
        _shelfRepository = shelfRepository;
        _boxRepository = boxRepository;
    }

    public async Task AddBoxToShelf(Guid shelfId, BoxModel box)
    {
        var shelf = await _shelfRepository.GetShelfWithBoxesAsync(shelfId);
        if (shelf == null)
        {
            throw new Exception("Shelf not found");
        }

        // Check if the shelf has space for the box type
        switch (box.Size)
        {
            case BoxSize.Large:
                if (shelf.AvailableLargeSlots <= 0)
                    throw new Exception("No available slots for large boxes");
                shelf.AvailableLargeSlots -= 1; // Decrease available large box slots
                break;

            case BoxSize.Medium:
                if (shelf.AvailableMediumSlots <= 0)
                    throw new Exception("No available slots for medium boxes");
                shelf.AvailableMediumSlots -= 1; // Decrease available medium box slots
                break;

            case BoxSize.Small:
                if (shelf.AvailableSmallSlots <= 0)
                    throw new Exception("No available slots for small boxes");
                shelf.AvailableSmallSlots -= 1; // Decrease available small box slots
                break;
        }

        // Check if the shelf is fully occupied
        shelf.Occupancy = (shelf.AvailableLargeSlots == 0 && shelf.AvailableMediumSlots == 0 && shelf.AvailableSmallSlots == 0);

        // Add the box to the shelf and save
        shelf.Boxes.Add(box);
        await _shelfRepository.UpdateShelfAsync(shelf);

        // Save the box to the database
        await _boxRepository.AddBoxAsync(box);
    }

    public async Task RemoveBoxFromShelf(Guid shelfId, BoxModel box)
    {
        var shelf = await _shelfRepository.GetShelfWithBoxesAsync(shelfId);
        if (shelf == null)
        {
            throw new Exception("Shelf not found");
        }

        // Remove the box and update available slots
        switch (box.Size)
        {
            case BoxSize.Large:
                shelf.AvailableLargeSlots += 1; // Increase available large box slots
                break;
            case BoxSize.Medium:
                shelf.AvailableMediumSlots += 1; // Increase available medium box slots
                break;
            case BoxSize.Small:
                shelf.AvailableSmallSlots += 1; // Increase available small box slots
                break;
        }

        // Remove the box from the shelf
        shelf.Boxes.Remove(box);

        // Update occupancy status
        shelf.Occupancy = false;

        await _shelfRepository.UpdateShelfAsync(shelf);
        await _boxRepository.RemoveBoxAsync(box);
    }
}
