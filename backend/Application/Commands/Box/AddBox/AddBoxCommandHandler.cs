using Application.Dto.Box;
using AutoMapper;
using Domain.Models.Box;
using Infrastructure.Repositories.BoxRepo;
using MediatR;
using Microsoft.Extensions.Logging;

// This class handles the command to add a new box. It uses MediatR for processing the command, 
// AutoMapper for mapping between the DTO and the domain model, and ILogger for logging errors. 
// The handler interacts with the box repository to save the new box and returns the created box as a DTO.


namespace Application.Commands.Box.AddBox
{
    public class AddBoxCommandHandler : IRequestHandler<AddBoxCommand, BoxDto>
    {
        private readonly IBoxRepository _boxRepository;
        private readonly ILogger<AddBoxCommandHandler> _logger;
        private readonly IMapper _mapper;

        public AddBoxCommandHandler(IBoxRepository boxRepository, ILogger<AddBoxCommandHandler> logger, IMapper mapper)
        {
            _boxRepository = boxRepository;
            _logger = logger;
            _mapper = mapper;
        }


        public async Task<BoxDto> Handle(AddBoxCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var boxModel = _mapper.Map<BoxModel>(request.NewBox);
                await _boxRepository.AddBoxAsync(boxModel);
                var boxDto = _mapper.Map<BoxDto>(boxModel);
                return boxDto;

            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error adding box");
                throw;
            }
        }
    }
}