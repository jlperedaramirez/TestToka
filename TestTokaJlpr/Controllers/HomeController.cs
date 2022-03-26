using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using TestTokaJlpr.Models;

namespace TestTokaJlpr.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private string urlBase = "";


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            var buildeer = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            urlBase = buildeer.GetSection("UrlApi").Value;
        }

        public async Task<ActionResult> Index()
        {
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

        public async Task<ActionResult> PersonasFisicasList()
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync(urlBase + "PersonasFisicas/lstPersonasFis?activo=true");
            var personasList = JsonConvert.DeserializeObject<List<PersonasFisicasModel>>(json);
            return View(personasList);
        }
    }
}