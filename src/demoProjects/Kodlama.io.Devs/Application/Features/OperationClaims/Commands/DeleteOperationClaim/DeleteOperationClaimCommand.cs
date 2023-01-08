using Application.Features.OperationClaims.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.OperationClaims.Commands.DeleteOperationClaim;

public class DeleteOperationClaimCommand : IRequest<DeletedOperationClaimDto>
{
    public string Id { get; set; }
    public string Name { get; set; }

    public class
        DeleteOperationClaimCommandHandler : IRequestHandler<DeleteOperationClaimCommand, DeletedOperationClaimDto>
    {
        private readonly IMapper _mapper;
        private readonly IOperationClaimRepository _operationClaimRepository;

        public DeleteOperationClaimCommandHandler(IMapper mapper,
            IOperationClaimRepository operationClaimRepository)
        {
            _mapper = mapper;
            _operationClaimRepository = operationClaimRepository;
        }

        public async Task<DeletedOperationClaimDto> Handle(DeleteOperationClaimCommand request,
            CancellationToken cancellationToken)
        {
            OperationClaim mappedOperationClaim = _mapper.Map<OperationClaim>(request);
            OperationClaim createdOperationClaim = await _operationClaimRepository.AddAsync(mappedOperationClaim);
            DeletedOperationClaimDto createdOperationClaimDto =
                _mapper.Map<DeletedOperationClaimDto>(createdOperationClaim);

            return createdOperationClaimDto;
        }
    }
}