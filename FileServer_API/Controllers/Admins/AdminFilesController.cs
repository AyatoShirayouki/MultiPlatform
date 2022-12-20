using Admins_ApplicationService.DTOs;
using Admins_ApplicationService.Implementations;
using Authorisation.Configuration;
using Authorisation.DTOs;
using Base.Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Utils.Files;

namespace FileServer_API.Controllers.Admins
{
    public class AdminFilesController : Controller
    {
        private readonly RefreshAdminTokenService _adminsAuthentication;
        private readonly TokenValidationParameters _tokenValidationParams;

        private readonly IWebHostEnvironment webHostEnvironment;

        private BaseResponseMessage response;
        private TokenRequestDTO tokenRequest;
        public AdminFilesController(IOptionsMonitor<JwtConfig> optionsMonitor, TokenValidationParameters tokenValidationParams, IWebHostEnvironment hostEnvironment)
        {
            _tokenValidationParams = tokenValidationParams;
            _adminsAuthentication = new RefreshAdminTokenService(optionsMonitor, _tokenValidationParams);

            webHostEnvironment = hostEnvironment;

            response = new BaseResponseMessage();
            tokenRequest = new TokenRequestDTO();
        }

        [HttpGet]
        [Route("AdminFiles/GetAll")]
        public async Task<IActionResult> GetAll([FromHeader] string token, [FromHeader] string refreshToken)
        {
            if (token != null && refreshToken != null)
            {
                tokenRequest.Token = token;
                tokenRequest.RefreshToken = refreshToken;

                JwtResult jwtAdminToken = await _adminsAuthentication.RefreshAdminToken(tokenRequest);

                if (jwtAdminToken.JwtSuccess == true)
                {
                    response.Code = 201;

                    List<AdminFileDTO> AdminFiles = await AdminFilesManagementService.GetAll();

                    foreach (AdminFileDTO adminFile in AdminFiles)
                    {
                        adminFile.File = FileGetService.GetFile(Path.Combine(webHostEnvironment.WebRootPath, adminFile.FileName));
                    }

                    response.Body = AdminFiles;

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
        [Route("AdminFiles/GetFilesByParentId")]
        public async Task<IActionResult> GetFilesByParentId([FromQuery] int id, [FromHeader] string token, [FromHeader] string refreshToken)
        {
            if (id != 0 && token != null && refreshToken != null)
            {
                tokenRequest.Token = token;
                tokenRequest.RefreshToken = refreshToken;

                JwtResult jwtAdminToken = await _adminsAuthentication.RefreshAdminToken(tokenRequest);
                
                if (jwtAdminToken.JwtSuccess == true)
                {
                    response.Code = 201;

                    List<AdminFileDTO> AdminFiles = await AdminFilesManagementService.GetFilesByParentId(id);

                    foreach (AdminFileDTO adminFile in AdminFiles)
                    {
                        adminFile.File = FileGetService.GetFile(Path.Combine(webHostEnvironment.WebRootPath, adminFile.FileName));
                    }

                    response.Body = AdminFiles;

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

        [HttpPost("upload-file")]
        [Route("AdminFiles/Save")]
        public async Task<IActionResult> Save([FromBody] List<AdminFileDTO> adminFileDTOs, [FromHeader] string token, [FromHeader] string refreshToken)
        {
            if (adminFileDTOs == null || token == null || refreshToken == null)
            {
                response.Code = 400;
                response.Error = " Missing data - File and/or token and/or refresh token is null or invalid!";

                return new JsonResult(response);
            }
            else
            {
                tokenRequest.Token = token;
                tokenRequest.RefreshToken = refreshToken;

                JwtResult jwtAdminToken = await _adminsAuthentication.RefreshAdminToken(tokenRequest);

                if (jwtAdminToken.JwtSuccess == true)
                {
                    response.Code = 201;

                    await SaveFile(adminFileDTOs);
                    response.Body = "File has been saved!";

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

            return new JsonResult(response);
        }

        private async Task SaveFile(List<AdminFileDTO> adminFileDTOs)
        {
            for (int i = 0; i < adminFileDTOs.Count; i++)
            {
                if (adminFileDTOs[i].FileName != null && adminFileDTOs[i].FileType != null && adminFileDTOs[i].File != null)
                {
                    if (adminFileDTOs[i].IsProfilePhoto == true)
                    {
                        List<AdminFileDTO> AdminFiles = await AdminFilesManagementService.GetAll();
                        foreach (var file in AdminFiles)
                        {
                            if (file.IsProfilePhoto == true)
                            {
                                FileDeleteService.DeleteFileIfExists(Path.Combine(webHostEnvironment.WebRootPath, file.FileName));
                                await AdminFilesManagementService.Delete(file.Id);
                            }
                        }
                    }

                    AdminDTO Admin = await AdminsManagementService.GetById(adminFileDTOs[i].AdminId);

                    string uploadDir = Path.Combine(webHostEnvironment.WebRootPath, "MultiPlatform", "Admins",
                        Admin.Id + "-" + Admin.Email, adminFileDTOs[i].FileType);

                    bool exists = Directory.Exists(uploadDir);

                    if (!exists)
                        Directory.CreateDirectory(uploadDir);

                    string fileName = adminFileDTOs[i].FileName;
                    string filePath = Path.Combine(uploadDir, fileName);

                    FileSaveService.SaveByteArrayToFileWithFileStream(adminFileDTOs[i].File, filePath);

                    adminFileDTOs[i].FileName = Path.Combine("MultiPlatform", "Admins",
                        Admin.Id + "-" + Admin.Email, adminFileDTOs[i].FileType, adminFileDTOs[i].FileName);

                    await AdminFilesManagementService.Save(adminFileDTOs[i]);
                }
            }
        }

        [HttpGet]
        [Route("AdminFiles/GetById")]
        public async Task<IActionResult> GetById([FromQuery] int id, [FromHeader] string token, [FromHeader] string refreshToken)
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

                JwtResult jwtAdminToken = await _adminsAuthentication.RefreshAdminToken(tokenRequest);

                if (jwtAdminToken.JwtSuccess == true)
                {
                    response.Code = 201;

                    AdminFileDTO adminFile = await AdminFilesManagementService.GetById(id);
                    adminFile.File = FileGetService.GetFile(Path.Combine(webHostEnvironment.WebRootPath, adminFile.FileName));

                    response.Body = adminFile;

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

        [HttpDelete]
        [Route("AdminFiles/Delete")]
        public async Task<IActionResult> Delete([FromQuery] int id, [FromHeader] string token, [FromHeader] string refreshToken)
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

                JwtResult jwtAdminToken = await _adminsAuthentication.RefreshAdminToken(tokenRequest);

                if (jwtAdminToken.JwtSuccess == true)
                {
                    response.Code = 201;

                    await DeleteFile(id);
                    response.Body = "File has been deleted!";

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

            return new JsonResult(response);
        }

        private async Task DeleteFile(int id)
        {
            if (id != 0)
            {
                AdminFileDTO file = await AdminFilesManagementService.GetById(id);
                AdminDTO Admin = await AdminsManagementService.GetById(file.AdminId);

                var path = Path.Combine(webHostEnvironment.WebRootPath, "MultiPlatform", "Admins",
                        Admin.Id + "-" + Admin.Email, file.FileType, file.FileName);

                FileDeleteService.DeleteFileIfExists(path);

                await AdminFilesManagementService.Delete(id);
            }
        }
    }
}
