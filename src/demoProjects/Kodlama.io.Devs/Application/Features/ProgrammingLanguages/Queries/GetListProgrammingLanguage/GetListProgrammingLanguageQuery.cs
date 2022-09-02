using Application.Features.ProgrammingLanguages.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using MediatR;

namespace Application.Features.ProgrammingLanguages.Queries.GetListProgrammingLanguage;

public class GetListProgrammingLanguageQuery : IRequest<ProgrammingLanguageListModel>
{
    public PageRequest PageRequest { get; set; }

    public class
        GetListProgrammingLanguageQueryHandler : IRequestHandler<GetListProgrammingLanguageQuery,
            ProgrammingLanguageListModel>
    {
        private readonly IMapper _mapper;
        private readonly IProgrammingLanguageRepository _programmingLanguageRepository;

        public GetListProgrammingLanguageQueryHandler(IProgrammingLanguageRepository programmingLanguageRepository,
            IMapper mapper)
        {
            _programmingLanguageRepository = programmingLanguageRepository;
            _mapper = mapper;
        }

        public async Task<ProgrammingLanguageListModel> Handle(GetListProgrammingLanguageQuery request,
            CancellationToken cancellationToken)
        {
            var programmingLanguage = await _programmingLanguageRepository.GetListAsync(
                index: request.PageRequest.Page,
                size: request.PageRequest.PageSize
            );
            var mappedProgrammingLanguageListModel = _mapper.Map<ProgrammingLanguageListModel>(programmingLanguage);

            return mappedProgrammingLanguageListModel;
        }
    }
}