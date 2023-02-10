using DAL.Entities.Identity;
using Google.Apis.Auth;
using Infrastructure.Models.Account;

namespace Infrastructure.Services.Interfaces
{
    public interface IJwtTokenService
    {
        Task<string> CreateToken(UserEntity user);
        Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(ExternalLoginVm request);
    }
}
