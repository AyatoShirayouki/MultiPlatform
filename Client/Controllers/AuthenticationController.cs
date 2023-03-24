using Client.RestComunication.Users;
using Client.RestComunication.Users.Responses.AddressInfo.Cities;
using Client.RestComunication.Users.Responses.AddressInfo.Countries;
using Client.RestComunication.Users.Responses.AddressInfo.Regions;
using Client.ViewModels.Authentication;
using Microsoft.AspNetCore.Mvc;
using Users_ApplicationService.DTOs.AddressInfo;

namespace Client.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly UsersRequestBuilder _usersRequestBuilder;
        private UsersRequestExecutor _usersRequestExecutor;

        private GetAllCountriesResponse _getAllCountriesResponse;
        private GetRegionsByCountryIdResponse _getRegionsByCountryIdResponse;
        private GetCitiesByRegionAndCountryIdResponse _getCitiesByRegionAndCountryIdResponse;

		private IConfiguration _configuration;
		private readonly IHttpContextAccessor _httpContextAccessor;

		private readonly string _baseUsersURI;

        public AuthenticationController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
			_configuration = configuration;
			_httpContextAccessor = httpContextAccessor;

            _usersRequestBuilder = new UsersRequestBuilder();
            _usersRequestExecutor = new UsersRequestExecutor(_httpContextAccessor);

            _getAllCountriesResponse = new GetAllCountriesResponse();
            _getRegionsByCountryIdResponse = new GetRegionsByCountryIdResponse();
			_getCitiesByRegionAndCountryIdResponse = new GetCitiesByRegionAndCountryIdResponse();

            _baseUsersURI = _configuration.GetValue<string>("UsersAPI");
		}

		[HttpGet]
        public async Task<IActionResult> Login()
        {
            LoginVM model = new LoginVM();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> SignUp()
        {
            SignUpVM model = new SignUpVM();

		    return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpVM model)
        {
            
            return View(model);
        }

        [HttpGet]
        public async Task<JsonResult> LoadAllCountries()
        {
            List<CountryDTO> response;
			using (var httpClient = new HttpClient())
			{
				_getAllCountriesResponse = await _usersRequestExecutor.GetAllCountriesAction(httpClient, _usersRequestBuilder.GetAllCountriesRequestBuilder(_baseUsersURI));
			}
			if (_getAllCountriesResponse != null)
			{
				response = _getAllCountriesResponse.Body;

				return Json(response);
			}
            else
            {
                return null;
            }
		}

        [HttpGet]
        public async Task<JsonResult> LoadRegionsForCountry(int id)
        {
            if (id != 0)
            {
				using (var httpClient = new HttpClient())
				{
					_getRegionsByCountryIdResponse = await _usersRequestExecutor.GetRegionsByCountryIdAction(httpClient, _usersRequestBuilder.GetRegionsByCountryIdRequestBuilder(_baseUsersURI, id));
				}

				List<RegionDTO> regions = _getRegionsByCountryIdResponse.Body;
				List<RegionDTO> result = new List<RegionDTO>();
				RegionDTO others = new RegionDTO();

				others.Name = "Other-";

				for (int i = 0; i < regions.Count; i++)
				{
					if (string.IsNullOrEmpty(regions[i].Name))
					{
						others.Name += regions[i].Id + ",";
					}
					else
					{
						result.Add(regions[i]);
					}
				}

				others.Name = others.Name.Remove(others.Name.Length - 1);
				if (others.Name != "Other")
				{
					result.Add(others);
				}


				return Json(result);
			}
            else
            {
                return null;
            }
        }
        
        public async Task<JsonResult> LoadCitiesForRegionAndCountry(int regionId, int countryId)
        {
			using (var httpClient = new HttpClient())
            {
				_getCitiesByRegionAndCountryIdResponse = await _usersRequestExecutor.GetCitiesByRegionAndCountryIdAction(httpClient, _usersRequestBuilder.GetCitiesByRegionAndCountryIdRequestBuilder(_baseUsersURI, regionId, countryId));
            }
            if (_getCitiesByRegionAndCountryIdResponse != null)
            {
				return Json(_getCitiesByRegionAndCountryIdResponse.Body);
			}
            else
            {
                return null;
            }
		} 
    }
}
