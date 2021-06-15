using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MidasoftTest.Common.Requests;
using MidasoftTest.Common.Responses;
using MidasoftTest.Data.DataContext;
using MidasoftTest.Domain.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MidasoftTest.Domain.Services
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        public AccountService(
            UserManager<Users> userManager,
            SignInManager<Users> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }


        public async Task<Response<UserTokenResponse>> CreateUser(UserLoginRequest userRequest)
        {
            var user = new Users { UserName = userRequest.Email, Email = userRequest.Email };
            var result = await _userManager.CreateAsync(user, userRequest.Password);
            await _userManager.AddToRoleAsync(user, "Admin");
            if (result.Succeeded)
            {
                return new Response<UserTokenResponse>
                {
                    IsSuccess = true,
                    Result = BuildToken(userRequest.Email, new List<string>())
                };
            }else
            {
                return new Response<UserTokenResponse>
                {
                    IsSuccess = false,
                    Message= "Username or password invalid"
                };
            }
        }

        public async Task<Response<UserTokenResponse>> Login(TokenRequest tokenRequest)
        {
            var result = await _signInManager.PasswordSignInAsync(tokenRequest.Email, tokenRequest.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var usuario = await _userManager.FindByEmailAsync(tokenRequest.Email);
                var roles = await _userManager.GetRolesAsync(usuario);
                return new Response<UserTokenResponse>
                {
                    Result = BuildToken(tokenRequest.Email, roles),
                    IsSuccess = true,
                };

            }
            else
            {
                return new Response<UserTokenResponse>
                {
                    IsSuccess = false,
                    Message = "Invalid login attempt."
                };

            }
        }

        private UserTokenResponse BuildToken(string email, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var rol in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, rol));
            }

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddYears(1);
            JwtSecurityToken token = new JwtSecurityToken(
                _configuration["Tokens:Issuer"],
                _configuration["Tokens:Audience"],
                claims,
                expires: expiration,
                signingCredentials: credentials);
            return new UserTokenResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
