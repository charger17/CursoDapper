using System.ComponentModel.DataAnnotations;

namespace BlogDapper.Models
{
    public class Etiqueta
    {
        public Etiqueta()
        {
            Articulo = new List<Articulo>();
        }
        [Key]
        public int IdEtiqueta { get; set; }

        [Required(ErrorMessage = "EL campo {0} es obligatorio.")]
        public string NombreEtiqueta { get; set; }

        public DateTime FechaCreacion { get; set; }

        //Esta indica la relacion con artículo, con una tabla intermedia ArticuloEtiquetas
        public List<Articulo> Articulo { get; set; }
    }
}
