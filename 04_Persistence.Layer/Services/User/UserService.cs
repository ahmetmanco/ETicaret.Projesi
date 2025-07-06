using _01_Domain.Layer.Entities;
using _02_Application.Layer.Abstraction;
using _02_Application.Layer.Abstraction.JWT;
using _02_Application.Layer.Abstraction.Services.UserService;
using _02_Application.Layer.DTO_s.Users;
using _02_Application.Layer.DTO_s.Users.GoogleLogin;
using _02_Application.Layer.DTO_s.Users.LoginUser;
using _02_Application.Layer.Exceptions;
using Azure.Core;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using static _02_Application.Layer.DTO_s.Users.LoginUser.LoginUserResponse;

namespace _04_Persistence.Layer.Services.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenHandler _tokenHandler;
        private readonly SignInManager<_01_Domain.Layer.Entities.AppUser> _signInManager;

        public UserService(UserManager<AppUser> userManager, ITokenHandler tokenHandler, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _signInManager = signInManager;
        }
        #region
        //async Task<Token> CreateUserExternalAsync(AppUser user, string email, string name,UserLoginInfo info, int accessTokenLifeTime)
        //{
        //    bool result = user != null;
        //    if(user == null)
        //    {
        //        user = await _userManager.FindByEmailAsync(email);
        //        if(user = null)
        //        {
        //            user = new()
        //            {
        //                Id = Guid.NewGuid().ToString(),
        //                Email = email,
        //                UserName = email,
        //                FirstName = email,
        //                LastName = email,
        //            };
        //            var identityResult = await _userManager.CreateAsync(user);
        //            result = identityResult.Succeeded;
        //        }

        //    }
        //    if (result)
        //    {
        //        await _userManager.AddLoginAsync(user, info);
        //        Token token = _tokenHandler.CreatedAccessToken(accessTokenLifeTime);
        //        return token;
        //    }
        //    throw new Exception("Invalid external authentication.");
        //}
        #endregion
        public async Task<CreateUserResponse> CreateAsync(CreateUser model)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.uname,
                Email = model.email,
            }, model.password);
            if (result.Succeeded)
            {
                return new()
                {
                    Message = " Kullanıcı başarıyla yüklendi",
                    Succeeded = true
                };
            }
            else
                return new()
                {
                    Succeeded = false,
                };
        }

        public async Task<GoogleLoginResponse> GoogleLoginAsync(GoogleLogin model)
        {
            if (string.IsNullOrEmpty(model.IdToken))
                throw new Exception("Geçersiz token");

            var payload = await ValidateGoogleToken(model.IdToken);

            var user = await FindOrCreate(payload);

            await AddExternalLogin(user, payload);
            var token = _tokenHandler.CreatedAccessToken(5);

            return new GoogleLoginResponse { Token = token };
        }


        private async Task<_01_Domain.Layer.Entities.AppUser> FindOrCreate(GoogleJsonWebSignature.Payload payload)
        {
            var user = await _userManager.FindByEmailAsync(payload.Email);
            if (user == null)
            {
                user = new()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = payload.Email,
                    UserName = payload.Email,
                    FirstName = payload.GivenName,
                    LastName = payload.FamilyName,
                    EmailConfirmed = true
                };
                var createResult = await _userManager.CreateAsync(user);
                if (!createResult.Succeeded)
                    throw new Exception($"Kullanıcı oluşturulamadı : {string.Join(", ", createResult.Errors)}");
            }
            return user;
        }

        private async Task AddExternalLogin(_01_Domain.Layer.Entities.AppUser appUser, GoogleJsonWebSignature.Payload payload)
        {
            var userLoginInfo = new UserLoginInfo(
                loginProvider: "Google",
                providerKey: payload.Subject,
                displayName: payload.Name
                );
            var result = await _userManager.AddLoginAsync(appUser, userLoginInfo);
            if (!result.Succeeded)
                throw new Exception($"External login eklenemedi: {string.Join(", ", result.Errors)}");
        }

        private async Task<GoogleJsonWebSignature.Payload> ValidateGoogleToken(string idToken)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new List<string> { "785983913023-c98fbq70qm7e9h2sa4ml82bhs00cl6c2.apps.googleusercontent.com" }
            };

            try
            {
                return await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
            }
            catch (InvalidJwtException e)
            {
                throw new Exception("Geçersiz Google Token ", e);
            }
        }

        public async Task<LoginUserResponse> LoginUserAsync(LoginUser model)
        {
            _01_Domain.Layer.Entities.AppUser user = await _userManager.FindByNameAsync(model.uname);
            if (user == null)
                user = await _userManager.FindByEmailAsync(model.uname);

            if (user == null)
                throw new NotFoundUserException();

            if (user != null)
            {
                SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, model.password, false);
                if (result.Succeeded) //Authentication başarılı
                {
                    //Yetkilendirme 
                    Token token = _tokenHandler.CreatedAccessToken(5);
                    return new LoginUserSuccessResponse()
                    {
                       Token = token,
                    };
                }
            }
            throw new AuthenticationErrorException();
        }
    }
}
