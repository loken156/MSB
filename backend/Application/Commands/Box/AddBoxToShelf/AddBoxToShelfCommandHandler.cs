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

            if (shelf == null)
            {
                throw new KeyNotFoundException("Shelf not found.");
            }

            // Assign the ShelfId to the box and add the box to the shelf
            boxToUpdate.ShelfId = request.ShelfId;
            shelf.Boxes.Add(boxToUpdate);

            // Let the repository handle the logic for updating available slots when updating the shelf
            await _shelfRepository.UpdateShelfAsync(shelf);
            await _boxRepository.UpdateBoxAsync(boxToUpdate);

            // Use AutoMapper to map BoxModel to BoxDto before returning
            var boxDto = _mapper.Map<BoxDto>(boxToUpdate);

            return boxDto;
        }
    }
}
