using Domain.Models.Box;
using Infrastructure.Repositories.BoxRepo;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.Box.UpdateBox
{
    public class UpdateBoxCommandHandler : IRequestHandler<UpdateBoxCommand, BoxModel>
    {
        private readonly IBoxRepository _boxRepository;

        public UpdateBoxCommandHandler(IBoxRepository boxRepository)
        {
            _boxRepository = boxRepository;
        }

        public async Task<BoxModel> Handle(UpdateBoxCommand request, CancellationToken cancellationToken)
        {
            // Get the box from the repository
            var boxToUpdate = await _boxRepository.GetBoxByIdAsync(request.Box.BoxId);

            if (boxToUpdate == null)
            {
                throw new KeyNotFoundException("Box not found.");
            }

            // Update basic properties
            boxToUpdate.Type = request.Box.Type;
            boxToUpdate.TimesUsed = request.Box.TimesUsed;
            boxToUpdate.Stock = request.Box.Stock;
            boxToUpdate.ImageUrl = request.Box.ImageUrl;
            boxToUpdate.UserNotes = request.Box.UserNotes;

            // Update BoxTypeId (the BoxType and Size will be handled via relationships in the domain model)
            boxToUpdate.BoxTypeId = request.Box.BoxType.BoxTypeId;

            // Save the updated box
            return await _boxRepository.UpdateBoxAsync(boxToUpdate);
        }
    }
}