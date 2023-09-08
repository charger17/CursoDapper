using BlogDapper.Models;

namespace BlogDapper.Repositorio
{
    public interface ICategoriaRepositorio
    {
        Categoria GetCategoria(int id);

        List<Categoria> GetCategorias();

        Categoria CrearCategoria(Categoria categoria);

        Categoria ActualizarCategoria(Categoria categoria);

        void BorrarCategoria(int id);

        //Metodo especial para el dropdown con la lista de catergorias en la vista de articulos, se debe crear aqui apra invocarse desde el controlador articulos
    }
}
