using Infrastructure.Models.Account;

namespace Infrastructure.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<ServiceResponse> LoginAsync(LoginVM model);
        public Task<ServiceResponse> ExternalLoginAsync(ExternalLoginVM model);
        public Task<ServiceResponse> RegisterAsync(RegisterVM model);
    }
}
