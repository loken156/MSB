using Application.Queries.Order.GetByID;
using Domain.Models.Order;
using Infrastructure.Repositories.OrderRepo;
using MediatR;

// This class handles the GetOrderByIdQuery, responsible for retrieving an order by its ID. 
// It depends on an IOrderRepository instance provided via its constructor for data access. 
// The Handle method asynchronously processes the query, attempting to retrieve the order 
// corresponding to the provided order ID from the repository. Any exceptions encountered 
// during the process are propagated.

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderModel>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderModel> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        return await _orderRepository.GetOrderByIdAsync(request.OrderId);
    }
}