using Application.Dto.Order;
using MediatR;

namespace Application.Commands.Order.AddOrder
{
    public class AddOrderCommand : IRequest<OrderDto>
    {
        public AddOrderCommand(AddOrderDto newOrder, Guid warehouseId)
        {
            NewOrder = newOrder;
            WarehouseId = warehouseId;
        }

        public AddOrderDto NewOrder { get; }
        public Guid WarehouseId { get; }
    }
}