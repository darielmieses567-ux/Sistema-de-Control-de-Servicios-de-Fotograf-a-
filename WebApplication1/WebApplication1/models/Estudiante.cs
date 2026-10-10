using System.ComponentModel.DataAnnotations;

namespace GestionAcademicaAPI.Models
{
    public class Estudiante
    {
        public int Id { get; set; }

        [Required]
        [StringLength(80)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(80)]
        public string Apellido { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Correo { get; set; } = string.Empty;

        public int CursoId { get; set; }

        public Curso? Curso { get; set; }
    }
}