using _02_Application.Layer.Abstraction.JWT;

namespace _02_Application.Layer.DTO_s.Users.LoginUser
{
    public class LoginUserResponse
    {
        public LoginUserResponse()
        {
            
        }

        public class LoginUserSuccessResponse : LoginUserResponse
        {
            public Token Token { get; set; }
        }

        public class LoginUserErrorResponse : LoginUserResponse
        {
            public string Mesage { get; set; }
        }
    }
}
