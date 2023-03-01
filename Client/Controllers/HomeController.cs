using Client.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private IConfiguration _configuration;

		private readonly string _FreelanceURI;
		private readonly string _UsersURI;
		private readonly string _FileServerURI;

		public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;

			_FreelanceURI = _configuration.GetValue<string>("FrelanceAPI");
			_UsersURI = _configuration.GetValue<string>("UsersAPI");
			_FileServerURI = _configuration.GetValue<string>("FileServerAPI");
		}

        public IActionResult Index()
        {
            ViewData["BaseImagesPath"] = _FileServerURI + "/Freelance/BaseImages/";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}