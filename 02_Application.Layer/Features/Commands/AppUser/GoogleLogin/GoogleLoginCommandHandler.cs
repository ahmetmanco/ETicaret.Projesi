using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _02_Application.Layer.Abstraction;
using _02_Application.Layer.Abstraction.JWT;
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

            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new List<string>
                {
                    "785983913023-c98fbq70qm7e9h2sa4ml82bhs00cl6c2.apps.googleusercontent.com",
                    "785983913023-c98fbq70qm7e9h2sa4ml82bhs00cl6c2.apps.googleusercontent.com" 
                }
            };

            // Token doğrulama
            var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, settings);

            var user = await _userManager.FindByEmailAsync(payload.Email);

            if (user == null)
            {
                user = new _01_Domain.Layer.Entities.AppUser
                {
                    Email = payload.Email,
                    UserName = payload.Email,
                    FirstName = payload.GivenName,
                    LastName = payload.FamilyName
                };

                var createResult = await _userManager.CreateAsync(user);
                if (!createResult.Succeeded)
                    throw new Exception("Kullanıcı oluşturulamadı");
            }

            // Token oluştur
            var token = _tokenHandler.CreatedAccessToken(5);

            return new GoogleLoginCommandResponse
            {
                Token = token
            };
        }
    }
}
