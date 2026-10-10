using System.ComponentModel.DataAnnotations;

namespace GestionAcademicaAPI.DTOs
{
    public class EstudianteUpdateDto
    {
        [Required]
        [StringLength(80)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(80)]
        public string Apellido { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Correo { get; set; } = string.Empty;

        [Range(1, int.MaxValue)]
        public int CursoId { get; set; }
    }
}
