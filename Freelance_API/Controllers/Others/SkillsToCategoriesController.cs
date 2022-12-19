using Admins_ApplicationService.Implementations;
using Authorisation.Configuration;
using Authorisation.DTOs;
using Base.Messages;
using Freelance_ApplicationService.DTOs.Bookmarks;
using Freelance_ApplicationService.DTOs.Others;
using Freelance_ApplicationService.Implementations.Bookmarks;
using Freelance_ApplicationService.Implementations.Others;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Users_ApplicationService.Implementations;

namespace Freelance_API.Controllers.Others
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class SkillsToCategoriesController : ControllerBase
    {
        private readonly RefreshUserTokenService _usersAuthentication;
        private readonly RefreshAdminTokenService _adminsAuthentication;
        private readonly TokenValidationParameters _tokenValidationParams;

        private BaseResponseMessage response;
        private TokenRequestDTO tokenRequest;

        public SkillsToCategoriesController(IOptionsMonitor<JwtConfig> optionsMonitor, TokenValidationParameters tokenValidationParams, IHttpContextAccessor httpContextAccessor)
        {
            _tokenValidationParams = tokenValidationParams;
            _usersAuthentication = new RefreshUserTokenService(optionsMonitor, _tokenValidationParams);
            _adminsAuthentication = new RefreshAdminTokenService(optionsMonitor, _tokenValidationParams);

            response = new BaseResponseMessage();
            tokenRequest = new TokenRequestDTO();
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll([FromHeader] string token, [FromHeader] string refreshToken)
        {
            if (token != null && refreshToken != null)
            {
                tokenRequest.Token = token;
                tokenRequest.RefreshToken = refreshToken;

                JwtResult jwtUserToken = await _usersAuthentication.RefreshUserToken(tokenRequest);
                JwtResult jwtAdminToken = await _adminsAuthentication.RefreshAdminToken(tokenRequest);

                if (jwtUserToken.JwtSuccess == true)
                {
                    response.Code = 201;

                    response.Body = await SkillsToCategoriesManagementService.GetAll();

                    HttpContext.Response.Headers.Add("token", jwtUserToken.Token);
                    HttpContext.Response.Headers.Add("refreshToken", jwtUserToken.RefreshToken);

                    response.JwtSuccess = jwtUserToken.JwtSuccess;
                    response.JwtErrors = jwtUserToken.JwtErrors;
                }
                else if (jwtAdminToken.JwtSuccess == true)
                {
                    response.Code = 201;

                    response.Body = await SkillsToCategoriesManagementService.GetAll();

                    HttpContext.Response.Headers.Add("token", jwtAdminToken.Token);
                    HttpContext.Response.Headers.Add("refreshToken", jwtAdminToken.RefreshToken);

                    response.JwtSuccess = jwtAdminToken.JwtSuccess;
                    response.JwtErrors = jwtAdminToken.JwtErrors;
                }
                else if (jwtUserToken.JwtSuccess != true)
                {
                    response.Code = 200;

                    response.JwtSuccess = jwtUserToken.JwtSuccess;
                    response.JwtErrors = jwtUserToken.JwtErrors;
                }
                else if (jwtAdminToken.JwtSuccess != true)
                {
                    response.Code = 200;

                    response.JwtSuccess = jwtAdminToken.JwtSuccess;
                    response.JwtErrors = jwtAdminToken.JwtErrors;
                }
            }
            else
            {
                response.Code = 400;
                response.Error = "Missing data - Token or refresh token is null or invalid!";
            }
            return new JsonResult(response);
        }

        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> Save(SkillToCategoryDTO skillToCategoryDTO, [FromHeader] string token, [FromHeader] string refreshToken)
        {
            if (skillToCategoryDTO == null || token == null || refreshToken == null)
            {
                response.Code = 400;
                response.Error = "Missing data - skillToCategory and/or token and/or refresh token is null or invalid!";

                return new JsonResult(response);
            }
            else
            {
                tokenRequest.Token = token;
                tokenRequest.RefreshToken = refreshToken;

                JwtResult jwtUserToken = await _usersAuthentication.RefreshUserToken(tokenRequest);
                JwtResult jwtAdminToken = await _adminsAuthentication.RefreshAdminToken(tokenRequest);

                if (jwtUserToken.JwtSuccess == true)
                {
                    response.Code = 201;

                    await SkillsToCategoriesManagementService.Save(skillToCategoryDTO);
                    response.Body = "skillToCategory has been saved!";

                    HttpContext.Response.Headers.Add("token", jwtUserToken.Token);
                    HttpContext.Response.Headers.Add("refreshToken", jwtUserToken.RefreshToken);

                    response.JwtSuccess = jwtUserToken.JwtSuccess;
                    response.JwtErrors = jwtUserToken.JwtErrors;
                }
                else if (jwtAdminToken.JwtSuccess == true)
                {
                    response.Code = 201;

                    await SkillsToCategoriesManagementService.Save(skillToCategoryDTO);
                    response.Body = "skillToCategory has been saved!";

                    HttpContext.Response.Headers.Add("token", jwtAdminToken.Token);
                    HttpContext.Response.Headers.Add("refreshToken", jwtAdminToken.RefreshToken);

                    response.JwtSuccess = jwtAdminToken.JwtSuccess;
                    response.JwtErrors = jwtAdminToken.JwtErrors;
                }
                else if (jwtUserToken.JwtSuccess != true)
                {
                    response.Code = 200;

                    response.JwtSuccess = jwtUserToken.JwtSuccess;
                    response.JwtErrors = jwtUserToken.JwtErrors;
                }
                else if (jwtAdminToken.JwtSuccess != true)
                {
                    response.Code = 200;

                    response.JwtSuccess = jwtAdminToken.JwtSuccess;
                    response.JwtErrors = jwtAdminToken.JwtErrors;
                }
            }

            return new JsonResult(response);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int id, [FromHeader] string token, [FromHeader] string refreshToken)
        {
            if (id == 0 || token == null || refreshToken == null)
            {
                response.Code = 400;
                response.Error = "Missing data - Id and/or token data is incorrect or null";

                return new JsonResult(response);
            }
            else
            {
                tokenRequest.Token = token;
                tokenRequest.RefreshToken = refreshToken;

                JwtResult jwtUserToken = await _usersAuthentication.RefreshUserToken(tokenRequest);
                JwtResult jwtAdminToken = await _adminsAuthentication.RefreshAdminToken(tokenRequest);

                if (jwtUserToken.JwtSuccess == true)
                {
                    response.Code = 201;

                    response.Body = await SkillsToCategoriesManagementService.GetById(id);

                    HttpContext.Response.Headers.Add("token", jwtUserToken.Token);
                    HttpContext.Response.Headers.Add("refreshToken", jwtUserToken.RefreshToken);

                    response.JwtSuccess = jwtUserToken.JwtSuccess;
                    response.JwtErrors = jwtUserToken.JwtErrors;
                }
                else if (jwtAdminToken.JwtSuccess == true)
                {
                    response.Code = 201;

                    response.Body = await SkillsToCategoriesManagementService.GetById(id);

                    HttpContext.Response.Headers.Add("token", jwtAdminToken.Token);
                    HttpContext.Response.Headers.Add("refreshToken", jwtAdminToken.RefreshToken);

                    response.JwtSuccess = jwtAdminToken.JwtSuccess;
                    response.JwtErrors = jwtAdminToken.JwtErrors;
                }
                else if (jwtUserToken.JwtSuccess != true)
                {
                    response.Code = 200;

                    response.JwtSuccess = jwtUserToken.JwtSuccess;
                    response.JwtErrors = jwtUserToken.JwtErrors;
                }
                else if (jwtAdminToken.JwtSuccess != true)
                {
                    response.Code = 200;

                    response.JwtSuccess = jwtAdminToken.JwtSuccess;
                    response.JwtErrors = jwtAdminToken.JwtErrors;
                }
                return new JsonResult(response);
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id, [FromHeader] string token, [FromHeader] string refreshToken)
        {
            if (id == 0 || token == null || refreshToken == null)
            {
                response.Code = 400;
                response.Error = "Missing data - Id and/or token data is incorrect or null";

                return new JsonResult(response);
            }
            else
            {
                tokenRequest.Token = token;
                tokenRequest.RefreshToken = refreshToken;

                JwtResult jwtUserToken = await _usersAuthentication.RefreshUserToken(tokenRequest);
                JwtResult jwtAdminToken = await _adminsAuthentication.RefreshAdminToken(tokenRequest);

                if (jwtUserToken.JwtSuccess == true)
                {
                    response.Code = 201;

                    await SkillsToCategoriesManagementService.Delete(id);
                    response.Body = "skillToCategory has been deleted!";

                    HttpContext.Response.Headers.Add("token", jwtUserToken.Token);
                    HttpContext.Response.Headers.Add("refreshToken", jwtUserToken.RefreshToken);

                    response.JwtSuccess = jwtUserToken.JwtSuccess;
                    response.JwtErrors = jwtUserToken.JwtErrors;
                }
                else if (jwtAdminToken.JwtSuccess == true)
                {
                    response.Code = 201;

                    await SkillsToCategoriesManagementService.Delete(id);
                    response.Body = "skillToCategory has been deleted!";

                    HttpContext.Response.Headers.Add("token", jwtAdminToken.Token);
                    HttpContext.Response.Headers.Add("refreshToken", jwtAdminToken.RefreshToken);

                    response.JwtSuccess = jwtAdminToken.JwtSuccess;
                    response.JwtErrors = jwtAdminToken.JwtErrors;
                }
                else if (jwtUserToken.JwtSuccess != true)
                {
                    response.Code = 200;

                    response.JwtSuccess = jwtUserToken.JwtSuccess;
                    response.JwtErrors = jwtUserToken.JwtErrors;
                }
                else if (jwtAdminToken.JwtSuccess != true)
                {
                    response.Code = 200;

                    response.JwtSuccess = jwtAdminToken.JwtSuccess;
                    response.JwtErrors = jwtAdminToken.JwtErrors;
                }
            }
            return new JsonResult(response);
        }
    }
}
