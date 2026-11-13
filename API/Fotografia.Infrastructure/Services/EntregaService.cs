
using Fotografia.Domain.Repository;
using Fotografia.Domain.Entities;

namespace Fotografia.Infrastructure.Services
{
    public class EntregaService
    {
        private readonly ISesionRepository _sesionRepository;

        public EntregaService(ISesionRepository sesionRepository)
        {
            _sesionRepository = sesionRepository;
        }

        // Asignar fecha estimada de entrega
        public async Task ProgramarEntregaAsync(
            int sesionId,
            DateTime fechaEstimada)
        {
            var sesion = await _sesionRepository
                .ObtenerPorIdAsync(sesionId);

            if (sesion == null)
                throw new InvalidOperationException(
                    "La sesión no existe.");

            if (sesion.Estado == "Cancelada")
                throw new InvalidOperationException(
                    "No se puede programar una entrega cancelada.");

            if (sesion.Entregado)
                throw new InvalidOperationException(
                    "Esta sesión ya fue entregada.");

            if (fechaEstimada < sesion.FechaSesion)
                throw new ArgumentException(
                    "La entrega no puede ser anterior a la sesión.");

            sesion.FechaEstimadaEntrega = fechaEstimada;

            await _sesionRepository.ActualizarAsync(sesion);
        }

        // Marcar una sesión como entregada
        public async Task MarcarEntregadaAsync(int sesionId)
        {
            var sesion = await _sesionRepository
                .ObtenerPorIdAsync(sesionId);

            if (sesion == null)
                throw new InvalidOperationException(
                    "La sesión no existe.");

            if (sesion.Estado == "Cancelada")
                throw new InvalidOperationException(
                    "No se puede entregar una sesión cancelada.");

            if (sesion.Entregado)
                throw new InvalidOperationException(
                    "Esta sesión ya fue entregada.");

            sesion.Entregado = true;
            sesion.FechaEntrega = DateTime.Now;

            await _sesionRepository.ActualizarAsync(sesion);
        }

        // Consultar entregas pendientes
        public async Task<List<Sesion>> ObtenerPendientesAsync()
        {
            return await _sesionRepository
                .ObtenerPendientesEntregaAsync();
        }

        // Consultar entregas atrasadas
        public async Task<List<Sesion>> ObtenerAtrasadasAsync()
        {
            var pendientes = await _sesionRepository
                .ObtenerPendientesEntregaAsync();

            DateTime ahora = DateTime.Now;

            return pendientes
                .Where(s =>
                    s.FechaEstimadaEntrega.HasValue &&
                    s.FechaEstimadaEntrega.Value < ahora)
                .OrderBy(s => s.FechaEstimadaEntrega)
                .ToList();
        }
    }
}
