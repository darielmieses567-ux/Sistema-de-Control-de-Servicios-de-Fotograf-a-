using System.ComponentModel.DataAnnotations;
using WebApplication1.models;

namespace GestionAcademicaAPI.Models
{
    public class Curso
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(250)]
        public string? Descripcion { get; set; }

        public ICollection<Estudiante> Estudiantes { get; set; }
            = new List<Estudiante>();
    }
}