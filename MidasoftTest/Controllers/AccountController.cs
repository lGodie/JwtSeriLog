using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using MidasoftTest.Common.Requests;
using MidasoftTest.Common.Responses;
using MidasoftTest.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidasoftTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
       
        private readonly IAccountService _accountService;
        private readonly Serilog.ILogger _seriLogger;

        public AccountController(IAccountService accountService, Serilog.ILogger seriLogger)
        {
            _accountService = accountService;
            _seriLogger = seriLogger;
        }
        /// <summary>
        /// Servicio para creación de usuario
        /// </summary>
        /// <returns></returns>
        [HttpPost("CreateUser")]
        public async Task<ActionResult<UserTokenResponse>> CreateUser([FromBody] UserLoginRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Response<UserTokenResponse> resp = await _accountService.CreateUser(request);

                if (resp.IsSuccess == true)
                {
                    _seriLogger.Information($"Proceso exitoso Request: {Request.Method}-->{Request.Path} ");
                    return StatusCode(200, resp);
                }
                _seriLogger.Error(resp.Message);
                return StatusCode(400, resp);

            }
            catch (Exception ex)
            {
                SaveLog(ex);
                return new BadRequestObjectResult(new { msg = "Error creando el grupo familiar" });
            }

        }
        /// <summary>
        /// Servicio para login de usuario
        /// </summary>
        /// <returns></returns>
        [HttpGet("Login")]
        public async Task<ActionResult<UserTokenResponse>> Login([FromBody] TokenRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Response<UserTokenResponse> resp = await _accountService.Login(request);

                if (resp.IsSuccess == true)
                {
                    _seriLogger.Information($"Proceso exitoso Request: {Request.Method}-->{Request.Path} ");
                    return StatusCode(200, resp);
                }
                _seriLogger.Error(resp.Message);
                return StatusCode(400, resp);
            }
            catch (Exception ex)
            {
                SaveLog(ex);
                return new BadRequestObjectResult(new { msg = "Error creando el grupo familiar" });
            }
        }
        private void SaveLog(Exception ex)
        {
            string[] url = UriHelper.GetDisplayUrl(Request).Split('/');
            string domain = string.Join('/', url.SkipLast(1));
            var sb = new StringBuilder();
            sb.AppendLine($"Error message:{ex.Message} /n");
            sb.AppendLine($"Error stack trace:{ex.StackTrace}");
            sb.AppendLine($"Url Request: {Request.Method}--> {domain}");
            _seriLogger.Error(sb.ToString());
        }
    }
}
