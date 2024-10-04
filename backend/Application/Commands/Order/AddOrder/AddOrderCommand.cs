using Application.Dto.Order;
using MediatR;

namespace Application.Commands.Order.AddOrder
{
    public class AddOrderCommand : IRequest<OrderDto>
    {
        public AddOrderCommand(AddOrderDto newOrder)
        {
            NewOrder = newOrder;
        }

        public AddOrderDto NewOrder { get; }
    }
}