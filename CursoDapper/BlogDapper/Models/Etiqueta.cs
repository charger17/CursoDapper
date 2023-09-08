using System.ComponentModel.DataAnnotations;

namespace BlogDapper.Models
{
    public class Etiqueta
    {
        [Key]
        public int IdEtiqueta { get; set; }

        [Required(ErrorMessage = "EL campo {0} es obligatorio.")]
        public string NombreEtiqeuta { get; set; }

        public DateTime FechaCreacion { get; set; }

        //Esta indica la relacion con artículo, con una tabla intermedia ArticuloEtiquetas
        public List<Articulo> Articulo { get; set; }
    }
}
