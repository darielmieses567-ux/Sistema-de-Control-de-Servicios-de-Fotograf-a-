using System.ComponentModel.DataAnnotations;

namespace GestionAcademicaAPI.DTOs
{
    public class EstudianteCreateDto
    {
        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string Apellido { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Correo { get; set; } = string.Empty;

        [Range(1, int.MaxValue)]
        public int CursoId { get; set; }
    }
}