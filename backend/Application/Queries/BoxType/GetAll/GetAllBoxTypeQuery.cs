using Domain.Models.BoxType;
using MediatR;

namespace Application.Queries.BoxType.GetAll
{
    public class GetAllBoxTypesQuery : IRequest<IEnumerable<BoxTypeModel>>
    {
    }
}