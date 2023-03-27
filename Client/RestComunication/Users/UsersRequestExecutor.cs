using Client.RestComunication.Users.Responses.AddressInfo.Addresses;
using Client.RestComunication.Users.Responses.AddressInfo.Cities;
using Client.RestComunication.Users.Responses.AddressInfo.Countries;
using Client.RestComunication.Users.Responses.AddressInfo.Regions;
using Client.RestComunication.Users.Responses.Authentication;
using Client.RestComunication.Users.Responses.Education.Degrees;
using Client.RestComunication.Users.Responses.Education.EducationDetails.AcademicFields;
using Client.RestComunication.Users.Responses.Education.EducationDetails.EducationalFacilities;
using Client.RestComunication.Users.Responses.Education.EducationDetails.EducationalFacilityTypes;
using Client.RestComunication.Users.Responses.Education.UserEducations;
using Client.RestComunication.Users.Responses.Others.PricingPlanFeatures;
using Client.RestComunication.Users.Responses.Others.PricingPlans;
using Client.RestComunication.Users.Responses.Others.UserFiles;
using Client.RestComunication.Users.Responses.Others.Users;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Users_ApplicationService.DTOs;
using Users_ApplicationService.DTOs.AddressInfo;
using Users_ApplicationService.DTOs.Authentication;
using Users_ApplicationService.DTOs.Education;

namespace Client.RestComunication.Users
{
    public class UsersRequestExecutor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public UsersRequestExecutor (IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        //Authentication
        public async Task<SignUpUserResponse> SignUpAction(HttpClient httpClient, SignUpDTO request, string requestQuery)
        {
            SignUpUserResponse _signUpResponse = new SignUpUserResponse();

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
                    var convert = JsonConvert.DeserializeObject<SignUpUserResponse>(apiResponse);

                    if (convert != null)
                    {
                        _signUpResponse = convert;
                    }
                }
            }

            return _signUpResponse;
        }
        public async Task<LoginUserResponse> LoginAction(HttpClient httpClient, string requestQuery)
        {
            LoginUserResponse _loginUserResponse = new LoginUserResponse();

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
                        var convert = JsonConvert.DeserializeObject<LoginUserResponse>(apiResponse);

                        if (convert != null)
                        {
                            _loginUserResponse = convert;
                        }
                    }
                }
            }

            return _loginUserResponse;
        }
        public async Task<LogoutResponse> LogoutAction(HttpClient httpClient, string requestQuery)
        {
            LogoutResponse _logoutResponse = new LogoutResponse();

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<LogoutResponse>(apiResponse);

                    if (convert != null)
                    {
                        _logoutResponse = convert;
                    }
                }
            }

            return _logoutResponse;
        }

        //Addresses
        public async Task<GetAllAddressesResponse> GetAllAddressesAction(HttpClient httpClient, string requestQuery)
        {
            GetAllAddressesResponse _getAllAddressesRespponse = new GetAllAddressesResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetAllAddressesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getAllAddressesRespponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getAllAddressesRespponse;
        }
        public async Task<DeleteAddressesResponse> DeleteAddressesAction(HttpClient httpClient, string requestQuery)
        {
            DeleteAddressesResponse _deleteAddressResponse = new DeleteAddressesResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.DeleteAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<DeleteAddressesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _deleteAddressResponse= convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }
            return _deleteAddressResponse;
        }
        public async Task<SaveAddressesResponse> SaveAddressesAction(HttpClient httpClient, AddressDTO request, string requestQuery)
        {
            SaveAddressesResponse _saveAddressResponse = new SaveAddressesResponse();

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
                    var convert = JsonConvert.DeserializeObject<SaveAddressesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _saveAddressResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }
            return _saveAddressResponse;
        }
        public async Task<GetAddressesByIdResponse> GetAddressesByIdAction(HttpClient httpClient, string requestQuery)
        {
            GetAddressesByIdResponse _getAddressByIdResponse = new GetAddressesByIdResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetAddressesByIdResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getAddressByIdResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getAddressByIdResponse;
        }

        //Countries
        public async Task<GetAllCountriesResponse> GetAllCountriesAction(HttpClient httpClient, string requestQuery)
        {
            GetAllCountriesResponse _getAllCountriesRespponse = new GetAllCountriesResponse();

            //httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            //httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetAllCountriesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getAllCountriesRespponse = convert;

                        //_session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        //_session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getAllCountriesRespponse;
        }
        public async Task<DeleteCountriesResponse> DeleteCountriesAction(HttpClient httpClient, string requestQuery)
        {
            DeleteCountriesResponse _deleteCountryResponse = new DeleteCountriesResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.DeleteAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<DeleteCountriesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _deleteCountryResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _deleteCountryResponse;
        }
        public async Task<SaveCountriesResponse> SaveCountriesAction(HttpClient httpClient, CountryDTO request, string requestQuery)
        {
            SaveCountriesResponse _saveCountryResponse = new SaveCountriesResponse();

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
                    var convert = JsonConvert.DeserializeObject<SaveCountriesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _saveCountryResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _saveCountryResponse;
        }
        public async Task<GetCountriesByIdResponse> GetCountriesByIdAction(HttpClient httpClient, string requestQuery)
        {
            GetCountriesByIdResponse _getCountryByIdResponse = new GetCountriesByIdResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetCountriesByIdResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getCountryByIdResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getCountryByIdResponse;
        }

        //Regions
        public async Task<GetAllRegionsResponse> GetAllRegionsAction(HttpClient httpClient, string requestQuery)
        {
            GetAllRegionsResponse _getAllRegionsRespponse = new GetAllRegionsResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetAllRegionsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getAllRegionsRespponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getAllRegionsRespponse;
        }
        public async Task<DeleteRegionsResponse> DeleteRegionsAction(HttpClient httpClient, string requestQuery)
        {
            DeleteRegionsResponse _deleteRegionsResponse = new DeleteRegionsResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.DeleteAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<DeleteRegionsResponse>(apiResponse);

                    if (convert != null)
                    {
						_deleteRegionsResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _deleteRegionsResponse;
        }
        public async Task<SaveRegionsResponse> SaveRegionsAction(HttpClient httpClient, CityDTO request, string requestQuery)
        {
            SaveRegionsResponse _saveCityResponse = new SaveRegionsResponse();

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
                    var convert = JsonConvert.DeserializeObject<SaveRegionsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _saveCityResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _saveCityResponse;
        }
        public async Task<GetRegionsByIdResponse> GetRegionsByIdAction(HttpClient httpClient, string requestQuery)
        {
            GetRegionsByIdResponse _getRegionsByIdResponse = new GetRegionsByIdResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetRegionsByIdResponse>(apiResponse);

                    if (convert != null)
                    {
						_getRegionsByIdResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getRegionsByIdResponse;
        }
		public async Task<GetRegionsByCountryIdResponse> GetRegionsByCountryIdAction(HttpClient httpClient, string requestQuery)
		{
			GetRegionsByCountryIdResponse _getRegionsByCountryIdResponse = new GetRegionsByCountryIdResponse();

			using (var response = await httpClient.GetAsync(requestQuery))
			{
				string apiResponse = await response.Content.ReadAsStringAsync();

				if (!string.IsNullOrEmpty(apiResponse))
				{
					var convert = JsonConvert.DeserializeObject<GetRegionsByCountryIdResponse>(apiResponse);

					if (convert != null)
					{
						_getRegionsByCountryIdResponse = convert;
					}
				}
			}

			return _getRegionsByCountryIdResponse;
		}

		//Cities
		public async Task<GetAllCitiesResponse> GetAllCitiesAction(HttpClient httpClient, string requestQuery)
        {
            GetAllCitiesResponse _getAllCitiesRespponse = new GetAllCitiesResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetAllCitiesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getAllCitiesRespponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getAllCitiesRespponse;
        }
        public async Task<DeleteCitiesResponse> DeleteCitiesAction(HttpClient httpClient, string requestQuery)
        {
            DeleteCitiesResponse _deleteCityResponse = new DeleteCitiesResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.DeleteAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<DeleteCitiesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _deleteCityResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _deleteCityResponse;
        }
        public async Task<SaveCitiesResponse> SaveCitiesAction(HttpClient httpClient, CityDTO request, string requestQuery)
        {
            SaveCitiesResponse _saveCityResponse = new SaveCitiesResponse();

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
                    var convert = JsonConvert.DeserializeObject<SaveCitiesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _saveCityResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _saveCityResponse;
        }
        public async Task<GetCitiesByIdResponse> GetCitiesByIdAction(HttpClient httpClient, string requestQuery)
        {
            GetCitiesByIdResponse _getCityByIdResponse = new GetCitiesByIdResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetCitiesByIdResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getCityByIdResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getCityByIdResponse;
        }
		public async Task<GetCitiesByRegionAndCountryIdResponse> GetCitiesByRegionAndCountryIdAction(HttpClient httpClient, string requestQuery)
		{
			GetCitiesByRegionAndCountryIdResponse _getCitiesByRegionAndCountryIdResponse = new GetCitiesByRegionAndCountryIdResponse();

			using (var response = await httpClient.GetAsync(requestQuery))
			{
				string apiResponse = await response.Content.ReadAsStringAsync();

				if (!string.IsNullOrEmpty(apiResponse))
				{
					var convert = JsonConvert.DeserializeObject<GetCitiesByRegionAndCountryIdResponse>(apiResponse);

					if (convert != null)
					{
						_getCitiesByRegionAndCountryIdResponse = convert;
					}
				}
			}

			return _getCitiesByRegionAndCountryIdResponse;
		}

		//Users
		public async Task<GetAllUsersResponse> GetAllUsersAction(HttpClient httpClient, string requestQuery)
        {
            GetAllUsersResponse _getAllUsersRespponse = new GetAllUsersResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetAllUsersResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getAllUsersRespponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }
            return _getAllUsersRespponse;
        }
        public async Task<DeleteUsersResponse> DeleteUsersAction(HttpClient httpClient, string requestQuery)
        {
            DeleteUsersResponse _deleteUserResponse = new DeleteUsersResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.DeleteAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<DeleteUsersResponse>(apiResponse);

                    if (convert != null)
                    {
                        _deleteUserResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }
            return _deleteUserResponse;
        }
        public async Task<SaveUsersResponse> SaveUsersAction(HttpClient httpClient, UserDTO request, string requestQuery)
        {
            SaveUsersResponse _saveUserResponse = new SaveUsersResponse();

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
                    var convert = JsonConvert.DeserializeObject<SaveUsersResponse>(apiResponse);

                    if (convert != null)
                    {
                        _saveUserResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }
            return _saveUserResponse;
        }
        public async Task<GetUsersByIdResponse> GetUserByIdAction(HttpClient httpClient, string requestQuery)
        {
            GetUsersByIdResponse _getUserByIdResponse = new GetUsersByIdResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetUsersByIdResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getUserByIdResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getUserByIdResponse;
        }

        // Users Education
        public async Task<GetAllUserEducationsResponse> GetAllUserEducationsAction(HttpClient httpClient, string requestQuery)
        {
            GetAllUserEducationsResponse _getAllUserEducationsRespponse = new GetAllUserEducationsResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetAllUserEducationsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getAllUserEducationsRespponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }
            return _getAllUserEducationsRespponse;
        }
        public async Task<DeleteUserEducationsResponse> DeleteUserEducationsAction(HttpClient httpClient, string requestQuery)
        {
            DeleteUserEducationsResponse _deleteUserEducationsResponse = new DeleteUserEducationsResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.DeleteAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<DeleteUserEducationsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _deleteUserEducationsResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }
            return _deleteUserEducationsResponse;
        }
        public async Task<SaveUserEducationsResponse> SaveUserEducationsAction(HttpClient httpClient, UserEducationDTO request, string requestQuery)
        {
            SaveUserEducationsResponse _saveUserEducationsResponse = new SaveUserEducationsResponse();

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
                    var convert = JsonConvert.DeserializeObject<SaveUserEducationsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _saveUserEducationsResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }
            return _saveUserEducationsResponse;
        }
        public async Task<GetUserEducationsByIdResponse> GetUserEducationsByIdAction(HttpClient httpClient, string requestQuery)
        {
            GetUserEducationsByIdResponse _getUserEducationsByIdResponse = new GetUserEducationsByIdResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetUserEducationsByIdResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getUserEducationsByIdResponse = convert;
                       
                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getUserEducationsByIdResponse;
        }

        // Degrees
        public async Task<GetAllDegreesResponse> GetAllDegreesAction(HttpClient httpClient, string requestQuery)
        {
            GetAllDegreesResponse _getAllDegreesRespponse = new GetAllDegreesResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetAllDegreesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getAllDegreesRespponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }
            return _getAllDegreesRespponse;
        }
        public async Task<DeleteDegreesResponse> DeleteDegreesAction(HttpClient httpClient, string requestQuery)
        {
            DeleteDegreesResponse _deleteDegreesResponse = new DeleteDegreesResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.DeleteAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<DeleteDegreesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _deleteDegreesResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }
            return _deleteDegreesResponse;
        }
        public async Task<SaveDegreesResponse> SaveDegreesAction(HttpClient httpClient, UserEducationDTO request, string requestQuery)
        {
            SaveDegreesResponse _saveDegreesResponse = new SaveDegreesResponse();

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
                    var convert = JsonConvert.DeserializeObject<SaveDegreesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _saveDegreesResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }
            return _saveDegreesResponse;
        }
        public async Task<GetDegreesByIdResponse> GetDegreesByIdAction(HttpClient httpClient, string requestQuery)
        {
            GetDegreesByIdResponse _getDegreesByIdResponse = new GetDegreesByIdResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetDegreesByIdResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getDegreesByIdResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getDegreesByIdResponse;
        }

        // AcademicFields
        public async Task<GetAllAcademicFieldsResponse> GetAllAcademicFieldsAction(HttpClient httpClient, string requestQuery)
        {
            GetAllAcademicFieldsResponse _getAllAcademicFieldsRespponse = new GetAllAcademicFieldsResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetAllAcademicFieldsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getAllAcademicFieldsRespponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }
            return _getAllAcademicFieldsRespponse;
        }
        public async Task<DeleteAcademicFieldsResponse> DeleteAcademicFieldsAction(HttpClient httpClient, string requestQuery)
        {
            DeleteAcademicFieldsResponse _deleteAcademicFieldsResponse = new DeleteAcademicFieldsResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.DeleteAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<DeleteAcademicFieldsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _deleteAcademicFieldsResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }
            return _deleteAcademicFieldsResponse;
        }
        public async Task<SaveAcademicFieldsResponse> SaveAcademicFieldsAction(HttpClient httpClient, UserEducationDTO request, string requestQuery)
        {
            SaveAcademicFieldsResponse _saveAcademicFieldsResponse = new SaveAcademicFieldsResponse();

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
                    var convert = JsonConvert.DeserializeObject<SaveAcademicFieldsResponse>(apiResponse);

                    if (convert != null)
                    {
                        _saveAcademicFieldsResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }
            return _saveAcademicFieldsResponse;
        }
        public async Task<GetAcademicFieldsByIdResponse> GetAcademicFieldsByIdAction(HttpClient httpClient, string requestQuery)
        {
            GetAcademicFieldsByIdResponse _getAcademicFieldsByIdResponse = new GetAcademicFieldsByIdResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetAcademicFieldsByIdResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getAcademicFieldsByIdResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getAcademicFieldsByIdResponse;
        }

        // EducationalFacilities
        public async Task<GetAllEducationalFacilitiesResponse> GetAllEducationalFacilitiesAction(HttpClient httpClient, string requestQuery)
        {
            GetAllEducationalFacilitiesResponse _getAllEducationalFacilitiesRespponse = new GetAllEducationalFacilitiesResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetAllEducationalFacilitiesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getAllEducationalFacilitiesRespponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }
            return _getAllEducationalFacilitiesRespponse;
        }
        public async Task<DeleteEducationalFacilitiesResponse> DeleteEducationalFacilitiesAction(HttpClient httpClient, string requestQuery)
        {
            DeleteEducationalFacilitiesResponse _deleteEducationalFacilitiesResponse = new DeleteEducationalFacilitiesResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.DeleteAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<DeleteEducationalFacilitiesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _deleteEducationalFacilitiesResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }
            return _deleteEducationalFacilitiesResponse;
        }
        public async Task<SaveEducationalFacilitiesResponse> SaveEducationalFacilitiesAction(HttpClient httpClient, UserEducationDTO request, string requestQuery)
        {
            SaveEducationalFacilitiesResponse _saveEducationalFacilitiesResponse = new SaveEducationalFacilitiesResponse();

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
                    var convert = JsonConvert.DeserializeObject<SaveEducationalFacilitiesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _saveEducationalFacilitiesResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }
            return _saveEducationalFacilitiesResponse;
        }
        public async Task<GetEducationalFacilitiesByIdResponse> GetEducationalFacilitiesByIdAction(HttpClient httpClient, string requestQuery)
        {
            GetEducationalFacilitiesByIdResponse _getEducationalFacilitiesByIdResponse = new GetEducationalFacilitiesByIdResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetEducationalFacilitiesByIdResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getEducationalFacilitiesByIdResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getEducationalFacilitiesByIdResponse;
        }

        // EducationalFacilityTypes
        public async Task<GetAllEducationalFacilityTypesResponse> GetAllEducationalFacilityTypesAction(HttpClient httpClient, string requestQuery)
        {
            GetAllEducationalFacilityTypesResponse _getAllEducationalFacilityTypesRespponse = new GetAllEducationalFacilityTypesResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetAllEducationalFacilityTypesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getAllEducationalFacilityTypesRespponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }
            return _getAllEducationalFacilityTypesRespponse;
        }
        public async Task<DeleteEducationalFacilityTypesResponse> DeleteEducationalFacilityTypesAction(HttpClient httpClient, string requestQuery)
        {
            DeleteEducationalFacilityTypesResponse _deleteEducationalFacilityTypesResponse = new DeleteEducationalFacilityTypesResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.DeleteAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<DeleteEducationalFacilityTypesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _deleteEducationalFacilityTypesResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }
            return _deleteEducationalFacilityTypesResponse;
        }
        public async Task<SaveEducationalFacilityTypesResponse> SaveEducationalFacilityTypesAction(HttpClient httpClient, UserEducationDTO request, string requestQuery)
        {
            SaveEducationalFacilityTypesResponse _saveEducationalFacilityTypesResponse = new SaveEducationalFacilityTypesResponse();

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
                    var convert = JsonConvert.DeserializeObject<SaveEducationalFacilityTypesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _saveEducationalFacilityTypesResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }
            return _saveEducationalFacilityTypesResponse;
        }
        public async Task<GetEducationalFacilityTypesByIdResponse> GetEducationalFacilityTypesByIdAction(HttpClient httpClient, string requestQuery)
        {
            GetEducationalFacilityTypesByIdResponse _getEducationalFacilityTypesByIdResponse = new GetEducationalFacilityTypesByIdResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetEducationalFacilityTypesByIdResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getEducationalFacilityTypesByIdResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getEducationalFacilityTypesByIdResponse;
        }

        // PricingPlanFeatures
        public async Task<GetAllPricingPlanFeaturesResponse> GetAllPricingPlanFeaturesAction(HttpClient httpClient, string requestQuery)
        {
            GetAllPricingPlanFeaturesResponse _getAllPricingPlanFeaturesRespponse = new GetAllPricingPlanFeaturesResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetAllPricingPlanFeaturesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getAllPricingPlanFeaturesRespponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }
            return _getAllPricingPlanFeaturesRespponse;
        }
        public async Task<DeletePricingPlanFeaturesResponse> DeletePricingPlanFeaturesAction(HttpClient httpClient, string requestQuery)
        {
            DeletePricingPlanFeaturesResponse _deletePricingPlanFeaturesResponse = new DeletePricingPlanFeaturesResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.DeleteAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<DeletePricingPlanFeaturesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _deletePricingPlanFeaturesResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }
            return _deletePricingPlanFeaturesResponse;
        }
        public async Task<SavePricingPlanFeaturesResponse> SavePricingPlanFeaturesAction(HttpClient httpClient, UserEducationDTO request, string requestQuery)
        {
            SavePricingPlanFeaturesResponse _savePricingPlanFeaturesResponse = new SavePricingPlanFeaturesResponse();

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
                    var convert = JsonConvert.DeserializeObject<SavePricingPlanFeaturesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _savePricingPlanFeaturesResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }
            return _savePricingPlanFeaturesResponse;
        }
        public async Task<GetPricingPlanFeaturesByIdResponse> GetPricingPlanFeaturesByIdAction(HttpClient httpClient, string requestQuery)
        {
            GetPricingPlanFeaturesByIdResponse _getPricingPlanFeaturesByIdResponse = new GetPricingPlanFeaturesByIdResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetPricingPlanFeaturesByIdResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getPricingPlanFeaturesByIdResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getPricingPlanFeaturesByIdResponse;
        }

        // PricingPlans
        public async Task<GetAllPricingPlansResponse> GetAllPricingPlansAction(HttpClient httpClient, string requestQuery)
        {
            GetAllPricingPlansResponse _getAllPricingPlansRespponse = new GetAllPricingPlansResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetAllPricingPlansResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getAllPricingPlansRespponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }
            return _getAllPricingPlansRespponse;
        }
        public async Task<DeletePricingPlansResponse> DeletePricingPlansAction(HttpClient httpClient, string requestQuery)
        {
            DeletePricingPlansResponse _deletePricingPlansResponse = new DeletePricingPlansResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.DeleteAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<DeletePricingPlansResponse>(apiResponse);

                    if (convert != null)
                    {
                        _deletePricingPlansResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }
            return _deletePricingPlansResponse;
        }
        public async Task<SavePricingPlansResponse> SavePricingPlansAction(HttpClient httpClient, UserEducationDTO request, string requestQuery)
        {
            SavePricingPlansResponse _savePricingPlansResponse = new SavePricingPlansResponse();

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
                    var convert = JsonConvert.DeserializeObject<SavePricingPlansResponse>(apiResponse);

                    if (convert != null)
                    {
                        _savePricingPlansResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }
            return _savePricingPlansResponse;
        }
        public async Task<GetPricingPlansByIdResponse> GetPricingPlansByIdAction(HttpClient httpClient, string requestQuery)
        {
            GetPricingPlansByIdResponse _getPricingPlansByIdResponse = new GetPricingPlansByIdResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetPricingPlansByIdResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getPricingPlansByIdResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getPricingPlansByIdResponse;
        }

        // UserFiles
        public async Task<GetAllUserFilesResponse> GetAllUserFilesAction(HttpClient httpClient, string requestQuery)
        {
            GetAllUserFilesResponse _getAllUserFilesRespponse = new GetAllUserFilesResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetAllUserFilesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getAllUserFilesRespponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }
            return _getAllUserFilesRespponse;
        }
        public async Task<DeleteUserFilesResponse> DeleteUserFilesAction(HttpClient httpClient, string requestQuery)
        {
            DeleteUserFilesResponse _deleteUserFilesResponse = new DeleteUserFilesResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.DeleteAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<DeleteUserFilesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _deleteUserFilesResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }
            return _deleteUserFilesResponse;
        }
        public async Task<SaveUserFilesResponse> SaveUserFilesAction(HttpClient httpClient, UserEducationDTO request, string requestQuery)
        {
            SaveUserFilesResponse _saveUserFilesResponse = new SaveUserFilesResponse();

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
                    var convert = JsonConvert.DeserializeObject<SaveUserFilesResponse>(apiResponse);

                    if (convert != null)
                    {
                        _saveUserFilesResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }
            return _saveUserFilesResponse;
        }
        public async Task<GetUserFilesByIdResponse> GetUserFilesByIdAction(HttpClient httpClient, string requestQuery)
        {
            GetUserFilesByIdResponse _getUserFilesByIdResponse = new GetUserFilesByIdResponse();

            httpClient.DefaultRequestHeaders.Add("token", _session.GetString("Token"));
            httpClient.DefaultRequestHeaders.Add("refreshToken", _session.GetString("RefreshToken"));

            using (var response = await httpClient.GetAsync(requestQuery))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var convert = JsonConvert.DeserializeObject<GetUserFilesByIdResponse>(apiResponse);

                    if (convert != null)
                    {
                        _getUserFilesByIdResponse = convert;

                        _session.SetString("Token", response.Headers.FirstOrDefault(x => x.Key == "token").Value.FirstOrDefault());
                        _session.SetString("RefreshToken", response.Headers.FirstOrDefault(x => x.Key == "refreshToken").Value.FirstOrDefault());
                    }
                }
            }

            return _getUserFilesByIdResponse;
        }
    }
}
