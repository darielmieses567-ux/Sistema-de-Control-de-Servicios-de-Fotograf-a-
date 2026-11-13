using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class CursoCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(250)]
        public string? Descripcion { get; set; }
    }
}