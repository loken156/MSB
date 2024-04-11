using Application.Dto.Order;
using Domain.Models.Order;
using MediatR;

namespace Application.Commands.Order.AddOrder
{
    public class AddOrderCommand : IRequest<OrderModel>
    {
        public AddOrderCommand(OrderDto newOrder, Guid warehouseId)
        {
            NewOrder = newOrder;
            WarehouseId = warehouseId;

        }
        public OrderDto NewOrder { get; }
        public Guid WarehouseId { get; }
    }
}