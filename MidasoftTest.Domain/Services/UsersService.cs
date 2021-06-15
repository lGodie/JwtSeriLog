using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MidasoftTest.Common.Responses;
using MidasoftTest.Data.DataContext;
using MidasoftTest.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MidasoftTest.Domain.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<Users> _signInManager;

        public UsersService( UserManager<Users> userManager, RoleManager<IdentityRole> roleManager, SignInManager<Users> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        public async Task<Response<bool>> Delete(Guid RequestId)
        {
            Users exits = await _userManager.FindByIdAsync(RequestId.ToString());
            if (exits == null)
            {
                return new Response<bool>
                {
                    IsSuccess = false
                };
            }
            await _userManager.DeleteAsync(exits);

            return new Response<bool>
            {
                IsSuccess = true
            };
        }

        public async Task<Response<List<Users>>> GetAll()
        {
            List<Users> list = await _userManager.Users.ToListAsync();

            return new Response<List<Users>>
            {
                IsSuccess = true,
                Result = list
            };
        }

        public async Task<Response<Users>> GetById(Guid RequestId)
        {
            Users exits = await _userManager.FindByIdAsync(RequestId.ToString());
            if (exits == null)
            {
                return new Response<Users>
                {
                    IsSuccess = false
                };
            }
            return new Response<Users>
            {
                IsSuccess = true,
                Result = exits
            };
        }
        public async Task<Response<bool>> Update(Users entity)
        {
            await _userManager.UpdateAsync(entity);
            return new Response<bool>
            {
                IsSuccess = true
            };
        }

        public async Task<Response<Users>> Create(Users entity)
        {
            Users exits = await _userManager.FindByIdAsync(entity.Id.ToString());
           
            if (exits != null)
            {
                return new Response<Users>
                {
                    IsSuccess = false,
                    Message = "Ya se encuentra registrado"
                };
            }

             await _userManager.CreateAsync(entity);

            return new Response<Users>
            {
                IsSuccess = true,
                Message = "Exitoso"
            };
        }
    }
}
