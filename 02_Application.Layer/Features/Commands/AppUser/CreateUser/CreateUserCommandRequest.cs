using MediatR;

namespace _02_Application.Layer.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandRequest : IRequest<CreateUserCommandResponse>
    {
        public string? uname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }
    }
}
//export class User
//{
//    uname!: string;
//  email!: string;
//  password!: string;
//  confirmPassword!: string;
//}