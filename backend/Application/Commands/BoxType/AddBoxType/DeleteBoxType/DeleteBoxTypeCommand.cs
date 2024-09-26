using MediatR;

namespace Application.Commands.Box.DeleteBoxType
{
    public class DeleteBoxTypeCommand : IRequest<Unit>
    {
        public int BoxTypeId { get; set; }
        public DeleteBoxTypeCommand(int boxtypeId)
        {
            BoxTypeId = boxtypeId;
        }
    }
}