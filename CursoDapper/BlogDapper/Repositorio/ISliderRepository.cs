using BlogDapper.Models;

namespace BlogDapper.Repositorio
{
    public interface ISliderRepository
    {
        public List<Slider> GetSliders();

        public List<Articulo> GetArticulosForSlider();
    }
}
