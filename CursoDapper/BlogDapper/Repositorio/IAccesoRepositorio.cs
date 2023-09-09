using BlogDapper.Models;
using System.Security.Claims;

namespace BlogDapper.Repositorio
{
    public interface IAccesoRepositorio
    {
        public List<Usuario> UserLogin(Usuario user);

        public bool ValidateUser(Usuario user);

        public void AddUser(Usuario user);
    }
}
