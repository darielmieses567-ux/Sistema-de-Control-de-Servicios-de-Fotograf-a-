
using Fotografia.Domain.Entities;
using Fotografia.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Fotografia.Infrastructure.Services
{
    public class PagoService
    {
        private readonly FotografiaContext _context;

        public PagoService(FotografiaContext context)
        {
            _context = context;
        }

        // Registrar un anticipo o abono
        public async Task RegistrarPagoAsync(
            int sesionId,
            decimal monto,
            string metodoPago,
            string? observacion = null)
        {
            if (monto <= 0)
                throw new ArgumentException(
                    "El monto debe ser mayor que cero.");

            if (string.IsNullOrWhiteSpace(metodoPago))
                throw new ArgumentException(
                    "Debe indicar un método de pago.");

            // Transacción para guardar pago y total juntos
            await using var transaccion =
                await _context.Database.BeginTransactionAsync(
                    System.Data.IsolationLevel.Serializable);

            var sesion = await _context.Sesiones
                .FromSqlInterpolated(
                    $@"SELECT * FROM dbo.Sesiones WITH (UPDLOCK, HOLDLOCK)
                       WHERE Id = {sesionId}")
                .SingleOrDefaultAsync();

            if (sesion == null)
                throw new InvalidOperationException(
                    "La sesión no existe.");

            if (sesion.Estado == "Cancelada")
                throw new InvalidOperationException(
                    "No se pueden registrar pagos en una sesión cancelada.");

            decimal saldo = sesion.PrecioAcordado - sesion.MontoPagado;

            if (monto > saldo)
                throw new InvalidOperationException(
                    $"El pago supera el saldo pendiente de {saldo:C}.");

            var pago = new Pago
            {
                SesionId = sesionId,
                FechaPago = DateTime.Now,
                Monto = monto,
                MetodoPago = metodoPago.Trim(),
                Observacion = observacion
            };

            _context.Pagos.Add(pago);

            sesion.MontoPagado += monto;

            await _context.SaveChangesAsync();
            await transaccion.CommitAsync();
        }

        // Consultar pagos de una sesión
        public async Task<List<Pago>> ObtenerPagosAsync(
            int sesionId)
        {
            return await _context.Pagos
                .AsNoTracking()
                .Where(p => p.SesionId == sesionId)
                .OrderBy(p => p.FechaPago)
                .ToListAsync();
        }

        // Consultar el saldo pendiente
        public async Task<decimal> ObtenerSaldoPendienteAsync(
            int sesionId)
        {
            var sesion = await _context.Sesiones
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == sesionId);

            if (sesion == null)
                throw new InvalidOperationException(
                    "La sesión no existe.");

            return sesion.PrecioAcordado - sesion.MontoPagado;
        }
    }
}
