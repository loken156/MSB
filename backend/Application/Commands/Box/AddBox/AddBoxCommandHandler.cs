using Application.Commands.Box.AddBox;
using Application.Dto.Box;
using AutoMapper;
using Domain.Models.Box;
using Infrastructure.Repositories.BoxRepo;
using Infrastructure.Repositories.BoxTypeRepo;
using MediatR;
using Microsoft.Extensions.Logging;

public class AddBoxCommandHandler : IRequestHandler<AddBoxCommand, BoxDto>
{
    private readonly IBoxRepository _boxRepository;
    private readonly IBoxTypeRepository _boxTypeRepository; // Added for BoxType fetching
    private readonly ILogger<AddBoxCommandHandler> _logger;
    private readonly IMapper _mapper;

    public AddBoxCommandHandler(IBoxRepository boxRepository, IBoxTypeRepository boxTypeRepository, ILogger<AddBoxCommandHandler> logger, IMapper mapper)
    {
        _boxRepository = boxRepository;
        _boxTypeRepository = boxTypeRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<BoxDto> Handle(AddBoxCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Fetch BoxType from the repository using BoxTypeId
            var boxType = await _boxTypeRepository.GetBoxTypeByIdAsync(request.NewBox.BoxTypeId);
            if (boxType == null)
            {
                throw new Exception($"BoxType with ID {request.NewBox.BoxTypeId} not found.");
            }

            // Map the incoming BoxDto to the domain model (BoxModel)
            var boxModel = _mapper.Map<BoxModel>(request.NewBox);
            
            // Assign the BoxType to the BoxModel (this links BoxType with Size and Type)
            boxModel.BoxType = boxType;

            // Save the box to the database
            await _boxRepository.AddBoxAsync(boxModel);

            // Map the result back to BoxDto and include the size and type from BoxType
            var boxDto = _mapper.Map<BoxDto>(boxModel);
            boxDto.Size = boxType.Size; // Manually set the Size in BoxDto
            boxDto.Type = boxType.Type; // Manually set the Type in BoxDto
            return boxDto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding box.");
            throw;
        }
    }
}