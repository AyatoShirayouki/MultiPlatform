using Admins_ApplicationService.Implementations;
using Authorisation.Configuration;
using Authorisation.DTOs;
using Base.Messages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Users_ApplicationService.DTOs;
using Users_ApplicationService.Implementations;
using Utils.Files;

namespace FileServer_API.Controllers.Users
{
    [Produces("application/json")]
    public class UserFilesControllerController : Controller
    {
        private readonly RefreshUserTokenService _usersAuthentication;
        private readonly RefreshAdminTokenService _adminsAuthentication;
        private readonly TokenValidationParameters _tokenValidationParams;

        private readonly IWebHostEnvironment webHostEnvironment;

        private BaseResponseMessage response;
        private TokenRequestDTO tokenRequest;

        public UserFilesControllerController(IOptionsMonitor<JwtConfig> optionsMonitor, TokenValidationParameters tokenValidationParams, IWebHostEnvironment hostEnvironment)
        {
            _tokenValidationParams = tokenValidationParams;
            _usersAuthentication = new RefreshUserTokenService(optionsMonitor, _tokenValidationParams);
            _adminsAuthentication = new RefreshAdminTokenService(optionsMonitor, _tokenValidationParams);

            webHostEnvironment = hostEnvironment;

            response = new BaseResponseMessage();
            tokenRequest = new TokenRequestDTO();
        }

        [HttpGet]
        [Route("UserFiles/GetAll")]
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

                    List<UserFileDTO> userFiles = await UserFilesManagementService.GetAll();

                    foreach (UserFileDTO userFile in userFiles)
                    {
                        userFile.File = FileGetService.GetFile(Path.Combine(webHostEnvironment.WebRootPath, userFile.FileName));
                    }

                    response.Body = userFiles;

                    HttpContext.Response.Headers.Add("token", jwtUserToken.Token);
                    HttpContext.Response.Headers.Add("refreshToken", jwtUserToken.RefreshToken);

                    response.JwtSuccess = jwtUserToken.JwtSuccess;
                    response.JwtErrors = jwtUserToken.JwtErrors;
                }
                else if (jwtAdminToken.JwtSuccess == true)
                {
                    response.Code = 201;

                    List<UserFileDTO> userFiles = await UserFilesManagementService.GetAll();

                    foreach (UserFileDTO userFile in userFiles)
                    {
                        userFile.File = FileGetService.GetFile(Path.Combine(webHostEnvironment.WebRootPath, userFile.FileName));
                    }

                    response.Body = userFiles;

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

        [HttpGet]
        [Route("UserFiles/GetFilesByParentId")]
        public async Task<IActionResult> GetFilesByParentId([FromQuery] int id, [FromHeader] string token, [FromHeader] string refreshToken)
        {
            if (id != 0 && token != null && refreshToken != null)
            {
                tokenRequest.Token = token;
                tokenRequest.RefreshToken = refreshToken;

                JwtResult jwtUserToken = await _usersAuthentication.RefreshUserToken(tokenRequest);
                JwtResult jwtAdminToken = await _adminsAuthentication.RefreshAdminToken(tokenRequest);

                if (jwtUserToken.JwtSuccess == true)
                {
                    response.Code = 201;

                    List<UserFileDTO> userFiles = await UserFilesManagementService.GetFilesByParentId(id);

                    foreach (UserFileDTO userFile in userFiles)
                    {
                        userFile.File = FileGetService.GetFile(Path.Combine(webHostEnvironment.WebRootPath, userFile.FileName));
                    }

                    response.Body = userFiles;

                    HttpContext.Response.Headers.Add("token", jwtUserToken.Token);
                    HttpContext.Response.Headers.Add("refreshToken", jwtUserToken.RefreshToken);

                    response.JwtSuccess = jwtUserToken.JwtSuccess;
                    response.JwtErrors = jwtUserToken.JwtErrors;
                }
                else if (jwtAdminToken.JwtSuccess == true)
                {
                    response.Code = 201;

                    List<UserFileDTO> userFiles = await UserFilesManagementService.GetFilesByParentId(id);

                    foreach (UserFileDTO userFile in userFiles)
                    {
                        userFile.File = FileGetService.GetFile(Path.Combine(webHostEnvironment.WebRootPath, userFile.FileName));
                    }

                    response.Body = userFiles;

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

        [HttpPost("upload-file")]
        [Route("UserFiles/Save")]
        public async Task<IActionResult> Save([FromBody] List<UserFileDTO> userFileDTOs, [FromHeader] string token, [FromHeader] string refreshToken)
        {
            if (userFileDTOs == null || token == null || refreshToken == null)
            {
                response.Code = 400;
                response.Error = " Missing data - File and/or token and/or refresh token is null or invalid!";

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

                    await SaveFile(userFileDTOs);
                    response.Body = "File has been saved!";

                    HttpContext.Response.Headers.Add("token", jwtUserToken.Token);
                    HttpContext.Response.Headers.Add("refreshToken", jwtUserToken.RefreshToken);

                    response.JwtSuccess = jwtUserToken.JwtSuccess;
                    response.JwtErrors = jwtUserToken.JwtErrors;
                }
                else if (jwtAdminToken.JwtSuccess == true)
                {
                    response.Code = 201;

                    await SaveFile(userFileDTOs);
                    response.Body = "File has been saved!";

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

        private async Task SaveFile(List<UserFileDTO> userFileDTOs)
        {
            for (int i = 0; i < userFileDTOs.Count; i++)
            {
                if (userFileDTOs[i].FileName != null && userFileDTOs[i].FileType != null && userFileDTOs[i].File != null)
                {
                    if (userFileDTOs[i].IsProfilePhoto == true)
                    {
                        List<UserFileDTO> userFiles = await UserFilesManagementService.GetAll();
                        foreach (var file in userFiles)
                        {
                            if (file.IsProfilePhoto == true)
                            {
                                FileDeleteService.DeleteFileIfExists(Path.Combine(webHostEnvironment.WebRootPath, file.FileName));
                                await UserFilesManagementService.Delete(file.Id);
                            }
                        }
                    }
                    else if (userFileDTOs[i].IsCoverPhoto == true)
                    {
                        List<UserFileDTO> userFiles = await UserFilesManagementService.GetAll();
                        foreach (var file in userFiles)
                        {
                            if (file.IsCoverPhoto == true)
                            {
                                FileDeleteService.DeleteFileIfExists(Path.Combine(webHostEnvironment.WebRootPath, file.FileName));
                                await UserFilesManagementService.Delete(file.Id);
                            }
                        }
                    }

                    UserDTO user = await UsersManagementService.GetById(userFileDTOs[i].UserId);

                    string uploadDir = Path.Combine(webHostEnvironment.WebRootPath, "MultiPlatform", "Users",
                        user.Id + "-" + user.Email, userFileDTOs[i].FileType);

                    bool exists = Directory.Exists(uploadDir);

                    if (!exists)
                        Directory.CreateDirectory(uploadDir);

                    string fileName = userFileDTOs[i].FileName;
                    string filePath = Path.Combine(uploadDir, fileName);

                    FileSaveService.SaveByteArrayToFileWithFileStream(userFileDTOs[i].File, filePath);

                    userFileDTOs[i].FileName = Path.Combine("MultiPlatform", "Users",
                        user.Id + "-" + user.Email, userFileDTOs[i].FileType, userFileDTOs[i].FileName);

                    await UserFilesManagementService.Save(userFileDTOs[i]);
                }
            }
        }

        [HttpGet]
        [Route("UserFiles/GetById")]
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

                JwtResult jwtUserToken = await _usersAuthentication.RefreshUserToken(tokenRequest);
                JwtResult jwtAdminToken = await _adminsAuthentication.RefreshAdminToken(tokenRequest);

                if (jwtUserToken.JwtSuccess == true)
                {
                    response.Code = 201;

                    UserFileDTO userFile = await UserFilesManagementService.GetById(id);
                    userFile.File = FileGetService.GetFile(Path.Combine(webHostEnvironment.WebRootPath, userFile.FileName));

                    response.Body = userFile;

                    HttpContext.Response.Headers.Add("token", jwtUserToken.Token);
                    HttpContext.Response.Headers.Add("refreshToken", jwtUserToken.RefreshToken);

                    response.JwtSuccess = jwtUserToken.JwtSuccess;
                    response.JwtErrors = jwtUserToken.JwtErrors;
                }
                else if (jwtAdminToken.JwtSuccess == true)
                {
                    response.Code = 201;

                    UserFileDTO userFile = await UserFilesManagementService.GetById(id);
                    userFile.File = FileGetService.GetFile(Path.Combine(webHostEnvironment.WebRootPath, userFile.FileName));

                    response.Body = userFile;

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
        [Route("UserFiles/Delete")]
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

                JwtResult jwtUserToken = await _usersAuthentication.RefreshUserToken(tokenRequest);
                JwtResult jwtAdminToken = await _adminsAuthentication.RefreshAdminToken(tokenRequest);

                if (jwtUserToken.JwtSuccess == true)
                {
                    response.Code = 201;

                    await DeleteFile(id);
                    response.Body = "File has been deleted!";

                    HttpContext.Response.Headers.Add("token", jwtUserToken.Token);
                    HttpContext.Response.Headers.Add("refreshToken", jwtUserToken.RefreshToken);

                    response.JwtSuccess = jwtUserToken.JwtSuccess;
                    response.JwtErrors = jwtUserToken.JwtErrors;
                }
                else if (jwtAdminToken.JwtSuccess == true)
                {
                    response.Code = 201;

                    await DeleteFile(id);
                    response.Body = "File has been deleted!";

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

        private async Task DeleteFile(int id)
        {
            if (id != 0)
            {
                UserFileDTO file = await UserFilesManagementService.GetById(id);
                UserDTO user = await UsersManagementService.GetById(file.UserId);

                var path = Path.Combine(webHostEnvironment.WebRootPath, "MultiPlatform", "Users",
                        user.Id + "-" + user.Email, file.FileType, file.FileName);

                FileDeleteService.DeleteFileIfExists(path);

                await UserFilesManagementService.Delete(id);
            }
        }
    }
}
