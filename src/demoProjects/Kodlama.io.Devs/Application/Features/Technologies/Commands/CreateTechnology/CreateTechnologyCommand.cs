using Application.Features.Technologies.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Technologies.Commands.CreateTechnology;

public class CreateTechnologyCommand : IRequest<CreatedTechnologyDto>
{
    public int ProgrammingLanguageId { get; set; }
    public string Name { get; set; }

    public class
        CreateTechnologyCommandHandler : IRequestHandler<CreateTechnologyCommand, CreatedTechnologyDto>
    {
        private readonly IMapper _mapper;
        private readonly ITechnologyRepository _technologyRepository;


        public CreateTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper)
        {
            _technologyRepository = technologyRepository;
            _mapper = mapper;
        }

        public async Task<CreatedTechnologyDto> Handle(CreateTechnologyCommand request,
            CancellationToken cancellationToken)
        {
            Technology mappedTechnology = _mapper.Map<Technology>(request);
            Technology createdTechnology = await _technologyRepository.AddAsync(mappedTechnology);
            CreatedTechnologyDto createdTechnologyDto = _mapper.Map<CreatedTechnologyDto>(createdTechnology);
            return createdTechnologyDto;
        }
    }
}