using _01_Domain.Layer.Entities;
using _02_Application.Layer.Abstraction.JWT;
using _02_Application.Layer.DTO_s.Users;
using _02_Application.Layer.DTO_s.Users.GoogleLogin;
using _02_Application.Layer.DTO_s.Users.LoginUser;

namespace _02_Application.Layer.Abstraction.Services.UserService
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateAsync(CreateUser model);
        Task<GoogleLoginResponse> GoogleLoginAsync(GoogleLogin model);
        Task<LoginUserResponse> LoginUserAsync(LoginUser model);
        
    }
}
