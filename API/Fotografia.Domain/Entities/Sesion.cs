using Fotografia.Domain.Core;
using System.ComponentModel.DataAnnotations;

namespace Fotografia.Domain.Entities
{
    public class Sesion : BaseEntity
    {
        public DateTime FechaSesion { get; set; }

        [StringLength(200)]
        public string? Lugar { get; set; }

        [StringLength(50)]
        public string Estado { get; set; } = "Pendiente";

        public decimal MontoPagado { get; set; }

        public DateTime? FechaEntrega { get; set; }

        public bool Entregado { get; set; }

        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

        public int FotografoId { get; set; }
        public Fotografo? Fotografo { get; set; }

        public int PaqueteId { get; set; }
        public Paquete? Paquete { get; set; }
    }
}
