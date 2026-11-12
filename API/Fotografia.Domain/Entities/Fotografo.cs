using Fotografia.Domain.Core;
using System.ComponentModel.DataAnnotations;
using static System.Collections.Specialized.BitVector32;

namespace Fotografia.Domain.Entities
{
    public class Fotografo : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Apellido { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Especialidad { get; set; }

        [StringLength(20)]
        public string? Telefono { get; set; }

        public ICollection<Sesion> Sesiones { get; set; }
            = new List<Sesion>();
    }
}