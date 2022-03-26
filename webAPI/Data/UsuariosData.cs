using webAPI.Models;
using System.Data.SqlClient;
using System.Data;

namespace webAPI.Data
{
    public class UsuariosData
    {
        public Usuarios buscarUsuario(string email, string passwordHash)
        {
           Usuarios ret = new Usuarios();
            var con = new ConexionSQL();

            using (var conexion = new SqlConnection(con.getConStringSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_BuscarUsuario", conexion);
                cmd.Parameters.AddWithValue("Email", email);
                //cmd.Parameters.AddWithValue("Pass", passwordHash);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dataRead = cmd.ExecuteReader())
                {
                    while (dataRead.Read())
                    {
                        ret.Id = Convert.ToInt32(dataRead["id"]);
                        ret.Login = dataRead["Login"].ToString();
                        ret.Email = dataRead["Email"].ToString();
                        ret.Token = dataRead["Token"].ToString();
                        ret.Password = dataRead["Password"].ToString();
                        ret.FechaRegistro = Convert.ToDateTime(dataRead["FechaRegistro"]);
                    }
                }
                
                return ret;
            }
        }
    }
}
