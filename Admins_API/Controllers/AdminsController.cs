﻿using Admins_ApplicationService.DTOs;
using Admins_ApplicationService.Implementations;
using Authorisation.Configuration;
using Authorisation.DTOs;
using Base.Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Admins_API.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly RefreshAdminTokenService _authentication;
        private readonly TokenValidationParameters _tokenValidationParams;

        private BaseResponseMessage response;
        private TokenRequestDTO tokenRequest;

        public AdminsController(IOptionsMonitor<JwtConfig> optionsMonitor, TokenValidationParameters tokenValidationParams, IHttpContextAccessor httpContextAccessor)
        {
            _tokenValidationParams = tokenValidationParams;
            _authentication = new RefreshAdminTokenService(optionsMonitor, _tokenValidationParams);

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

                JwtResult jwtAdminToken = await _authentication.RefreshAdminToken(tokenRequest);

                if (jwtAdminToken.JwtSuccess == true)
                {
                    response.Code = 201;

                    response.Body = await AdminsManagementService.GetAll();

                    HttpContext.Response.Headers.Add("token", jwtAdminToken.Token);
                    HttpContext.Response.Headers.Add("refreshToken", jwtAdminToken.RefreshToken);

                    response.JwtSuccess = jwtAdminToken.JwtSuccess;
                    response.JwtErrors = jwtAdminToken.JwtErrors;
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

                JwtResult jwtAdminToken = await _authentication.RefreshAdminToken(tokenRequest);

                if (jwtAdminToken.JwtSuccess == true)
                {
                    response.Code = 201;

                    response.Body = await AdminsManagementService.GetById(id);

                    HttpContext.Response.Headers.Add("token", jwtAdminToken.Token);
                    HttpContext.Response.Headers.Add("refreshToken", jwtAdminToken.RefreshToken);

                    response.JwtSuccess = jwtAdminToken.JwtSuccess;
                    response.JwtErrors = jwtAdminToken.JwtErrors;
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

        [HttpGet]
        [Route("Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            BaseResponseMessage response = new BaseResponseMessage();

            if (email == null || password == null)
            {
                response.Code = 400;
                response.Error = "Missing data - Email or password is empty!";

                return new JsonResult(response);
            }
            else
            {
                List<AdminDTO> admins = await AdminsManagementService.GetAll();

                AdminDTO? admin = admins
                    .Where(a => a.Email == email && a.Password == password)
                    .FirstOrDefault();

                if (admin == null)
                {
                    response.Code = 200;
                    response.Error = "Missing data - Admin does not exist.";
                }
                else
                {
                    AdminDTO adminDTO = new AdminDTO
                    {
                        Id = admin.Id,
                        FirstName = admin.FirstName,
                        LastName = admin.LastName,
                        Email = email,
                        Password = password
                    };

                    await _authentication.ClearAllAdminTokens(adminDTO);
                    JwtResult jwtAdminToken = await _authentication.GenerateAdminJwtToken(adminDTO);

                    HttpContext.Response.Headers.Add("token", jwtAdminToken.Token);
                    HttpContext.Response.Headers.Add("refreshToken", jwtAdminToken.RefreshToken);

                    response.Code = 201;
                    response.Body = adminDTO;
                    response.JwtSuccess = jwtAdminToken.JwtSuccess;
                    response.JwtErrors = jwtAdminToken.JwtErrors;
                }
            }

            return new JsonResult(response);
        }

        [HttpPost]
        [Route("SignUp")]
        public async Task<IActionResult> SignUp(AdminDTO adminDTO)
        {
            BaseResponseMessage response = new BaseResponseMessage();

            if (adminDTO.FirstName == null || adminDTO.LastName == null
                || adminDTO.Email == null || adminDTO.Password == null ||
                AdminsManagementService.VerifyEmail(adminDTO.Email).Equals(true))
            {
                response.Code = 400;
                response.Error = "Missing data - Admin data is incomplete or admin email Already Exists!";

                return new JsonResult(response);
            }
            else
            {
                await AdminsManagementService.Save(adminDTO);
                response.Code = 201;
                response.Body = "Admin has been signed up!";
            }

            return new JsonResult(response);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update(AdminDTO adminDTO, [FromHeader] string token, [FromHeader] string refreshToken)
        {
            BaseResponseMessage response = new BaseResponseMessage();

            if (token == null || refreshToken == null)
            {
                response.Code = 400;
                response.Error = "Missing data - Id and/or token data is incorrect or null";

                return new JsonResult(response);
            }
            else
            {
                if (adminDTO.FirstName == null || adminDTO.LastName == null
                || adminDTO.Email == null || AdminsManagementService.VerifyEmail(adminDTO.Email).Equals(true))
                {
                    response.Code = 400;
                    response.Error = "Missing data - Admin data is incomplete or admin email Already Exists!";

                    return new JsonResult(response);
                }
                else
                {
                    tokenRequest.Token = token;
                    tokenRequest.RefreshToken = refreshToken;

                    JwtResult jwtAdminToken = await _authentication.RefreshAdminToken(tokenRequest);

                    if (jwtAdminToken.JwtSuccess == true)
                    {
                        await AdminsManagementService.Save(adminDTO);
                        response.Code = 201;
                        response.Body = "Admin has been saved!";

                        HttpContext.Response.Headers.Add("token", jwtAdminToken.Token);
                        HttpContext.Response.Headers.Add("refreshToken", jwtAdminToken.RefreshToken);
                    }
                    else
                    {
                        response.Code = 201;
                    }

                    response.JwtSuccess = jwtAdminToken.JwtSuccess;
                    response.JwtErrors = jwtAdminToken.JwtErrors;
                }
            }

            return new JsonResult(response);
        }

        [HttpGet]
        [Route("Logout")]
        public async Task<IActionResult> Logout(int userId)
        {
            AdminDTO admin = await AdminsManagementService.GetById(userId);

            await _authentication.ClearAllAdminTokens(admin);

            if (await _authentication.GetAdminTokensCount(admin) == 0)
            {
                response.Code = 201;
            }
            else
            {
                response.Code = 200;

                response.Error = "Something happend!";
            }

            return new JsonResult(response);
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

                JwtResult jwtAdminToken = await _authentication.RefreshAdminToken(tokenRequest);

                if (jwtAdminToken.JwtSuccess == true)
                {
                    response.Code = 201;

                    await AdminsManagementService.Delete(id);
                    response.Body = "Admin has been deleted!";

                    HttpContext.Response.Headers.Add("token", jwtAdminToken.Token);
                    HttpContext.Response.Headers.Add("refreshToken", jwtAdminToken.RefreshToken);
                }
                else
                {
                    response.Code = 200;
                }

                response.JwtSuccess = jwtAdminToken.JwtSuccess;
                response.JwtErrors = jwtAdminToken.JwtErrors;
            }

            return new JsonResult(response);
        }

        [HttpPost]
		[Route("FillCountriesRegionsAndCitiesActionMSSQL")]
		public async Task<IActionResult> FillCountriesRegionsAndCitiesActionMSSQL([FromHeader] string token, [FromHeader] string refreshToken)
        {
			if (token == null || refreshToken == null)
			{
				response.Code = 400;
				response.Error = "Missing data - token data is incorrect or null";

				return new JsonResult(response);
			}
			else
			{
				tokenRequest.Token = token;
				tokenRequest.RefreshToken = refreshToken;

				JwtResult jwtAdminToken = await _authentication.RefreshAdminToken(tokenRequest);

				if (jwtAdminToken.JwtSuccess == true)
				{
					response.Code = 201;

					int result = await AdminsManagementService.FillCountriesRegionsAndCitiesActionMSSQL();
                    if (result == 1)
                    {
						response.Body = "Success";
					}
                    else
                    {
                        response.Body = "Failiure";
                    }

					HttpContext.Response.Headers.Add("token", jwtAdminToken.Token);
					HttpContext.Response.Headers.Add("refreshToken", jwtAdminToken.RefreshToken);
				}
				else
				{
					response.Code = 200;
				}

				response.JwtSuccess = jwtAdminToken.JwtSuccess;
				response.JwtErrors = jwtAdminToken.JwtErrors;
			}

			return new JsonResult(response);
		}

		[HttpPost]
		[Route("FillCountriesRegionsAndCitiesActionPostgreSQL")]
		public async Task<IActionResult> FillCountriesRegionsAndCitiesActionPostgreSQL([FromHeader] string token, [FromHeader] string refreshToken)
		{
			if (token == null || refreshToken == null)
			{
				response.Code = 400;
				response.Error = "Missing data - token data is incorrect or null";

				return new JsonResult(response);
			}
			else
			{
				tokenRequest.Token = token;
				tokenRequest.RefreshToken = refreshToken;

				JwtResult jwtAdminToken = await _authentication.RefreshAdminToken(tokenRequest);

				if (jwtAdminToken.JwtSuccess == true)
				{
					response.Code = 201;

					int result = await AdminsManagementService.FillCountriesRegionsAndCitiesActionPostgreSQL();
					if (result == 1)
					{
						response.Body = "Success";
					}
					else
					{
						response.Body = "Failiure";
					}

					HttpContext.Response.Headers.Add("token", jwtAdminToken.Token);
					HttpContext.Response.Headers.Add("refreshToken", jwtAdminToken.RefreshToken);
				}
				else
				{
					response.Code = 200;
				}

				response.JwtSuccess = jwtAdminToken.JwtSuccess;
				response.JwtErrors = jwtAdminToken.JwtErrors;
			}

			return new JsonResult(response);
		}

        [HttpPost]
        [Route("FillCategoriesActionMSSQL")]
        public async Task<IActionResult> FillCategoriesActionMSSQL([FromHeader] string token, [FromHeader] string refreshToken)
        {
            if (token == null || refreshToken == null)
            {
                response.Code = 400;
                response.Error = "Missing data - token data is incorrect or null";

                return new JsonResult(response);
            }
            else
            {
                tokenRequest.Token = token;
                tokenRequest.RefreshToken = refreshToken;

                JwtResult jwtAdminToken = await _authentication.RefreshAdminToken(tokenRequest);

                if (jwtAdminToken.JwtSuccess == true)
                {
                    response.Code = 201;

                    int result = await AdminsManagementService.FillCategoriesActionMSSQL();
                    if (result == 1)
                    {
                        response.Body = "Success";
                    }
                    else
                    {
                        response.Body = "Failiure";
                    }

                    HttpContext.Response.Headers.Add("token", jwtAdminToken.Token);
                    HttpContext.Response.Headers.Add("refreshToken", jwtAdminToken.RefreshToken);
                }
                else
                {
                    response.Code = 200;
                }

                response.JwtSuccess = jwtAdminToken.JwtSuccess;
                response.JwtErrors = jwtAdminToken.JwtErrors;
            }

            return new JsonResult(response);
        }

        [HttpPost]
        [Route("FillCategoriesActionPostgreSQL")]
        public async Task<IActionResult> FillCategoriesActionPostgreSQL([FromHeader] string token, [FromHeader] string refreshToken)
        {
            if (token == null || refreshToken == null)
            {
                response.Code = 400;
                response.Error = "Missing data - token data is incorrect or null";

                return new JsonResult(response);
            }
            else
            {
                tokenRequest.Token = token;
                tokenRequest.RefreshToken = refreshToken;

                JwtResult jwtAdminToken = await _authentication.RefreshAdminToken(tokenRequest);

                if (jwtAdminToken.JwtSuccess == true)
                {
                    response.Code = 201;

                    int result = await AdminsManagementService.FillCategoriesActionPostgreSQL();
                    if (result == 1)
                    {
                        response.Body = "Success";
                    }
                    else
                    {
                        response.Body = "Failiure";
                    }

                    HttpContext.Response.Headers.Add("token", jwtAdminToken.Token);
                    HttpContext.Response.Headers.Add("refreshToken", jwtAdminToken.RefreshToken);
                }
                else
                {
                    response.Code = 200;
                }

                response.JwtSuccess = jwtAdminToken.JwtSuccess;
                response.JwtErrors = jwtAdminToken.JwtErrors;
            }

            return new JsonResult(response);
        }

        [HttpPost]
        [Route("FillPricingPlansAndFeaturesActionMSSQL")]
        public async Task<IActionResult> FillPricingPlansAndFeaturesActionMSSQL([FromHeader] string token, [FromHeader] string refreshToken)
        {
            if (token == null || refreshToken == null)
            {
                response.Code = 400;
                response.Error = "Missing data - token data is incorrect or null";

                return new JsonResult(response);
            }
            else
            {
                tokenRequest.Token = token;
                tokenRequest.RefreshToken = refreshToken;

                JwtResult jwtAdminToken = await _authentication.RefreshAdminToken(tokenRequest);

                if (jwtAdminToken.JwtSuccess == true)
                {
                    response.Code = 201;

                    int result = await AdminsManagementService.FillPricingPlansAndFeaturesActionMSSQL();
                    if (result == 1)
                    {
                        response.Body = "Success";
                    }
                    else
                    {
                        response.Body = "Failiure";
                    }

                    HttpContext.Response.Headers.Add("token", jwtAdminToken.Token);
                    HttpContext.Response.Headers.Add("refreshToken", jwtAdminToken.RefreshToken);
                }
                else
                {
                    response.Code = 200;
                }

                response.JwtSuccess = jwtAdminToken.JwtSuccess;
                response.JwtErrors = jwtAdminToken.JwtErrors;
            }

            return new JsonResult(response);
        }

        [HttpPost]
        [Route("FillPricingPlansAndFeaturesActionPostgreSQL")]
        public async Task<IActionResult> FillPricingPlansAndFeaturesActionPostgreSQL([FromHeader] string token, [FromHeader] string refreshToken)
        {
            if (token == null || refreshToken == null)
            {
                response.Code = 400;
                response.Error = "Missing data - token data is incorrect or null";

                return new JsonResult(response);
            }
            else
            {
                tokenRequest.Token = token;
                tokenRequest.RefreshToken = refreshToken;

                JwtResult jwtAdminToken = await _authentication.RefreshAdminToken(tokenRequest);

                if (jwtAdminToken.JwtSuccess == true)
                {
                    response.Code = 201;

                    int result = await AdminsManagementService.FillPricingPlansAndFeaturesActionPostgreSQL();
                    if (result == 1)
                    {
                        response.Body = "Success";
                    }
                    else
                    {
                        response.Body = "Failiure";
                    }

                    HttpContext.Response.Headers.Add("token", jwtAdminToken.Token);
                    HttpContext.Response.Headers.Add("refreshToken", jwtAdminToken.RefreshToken);
                }
                else
                {
                    response.Code = 200;
                }

                response.JwtSuccess = jwtAdminToken.JwtSuccess;
                response.JwtErrors = jwtAdminToken.JwtErrors;
            }

            return new JsonResult(response);
        }
    }
}
