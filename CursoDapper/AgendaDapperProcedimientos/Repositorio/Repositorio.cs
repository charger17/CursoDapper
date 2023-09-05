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
            var parametros = new DynamicParameters();
            parametros.Add("@ClienteId", cliente.IdCliente, DbType.Int32);
            parametros.Add("@Nombres", cliente.Nombres);
            parametros.Add("@Apellidos", cliente.Apellidos);
            parametros.Add("@Telefono", cliente.Telefono);
            parametros.Add("@Email", cliente.Email);
            parametros.Add("@Pais", cliente.Pais);
            parametros.Add("@FechaCreacion", cliente.FechaCreacion, DbType.DateTime, ParameterDirection.Input);

            _bd.Execute("sp_ActualizarCliente", parametros, commandType: CommandType.StoredProcedure);

            return cliente;
        }

        public Cliente AgregarCliente(Cliente cliente)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@ClienteId", 0, DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@Nombres", cliente.Nombres);
            parametros.Add("@Apellidos", cliente.Apellidos);
            parametros.Add("@Telefono", cliente.Telefono);
            parametros.Add("@Email", cliente.Email);
            parametros.Add("@Pais", cliente.Pais);
            parametros.Add("@FechaCreacion", cliente.FechaCreacion, DbType.DateTime, ParameterDirection.Input);

            _bd.Execute("sp_CrearCliente", parametros, commandType: CommandType.StoredProcedure);

            cliente.IdCliente = parametros.Get<int>("ClienteId");
            return cliente;
        }

        public void BorrarCliente(int id)
        {
            _bd.Execute("sp_BorrarCliente", new { ClienteId = id }, commandType: CommandType.StoredProcedure);
        }

        public Cliente GetCliente(int id)
        {
            return _bd.Query<Cliente>("sp_ClienteId", new {ClienteId = id} ,commandType: CommandType.StoredProcedure).SingleOrDefault();
        }

        public List<Cliente> GetClientes()
        {
            return _bd.Query<Cliente>("sp_GetClientes", commandType: CommandType.StoredProcedure).ToList(); 
        }
    }
}
