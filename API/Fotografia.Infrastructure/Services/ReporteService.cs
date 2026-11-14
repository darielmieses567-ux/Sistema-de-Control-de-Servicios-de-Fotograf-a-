
using Fotografia.Infrastructure.Context;
using Fotografia.Infrastructure.Contract;
using Fotografia.Infrastructure.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Fotografia.Infrastructure.Services
{
    public class ReporteService : IReporteService
    {
        private readonly FotografiaContext _context;

        public ReporteService(FotografiaContext context)
        {
            _context = context;
        }

        // Obtener ingresos por periodo
        public async Task<decimal> ObtenerIngresosAsync(
            DateTime fechaInicio,
            DateTime fechaFin)
        {
            // Validar el rango de fechas
            if (fechaFin < fechaInicio)
            {
                throw new ArgumentException(
                    "La fecha final no puede ser anterior a la inicial.");
            }

            // Calcular los ingresos registrados
            return await _context.Pagos
                .Where(p =>
                    p.FechaPago >= fechaInicio &&
                    p.FechaPago <= fechaFin)
                .SumAsync(p => (decimal?)p.Monto) ?? 0m;
        }

        // Obtener cantidad de sesiones por fotógrafo
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

        // Obtener las sesiones con pagos pendientes
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
}
