using Application.Dto.BoxType;
using Domain.Models.BoxType;
using MediatR;

namespace Application.Commands.BoxType.AddBoxType
{
    public class AddBoxTypeCommand : IRequest<BoxTypeDto>
    {
        public BoxTypeDto NewBoxType { get; }
        public AddBoxTypeCommand(BoxTypeDto newBoxType)
        {
            NewBoxType = newBoxType;
        }


    }
}