using MediatR;

namespace _02_Application.Layer.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandRequest : IRequest<LoginUserCommandResponse>
    {
        public string uname { get; set; }
        public string password { get; set; }
    }
}
