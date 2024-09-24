using Domain.Models.BoxType;
using Infrastructure.Repositories.BoxTypeRepo;
using MediatR;

namespace Application.Commands.BoxType.UpdateBoxType
{
    public class UpdateBoxTypeCommandHandler : IRequestHandler<UpdateBoxTypeCommand, BoxTypeModel>
    {
        private readonly IBoxTypeRepository _boxtypeRepository;

        public UpdateBoxTypeCommandHandler(IBoxTypeRepository boxtypeRepository)
        {
            _boxtypeRepository = boxtypeRepository;
        }

        public async Task<BoxTypeModel> Handle(UpdateBoxTypeCommand request, CancellationToken cancellationToken)
        {
            // Use the constructor to initialize BoxTypeModel
            BoxTypeModel boxtypeToUpdate = new BoxTypeModel(
                request.Boxtype.Size, 
                request.Boxtype.Stock, 
                request.Boxtype.Description
            )
            {
                BoxTypeId = request.Boxtype.BoxTypeId // Set BoxTypeId separately as it's not part of the constructor
            };

            return await _boxtypeRepository.UpdateBoxTypeAsync(boxtypeToUpdate);
        }
    }
}
