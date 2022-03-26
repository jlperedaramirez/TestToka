using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using TestTokaJlpr.Models;

namespace TestTokaJlpr.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<PersonasFisicasController> _logger;
        private string urlBase = "";
        HttpResponseMessage response = new HttpResponseMessage();

        public LoginController(ILogger<PersonasFisicasController> logger)
        {
            _logger = logger;
            var buildeer = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            urlBase = buildeer.GetSection("UrlApi").Value;
        }

        public async Task<ActionResult<Usuarios>> Login(LoginModel login)
        {
            Usuarios user = new Usuarios();
            string pathView = "";
            if (login.Email == null)
            {
                return View();
            }
            else
            {
                user.Email = login.Email;
                user.Password = login.Password;
                string JsonCre = JsonConvert.SerializeObject(user);
                var httpClient = new HttpClient();
                response = httpClient.PostAsync(urlBase + "Usuarios/login", new StringContent(JsonCre.ToString(), Encoding.UTF8, "application/json")).Result;
                var JsonResult = response.Content.ReadAsStringAsync().Result;
                var usuarioLogin = JsonConvert.DeserializeObject<Usuarios>(JsonResult.ToString());

                pathView = "../Home/Index";
                //if (usuarioLogin != null)
                //{
                //    pathView = "../Home/Index";
                //}
            }
            
            return View(pathView, user);
        }


    }
}
