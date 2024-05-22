using Application.Dto.Box;
using Domain.Models.Box;
using MediatR;

namespace Application.Commands.Box.AddBox
{
    public class AddBoxCommand : IRequest<BoxDto>
    {
        public BoxDto NewBox { get; }
        public AddBoxCommand(BoxDto newBox)
        {
            NewBox = newBox;
        }


    }
}