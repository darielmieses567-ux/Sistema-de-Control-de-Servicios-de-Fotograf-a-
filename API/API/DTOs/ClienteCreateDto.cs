
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class ClienteCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Apellido { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Telefono { get; set; }

        [EmailAddress]
        [StringLength(150)]
        public string? Correo { get; set; }
    }
}
