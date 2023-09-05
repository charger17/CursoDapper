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
            var sql = $@"UPDATE Cliente SET Nombres = @Nombres, Apellidos = @Apellidos, Telefono = @Telefono, Email = @Email, Pais = @Pais"
                + " WHERE IdCliente = @IdCliente";
            _bd.Execute(sql, cliente);

            return cliente;
        }

        public Cliente AgregarCliente(Cliente cliente)
        {
            //Opcion 1
            var sql = $"INSERT INTO Cliente(Nombres, Apellidos, Telefono, Email, Pais, FechaCreacion) VALUES (@Nombres, @Apellidos, @Telefono, @Email, @Pais, @FechaCreacion);"
                 + "SELECT CAST(SCOPE_IDENTITY() as int);";
            var id = _bd.Query<int>(sql, new {
                cliente.Nombres,
                cliente.Apellidos,
                cliente.Telefono,
                cliente.Email,
                cliente.Pais,
                cliente.FechaCreacion
            }).SingleOrDefault();

            cliente.IdCliente = id;

            return cliente;

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
