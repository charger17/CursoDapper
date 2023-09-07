using BlogDapper.Models;

namespace BlogDapper.Repositorio
{
    public interface IArticuloRepositorio
    {
        Articulo GetArticulo(int id);

        List<Articulo> GetArticulos();

        Articulo CrearArticulo(Articulo Articulo);

        Articulo ActualizarArticulo(Articulo Articulo);

        void BorrarArticulo(int id);
    }
}
