using AgendaDapper.Models;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace AgendaDapper.Repositorio
{
    public class Repositorio : IRepositorio
    {
        private readonly IDbConnection _bd;

        public Repositorio(IConfiguration configuration)
        {
            _bd = new SqlConnection(configuration.GetConnectionString("ConexionSQLLocalDB"));
        }

        public Cliente ActualizarCliente(Cliente cliente)
        {
            throw new NotImplementedException();
        }

        public Cliente AgregarCliente(Cliente cliente)
        {
            throw new NotImplementedException();
        }

        public void BorrarCliente(int id)
        {
            var sql = "DELETE FROM Cliente WHERE IdCliente = @IdCliente";
            _bd.Execute(sql, new {@IdCliente = id});
        }

        public Cliente GetCliente(int id)
        {
            var sql = $"SELECT * FROM Cliente WHERE IdCliente = @IdCliente";
            return _bd.Query<Cliente>(sql, new { @IdCliente = id }).SingleOrDefault();
        }

        public List<Cliente> GetClientes()
        {
            var sql = "SELECT * FROM Cliente";

            return _bd.Query<Cliente>(sql).ToList(); ;
        }
    }
}
