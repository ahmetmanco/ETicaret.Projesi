using _02_Application.Layer.Abstraction.Services.UserService;
using _02_Application.Layer.DTO_s.Users;
using MediatR;

namespace _02_Application.Layer.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        private readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
           CreateUserResponse response = await _userService.CreateAsync(new()
           {
               uname = request.uname,
               confirmPassword = request.confirmPassword,
               email = request.email,
               password = request.password
           });

            return new()
            {
                Message = response.Message,
                Succeeded = response.Succeeded,
            };
        }
    }
}
