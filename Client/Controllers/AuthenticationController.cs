using Client.ViewModels.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class AuthenticationController : Controller
    {
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

        /*
        public async Task<JsonResult> LoadRegionsForCountry(int id)
        {

        }
        public async Task<JsonResult> LoadCitiesForRegions(int id)
        {

        }
        */
    }
}
