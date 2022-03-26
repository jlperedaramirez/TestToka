namespace webAPI.Models
{
    public class PersonasFisicas
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? RFC { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public Boolean Activo { get; set; }
        public int IdUsuarioAdd { get; set; }
    }
}
