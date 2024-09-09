using Domain.Models.Order;
using Infrastructure.Repositories.OrderRepo;
using MediatR;

// This class handles the GetAllOrdersQuery, responsible for retrieving all orders. 
// It depends on an IOrderRepository instance provided via its constructor for data access. 
// The Handle method asynchronously processes the query, attempting to retrieve all orders 
// from the repository. Any exceptions encountered during the process are propagated.

namespace Application.Queries.Order.GetAll
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderModel>>
    {
        private readonly IOrderRepository _orderRepository;
        public GetAllOrdersQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<IEnumerable<OrderModel>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            return await _orderRepository.GetAllOrdersAsync();
        }
    }
}