using Infrastructure.Models.Account;

namespace Infrastructure.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<ServiceResponse> LoginAsync(LoginVm model);
        public Task<ServiceResponse> ExternalLoginAsync(ExternalLoginVm model);
        public Task<ServiceResponse> RegisterAsync(RegisterVm model);
    }
}
