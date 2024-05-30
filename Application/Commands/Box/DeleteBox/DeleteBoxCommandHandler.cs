using Infrastructure.Repositories.BoxRepo;
using MediatR;

// This class handles the command to delete a box. It utilizes MediatR for processing the command. 
// The handler interacts with the box repository to delete the specified box from the data source.

namespace Application.Commands.Box.DeleteBox
{
    public class DeleteBoxCommandHandler : IRequestHandler<DeleteBoxCommand, Unit>
    {
        private readonly IBoxRepository _boxRepository;
        public DeleteBoxCommandHandler(IBoxRepository boxRepository)
        {
            _boxRepository = boxRepository;
        }
        public async Task<Unit> Handle(DeleteBoxCommand request, CancellationToken cancellationToken)
        {
            await _boxRepository.DeleteBoxAsync(request.BoxId);
            return Unit.Value;
        }

    }
}