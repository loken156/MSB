using Application.Dto.Box;
using AutoMapper;
using Domain.Models.Box;
using Infrastructure.Repositories.BoxRepo;
using MediatR;
using Microsoft.Extensions.Logging;

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