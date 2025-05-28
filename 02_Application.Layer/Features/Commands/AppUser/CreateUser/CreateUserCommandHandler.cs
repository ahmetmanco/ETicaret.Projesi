using _02_Application.Layer.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace _02_Application.Layer.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        private readonly UserManager<_01_Domain.Layer.Entities.AppUser> _userManager;

        public CreateUserCommandHandler(UserManager<_01_Domain.Layer.Entities.AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = request.uname,
                Email = request.email,
            }, request.password);
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
    }
}
