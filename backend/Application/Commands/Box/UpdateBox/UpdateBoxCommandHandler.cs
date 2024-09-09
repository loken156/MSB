using Domain.Models.Box;
using Infrastructure.Repositories.BoxRepo;
using MediatR;

// This class resides in the Application layer and handles the command to update a box. 
// It interacts with the box repository in the Infrastructure layer to persist the changes 
// to the box entity. MediatR is used for processing the command. The class implements the 
// IRequestHandler interface with UpdateBoxCommand for input and BoxModel for output, indicating 
// that it processes commands to update boxes and returns the updated box model.

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

            BoxModel boxToUpdate = new BoxModel
            {
                BoxId = request.Box.BoxId,
                Type = request.Box.Type,
                TimesUsed = request.Box.TimesUsed,
                Stock = request.Box.Stock,
                ImageUrl = request.Box.ImageUrl,
                UserNotes = request.Box.UserNotes,
                Size = request.Box.Size

            };
            return await _boxRepository.UpdateBoxAsync(boxToUpdate);
        }
    }
}