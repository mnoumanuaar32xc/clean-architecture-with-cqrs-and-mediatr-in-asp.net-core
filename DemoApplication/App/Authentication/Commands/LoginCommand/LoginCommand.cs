using DemoApplication.App.Authentication.DTOs;
using DemoApplication.Common.Interfaces.Jwt;
using DemoApplication.Common.Interfaces.UnitOfWorks;
using DemoDomain.Entites.DemoApp.AppUserEntites;
using DemoDomain.Enums.DemoApp.BaseEnums;
using DemoDomain.Enums.DemoApp.Exception;
using DemoDomain.Exceptions;
using FluentValidation;
using MediatR;
using SharedModels;

namespace DemoApplication.App.Authentication.Commands.LoginCommand
{
    public class LoginCommand : IRequest<LoginResponseDTO>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Username).NotNull().NotEmpty();
            RuleFor(x => x.Password).NotNull().NotEmpty();
        }
    }
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDTO>
    {
        private readonly IMainUnitOfWork _mainUnitOfWork;

        private readonly IJwtService _jwtService;
        public LoginCommandHandler(IMainUnitOfWork mainUnitOfWork, IJwtService jwtService)
        {
            _mainUnitOfWork = mainUnitOfWork ?? throw new ArgumentNullException(nameof(mainUnitOfWork));
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
        }

        public async Task<LoginResponseDTO> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            command = command ?? throw new InvalidNullInputException(nameof(LoginCommand));

            bool _isValidUserCredentials = false;

            var _userLoginDetails = await _mainUnitOfWork.UserRepository.ValidateLoginUserAsync(command.Username, command.Password, cancellationToken: cancellationToken);

            if (_userLoginDetails != null && _userLoginDetails.Response == BaseEnumLoginStatus.ValidUserLogin.ToInt().ToString())

                _isValidUserCredentials = true;

            else if (_userLoginDetails == null)

                throw new InvalidCredentialException(EnumExceptionErrorMessages.AppUserNotExistInSystem.Value);

            else if (_userLoginDetails.Response == BaseEnumLoginStatus.WrongPassword.ToInt().ToString() || _userLoginDetails.Response == BaseEnumLoginStatus.WrongUsernameOrUserIsNotActive.ToInt().ToString())

                throw new InvalidCredentialException(EnumExceptionErrorMessages.AppuserInvalidCredentials.Value);

            if (_isValidUserCredentials)
            {
               // var jwt = await _jwtService.GenerateAppUserJwtAsync(_appUser, _appUsersTypes, _appUserIdentifiers, _appUserPermissions, cancellationToken);


            }
            return new LoginResponseDTO
            {
                //accessToken = jwt.access_token,
               // refreshToken = _refToken,
               // ExpiredOn = DateTime.Now.AddSeconds(jwt.expires_in),
               // RefreshTokenExpiredOn = _refTokenExpiryTime
            };
        }
    }

}
