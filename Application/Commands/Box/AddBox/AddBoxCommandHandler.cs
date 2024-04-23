using Domain.Models.Box;
using Infrastructure.Repositories.BoxRepo;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Box.AddBox
{
    public class AddBoxCommandHandler : IRequestHandler<AddBoxCommand, BoxModel>
    {
        private readonly IBoxRepository _boxRepository;
        private readonly ILogger<AddBoxCommandHandler> _logger;

        public AddBoxCommandHandler(IBoxRepository boxRepository, ILogger<AddBoxCommandHandler> logger)
        {
            _boxRepository = boxRepository;
            _logger = logger;
        }


        public async Task<BoxModel> Handle(AddBoxCommand request, CancellationToken cancellationToken)
        {
            var boxToCreate = new BoxModel
            {
                BoxId = Guid.NewGuid(),
                Type = request.NewBox.Type,
                TimesUsed = request.NewBox.TimesUsed,
                Stock = request.NewBox.Stock,
                ImageUrl = request.NewBox.ImageUrl,
                UserNotes = request.NewBox.UserNotes,
                Order = request.NewBox.Order,
                Size = request.NewBox.Size,
                ShelfId = request.ShelfId
            };

            try
            {
                var createdBox = await _boxRepository.AddBoxAsync(boxToCreate, request.ShelfId);
                return createdBox;

            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error adding box");
                throw;
            }
        }
    }
}