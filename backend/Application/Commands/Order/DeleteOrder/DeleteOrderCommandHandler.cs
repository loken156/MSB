using Infrastructure.Repositories.OrderRepo;
using MediatR;

// This class resides in the Application layer and handles the command to delete an order. 
// It implements the IRequestHandler interface provided by MediatR for processing the command. 
// The handler interacts with the order repository in the Infrastructure layer to delete 
// the order entity based on the provided OrderId. It asynchronously invokes the 
// DeleteOrderAsync method of the repository and returns Unit.Value upon successful deletion.

namespace Application.Commands.Order.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Unit>
    {
        private readonly IOrderRepository _orderRepository;

        public DeleteOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            await _orderRepository.DeleteOrderAsync(request.OrderId);
            return Unit.Value;
        }
    }
}