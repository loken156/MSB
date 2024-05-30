using Infrastructure.Repositories.ShelfRepo;
using MediatR;

// This class resides in the Application layer and handles the command to delete a shelf. 
// It implements the IRequestHandler interface provided by MediatR for processing the command. 
// The handler interacts with the shelf repository in the Infrastructure layer to delete the 
// shelf entity based on the provided ShelfId. It asynchronously invokes the DeleteShelfAsync 
// method of the repository and returns Unit.Value upon successful deletion.

namespace Application.Commands.Shelf.DeleteShelf
{
    public class DeleteShelfCommandHandler : IRequestHandler<DeleteShelfCommand, Unit>
    {
        private readonly IShelfRepository _shelfRepository;
        public DeleteShelfCommandHandler(IShelfRepository shelfRepository)
        {
            _shelfRepository = shelfRepository;
        }
        public async Task<Unit> Handle(DeleteShelfCommand request, CancellationToken cancellationToken)
        {
            await _shelfRepository.DeleteShelfAsync(request.ShelfId);
            return Unit.Value;
        }
    }
}