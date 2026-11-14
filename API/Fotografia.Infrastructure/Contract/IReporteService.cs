
using Fotografia.Infrastructure.Dtos;
using Fotografia.Infrastructure.Services;

namespace Fotografia.Infrastructure.Contract
{
    public interface IReporteService
    {
        Task<decimal> ObtenerIngresosAsync(
            DateTime fechaInicio,
            DateTime fechaFin);

        Task<Dictionary<int, int>>
            ObtenerSesionesPorFotografoAsync();

        Task<List<ReporteSaldoPendiente>>
            ObtenerPagosPendientesAsync();
    }
}
