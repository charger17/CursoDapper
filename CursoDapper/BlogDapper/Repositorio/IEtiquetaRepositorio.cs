using BlogDapper.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogDapper.Repositorio
{
    public interface IEtiquetaRepositorio
    {
        Etiqueta GetEtiqueta(int id);

        List<Etiqueta> GetEtiquetas();

        Etiqueta CrearEtiqueta(Etiqueta etiqueta);

        Etiqueta ActualizarEtiqueta(Etiqueta etiqueta);

        void BorrarEtiqueta(int id);

        //Metodo especial para el dropdown con la lista de etiquetas en artículos 
        IEnumerable<SelectListItem> GetListaEtiquetas();

        //Método especial para la acción de asignar etiquetas


        //Método especial para obtener los artículos con las etiquetas asigandas
        List<Articulo> GetArticuloEtiquetas();
    }
}
