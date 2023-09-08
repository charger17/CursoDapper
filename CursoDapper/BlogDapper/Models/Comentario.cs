using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace BlogDapper.Models
{
    public class Comentario
    {
        [Key]
        public int IdComentario { get; set; }

        [Required(ErrorMessage = "El {0} de categoria es obligatorio.")]
        public string Titulo { get; set; }

        [Required]
        [StringLength(300, MinimumLength =10, ErrorMessage = "El {0} debe tener {2} y máximo {1} de caracteres")]
        public string Mensaje { get; set; }

        public DateTime FechaCreacion { get; set; }

        //Llave foránea
        [Required(ErrorMessage = "El articulo es obligatorio.")]
        public int ArticuloId { get; set; }
        
        //Esta indica la relación con categoría de que un artículo debe pertenecer a una sola categoría
        public virtual Articulo Articulo { get; set; }

    }
}
