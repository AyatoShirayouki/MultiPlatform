using Administration_Panel.RestComunication.Admins.Responses.Admins;
using Admins_ApplicationService.DTOs;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Administration_Panel.RestComunication.Admins.Responses.Authentication;
using Freelance_ApplicationService.DTOs.Bookmarks;
using Administration_Panel.RestComunication.Admins.Responses.AdminFiles;
using Administration_Panel.RestComunication.Admins.Responses.ScriptExecutors;

namespace Administration_Panel.RestComunication.Admins
{
	public class AdminsRequestExecutor
	{
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public AdminsRequestExecutor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        //Admins
        public async Task<GetAllAdminsResponse> GetAllAdminsAction(HttpClient httpClient, string requestQuery)
        {
            GetAllAdminsResponse _getAllAdminsRespponse = new GetAllAdminsResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetAllAdminsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getAllAdminsRespponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getAllAdminsRespponse;
        }

        public async Task<DeleteAdminsResponse> DeleteAdminAction(HttpClient httpClient, string requestQuery)
        {
            DeleteAdminsResponse _deleteAdminResponse = new DeleteAdminsResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.DeleteAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<DeleteAdminsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _deleteAdminResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _deleteAdminResponse;
        }

        public async Task<UpdateAdminsResponse> UpdateAdminAction(HttpClient httpClient, AdminDTO request, string requestQuery)
        {
            UpdateAdminsResponse _UpdateAdminResponse = new UpdateAdminsResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            httpClient.BaseAddress = new Uri(requestQuery);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = JsonConvert.SerializeObject(request);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = await httpClient.PostAsync("", byteContent))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<UpdateAdminsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _UpdateAdminResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _UpdateAdminResponse;
        }

        public async Task<GetAdminsByIdResponse> GetAdminByIdAction(HttpClient httpClient, string requestQuery)
        {
            GetAdminsByIdResponse _getAdminByIdResponse = new GetAdminsByIdResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetAdminsByIdResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getAdminByIdResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getAdminByIdResponse;
        }

        //Authentication
        public async Task<SignUpAdminsResponse> SignUpAction(HttpClient httpClient, AdminDTO request, string requestQuery)
        {
            SignUpAdminsResponse _signUpResponse = new SignUpAdminsResponse();

            httpClient.BaseAddress = new Uri(requestQuery);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = JsonConvert.SerializeObject(request);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = await httpClient.PostAsync("", byteContent))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<SignUpAdminsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _signUpResponse = convert;
                    }
                }
            }

            return _signUpResponse;
        }

        public async Task<LoginAdminsResponse> LoginAction(HttpClient httpClient, string requestQuery)
        {
            LoginAdminsResponse _loginAdminsResponse = new LoginAdminsResponse();

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                if (response.Headers.FirstOrDefault(x => x.Key == "token").Value != null ||
                    response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value != null)
                {
                    _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                    _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());

                    string apiResponse = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(apiResponse))
                    {
                        var convert = JsonConvert.DeserializeObject<LoginAdminsResponse>(apiResponse);

                        if (convert != null)
                        {
                            _loginAdminsResponse = convert;
                        }
                    }
                }
            }

            return _loginAdminsResponse;
        }

        public async Task<LogoutAdminResponse> LogoutAction(HttpClient httpClient, string requestQuery)
        {
            LogoutAdminResponse _logoutResponse = new LogoutAdminResponse();

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<LogoutAdminResponse>(apiResponse);

                    if (convert != null)
                    {
                        _logoutResponse = convert;
                    }
                }
            }

            return _logoutResponse;
        }

        //AdminFiles
        public async Task<GetAllAdminFilesResponse> GetAllAdminFilesAction(HttpClient httpClient, string requestQuery)
        {
            GetAllAdminFilesResponse _getAllAdminFilesRespponse = new GetAllAdminFilesResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetAllAdminFilesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getAllAdminFilesRespponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getAllAdminFilesRespponse;
        }

        public async Task<DeleteAdminFilesResponse> DeleteAdminFilesAction(HttpClient httpClient, string requestQuery)
        {
            DeleteAdminFilesResponse _deleteAdminFilesResponse = new DeleteAdminFilesResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.DeleteAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<DeleteAdminFilesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _deleteAdminFilesResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _deleteAdminFilesResponse;
        }

        public async Task<SaveAdminFilesResponse> SaveAdminFilesAction(HttpClient httpClient, BookmarkedJobDTO request, string requestQuery)
        {
            SaveAdminFilesResponse _saveAdminFilesResponse = new SaveAdminFilesResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            httpClient.BaseAddress = new Uri(requestQuery);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = JsonConvert.SerializeObject(request);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = await httpClient.PostAsync("", byteContent))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<SaveAdminFilesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _saveAdminFilesResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _saveAdminFilesResponse;
        }

        public async Task<GetAdminFilesByIdResponse> GetAdminFilesByIdAction(HttpClient httpClient, string requestQuery)
        {
            GetAdminFilesByIdResponse _getAdminFilesByIdResponse = new GetAdminFilesByIdResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetAdminFilesByIdResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getAdminFilesByIdResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getAdminFilesByIdResponse;
        }

        //Database Actions
        public async Task<FillCountriesRegionsAndCitiesMSSQLResponse> FillCountriesRegionsAndCitiesMSSQLAction(HttpClient httpClient, string requestQuery)
        {
            FillCountriesRegionsAndCitiesMSSQLResponse _fillCountriesRegionsAndCitiesMSSQLResponse = new FillCountriesRegionsAndCitiesMSSQLResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<FillCountriesRegionsAndCitiesMSSQLResponse>(apiResponse);

                    if (convert != null)
                    {
                        _fillCountriesRegionsAndCitiesMSSQLResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _fillCountriesRegionsAndCitiesMSSQLResponse;
        }

        public async Task<FillCountriesRegionsAndCitiesPostgreSQLResponse> FillCountriesRegionsAndCitiesPostgreSQLAction(HttpClient httpClient, string requestQuery)
        {
            FillCountriesRegionsAndCitiesPostgreSQLResponse _fillCountriesRegionsAndCitiesPostgreSQLResponse = new FillCountriesRegionsAndCitiesPostgreSQLResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<FillCountriesRegionsAndCitiesPostgreSQLResponse>(apiResponse);

                    if (convert != null)
                    {
                        _fillCountriesRegionsAndCitiesPostgreSQLResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _fillCountriesRegionsAndCitiesPostgreSQLResponse;
        }

        public async Task<FillCategoriesMSSQLResponse> FillCategoriesMSSQLAction(HttpClient httpClient, string requestQuery)
        {
            FillCategoriesMSSQLResponse _fillCategoriesMSSQLResponse = new FillCategoriesMSSQLResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<FillCategoriesMSSQLResponse>(apiResponse);

                    if (convert != null)
                    {
                        _fillCategoriesMSSQLResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _fillCategoriesMSSQLResponse;
        }

        public async Task<FillCategoriesPostgreSQLResponse> FillCategoriesPostgreSQLAction(HttpClient httpClient, string requestQuery)
        {
            FillCategoriesPostgreSQLResponse _fillCategoriesPostgreSQLResponse = new FillCategoriesPostgreSQLResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<FillCategoriesPostgreSQLResponse>(apiResponse);

                    if (convert != null)
                    {
                        _fillCategoriesPostgreSQLResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _fillCategoriesPostgreSQLResponse;
        }
    }
}
