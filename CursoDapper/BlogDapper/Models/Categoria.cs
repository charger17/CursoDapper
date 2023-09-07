using System.ComponentModel.DataAnnotations;

namespace BlogDapper.Models
{
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }

        [Required(ErrorMessage = "EL campo {0} es obligatorio.")]
        public string Nombre { get; set; }

        public DateTime FechaCreacion { get; set; }

        //Esta indica la relacion con artículo, donde una categoría puede tener muchos artículos
        public List<Articulo> Articulo { get; set; }
    }
}
