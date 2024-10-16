using Application.Dto.Detrack;
using Application.Dto.Order;

namespace Application.Services.Detrack
{
    public interface IDetrackService
    {
        Task<bool> CreateDetrackJobAsync(OrderDto orderDto);
    }
}
