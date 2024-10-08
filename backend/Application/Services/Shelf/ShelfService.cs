using Domain.Models.Box;
using Domain.Models.BoxType;
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

        // Find the BoxType for the box being added
        var boxType = shelf.BoxTypes.FirstOrDefault(bt => bt.Size == box.BoxType.Size);
        if (boxType == null)
        {
            throw new Exception("Invalid box size for this shelf");
        }

        // Check if the shelf has space for the box type
        switch (boxType.Size)
        {
            case "Large":
                if (shelf.AvailableLargeSlots <= 0)
                    throw new Exception("No available slots for large boxes");
                shelf.AvailableLargeSlots -= 1;
                break;

            case "Medium":
                if (shelf.AvailableMediumSlots <= 0)
                    throw new Exception("No available slots for medium boxes");
                shelf.AvailableMediumSlots -= 1;
                break;

            case "Small":
                if (shelf.AvailableSmallSlots <= 0)
                    throw new Exception("No available slots for small boxes");
                shelf.AvailableSmallSlots -= 1;
                break;

            default:
                throw new Exception("Unknown box size");
        }

        // Check if the shelf is fully occupied
        shelf.Occupancy = (shelf.AvailableLargeSlots == 0 && shelf.AvailableMediumSlots == 0 && shelf.AvailableSmallSlots == 0);

        // Add the box to the shelf and update the stock in the box type
        shelf.Boxes.Add(box);
        boxType.UpdateStock(-1); // Reduce stock by 1 when adding a box

        await _shelfRepository.UpdateShelfAsync(shelf);
        await _boxRepository.AddBoxAsync(box);
    }

    public async Task RemoveBoxFromShelf(Guid shelfId, BoxModel box)
    {
        var shelf = await _shelfRepository.GetShelfWithBoxesAsync(shelfId);
        if (shelf == null)
        {
            throw new Exception("Shelf not found");
        }

        // Find the BoxType for the box being removed
        var boxType = shelf.BoxTypes.FirstOrDefault(bt => bt.Size == box.BoxType.Size);
        if (boxType == null)
        {
            throw new Exception("Invalid box size for this shelf");
        }

        // Remove the box and update available slots
        switch (boxType.Size)
        {
            case "Large":
                shelf.AvailableLargeSlots += 1; // Increase available large box slots
                break;
            case "Medium":
                shelf.AvailableMediumSlots += 1; // Increase available medium box slots
                break;
            case "Small":
                shelf.AvailableSmallSlots += 1; // Increase available small box slots
                break;
        }

        // Remove the box from the shelf
        shelf.Boxes.Remove(box);
        boxType.UpdateStock(1); // Increase stock by 1 when removing a box

        // Update occupancy status
        shelf.Occupancy = (shelf.AvailableLargeSlots == 0 && shelf.AvailableMediumSlots == 0 && shelf.AvailableSmallSlots == 0);

        await _shelfRepository.UpdateShelfAsync(shelf);
        await _boxRepository.DeleteBoxAsync(box);
    }
}
