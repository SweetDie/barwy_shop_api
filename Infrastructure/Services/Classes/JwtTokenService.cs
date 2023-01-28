﻿using DAL.Entities.Identity;
using Google.Apis.Auth;
using Infrastructure.Models.Account;
using Infrastructure.Services.Interfaces;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services.Classes
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<UserEntity> _userManager;
        private readonly GoogleAuthSettings _googleAuthSettings;

        public JwtTokenService(IConfiguration configuration,
            UserManager<UserEntity> userManager,
            GoogleAuthSettings googleAuthSettings)
        {
            _configuration = configuration;
            _userManager = userManager;
            _googleAuthSettings = googleAuthSettings;
        }

        public async Task<string> CreateToken(UserEntity user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            List<Claim> claims = new List<Claim>()
            {
                new Claim("name", user.UserName)
            };

            foreach (var role in roles)
                claims.Add(new Claim("roles", role));

            var key = _configuration.GetValue<string>("JwtKey");
            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var signinCredentials = new SigningCredentials(signinKey,
                SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                signingCredentials: signinCredentials,
                expires: DateTime.Now.AddDays(10),
                claims: claims
                );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(ExternalLoginVM request)
        {
            string clientID = _googleAuthSettings.ClientId;
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>() { clientID }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(request.Token, settings);
            return payload;
        }
    }
}
