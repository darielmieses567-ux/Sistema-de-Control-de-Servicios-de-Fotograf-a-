
using Fotografia.Domain.Core;
using System.ComponentModel.DataAnnotations;

namespace Fotografia.Domain.Entities
{
    public class Pago : BaseEntity
    {
        public int SesionId { get; set; }

        public Sesion? Sesion { get; set; }

        public DateTime FechaPago { get; set; } = DateTime.Now;

        [Range(typeof(decimal), "0.01", "9999999999999999.99")]
        public decimal Monto { get; set; }

        [Required]
        [StringLength(50)]
        public string MetodoPago { get; set; } = "Efectivo";

        [StringLength(200)]
        public string? Observacion { get; set; }
    }
}
