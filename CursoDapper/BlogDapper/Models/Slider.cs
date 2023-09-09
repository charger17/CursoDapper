using System.ComponentModel.DataAnnotations;

namespace BlogDapper.Models
{
    public class Slider
    {
        [Key]
        public int IdSlider { get; set; }

        [Required(ErrorMessage = "EL campo {0} es obligatorio.")]
        public string Nombre { get; set; }

        public string UrlImagen { get; set; }

    }
}
