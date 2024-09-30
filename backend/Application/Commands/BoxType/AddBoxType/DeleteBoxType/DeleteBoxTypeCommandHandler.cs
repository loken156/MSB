using Application.Commands.Box.DeleteBoxType;
using Infrastructure.Repositories.BoxTypeRepo;
using MediatR;

// This class handles the command to delete a box. It utilizes MediatR for processing the command. 
// The handler interacts with the box repository to delete the specified box from the data source.

namespace Application.Commands.BoxType.DeleteBoxType
{
    public class DeleteBoxTypeCommandHandler : IRequestHandler<DeleteBoxTypeCommand, Unit>
    {
        private readonly IBoxTypeRepository _boxtypeRepository;
        public DeleteBoxTypeCommandHandler(IBoxTypeRepository boxtypeRepository)
        {
            _boxtypeRepository = boxtypeRepository;
        }
        public async Task<Unit> Handle(DeleteBoxTypeCommand request, CancellationToken cancellationToken)
        {
            await _boxtypeRepository.DeleteBoxTypeAsync(request.BoxTypeId);
            return Unit.Value;
        }

    }
}