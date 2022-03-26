using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using TestTokaJlpr.Models;

namespace TestTokaJlpr.Controllers
{
    public class PersonasFisicasController : Controller
    {
        private readonly ILogger<PersonasFisicasController> _logger;
        private string urlBase = "";
        HttpResponseMessage response = new HttpResponseMessage();

        public PersonasFisicasController(ILogger<PersonasFisicasController> logger)
        {
            _logger = logger;
            var buildeer = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            urlBase = buildeer.GetSection("UrlApi").Value;
        }
        // GET: PersonasFisicasController
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> PersonasFisicasList()
        {
            var httpClient = new HttpClient();
            response = httpClient.GetAsync(urlBase + "PersonasFisicas/lstPersonasFis").Result;
            var JsonResult = response.Content.ReadAsStringAsync().Result;
            var customerList = JsonConvert.DeserializeObject<List<PersonasFisicasModel>>(JsonResult);
            return View(customerList);
        }

        public async Task<ActionResult> BorrarPersona(PersonasFisicasModel Persona)
        {
            var httpClient = new HttpClient();
            response = await httpClient.DeleteAsync(urlBase + "PersonasFisicas/borrarPersonaFis/" + Persona.Id);
            //var borrarPersona = JsonConvert.DeserializeObject<ObjRespuesta>(json.ToString());
            var JsonResult = response.Content.ReadAsStringAsync().Result;
            //var customerList = JsonConvert.DeserializeObject<string>(JsonResult);
            httpClient = new HttpClient();
            response = httpClient.GetAsync(urlBase + "PersonasFisicas/lstPersonasFis").Result;
            JsonResult = response.Content.ReadAsStringAsync().Result;
            var customerList = JsonConvert.DeserializeObject<List<PersonasFisicasModel>>(JsonResult);
            return View("PersonasFisicasList", customerList);
        }

        public async Task<ActionResult> ActivarPersona(PersonasFisicasModel Persona)
        {
            var httpClient = new HttpClient();
            response = await httpClient.PutAsync(urlBase + "PersonasFisicas/activarPersonaFis/" + Persona.Id, null);
            var JsonResult = response.Content.ReadAsStringAsync().Result;
            //var json = JsonConvert.DeserializeObject<string>(JsonResult);
            httpClient = new HttpClient();
            response = httpClient.GetAsync(urlBase + "PersonasFisicas/lstPersonasFis").Result;
            JsonResult = response.Content.ReadAsStringAsync().Result;
            var customerList = JsonConvert.DeserializeObject<List<PersonasFisicasModel>>(JsonResult);
            return View("PersonasFisicasList", customerList);
        }

        public async Task<ActionResult> PersonaFisicaAdd(PersonasFisicasModel Persona)
        {
            if (Persona.Nombre != null) {
                string JsonCre = JsonConvert.SerializeObject(Persona);
                var httpClient = new HttpClient();
                response = httpClient.PostAsync(urlBase + "PersonasFisicas/guardarPersonasFis", new StringContent(JsonCre.ToString(), Encoding.UTF8, "application/json")).Result;
                var JsonResult = response.Content.ReadAsStringAsync().Result;
                //var json = JsonConvert.DeserializeObject<ObjRespuesta>(JsonResult);

                var httpClient1 = new HttpClient();
                response = httpClient1.GetAsync(urlBase + "PersonasFisicas/lstPersonasFis").Result;
                var JsonResult1 = response.Content.ReadAsStringAsync().Result;
                var customerList = JsonConvert.DeserializeObject<List<PersonasFisicasModel>>(JsonResult1);
                return View("PersonasFisicasList", customerList);
            } else
            {
                return View();
            }
            

        }

        public async Task<ActionResult> PersonaFisicaEdit(PersonasFisicasModel Persona)
        {
            if (Persona.Nombre != null)
            {
                string JsonCre = JsonConvert.SerializeObject(Persona);
                var httpClient = new HttpClient();
                response = httpClient.PutAsync(urlBase + "PersonasFisicas/editarPersonasFis", new StringContent(JsonCre.ToString(), Encoding.UTF8, "application/json")).Result;
                var JsonResult = response.Content.ReadAsStringAsync().Result;
                var editarPersona = JsonConvert.DeserializeObject<ObjRespuesta>(JsonResult);

            }
            var httpClient1 = new HttpClient();
            response = httpClient1.GetAsync(urlBase + "PersonasFisicas/lstPersonasFis").Result;
            var JsonResult1 = response.Content.ReadAsStringAsync().Result;
            var customerList = JsonConvert.DeserializeObject<List<PersonasFisicasModel>>(JsonResult1);
            return View("PersonasFisicasList", customerList);
        }

        public async Task<ActionResult> BuscarPersonaFisica(int Id)
        {
            var httpClient = new HttpClient();
            response = httpClient.GetAsync(urlBase + "PersonasFisicas/buscarPersonaFis/" + Id).Result;
            var JsonResult = response.Content.ReadAsStringAsync().Result;
            var buscarPersona = JsonConvert.DeserializeObject<PersonasFisicasModel>(JsonResult);

            return View("PersonaFisicaEdit", buscarPersona);
        }

    }
}
