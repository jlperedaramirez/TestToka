using TestTokaJlpr.Models;
using System.Data.SqlClient;
using System.Data;

namespace TestTokaJlpr.Data
{
    public class PersonaFisicaData
    {
        public List<PersonasFisicasModel> listaPersonasFisicas()
        {
            var objLista = new List<PersonasFisicasModel>();
            var con = new ConexionSQL();

            using (var conexion = new SqlConnection(con.getConStringSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ListaPersonasFisicas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dataRead = cmd.ExecuteReader())
                {
                    while(dataRead.Read())
                    {
                        objLista.Add(new PersonasFisicasModel()
                        {
                            Id = Convert.ToInt32(dataRead["id"]),
                            Nombre = dataRead["Nombre"].ToString(),
                            ApellidoPaterno = dataRead["ApellidoPaterno"].ToString(),
                            ApellidoMaterno = dataRead["ApellidoMaterno"].ToString(),
                            RFC = dataRead["RFC"].ToString(),
                            FechaNacimiento = Convert.ToDateTime(dataRead["FechaNacimiento"]),
                            IdUsuarioAdd = Convert.ToInt32(dataRead["IdUsuarioAdd"]),
                            Activo = Convert.ToBoolean(dataRead["Activo"]),
                            FechaRegistro = Convert.ToDateTime(dataRead["FechaRegistro"]),
                            FechaActualizacion = Convert.ToDateTime(dataRead["FechaActualizacion"])
                        });
                    }
                }
            return objLista;
            }
        }

        public List<PersonasFisicasModel> buscarPersonasFisicas(string Nombre, string ApellidoPaterno, string ApellidoMaterno,
            string RFC)
        {
            var objLista = new List<PersonasFisicasModel>();
            var con = new ConexionSQL();

            using (var conexion = new SqlConnection(con.getConStringSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_AgregarPersonaFisica", conexion);
                cmd.Parameters.AddWithValue("Nombre", Nombre);
                cmd.Parameters.AddWithValue("ApellidoPaterno", ApellidoPaterno);
                cmd.Parameters.AddWithValue("ApellidoMaterno", ApellidoMaterno);
                cmd.Parameters.AddWithValue("RFC", RFC);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dataRead = cmd.ExecuteReader())
                {
                    while (dataRead.Read())
                    {
                        objLista.Add(new PersonasFisicasModel()
                        {
                            Id = Convert.ToInt32(dataRead["id"]),
                            Nombre = dataRead["Nombre"].ToString(),
                            ApellidoPaterno = dataRead["ApellidoPaterno"].ToString(),
                            ApellidoMaterno = dataRead["ApellidoMaterno"].ToString(),
                            RFC = dataRead["RFC"].ToString(),
                            FechaNacimiento = Convert.ToDateTime(dataRead["FechaNacimiento"]),
                            IdUsuarioAdd = Convert.ToInt32(dataRead["IdUsuarioAdd"]),
                            Activo = Convert.ToBoolean(dataRead["Activo"]),
                            FechaRegistro = Convert.ToDateTime(dataRead["FechaRegistro"]),
                            FechaActualizacion = Convert.ToDateTime(dataRead["FechaActualizacion"])
                        });
                    }
                }
                return objLista;
            }
        }

        public string guardarPersonasFisicas(string Nombre, string ApellidoPaterno, string ApellidoMaterno,
            string RFC, string FechaNacimiento, int IdUsuarioAdd)
        {
            string ret = string.Empty;
            var objLista = new List<PersonasFisicasModel>();
            try
            {
                var con = new ConexionSQL();

                using (var conexion = new SqlConnection(con.getConStringSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_BuscarPersonaFisica", conexion);
                    cmd.Parameters.AddWithValue("Nombre", Nombre);
                    cmd.Parameters.AddWithValue("ApellidoPaterno", ApellidoPaterno);
                    cmd.Parameters.AddWithValue("ApellidoMaterno", ApellidoMaterno);
                    cmd.Parameters.AddWithValue("RFC", RFC);
                    cmd.Parameters.AddWithValue("FechaNacimiento", FechaNacimiento);
                    cmd.Parameters.AddWithValue("IdUsuarioAdd", IdUsuarioAdd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
            } catch(Exception ex)
            {
                ret = ex.Message;
            }
            return ret;
        }

        public string editarPersonasFisicas(int Id, string Nombre, string ApellidoPaterno, string ApellidoMaterno,
            string RFC, string FechaNacimiento, int IdUsuarioAdd)
        {
            string ret = string.Empty;
            var objLista = new List<PersonasFisicasModel>();
            try
            {
                var con = new ConexionSQL();

                using (var conexion = new SqlConnection(con.getConStringSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_ActualizarPersonaFisica", conexion);
                    cmd.Parameters.AddWithValue("Id", Id);
                    cmd.Parameters.AddWithValue("Nombre", Nombre);
                    cmd.Parameters.AddWithValue("ApellidoPaterno", ApellidoPaterno);
                    cmd.Parameters.AddWithValue("ApellidoMaterno", ApellidoMaterno);
                    cmd.Parameters.AddWithValue("RFC", RFC);
                    cmd.Parameters.AddWithValue("FechaNacimiento", FechaNacimiento);
                    cmd.Parameters.AddWithValue("IdUsuarioAdd", IdUsuarioAdd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    
                }
            }
            catch (Exception ex)
            {
                ret = ex.Message;
            }
            return ret;
        }

        public string borrarPersonasFisicas(int Id)
        {
            string ret = string.Empty;
            var objLista = new List<PersonasFisicasModel>();
            try
            {
                var con = new ConexionSQL();

                using (var conexion = new SqlConnection(con.getConStringSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EliminarPersonaFisica", conexion);
                    cmd.Parameters.AddWithValue("Id", Id);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                ret = ex.Message;
            }
            return ret;
        }

    }
}
