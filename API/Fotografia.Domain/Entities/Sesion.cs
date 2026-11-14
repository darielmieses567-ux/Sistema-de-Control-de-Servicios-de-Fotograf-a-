
using Fotografia.Domain.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fotografia.Domain.Entities
{
    public class Sesion : BaseEntity
    {
        public DateTime FechaSesion { get; set; }

        [StringLength(200)]
        public string? Lugar { get; set; }

        [StringLength(50)]
        public string Estado { get; set; } = "Pendiente";

        [Range(1, int.MaxValue)]
        public int DuracionMinutos { get; set; } = 60;

        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal PrecioAcordado { get; set; }

        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal MontoPagado { get; set; }

        [NotMapped]
        public decimal SaldoPendiente =>
            PrecioAcordado - MontoPagado;

        public DateTime? FechaEstimadaEntrega { get; set; }

        public DateTime? FechaEntrega { get; set; }

        public bool Entregado { get; set; }

        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

        public int FotografoId { get; set; }
        public Fotografo? Fotografo { get; set; }

        public int PaqueteId { get; set; }
        public Paquete? Paquete { get; set; }


        public ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    }
}