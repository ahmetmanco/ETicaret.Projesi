using _02_Application.Layer.Abstraction;
using _02_Application.Layer.Abstraction.JWT;
using _02_Application.Layer.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace _02_Application.Layer.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly UserManager<_01_Domain.Layer.Entities.AppUser> _userManager;
        private readonly SignInManager<_01_Domain.Layer.Entities.AppUser> _signInManager;
        private readonly ITokenHandler _tokenHandler;
        public LoginUserCommandHandler(UserManager<_01_Domain.Layer.Entities.AppUser> userManager, SignInManager<_01_Domain.Layer.Entities.AppUser> signInManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            _01_Domain.Layer.Entities.AppUser user = await _userManager.FindByNameAsync(request.uname);
            if (user == null)
                user = await _userManager.FindByEmailAsync(request.uname);

            if (user == null) throw new NotFoundUserException();
            if (user != null)
            {
                SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.password, false);
                if (result.Succeeded) //Authentication başarılı
                {
                    //Yetkilendirme 
                    Token token = _tokenHandler.CreatedAccessToken(5);
                    return new LoginUserSuccessCommandResponse()
                    {
                        Token = token,
                    };
                }
            }
            return new LoginUserErrorCommandResponse()
            {
                Mesage = "Kullanıcı adi veya şifre hatalı"
            };
        }
    }
}
