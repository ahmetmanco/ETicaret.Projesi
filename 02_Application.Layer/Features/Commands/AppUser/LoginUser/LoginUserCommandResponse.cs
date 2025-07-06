using _02_Application.Layer.Abstraction.JWT;

namespace _02_Application.Layer.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandResponse
    {
        
    }
    public class LoginUserSuccessCommandResponse : LoginUserCommandResponse 
    {
        public Token Token { get; set; }
    }

    public class LoginUserErrorCommandResponse : LoginUserCommandResponse
    {
        public string Message { get; set; }
    }
}
