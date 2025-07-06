using _02_Application.Layer.Abstraction.Services.UserService;
using _02_Application.Layer.DTO_s.Users.GoogleLogin;
using MediatR;

namespace _02_Application.Layer.Features.Commands.AppUser.GoogleLogin
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {
        private readonly IUserService _userService;

        public GoogleLoginCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {
            GoogleLoginResponse response = await _userService.GoogleLoginAsync(new()
            {
                IdToken = request.IdToken,
            });

            return new() { Token = response.Token };
        }
    }
}
