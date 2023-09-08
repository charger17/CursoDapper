using BlogDapper.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogDapper.Repositorio
{
    public interface IComentarioRepositorio
    {
        Comentario GetComentario(int id);

        List<Comentario> GetComentarios();

        Comentario CrearComentario(Comentario comentario);

        Comentario ActualizarComentario(Comentario comentario);

        void BorrarComentario(int id);

        //Se agrega nuevo método para obtener la relacion entre artículo y comentario
        List<Comentario> GetComentarioArticulo();
    }
}
