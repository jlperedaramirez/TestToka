namespace TestTokaJlpr.Data
{
    public class ConexionSQL
    {
        private string ConStringSQL = string.Empty;

        public  ConexionSQL()
        {
            var buildeer = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            ConStringSQL = buildeer.GetSection("ConnectionStrings: ConSQL").Value;
        }

        public string getConStringSQL()
        {
            return ConStringSQL;
        }
    }
}
