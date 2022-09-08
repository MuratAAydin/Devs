using System.Net;
using Application.Features.Auths.Dtos;
using Application.Features.Auths.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;

namespace Application.Features.Auths.Commands.Login;

public class LoginCommand : IRequest<LoggedDto>
{
    public UserForLoginDto UserForLoginDto { get; set; }
    public string? IpAddress { get; set; }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoggedDto>
    {
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
        private readonly AuthBusinessRules _authBusinessRules;

        public LoginCommandHandler(IMapper mapper, IAuthService authService, IUserRepository userRepository,
            AuthBusinessRules authBusinessRules)
        {
            _mapper = mapper;
            _authService = authService;
            _userRepository = userRepository;
            _authBusinessRules = authBusinessRules;
        }

        public async Task<LoggedDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetAsync(e => e.Email == request.UserForLoginDto.Email);

            _authBusinessRules.UserShouldBeExists(user!);
            _authBusinessRules.CheckPassword(user!, request.UserForLoginDto.Password);

            AccessToken createdAccessToken = await _authService.GenerateAccessToken(user!);
            RefreshToken createdRefreshToken = await _authService.GenerateRefreshToken(user!, request.IpAddress!);
            LoggedDto loggedDto = new()
            {
                AccessToken = createdAccessToken,
                RefreshToken = createdRefreshToken
            };
            
            return loggedDto;
        }
    }
}