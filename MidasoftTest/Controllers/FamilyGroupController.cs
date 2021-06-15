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
    public class FamilyGroupController : ControllerBase
    {
        private readonly IFamilyGroupService _familyGroupService;
        private IMapper _mapper;
        private readonly Serilog.ILogger _seriLogger;

        public FamilyGroupController(IFamilyGroupService familyGroupService,
            IMapper mapper,
            Serilog.ILogger seriLogger)
        {
            _familyGroupService = familyGroupService;
            _mapper = mapper;
            _seriLogger = seriLogger;
        }

        [HttpGet("GetAllDapper/")]
        public async Task<IActionResult> GetAllDapper()
        {
            try
            {
                Response<IEnumerable<FamilyGroup>> resp = await _familyGroupService.GetAllDapper();

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
        [HttpGet("GetByUserDapper/")]
        public async Task<IActionResult> GetByUserDapper([FromQuery] string userName)
        {
            try
            {
                Response<FamilyGroup> resp = await _familyGroupService.GetByUserName(userName);

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

        [HttpGet("All/")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                Response <List<FamilyGroup>> resp = await _familyGroupService.GetAll();

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

        [HttpPost()]
        public async Task<IActionResult> create(FamilyGroupRequest request)
        {
            

            try
            {
                FamilyGroup familyGroup= _mapper.Map<FamilyGroup>(request);

                Response<FamilyGroup> resp = await _familyGroupService.Create(familyGroup);

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

        [HttpGet("{FamilyGroupId}")]
        public async Task<ActionResult<FamilyGroupResponse>> GetProduct(Guid FamilyGroupId)
        {
            if (FamilyGroupId == Guid.Empty)
                return BadRequest("Invalid Id");
            try
            {

                Response<FamilyGroup> resp = await _familyGroupService.GetById(FamilyGroupId);

                FamilyGroup familyGroup = _mapper.Map<FamilyGroup>(resp.Result);
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
                return new BadRequestObjectResult(new { msg = "Error consultando el grupo familiar" });
            }

        }

        [HttpPut]
        public async Task<IActionResult> PutFamilyGroup(FamilyGroupRequest request)
        {
            if (Request == null)
                return BadRequest();
            try
            {
                FamilyGroup familyGroup = _mapper.Map<FamilyGroup>(request);

                Response<bool> resp = await _familyGroupService.Update(familyGroup);
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
                return new BadRequestObjectResult(new { msg = "Error actualizando el grupo familiar" });
            }
        }

        [HttpDelete("{FamilyGroupId}")]
        public async Task<IActionResult> Delete(Guid FamilyGroupId)
        {
            if (FamilyGroupId == Guid.Empty)
                return BadRequest("Invalid Id");
            try
            {
                Response<bool> resp = await _familyGroupService.Delete(FamilyGroupId);
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
