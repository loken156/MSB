using Application.Commands.BoxType.AddBoxType;
using Application.Commands.BoxType.AddBoxType;
using Application.Dto.BoxType;
using AutoMapper;
using Domain.Models.BoxType;
using Infrastructure.Repositories.BoxTypeRepo;
using MediatR;
using Microsoft.Extensions.Logging;

// This class handles the command to add a new box type. It uses MediatR for processing the command, 
// AutoMapper for mapping between the DTO and the domain model, and ILogger for logging errors. 
// The handler interacts with the box type repository to save the new box type and returns the created box type as a DTO.


namespace Application.Commands.BoxType.AddBoxType
{
    public class AddBoxTypeCommandHandler : IRequestHandler<AddBoxTypeCommand, BoxTypeDto>
    {
        private readonly IBoxTypeRepository _boxtypeRepository;
        private readonly ILogger<AddBoxTypeCommandHandler> _logger;
        private readonly IMapper _mapper;

        public AddBoxTypeCommandHandler(IBoxTypeRepository boxtypeRepository, ILogger<AddBoxTypeCommandHandler> logger, IMapper mapper)
        {
            _boxtypeRepository = boxtypeRepository;
            _logger = logger;
            _mapper = mapper;
        }


        public async Task<BoxTypeDto> Handle(AddBoxTypeCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var boxtypeModel = _mapper.Map<BoxTypeModel>(request.NewBoxType);
                await _boxtypeRepository.AddBoxTypeAsync(boxtypeModel);
                var boxtypeDto = _mapper.Map<BoxTypeDto>(boxtypeModel);
                return boxtypeDto;

            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error adding box type");
                throw;
            }
        }
    }
}