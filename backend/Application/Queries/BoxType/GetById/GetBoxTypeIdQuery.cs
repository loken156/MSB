using Domain.Models.BoxType;
using MediatR;

namespace Application.Queries.BoxType.GetByID
{
    public class GetBoxTypeByIdQuery : IRequest<BoxTypeModel>
    {
        public int BoxTypeId { get; }
    
        public GetBoxTypeByIdQuery(int boxTypeId)
        {
            BoxTypeId = boxTypeId;
        }
    }
}