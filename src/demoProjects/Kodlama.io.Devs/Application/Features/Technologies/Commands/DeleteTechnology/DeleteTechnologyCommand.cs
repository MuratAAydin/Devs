using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Technologies.Commands.DeleteTechnology;

public class DeleteTechnologyCommand : IRequest<DeletedTechnologyDto>
{
    public int Id { get; set; }

    public class
        DeleteTechnologyCommandHandler : IRequestHandler<DeleteTechnologyCommand, DeletedTechnologyDto>
    {
        private readonly IMapper _mapper;
        private readonly ITechnologyRepository _technologyRepository;
        private readonly TechnologyBusinessRules _technologyBusinessRules;

        public DeleteTechnologyCommandHandler(IMapper mapper, ITechnologyRepository technologyRepository,
            TechnologyBusinessRules technologyBusinessRules)
        {
            _mapper = mapper;
            _technologyRepository = technologyRepository;
            _technologyBusinessRules = technologyBusinessRules;
        }

        public async Task<DeletedTechnologyDto> Handle(DeleteTechnologyCommand request,
            CancellationToken cancellationToken)
        {
            Technology? technology = await _technologyRepository.GetAsync(e => e.Id == request.Id);

            _technologyBusinessRules.ProgrammingLanguageShouldExistWhenRequested(technology);

            Technology deletedTechnology = await _technologyRepository.DeleteAsync(technology);
            DeletedTechnologyDto deletedTechnologyDto = _mapper.Map<DeletedTechnologyDto>(deletedTechnology);
            return deletedTechnologyDto;
        }
    }
}