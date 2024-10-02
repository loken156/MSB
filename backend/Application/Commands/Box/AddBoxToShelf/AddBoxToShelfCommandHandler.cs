using AutoMapper;
using Domain.Models.Box;
using Infrastructure.Repositories.BoxRepo;
using Infrastructure.Repositories.ShelfRepo;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Dto.Box;

namespace Application.Commands.Box.AddBoxToShelf
{
    public class AddBoxToShelfCommandHandler : IRequestHandler<AddBoxToShelfCommand, BoxDto> // Return BoxDto instead of BoxModel
    {
        private readonly IShelfRepository _shelfRepository;
        private readonly IBoxRepository _boxRepository;
        private readonly ILogger<AddBoxToShelfCommandHandler> _logger;
        private readonly IMapper _mapper;

        // Constructor with IMapper injection
        public AddBoxToShelfCommandHandler(
            IShelfRepository shelfRepository,
            IBoxRepository boxRepository,
            ILogger<AddBoxToShelfCommandHandler> logger,
            IMapper mapper) // Note the IMapper is injected here
        {
            _shelfRepository = shelfRepository;
            _boxRepository = boxRepository;
            _logger = logger;
            _mapper = mapper; // Assigning the mapper to the private field
        }

        public async Task<BoxDto> Handle(AddBoxToShelfCommand request, CancellationToken cancellationToken)
        {
            // Fetch the box from the repository using the BoxId from the command
            var boxToUpdate = await _boxRepository.GetBoxByIdAsync(request.BoxId);

            if (boxToUpdate == null)
            {
                throw new KeyNotFoundException("Box not found.");
            }

            // Fetch the shelf from the repository using the ShelfId from the command
            var shelf = await _shelfRepository.GetShelfWithBoxesAsync(request.ShelfId);

            // Access the Box Size through the BoxType reference
            var boxSize = boxToUpdate.BoxType.Size;

            // Check for available slots based on the box size
            if (boxSize == "Large" && shelf.AvailableLargeSlots > 0)
            {
                shelf.AvailableLargeSlots--;
            }
            else if (boxSize == "Medium" && shelf.AvailableMediumSlots > 0)
            {
                shelf.AvailableMediumSlots--;
            }
            else if (boxSize == "Small" && shelf.AvailableSmallSlots > 0)
            {
                shelf.AvailableSmallSlots--;
            }
            else
            {
                throw new InvalidOperationException("No available slots for this box size.");
            }

            // Assign the ShelfId to the box
            boxToUpdate.ShelfId = request.ShelfId;
            shelf.Boxes.Add(boxToUpdate);

            // Update the shelf and save the box
            await _shelfRepository.UpdateShelfAsync(shelf);
            await _boxRepository.UpdateBoxAsync(boxToUpdate);

            // Use AutoMapper to map BoxModel to BoxDto before returning
            var boxDto = _mapper.Map<BoxDto>(boxToUpdate);

            return boxDto;
        }
    }
}
