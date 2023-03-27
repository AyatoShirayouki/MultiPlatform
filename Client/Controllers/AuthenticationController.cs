using Client.RestComunication.Users;
using Client.RestComunication.Users.Responses.AddressInfo.Addresses;
using Client.RestComunication.Users.Responses.AddressInfo.Cities;
using Client.RestComunication.Users.Responses.AddressInfo.Countries;
using Client.RestComunication.Users.Responses.AddressInfo.Regions;
using Client.RestComunication.Users.Responses.Authentication;
using Client.ViewModels.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Security.Policy;
using Users_ApplicationService.DTOs;
using Users_ApplicationService.DTOs.AddressInfo;
using Users_ApplicationService.DTOs.Authentication;
using Users_Data.Entities;

namespace Client.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly UsersRequestBuilder _usersRequestBuilder;
        private UsersRequestExecutor _usersRequestExecutor;

        private GetAllCountriesResponse _getAllCountriesResponse;
        private GetRegionsByCountryIdResponse _getRegionsByCountryIdResponse;
        private GetCitiesByRegionAndCountryIdResponse _getCitiesByRegionAndCountryIdResponse;
        private SignUpUserResponse _signUpUserResponse;
		private LoginUserResponse _loginUserResponse;

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
            _signUpUserResponse= new SignUpUserResponse();
			_loginUserResponse= new LoginUserResponse();

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
			//==============Validations===============
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			try
			{
				MailAddress m = new MailAddress(model.Email);
			}
			catch (FormatException)
			{
				ModelState.AddModelError("Email-format", "Email is not in correct format.");
				return View(model);
			}
			//==============Validations===============

			using (var httpClient = new HttpClient())
			{
				_loginUserResponse = await _usersRequestExecutor.LoginAction(httpClient, _usersRequestBuilder.LoginRequestBuilder(_baseUsersURI, model.Email, model.Password));
			}

			if (_loginUserResponse != null && int.Parse(_loginUserResponse.Code.ToString()) == 201)
			{
				UserDTO user = _loginUserResponse.Body;

				this.HttpContext.Session.SetString("AccountType", user.AccountType.ToString());
				this.HttpContext.Session.SetString("Email", user.Email);
				this.HttpContext.Session.SetString("Id", user.Id.ToString());
				
				if (user.AccountType == 1)
				{
					this.HttpContext.Session.SetString("FirstName", user.FirstName);
					this.HttpContext.Session.SetString("LastName", user.LastName);
					this.HttpContext.Session.SetString("Gender", user.Gender);
					this.HttpContext.Session.SetString("DOB", user.DOB.ToString());
				}
				else
				{
					if (user.IsCompany == true)
					{
						this.HttpContext.Session.SetString("CompanyName", user.CompanyName);
					}
					else
					{
						this.HttpContext.Session.SetString("FirstName", user.FirstName);
						this.HttpContext.Session.SetString("LastName", user.LastName);
						this.HttpContext.Session.SetString("Gender", user.Gender);
						this.HttpContext.Session.SetString("DOB", user.DOB.ToString());
					}
				}

				return RedirectToAction("Index", "Home");
			}
			else
			{
				ModelState.AddModelError("AuthenticationFailed", "Email or password is incorrect.");
				return View(model);
			}
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
			bool validated = true;

			//==============Validations===============
			if (model.AccountType == 1)
			{
				if (string.IsNullOrEmpty(model.FirstName))
				{
					ModelState.AddModelError("FirstName-empty", "First Name field is required.");
					validated = false;
				}
				if (string.IsNullOrEmpty(model.LastName))
				{
					ModelState.AddModelError("LastName-empty", "Last Name field is required.");
					validated = false;
				}
				if (model.DOB == DateTime.MinValue)
				{
					ModelState.AddModelError("DOB-empty", "Date of birth field is required.");
					validated = false;
				}
			}
			else if (model.AccountType == 2)
			{
				if (model.IsCompany == true)
				{
					if (string.IsNullOrEmpty(model.CompanyName))
					{
						ModelState.AddModelError("CompanyName-empty", "Company Name field is required.");
						validated = false;
					}
				}
				else
				{
					if (string.IsNullOrEmpty(model.FirstName))
					{
						ModelState.AddModelError("FirstName-empty", "First Name field is required.");
						validated = false;
					}
					if (string.IsNullOrEmpty(model.LastName))
					{
						ModelState.AddModelError("LastName-empty", "Last Name field is required.");
						validated = false;
					}
					if (model.DOB == DateTime.MinValue)
					{
						ModelState.AddModelError("DOB-empty", "Date of birth field is required.");
						validated = false;
					}
				}
			}
			if (string.IsNullOrEmpty(model.Email))
			{
				ModelState.AddModelError("Email-empty", "Email field is required.");
				validated = false;
			}
			if (string.IsNullOrEmpty(model.Password))
			{
				ModelState.AddModelError("Password-empty", "Password field is required.");
				validated = false;
			}
			if (string.IsNullOrEmpty(model.ConfirmPassword))
			{
				ModelState.AddModelError("ConfirmPassword-empty", "Confirm Password field is required.");
				validated = false;
			}
			if (string.IsNullOrEmpty(model.AddressInfo))
			{
				ModelState.AddModelError("AddressInfo-empty", "Address Info field is required.");
				validated = false;
			}
			if (model.Password != model.ConfirmPassword)
			{
				ModelState.AddModelError("ConfirmPassword-mismatch", "Confirm Password is not equal to Password.");
				validated = false;
			}
			if (model.CountryId == 0)
			{
				ModelState.AddModelError("CountryId-empty", "Please select a country.");
				validated = false;
			}
			if (model.RegionId == 0)
			{
				ModelState.AddModelError("RegionId-empty", "Please select a state.");
				validated = false;
			}
			if (model.CityId == 0)
			{
				ModelState.AddModelError("CityId-empty", "Please select a city.");
				validated = false;
			}
			try
			{
				MailAddress m = new MailAddress(model.Email);
			}
			catch (FormatException)
			{
				ModelState.AddModelError("Email-format", "Email is not in correct format.");
				validated = false;
			}
			//==============Validations===============

			if (validated == false)
			{
				return View(model);
			}

			UserDTO user = new UserDTO
			{
				Email= model.Email,//
				FirstName= model.FirstName,//
				LastName= model.LastName,//
				Password= model.Password,//
				IsCompany= model.IsCompany,///
				AccountType= model.AccountType,///
				DOB = model.DOB,//
				Gender= model.Gender,///
				LinkedInAccount= model.LinkedInAccount,///
				PhoneNumber= model.PhoneNumber,///
				Description= model.Description,///
				CompanyName= model.CompanyName,///
			};

			SignUpDTO request = new SignUpDTO();

			request.CountryId = model.CountryId;
			request.RegionId = model.RegionId;
			request.CityId = model.CityId;
			request.AddressInfo = model.AddressInfo;
			request.User = user;

			using (var httpClient = new HttpClient())
			{
				_signUpUserResponse = await _usersRequestExecutor.SignUpAction(httpClient, request, _usersRequestBuilder.SignUpRequestBuilder(_baseUsersURI));
			}

			if (_signUpUserResponse != null)
			{
				if (int.Parse(_signUpUserResponse.Code.ToString()) == 201)
				{
					return RedirectToAction("Login", "Authentication");
				}
				else
				{
					ModelState.AddModelError("AuthenticationFailed", "SignUp failed.");
					return View(model);
				}
			}
			else
			{
				ModelState.AddModelError("AuthenticationFailed", "SignUp failed.");
				return View(model);
			}
			
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
