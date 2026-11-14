
namespace Fotografia.Infrastructure.Dtos
{
    public class ReporteSaldoPendiente
    {
        public int SesionId { get; set; }

        public int ClienteId { get; set; }

        public decimal PrecioAcordado { get; set; }

        public decimal MontoPagado { get; set; }

        public decimal SaldoPendiente { get; set; }
    }
}
