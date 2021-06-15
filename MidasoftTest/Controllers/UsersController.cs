using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MidasoftTest.Common.Requests;
using MidasoftTest.Common.Responses;
using MidasoftTest.Data.DataContext;
using MidasoftTest.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidasoftTest.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private IMapper _mapper;
        private readonly Serilog.ILogger _seriLogger;

        public UsersController(IUsersService usersService,
            IMapper mapper,
            Serilog.ILogger seriLogger)
        {
            _usersService = usersService;
            _mapper = mapper;
            _seriLogger = seriLogger;
        }

        [HttpGet("All/")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                Response <List<Users>> resp = await _usersService.GetAll();
                IEnumerable<UsersResponse> users = _mapper.Map<IEnumerable<UsersResponse>>(resp.Result);

                if (resp.IsSuccess == true)
                {
                    _seriLogger.Information($"Proceso exitoso Request: {Request.Method}-->{Request.Path} ");
                    return StatusCode(200, new Response<IEnumerable<UsersResponse>> { Message= resp.Message, IsSuccess = resp.IsSuccess,Result= users });
                }
                _seriLogger.Error(resp.Message);
                return StatusCode(400, resp);
            }
            catch (Exception ex)
            {
                _seriLogger.Error(ex.Message);            

                return new BadRequestObjectResult(new
                {
                    msg = ex.Message
                });
            }
        }

        [HttpPost()]
        public async Task<IActionResult> Create(UserRequest request)
        {           

            try
            {
                Users User = _mapper.Map<Users>(request);

                Response<Users> resp = await _usersService.Create(User);

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
                return new BadRequestObjectResult(new { msg = "Error creando el usuario" });

            }
        }

        [HttpGet("{UserId}")]
        public async Task<ActionResult<UsersResponse>> Get(Guid UserId)
        {
            if (UserId == Guid.Empty)
            {
                _seriLogger.Error("Invalid Id");
                return StatusCode(400, new Response<object> { Message = "Invalid Id", IsSuccess = false });
            }
            try
            {

                Response<Users> resp = await _usersService.GetById(UserId);

                UsersResponse users= _mapper.Map<UsersResponse>(resp.Result);
                if (resp.IsSuccess == true)
                {
                    _seriLogger.Information($"Proceso exitoso Request: {Request.Method}-->{Request.Path} ");
                    return StatusCode(200, new Response<UsersResponse> { Message = resp.Message, IsSuccess = resp.IsSuccess, Result = users });
                }
                _seriLogger.Error(resp.Message);
                return StatusCode(400, resp);

            }
            catch (Exception ex)
            {
                SaveLog(ex);
                return new BadRequestObjectResult(new { msg = "Error consultando el usuario" });
            }

        }

        [HttpPut]
        public async Task<IActionResult> Put(UserRequest request)
        {
            if (Request == null)
            {
                _seriLogger.Error("not object found");
                return StatusCode(400, new Response<object> { IsSuccess = false });
            }
                
            try
            {
                
                    Users User = _mapper.Map<Users>(request);

                    Response<bool> resp = await _usersService.Update(User);
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
                return new BadRequestObjectResult(new { msg = "Error actualizando el usuario" });
            }
        }

        [HttpDelete("{UserId}")]
        public async Task<IActionResult> Delete(Guid UserId)
        {
            if (UserId == Guid.Empty)
            {
                _seriLogger.Error("Invalid Id");
                return StatusCode(400, new Response<object> { Message = "Invalid Id", IsSuccess = false });
            }                

            try
            {
                Response<bool> resp = await _usersService.Delete(UserId);
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
                return new BadRequestObjectResult(new { msg = "Error eliminando el grupo familiar" });
            }
        }

        private void SaveLog(Exception ex)
        {
            string[] url = UriHelper.GetDisplayUrl(Request).Split('/');
            string  domain = string.Join('/', url.SkipLast(1));
            var sb = new StringBuilder();
            sb.AppendLine($"Error message:{ex.Message} /n");
            sb.AppendLine($"Error stack trace:{ex.StackTrace}");
            sb.AppendLine($"Url Request: {Request.Method}--> {domain}");
            _seriLogger.Error(sb.ToString());
        }
    }
}
