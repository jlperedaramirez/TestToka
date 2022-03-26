namespace TestTokaJlpr.Models
{
    public enum Tipo { ERROR, OK }
    public class ObjRespuesta
    {
        public Tipo TipoRespuesta { get; set; }
        public string Mensaje { get; set; }
    }
}