﻿using Domain.Models.Box;
using MediatR;

namespace Application.Queries.Box.GetByID
{
    public class GetBoxByIdQuery : IRequest<BoxModel>
    {
        public Guid BoxId { get; }
        public GetBoxByIdQuery(Guid boxId)
        {
            BoxId = boxId;
        }
    }
}