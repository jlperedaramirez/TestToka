using webAPI.Models;
using System.Data.SqlClient;
using System.Data;

namespace webAPI.Data
{
    public class PersonaFisicaData
    {
        public List<PersonasFisicas> listaPersonasFisicas()
        {
            var objLista = new List<PersonasFisicas>();
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
                        objLista.Add(new PersonasFisicas()
                        {
                            Id = Convert.ToInt32(dataRead["id"]),
                            Nombre = dataRead["Nombre"].ToString(),
                            ApellidoPaterno = dataRead["ApellidoPaterno"].ToString(),
                            ApellidoMaterno = dataRead["ApellidoMaterno"].ToString(),
                            RFC = dataRead["RFC"].ToString(),
                            FechaNacimiento = Convert.ToDateTime(dataRead["FechaNacimiento"]),
                            IdUsuarioAdd = Convert.ToInt32(dataRead["IdUsuarioAdd"]),
                            Activo = Convert.ToBoolean(dataRead["Activo"]),
                            FechaRegistro = Convert.ToDateTime(dataRead["FechaRegistro"])
                            ,FechaActualizacion = dataRead["FechaActualizacion"].ToString() == ""? null : Convert.ToDateTime(dataRead["FechaActualizacion"].ToString())
                        });
                    }
                }
            return objLista;
            }
        }

        public List<PersonasFisicas> buscarPersonasFisicas(PersonasFisicas persona)
        {
            var objLista = new List<PersonasFisicas>();
            var con = new ConexionSQL();

            using (var conexion = new SqlConnection(con.getConStringSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_BuscarPersonaFisica", conexion);
                cmd.Parameters.AddWithValue("Nombre", persona.Nombre.ToUpper());
                cmd.Parameters.AddWithValue("ApellidoPaterno", persona.ApellidoPaterno.ToUpper());
                cmd.Parameters.AddWithValue("ApellidoMaterno", persona.ApellidoMaterno.ToUpper());
                cmd.Parameters.AddWithValue("RFC", persona.RFC);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dataRead = cmd.ExecuteReader())
                {
                    while (dataRead.Read())
                    {
                        objLista.Add(new PersonasFisicas()
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

        public ObjRespuesta guardarPersonasFisicas(PersonasFisicas persona)
        {
            ObjRespuesta ret = new ObjRespuesta();
            var objLista = new List<PersonasFisicas>();
            try
            {
                var con = new ConexionSQL();

                using (var conexion = new SqlConnection(con.getConStringSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_AgregarPersonaFisica", conexion);
                    cmd.Parameters.AddWithValue("Nombre", persona.Nombre.ToUpper());
                    cmd.Parameters.AddWithValue("ApellidoPaterno", persona.ApellidoPaterno.ToUpper());
                    cmd.Parameters.AddWithValue("ApellidoMaterno", persona.ApellidoMaterno.ToUpper());
                    cmd.Parameters.AddWithValue("RFC", persona.RFC.ToUpper());
                    cmd.Parameters.AddWithValue("FechaNacimiento", persona.FechaNacimiento);
                    cmd.Parameters.AddWithValue("IdUsuarioAdd", persona.IdUsuarioAdd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    string[] retSQL = cmd.ExecuteScalar().ToString().Split('-');
                    ret.Mensaje = retSQL[1];
                    ret.TipoRespuesta = retSQL[0].Equals("0")? Tipo.ERROR : Tipo.OK;
                }
            } catch(Exception ex)
            {
                ret.Mensaje = ex.Message;
                ret.TipoRespuesta = Tipo.ERROR;
            }
            return ret;
        }

        public ObjRespuesta editarPersonasFisicas(PersonasFisicas persona)
        {
            ObjRespuesta ret = new ObjRespuesta();
            var objLista = new List<PersonasFisicas>();
            try
            {
                var con = new ConexionSQL();

                using (var conexion = new SqlConnection(con.getConStringSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_ActualizarPersonaFisica", conexion);
                    cmd.Parameters.AddWithValue("Id", persona.Id);
                    cmd.Parameters.AddWithValue("Nombre", persona.Nombre.ToUpper());
                    cmd.Parameters.AddWithValue("ApellidoPaterno", persona.ApellidoPaterno.ToUpper());
                    cmd.Parameters.AddWithValue("ApellidoMaterno", persona.ApellidoMaterno.ToUpper());
                    cmd.Parameters.AddWithValue("RFC", persona.RFC.ToUpper());
                    cmd.Parameters.AddWithValue("FechaNacimiento", persona.FechaNacimiento);
                    cmd.Parameters.AddWithValue("IdUsuarioAdd", persona.IdUsuarioAdd);
                    cmd.CommandType = CommandType.StoredProcedure;
                    string[] retSQL = cmd.ExecuteScalar().ToString().Split('-');
                    ret.Mensaje = retSQL[1];
                    ret.TipoRespuesta = retSQL[0].Equals("0") ? Tipo.ERROR : Tipo.OK;

                }
            }
            catch (Exception ex)
            {
                ret.Mensaje = ex.Message;
                ret.TipoRespuesta = Tipo.ERROR;
            }
            return ret;
        }

        public ObjRespuesta borrarPersonasFisicas(int Id)
        {
            ObjRespuesta ret = new ObjRespuesta();
            var objLista = new List<PersonasFisicas>();
            try
            {
                var con = new ConexionSQL();

                using (var conexion = new SqlConnection(con.getConStringSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EliminarPersonaFisica", conexion);
                    cmd.Parameters.AddWithValue("Id", Id);
                    cmd.CommandType = CommandType.StoredProcedure;
                    string[] retSQL = cmd.ExecuteScalar().ToString().Split('-');
                    ret.Mensaje = retSQL[1];
                    ret.TipoRespuesta = retSQL[0].Equals("0") ? Tipo.ERROR : Tipo.OK;

                }
            }
            catch (Exception ex)
            {
                ret.Mensaje = ex.Message;
                ret.TipoRespuesta = Tipo.ERROR;
            }
            return ret;
        }

        public ObjRespuesta activarPersonasFisicas(int Id)
        {
            ObjRespuesta ret = new ObjRespuesta();
            var objLista = new List<PersonasFisicas>();
            try
            {
                var con = new ConexionSQL();

                using (var conexion = new SqlConnection(con.getConStringSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_ActivarPersonaFisica", conexion);
                    cmd.Parameters.AddWithValue("Id", Id);
                    cmd.CommandType = CommandType.StoredProcedure;
                    string[] retSQL = cmd.ExecuteScalar().ToString().Split('-');
                    ret.Mensaje = retSQL[1];
                    ret.TipoRespuesta = retSQL[0].Equals("0") ? Tipo.ERROR : Tipo.OK;

                }
            }
            catch (Exception ex)
            {
                ret.Mensaje = ex.Message;
                ret.TipoRespuesta = Tipo.ERROR;
            }
            return ret;
        }

        public PersonasFisicas buscarPersonaFisica(int Id)
        {
            PersonasFisicas ret = new PersonasFisicas();
            var con = new ConexionSQL();

            using (var conexion = new SqlConnection(con.getConStringSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_BuscarPersonaFisica", conexion);
                cmd.Parameters.AddWithValue("Id", Id);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dataRead = cmd.ExecuteReader())
                {
                    while (dataRead.Read())
                    {
                        ret.Nombre = dataRead["Nombre"].ToString();
                        ret.Id = Convert.ToInt32(dataRead["id"]);
                        ret.Nombre = dataRead["Nombre"].ToString();
                        ret.ApellidoPaterno = dataRead["ApellidoPaterno"].ToString();
                        ret.ApellidoMaterno = dataRead["ApellidoMaterno"].ToString();
                        ret.RFC = dataRead["RFC"].ToString();
                        ret.FechaNacimiento = Convert.ToDateTime(dataRead["FechaNacimiento"]);
                        ret.IdUsuarioAdd = Convert.ToInt32(dataRead["IdUsuarioAdd"]);
                        ret.Activo = Convert.ToBoolean(dataRead["Activo"]);
                        ret.FechaRegistro = Convert.ToDateTime(dataRead["FechaRegistro"]);
                    }
                }
                return ret;
            }
        }

    }
}
