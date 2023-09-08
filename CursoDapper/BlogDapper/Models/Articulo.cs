using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace BlogDapper.Models
{
    public class Articulo
    {
        [Key]
        public int IdArticulo { get; set; }

        [Required(ErrorMessage = "El {0} de categoria es obligatorio.")]
        public string Titulo { get; set; }

        [Required]
        [StringLength(1000, MinimumLength =10, ErrorMessage = "La {0} debe tener {2} y máximo {1} de caracteres")]
        public string Descripcion { get; set; }

        public string Imagen { get; set; }

        public bool Estado { get; set; }

        public DateTime FechaCreacion { get; set; }

        //Llave foránea
        [Required(ErrorMessage = "El nombre de categoria es obligatorio.")]
        public int CategoriaId { get; set; }
        
        //Esta indica la relación con categoría de que un artículo debe pertenecer a una sola categoría
        public virtual Categoria Categoria { get; set; }

        public List<Comentario> Comentario { get; set; }

        public List<Etiqueta> Etiqueta { get; set; }

    }
}
