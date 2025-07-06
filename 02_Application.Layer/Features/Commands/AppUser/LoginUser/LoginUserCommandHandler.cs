using _02_Application.Layer.Abstraction;
using _02_Application.Layer.Abstraction.JWT;
using _02_Application.Layer.Abstraction.Services.UserService;
using _02_Application.Layer.DTO_s.Users.LoginUser;
using _02_Application.Layer.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using static _02_Application.Layer.DTO_s.Users.LoginUser.LoginUserResponse;

namespace _02_Application.Layer.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {

        private readonly IUserService _userService;
        public LoginUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            LoginUserResponse response = await _userService.LoginUserAsync(new()
            {
                uname = request.uname,
                password = request.password
            });

            if (response is LoginUserSuccessResponse successResponse)
            {
                return new LoginUserSuccessCommandResponse()
                {
                    Token = successResponse.Token
                };
            }
            else if (response is LoginUserErrorResponse errorResponse)
            {
                return new LoginUserErrorCommandResponse()
                {
                    Message = errorResponse.Mesage
                };
            }
            throw new AuthenticationErrorException();
        }
    }
}
