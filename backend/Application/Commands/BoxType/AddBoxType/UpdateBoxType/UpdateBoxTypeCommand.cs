using Application.Dto.BoxType;
using Domain.Models.BoxType;
using MediatR;

public class UpdateBoxTypeCommand : IRequest<BoxTypeModel>
{
    public BoxTypeDto Boxtype { get; }

    public UpdateBoxTypeCommand(BoxTypeDto boxtype)
    {
        Boxtype = boxtype;
    }
}