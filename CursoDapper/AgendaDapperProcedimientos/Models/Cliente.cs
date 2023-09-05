using System.ComponentModel.DataAnnotations;

namespace AgendaDapper.Models
{
    [Dapper.Contrib.Extensions.Table("Cliente")]
    public class Cliente
    {
        [Dapper.Contrib.Extensions.Key]
        public int IdCliente { get; set; }

        [Required]
        public string Nombres { get; set; }

        [Required]
        public string Apellidos { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public int Telefono { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Pais { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Creación")]
        public DateTime FechaCreacion { get; set; }
    }
}
