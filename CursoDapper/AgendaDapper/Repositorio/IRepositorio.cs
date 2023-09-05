using AgendaDapper.Models;

namespace AgendaDapper.Repositorio
{
    public interface IRepositorio
    {
        Cliente GetCliente(int id);

        List<Cliente> GetClientes();

        Cliente AgregarCliente(Cliente cliente);

        Cliente ActualizarCliente(Cliente cliente);

        void BorrarCliente(int id);
    }
}
