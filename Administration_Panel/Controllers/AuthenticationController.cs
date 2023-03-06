using Microsoft.AspNetCore.Mvc;

namespace Administration_Panel.Controllers
{
	public class AuthenticationController : Controller
	{
		public IActionResult Login()
		{
			return View();
		}
		public IActionResult SignUp()
		{
			return View();
		}
	}
}
