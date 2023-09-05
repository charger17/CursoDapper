using AgendaDapper.Models;
using Dapper.Contrib.Extensions;
using System.Data;
using System.Data.SqlClient;

namespace AgendaDapper.Repositorio
{
    public class RepositorioContrib : IRepositorio
    {
        private readonly IDbConnection _bd;

        public RepositorioContrib(IConfiguration configuration)
        {
            _bd = new SqlConnection(configuration.GetConnectionString("ConexionSQLLocalDB"));
        }

        public Cliente ActualizarCliente(Cliente cliente)
        {
            _bd.Update<Cliente>(cliente);
            return cliente;
        }

        public Cliente AgregarCliente(Cliente cliente)
        {
            var id = _bd.Insert<Cliente>(cliente);
            cliente.IdCliente = Convert.ToInt32(id);
            return cliente;
        }

        public void BorrarCliente(int id)
        {
            _bd.Delete<Cliente>(new Cliente { IdCliente = id });
        }

        public Cliente GetCliente(int id)
        {
            return _bd.Get<Cliente>(id);
        }

        public List<Cliente> GetClientes()
        {
            return _bd.GetAll<Cliente>().ToList();
        }
    }
}
