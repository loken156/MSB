using Domain.Models.Box;
using MediatR;

namespace Application.Queries.Box.GetAll
{
    public class GetAllBoxesQuery : IRequest<IEnumerable<BoxModel>>
    {
    }
}