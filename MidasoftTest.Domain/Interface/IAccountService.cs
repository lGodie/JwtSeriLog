using MidasoftTest.Common.Requests;
using MidasoftTest.Common.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MidasoftTest.Domain.Interface
{
    public interface IAccountService
    {
        Task<Response<UserTokenResponse>> CreateUser(UserLoginRequest userRequest);
        Task<Response<UserTokenResponse>> Login(TokenRequest tokenRequest);
    }
}
