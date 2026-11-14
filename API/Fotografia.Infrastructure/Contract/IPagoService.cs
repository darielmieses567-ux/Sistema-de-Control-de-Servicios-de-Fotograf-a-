
using Fotografia.Domain.Entities;

namespace Fotografia.Infrastructure.Contract
{
    public interface IPagoService
    {
        Task RegistrarPagoAsync(
            int sesionId,
            decimal monto,
            string metodoPago,
            string? observacion = null);

        Task<List<Pago>> ObtenerPagosAsync(int sesionId);

        Task<decimal> ObtenerSaldoPendienteAsync(int sesionId);
    }
}
