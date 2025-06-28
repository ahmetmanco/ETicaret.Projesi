using _02_Application.Layer.Abstraction;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace _02_Application.Layer.Features.Commands.AppUser.GoogleLogin
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {
        private readonly UserManager<_01_Domain.Layer.Entities.AppUser> _userManager;
        private readonly ITokenHandler _tokenHandler;
        private readonly IConfiguration _configuration;

        public GoogleLoginCommandHandler(UserManager<_01_Domain.Layer.Entities.AppUser> userManager, ITokenHandler tokenHandler, IConfiguration configuration)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _configuration = configuration;
        }

        public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.IdToken))
                throw new Exception("Geçersiz token");

            var payload = await ValidateGoogleToken(request.IdToken);

            var user = await FindOrCreate(payload);

            await AddExternalLogin(user, payload);
            var token = _tokenHandler.CreatedAccessToken(5);

            return new GoogleLoginCommandResponse { Token = token };
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
                if(!createResult.Succeeded)
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
            if(!result.Succeeded)
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
    }
}
