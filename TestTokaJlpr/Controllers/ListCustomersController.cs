using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TestTokaJlpr.Models;
using OfficeOpenXml;
using OfficeOpenXml.Table;


namespace TestTokaJlpr.Controllers
{
    public class ListCustomersController : Controller
    {
        HttpResponseMessage response = new HttpResponseMessage();
        private PaginadorGenerico<CustomersModel> _PaginadorCustomers;
        private readonly int _RegistrosPorPagina = 20;
        List<CustomersModel> customers = new List<CustomersModel>();

        // GET: ListCustomersController
        public ActionResult Index(int pagina = 1)
        {
            string token = Token();

            LoginAuthenticate login = new LoginAuthenticate();
            var httpClient = new HttpClient();
            if(token != null)
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            }
            response = httpClient.GetAsync("https://api.toka.com.mx/candidato/api/customers").Result;
            var JsonResult = response.Content.ReadAsStringAsync().Result;
            var customerList = JsonConvert.DeserializeObject<DataModel>(JsonResult);
            //List<CustomersModel> customers = new List<CustomersModel>();
            customers = customerList.Data.ToList();

            string pathView = "../Customers/ListCustomers";

            var students = customers;
            int _TotalRegistros = 0;

            _TotalRegistros = customers.Count;
            var _TotalPaginas = (int)Math.Ceiling((double)_TotalRegistros / _RegistrosPorPagina);
            customers = customers.OrderBy(x => x.IdCliente)
                                                 .Skip((pagina - 1) * _RegistrosPorPagina)
                                                 .Take(_RegistrosPorPagina)
                                                 .ToList();

            _PaginadorCustomers = new PaginadorGenerico<CustomersModel>()
                {
                    RegistrosPorPagina = _RegistrosPorPagina,
                    TotalRegistros = _TotalRegistros,
                    TotalPaginas = _TotalPaginas,
                    PaginaActual = 1,
                    Resultado = customers
                };

            return View(pathView, _PaginadorCustomers);
        }

        public string Token()
        {
            LoginAuthenticate login = new LoginAuthenticate();
            login.Username = "ucand0021";
            login.Password = "yNDVARG80sr@dDPc2yCT!";
            string JsonCre = JsonConvert.SerializeObject(login);
            var httpClient = new HttpClient();
            response = httpClient.PostAsync("https://api.toka.com.mx/candidato/api/login/authenticate", new StringContent(JsonCre.ToString(), Encoding.UTF8, "application/json")).Result;
            var JsonResult = response.Content.ReadAsStringAsync().Result;
            var token = JsonConvert.DeserializeObject<LoginCustomer>(JsonResult.ToString());

            return token.Data;
        }

        public ActionResult Home()
        {
            return View("../Home/Index");
        }

        public class PaginadorGenerico<T> where T : class
        {
            public int PaginaActual { get; set; }
            public int RegistrosPorPagina { get; set; }
            public int TotalRegistros { get; set; }
            public int TotalPaginas { get; set; }
            public IEnumerable<T> Resultado { get; set; }
        }

        public IActionResult ExportarExcel(List<CustomersModel> customers_)
        {
            string excelContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //var productos = _context.Productos.AsNoTracking().ToList();
            using (var libro = new ExcelPackage())
            {
                var worksheet = libro.Workbook.Worksheets.Add("CustomersList");
                worksheet.Cells["A1"].LoadFromCollection(customers_, PrintHeaders: true);
                for (var col = 1; col < customers_.Count + 1; col++)
                {
                    worksheet.Column(col).AutoFit();
                }

                // Agregar formato de tabla
                var tabla = worksheet.Tables.Add(new ExcelAddressBase(fromRow: 1, fromCol: 1, toRow: customers_.Count + 1, toColumn: 5), "CustomersList");
                tabla.ShowHeader = true;
                tabla.TableStyle = TableStyles.Light6;
                tabla.ShowTotal = true;

                return File(libro.GetAsByteArray(), excelContentType, "CustomersList.xlsx");
            }
        }


    }
}
