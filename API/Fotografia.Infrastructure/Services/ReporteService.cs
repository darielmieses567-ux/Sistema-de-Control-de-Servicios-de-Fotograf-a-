
using Fotografia.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Fotografia.Infrastructure.Services
{
    public class ReporteService
    {
        private readonly FotografiaContext _context;

        public ReporteService(FotografiaContext context)
        {
            _context = context;
        }

        // Total de ingresos recibidos
        public async Task<decimal> ObtenerIngresosAsync(
            DateTime fechaInicio,
            DateTime fechaFin)
        {
            if (fechaFin < fechaInicio)
                throw new ArgumentException(
                    "La fecha final no puede ser anterior a la inicial.");

            return await _context.Pagos
                .Where(p =>
                    p.FechaPago >= fechaInicio &&
                    p.FechaPago <= fechaFin)
                .SumAsync(p => (decimal?)p.Monto) ?? 0m;
        }

        // Cantidad de sesiones por fotógrafo
        public async Task<Dictionary<int, int>>
            ObtenerSesionesPorFotografoAsync()
        {
            return await _context.Sesiones
                .AsNoTracking()
                .GroupBy(s => s.FotografoId)
                .Select(g => new
                {
                    FotografoId = g.Key,
                    Total = g.Count()
                })
                .ToDictionaryAsync(
                    x => x.FotografoId,
                    x => x.Total);
        }

        // Sesiones con saldo pendiente
        public async Task<List<ReporteSaldoPendiente>>
            ObtenerPagosPendientesAsync()
        {
            return await _context.Sesiones
                .AsNoTracking()
                .Where(s =>
                    s.Estado != "Cancelada" &&
                    s.PrecioAcordado > s.MontoPagado)
                .Select(s => new ReporteSaldoPendiente
                {
                    SesionId = s.Id,
                    ClienteId = s.ClienteId,
                    PrecioAcordado = s.PrecioAcordado,
                    MontoPagado = s.MontoPagado,
                    SaldoPendiente =
                        s.PrecioAcordado - s.MontoPagado
                })
                .ToListAsync();
        }
    }

    public class ReporteSaldoPendiente
    {
        public int SesionId { get; set; }
        public int ClienteId { get; set; }
        public decimal PrecioAcordado { get; set; }
        public decimal MontoPagado { get; set; }
        public decimal SaldoPendiente { get; set; }
    }
}
