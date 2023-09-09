using BlogDapper.Models;

namespace BlogDapper.Repositorio
{
    public interface ISliderRepository
    {
        public List<Slider> GetSliders();

        public List<Articulo> GetArticulosForSlider();

        public List<Categoria> GetCategoriasForSlider();

        public Articulo GetArticuloForSlider(int id);

        public void InsertComentarioForSlider(Comentario comentario);

        public List<Comentario> GetComentariosForSlider(int id);
    }
}
