using Application.Features.Auths.Dtos;
using Application.Features.Auths.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;

namespace Application.Features.Auths.Commands.Register;

public class RegisterCommand : IRequest<RegisteredDto>
{
    public UserForRegisterDto? UserForRegisterDto { get; set; }
    public string? IpAddress { get; set; }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisteredDto>
    {
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
        private readonly AuthBusinessRules _authBusinessRules;

        public RegisterCommandHandler(IMapper mapper, IAuthService authService, IUserRepository userRepository,
            AuthBusinessRules authBusinessRules)
        {
            _mapper = mapper;
            _authService = authService;
            _userRepository = userRepository;
            _authBusinessRules = authBusinessRules;
        }

        public async Task<RegisteredDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await _authBusinessRules.UserEmailShouldBeNotExists(request.UserForRegisterDto!.Email);

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out passwordHash, out passwordSalt);

            User newUser = new()
            {
                Email = request.UserForRegisterDto.Email,
                FirstName = request.UserForRegisterDto.FirstName,
                LastName = request.UserForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            User createdUser = await _userRepository.AddAsync(newUser);
            AccessToken createdAccessToken = await _authService.GenerateAccessToken(createdUser);
            RefreshToken createdRefreshToken = await _authService.GenerateRefreshToken(createdUser, request.IpAddress!);
            RegisteredDto registeredDto = new()
                { AccessToken = createdAccessToken, RefreshToken = createdRefreshToken };

            return registeredDto;
        }
    }
}