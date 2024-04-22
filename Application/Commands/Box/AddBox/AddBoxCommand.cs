using Application.Dto.Box;
using Domain.Models.Box;
using MediatR;

namespace Application.Commands.Box.AddBox
{
    public class AddBoxCommand : IRequest<BoxModel>
    {
        public AddBoxCommand(BoxDto newBox, Guid shelfId)
        {
            NewBox = newBox;
            ShelfId = shelfId;
        }
        public BoxDto NewBox { get; }
        public Guid ShelfId { get; }
    }
}