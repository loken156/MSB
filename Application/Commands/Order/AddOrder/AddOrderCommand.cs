using Application.Dto.Order;
using Domain.Models.Order;
using MediatR;

namespace Application.Commands.Order.AddOrder
{
    public class AddOrderCommand : IRequest<OrderModel>
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