﻿using Infrastructure;
using Infrastructure.Models.Account;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BarwyShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginVM model)
        {
            var result = await _accountService.LoginAsync(model);
            return SendResponse(result);
        }

        [HttpPost("externalLogin")]
        public async Task<IActionResult> ExternalLoginAsync([FromBody] ExternalLoginVM model)
        {
            var result = await _accountService.ExternalLoginAsync(model);
            return SendResponse(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterVM model)
        {
            var result = await _accountService.RegisterAsync(model);
            return SendResponse(result);
        }
        
        private IActionResult SendResponse(ServiceResponse response)
        {
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
