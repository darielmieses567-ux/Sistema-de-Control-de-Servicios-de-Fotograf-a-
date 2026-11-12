using Fotografia.Domain.Core;
using System.ComponentModel.DataAnnotations;
using static System.Collections.Specialized.BitVector32;

namespace Fotografia.Domain.Entities
{
    public class Paquete : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(300)]
        public string? Descripcion { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Precio { get; set; }

        public int CantidadFotos { get; set; }

        public ICollection<Sesion> Sesiones { get; set; }
            = new List<Sesion>();
    }
}