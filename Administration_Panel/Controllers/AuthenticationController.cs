using Administration_Panel.RestComunication.Admins;
using Administration_Panel.RestComunication.Admins.Responses.Authentication;
using Administration_Panel.ViewModels.Authentication;
using Admins_ApplicationService.DTOs;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Users_Data.Entities;

namespace Administration_Panel.Controllers
{
	public class AuthenticationController : Controller
	{
		private readonly AdminsRequestBuilder _adminsRquestBuilder;
        private AdminsRequestExecutor _adminsRequestExecutor;

		private LoginAdminsResponse _loginAdminsResponse;
		private LogoutAdminsResponse _logoutAdminsResponse;
		private SignUpAdminsResponse _signUpAdminsResponse;

        private IConfiguration _configuration;
        private IHttpContextAccessor _httpContextAccessor;

        private readonly string _baseAdminsURI;

        public AuthenticationController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _baseAdminsURI = _configuration.GetValue<string>("AdminsAPI");
            _adminsRquestBuilder = new AdminsRequestBuilder();

            _loginAdminsResponse = new LoginAdminsResponse();
            _logoutAdminsResponse = new LogoutAdminsResponse();
            _signUpAdminsResponse = new SignUpAdminsResponse();

            _httpContextAccessor = httpContextAccessor;
            _adminsRequestExecutor = new AdminsRequestExecutor(_httpContextAccessor);
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
            if (this.HttpContext.Session.GetString("AdminId") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            AdminDTO? adminDTO = new AdminDTO();

            using (var httpClient = new HttpClient())
            {
                _loginAdminsResponse = await _adminsRequestExecutor.LoginAction(httpClient,
                    _adminsRquestBuilder.LoginRequestBuilder(_baseAdminsURI,
                model.Email, model.Password));

                adminDTO = _loginAdminsResponse.Body;
            }

            if (adminDTO != null)
            {
                this.HttpContext.Session.SetString("AdminId", adminDTO.Id.ToString());
                this.HttpContext.Session.SetString("AdminEmail", adminDTO.Email);
                this.HttpContext.Session.SetString("AdminFirstName", adminDTO.FirstName);
                this.HttpContext.Session.SetString("AdminLastName", adminDTO.LastName);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("AuthenticationFailed", "Wrong email or password!");
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
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("AuthenticationFailed", "Password doesn't match Confirm Password!");
                return View(model);
            }

            AdminDTO adminDTO = new AdminDTO();

            adminDTO.Email = model.Email;
            adminDTO.Password = model.Password;
            adminDTO.FirstName = model.FirstName;
            adminDTO.LastName = model.LastName;
            adminDTO.DOB = model.DOB;

            using (var httpClient = new HttpClient())
            {
                _signUpAdminsResponse = await _adminsRequestExecutor.SignUpAction(httpClient, adminDTO, _adminsRquestBuilder.SignUpRequestBuilder(_baseAdminsURI));
            }

            if (int.Parse(_signUpAdminsResponse.Code.ToString()) == 201)
            {
                return RedirectToAction("Login", "Authentication");
            }
            else
            {
                return View(model);
            }
        }
    }
}
