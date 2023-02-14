using Admins_ApplicationService.Implementations;
using Authorisation.Configuration;
using Authorisation.DTOs;
using Base.Messages;
using Freelance_ApplicationService.DTOs.Others;
using Freelance_ApplicationService.Implementations.Others;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Users_ApplicationService.DTOs;
using Users_ApplicationService.Implementations;
using Utils.Files;

namespace FileServer_API.Controllers.Freelance
{
    [Produces("application/json")]
    public class FreelanceFilesController : Controller
    {
        private readonly RefreshUserTokenService _usersAuthentication;
        private readonly RefreshAdminTokenService _adminsAuthentication;
        private readonly TokenValidationParameters _tokenValidationParams;

        private readonly IWebHostEnvironment webHostEnvironment;

        private BaseResponseMessage response;
        private TokenRequestDTO tokenRequest;

        public FreelanceFilesController(IOptionsMonitor<JwtConfig> optionsMonitor, TokenValidationParameters tokenValidationParams, IWebHostEnvironment hostEnvironment)
        {
            _tokenValidationParams = tokenValidationParams;
            _usersAuthentication = new RefreshUserTokenService(optionsMonitor, _tokenValidationParams);
            _adminsAuthentication = new RefreshAdminTokenService(optionsMonitor, _tokenValidationParams);

            webHostEnvironment = hostEnvironment;

            response = new BaseResponseMessage();
            tokenRequest = new TokenRequestDTO();
        }

        
        [HttpGet]
        [Route("FreelanceFiles/GetAll")]
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

                    List<FreelanceFileDTO> freelanceFiles = await FreelanceFilesManagementService.GetAll();

                    foreach (FreelanceFileDTO freelanceFile in freelanceFiles)
                    {
                        freelanceFile.File = FileGetService.GetFile(Path.Combine(webHostEnvironment.WebRootPath, freelanceFile.FileName));
                    }

                    response.Body = freelanceFiles;

                    HttpContext.Response.Headers.Add("token", jwtUserToken.Token);
                    HttpContext.Response.Headers.Add("refreshToken", jwtUserToken.RefreshToken);

                    response.JwtSuccess = jwtUserToken.JwtSuccess;
                    response.JwtErrors = jwtUserToken.JwtErrors;
                }
                else if (jwtAdminToken.JwtSuccess == true)
                {
                    response.Code = 201;

                    List<FreelanceFileDTO> freelanceFiles = await FreelanceFilesManagementService.GetAll();

                    foreach (FreelanceFileDTO freelanceFile in freelanceFiles)
                    {
                        freelanceFile.File = FileGetService.GetFile(Path.Combine(webHostEnvironment.WebRootPath, freelanceFile.FileName));
                    }

                    response.Body = freelanceFiles;

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
        /*
        [HttpGet]
        [Route("FreelanceFiles/GetFilesByParentId")]
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

                    List<FreelanceFileDTO> freelanceFiles = await FreelanceFilesManagementService.GetFilesByParentId(id);

                    foreach (FreelanceFileDTO freelanceFile in freelanceFiles)
                    {
                        freelanceFile.File = FileGetService.GetFile(Path.Combine(webHostEnvironment.WebRootPath, freelanceFile.FileName));
                    }

                    response.Body = freelanceFiles;

                    HttpContext.Response.Headers.Add("token", jwtUserToken.Token);
                    HttpContext.Response.Headers.Add("refreshToken", jwtUserToken.RefreshToken);

                    response.JwtSuccess = jwtUserToken.JwtSuccess;
                    response.JwtErrors = jwtUserToken.JwtErrors;
                }
                else if (jwtAdminToken.JwtSuccess == true)
                {
                    response.Code = 201;

                    List<FreelanceFileDTO> freelanceFiles = await FreelanceFilesManagementService.GetFilesByParentId(id);

                    foreach (FreelanceFileDTO freelanceFile in freelanceFiles)
                    {
                        freelanceFile.File = FileGetService.GetFile(Path.Combine(webHostEnvironment.WebRootPath, freelanceFile.FileName));
                    }

                    response.Body = freelanceFiles;

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
        */

        [HttpPost("upload-file")]
        [Route("FreelanceFiles/Save")]
        public async Task<IActionResult> Save([FromBody] List<FreelanceFileDTO> userFileDTOs, [FromBody] int userId, [FromBody] string fromType, [FromBody] int typeId, [FromHeader] string token, [FromHeader] string refreshToken)
        {
            if (userFileDTOs == null || userId == 0 || string.IsNullOrEmpty(fromType) || typeId == 0 || token == null || refreshToken == null)
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

                    await SaveFile(userFileDTOs, userId, fromType, typeId);
                    response.Body = "File has been saved!";

                    HttpContext.Response.Headers.Add("token", jwtUserToken.Token);
                    HttpContext.Response.Headers.Add("refreshToken", jwtUserToken.RefreshToken);

                    response.JwtSuccess = jwtUserToken.JwtSuccess;
                    response.JwtErrors = jwtUserToken.JwtErrors;
                }
                else if (jwtAdminToken.JwtSuccess == true)
                {
                    response.Code = 201;

                    await SaveFile(userFileDTOs, userId, fromType, typeId);
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

        private async Task SaveFile(List<FreelanceFileDTO> freelanceFileDTOs, int userId, string fromType, int typeId)
        {
            for (int i = 0; i < freelanceFileDTOs.Count; i++)
            {
                if (freelanceFileDTOs[i].FileName != null && freelanceFileDTOs[i].FileType != null && freelanceFileDTOs[i].File != null)
                {

                    UserDTO Freelance = await UsersManagementService.GetById(userId);

                    string uploadDir = Path.Combine(webHostEnvironment.WebRootPath, "MultiPlatform", "Users", fromType,
                        userId + "-" + typeId + "-" + Freelance.Id + "-" + Freelance.Email, freelanceFileDTOs[i].FileType);

                    bool exists = Directory.Exists(uploadDir);

                    if (!exists)
                        Directory.CreateDirectory(uploadDir);

                    string fileName = freelanceFileDTOs[i].FileName;
                    string filePath = Path.Combine(uploadDir, fileName);

                    FileSaveService.SaveByteArrayToFileWithFileStream(freelanceFileDTOs[i].File, filePath);

                    freelanceFileDTOs[i].FileName = Path.Combine("MultiPlatform", "Users", fromType,
                        userId + "-" + typeId + "-" + Freelance.Id + "-" + Freelance.Email, freelanceFileDTOs[i].FileType);

                    await FreelanceFilesManagementService.Save(freelanceFileDTOs[i]);
                }
            }
        }

        [HttpGet]
        [Route("freelanceFiles/GetById")]
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

                    FreelanceFileDTO freelanceFile = await FreelanceFilesManagementService.GetById(id);
                    freelanceFile.File = FileGetService.GetFile(Path.Combine(webHostEnvironment.WebRootPath, freelanceFile.FileName));

                    response.Body = freelanceFile;

                    HttpContext.Response.Headers.Add("token", jwtUserToken.Token);
                    HttpContext.Response.Headers.Add("refreshToken", jwtUserToken.RefreshToken);

                    response.JwtSuccess = jwtUserToken.JwtSuccess;
                    response.JwtErrors = jwtUserToken.JwtErrors;
                }
                else if (jwtAdminToken.JwtSuccess == true)
                {
                    response.Code = 201;

                    FreelanceFileDTO freelanceFile = await FreelanceFilesManagementService.GetById(id);
                    freelanceFile.File = FileGetService.GetFile(Path.Combine(webHostEnvironment.WebRootPath, freelanceFile.FileName));

                    response.Body = freelanceFile;

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
        [Route("freelanceFiles/Delete")]
        public async Task<IActionResult> Delete([FromQuery] int fileId, [FromQuery] int userId, [FromQuery] string fromType, [FromQuery] int typeId, [FromHeader] string token, [FromHeader] string refreshToken)
        {
            if (fileId == 0 || userId == 0 || string.IsNullOrEmpty(fromType) || typeId == 0 || token == null || refreshToken == null)
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

                    await DeleteFile(fileId, userId, fromType, typeId);
                    response.Body = "File has been deleted!";

                    HttpContext.Response.Headers.Add("token", jwtUserToken.Token);
                    HttpContext.Response.Headers.Add("refreshToken", jwtUserToken.RefreshToken);

                    response.JwtSuccess = jwtUserToken.JwtSuccess;
                    response.JwtErrors = jwtUserToken.JwtErrors;
                }
                else if (jwtAdminToken.JwtSuccess == true)
                {
                    response.Code = 201;

                    await DeleteFile(fileId, userId, fromType, typeId);
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

        private async Task DeleteFile(int fileId, int userId, string fromType,  int typeId)
        {
            if (fileId != 0 || userId == 0 || string.IsNullOrEmpty(fromType) || typeId == 0)
            {
                FreelanceFileDTO file = await FreelanceFilesManagementService.GetById(fileId);
               UserDTO user = await UsersManagementService.GetById(userId);

                var path = Path.Combine(webHostEnvironment.WebRootPath, "MultiPlatform", "Users", fromType,
                        userId + "-" + typeId + "-" + file.Id + "-" + user.Email, file.FileType);

                FileDeleteService.DeleteFileIfExists(path);

                await FreelanceFilesManagementService.Delete(fileId);
            }
        }
        
    }
}
