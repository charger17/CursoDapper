using BlogDapper.Models;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Security.Claims;

namespace BlogDapper.Repositorio
{
    public class AccesoRepositorio : IAccesoRepositorio
    {
        private readonly IDbConnection _bd;
        public AccesoRepositorio(IConfiguration configuration)
        {
            _bd = new SqlConnection(configuration.GetConnectionString("ConexionSQLLocalDB"));
        }

        public List<Usuario> UserLogin(Usuario user)
        {
            var sql = "SELECT * FROM Usuario WHERE Login=@Login AND Password =@Password";

            //Convertir password a md5 antes de enviar la consulta
            var Password = obtenerMd5(user.Password);

            var validar = _bd.Query<Usuario>(sql, new
            {
                user.Login,
                Password
            });

            return validar.ToList();
        }
    }
}
