using DAL.Entities.Identity;
using Infrastructure.Constants;
using Infrastructure.Models.Account;
using Infrastructure.Services.Interfaces;
using Infrastructure.Validation.Account;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services.Classes
{
    public class AccountService : IAccountService
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly UserManager<UserEntity> _userManager;

        public AccountService(UserManager<UserEntity> userManager, IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<ServiceResponse> ExternalLoginAsync(ExternalLoginVm model)
        {
            var validator = new ExternalLoginValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                return new ServiceResponse
                {
                    IsSuccess = false,
                    Message = "Щось пішло не так!",
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage)
                };
            }

            try
            {
                var payload = await _jwtTokenService.VerifyGoogleToken(model);

                if (payload == null)
                {
                    return new ServiceResponse
                    {
                        IsSuccess = false,
                        Message = "Щось пішло не так!"
                    };
                }

                var info = new UserLoginInfo(model.Provider, payload.Subject, model.Provider);
                var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

                if (user == null)
                {
                    user = await _userManager.FindByEmailAsync(payload.Email);
                    if (user == null)
                    {
                        user = new UserEntity
                        {
                            Email = payload.Email,
                            UserName = payload.Email,
                            FirstName = payload.GivenName,
                            LastName = payload.FamilyName
                        };
                        var resultCreate = await _userManager.CreateAsync(user);

                        if (!resultCreate.Succeeded)
                        {
                            return new ServiceResponse
                            {
                                IsSuccess = false,
                                Message = "Помилка створення користувача",
                                Errors = resultCreate.Errors.Select(e => e.Description)
                            };
                        }
                    }

                    var resultLogin = await _userManager.AddLoginAsync(user, info);

                    if (!resultLogin.Succeeded)
                    {
                        return new ServiceResponse
                        {
                            IsSuccess = false,
                            Message = "Створення входу через гугл"
                        };
                    }
                }
                var token = await _jwtTokenService.CreateToken(user);
                return new ServiceResponse
                {
                    IsSuccess = true,
                    Payload = token,
                    Message = "Успішний вхід"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ServiceResponse> LoginAsync(LoginVm model)
        {
            var validator = new LoginValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                return new ServiceResponse
                {
                    IsSuccess = false,
                    Message = "Дані вказано не вірно",
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage)
                };
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new ServiceResponse
                {
                    IsSuccess = false,
                    Message = "Дані вказано не вірно"
                };
            }
            var checkPassword = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!checkPassword)
            {
                return new ServiceResponse
                {
                    IsSuccess = false,
                    Message = "Дані вказано не вірно"
                };
            }
            var token = await _jwtTokenService.CreateToken(user);
            return new ServiceResponse
            {
                IsSuccess = true,
                Payload = token,
                Message = "Успішний вхід"
            };
        }

        public async Task<ServiceResponse> RegisterAsync(RegisterVm model)
        {
            var validator = new RegisterValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                return new ServiceResponse
                {
                    IsSuccess = false,
                    Message = "Дані вказано не вірно",
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage)
                };
            }

            var userExist = await _userManager.FindByEmailAsync(model.Email);
            if (userExist != null)
            {
                return new ServiceResponse
                {
                    IsSuccess = false,
                    Message = "Такий користувач вже зареєстрований"
                };
            }

            var newUser = new UserEntity
            {
                Email = model.Email,
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var resultCreate = await _userManager.CreateAsync(newUser, model.Password);
            if (!resultCreate.Succeeded)
            {
                return new ServiceResponse
                {
                    IsSuccess = false,
                    Message = "Помилка реєстрації",
                    Errors = resultCreate.Errors.Select(e => e.Description)
                };
            }
            var resultRole = await _userManager.AddToRoleAsync(newUser, Roles.User);
            if (!resultRole.Succeeded)
            {
                return new ServiceResponse
                {
                    IsSuccess = false,
                    Message = "Помилка реєстрації",
                    Errors = resultRole.Errors.Select(e => e.Description)
                };
            }

            return new ServiceResponse
            {
                IsSuccess = true,
                Message = "Успішна реєстрація"
            };
        }
    }
}