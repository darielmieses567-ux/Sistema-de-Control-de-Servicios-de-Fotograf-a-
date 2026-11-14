
using Fotografia.Domain.Entities;
using Fotografia.Domain.Interfaces;
using Fotografia.Domain.Repository;

namespace Fotografia.Infrastructure.Services
{
    public class SesionService
    {
        private readonly ISesionRepository _sesionRepository;
        private readonly IFotografoRepository _fotografoRepository;
        private readonly IRepository<Paquete> _paqueteRepository;
        private readonly IClienteRepository _clienteRepository;

        public SesionService(
            ISesionRepository sesionRepository,
            IFotografoRepository fotografoRepository,
            IRepository<Paquete> paqueteRepository,
            IClienteRepository clienteRepository)
        {
            _sesionRepository = sesionRepository;
            _fotografoRepository = fotografoRepository;
            _paqueteRepository = paqueteRepository;
            _clienteRepository = clienteRepository;
        }

        public async Task RegistrarSesionAsync(Sesion sesion)
        {
            if (sesion == null)
                throw new ArgumentNullException(nameof(sesion));

            if (sesion.DuracionMinutos <= 0)
                throw new ArgumentException(
                    "La duración debe ser mayor que cero.");

            if (sesion.FechaSesion <= DateTime.Now)
                throw new ArgumentException(
                    "La sesión debe programarse para una fecha futura.");

            var cliente = await _clienteRepository
                .ObtenerPorIdAsync(sesion.ClienteId);

            if (cliente == null)
                throw new InvalidOperationException(
                    "El cliente seleccionado no existe.");

            var fotografo = await _fotografoRepository
                .ObtenerPorIdAsync(sesion.FotografoId);

            if (fotografo == null)
                throw new InvalidOperationException(
                    "El fotógrafo seleccionado no existe.");

            var paquete = await _paqueteRepository
                .ObtenerPorIdAsync(sesion.PaqueteId);

            if (paquete == null)
                throw new InvalidOperationException(
                    "El paquete seleccionado no existe.");

            DateTime fechaFin = sesion.FechaSesion
                .AddMinutes(sesion.DuracionMinutos);

            bool existeChoque = await _fotografoRepository
                .TieneSesionEnHorarioAsync(
                    sesion.FotografoId,
                    sesion.FechaSesion,
                    fechaFin);

            if (existeChoque)
                throw new InvalidOperationException(
                    "El fotógrafo ya tiene una sesión en ese horario.");

            // Guardar el precio acordado al reservar
            sesion.PrecioAcordado = paquete.Precio;
            sesion.MontoPagado = 0;
            sesion.Estado = "Pendiente";
            sesion.Entregado = false;

            await _sesionRepository.AgregarAsync(sesion);
        }

        public async Task ReprogramarSesionAsync(
            int sesionId,
            DateTime nuevaFecha)
        {
            var sesion = await _sesionRepository
                .ObtenerPorIdAsync(sesionId);

            if (sesion == null)
                throw new InvalidOperationException(
                    "La sesión no existe.");

            if (sesion.Estado == "Cancelada")
                throw new InvalidOperationException(
                    "No se puede reprogramar una sesión cancelada.");

            if (nuevaFecha <= DateTime.Now)
                throw new ArgumentException(
                    "La nueva fecha debe ser futura.");

            DateTime nuevaFechaFin = nuevaFecha
                .AddMinutes(sesion.DuracionMinutos);

            bool existeChoque = await _fotografoRepository
                .TieneSesionEnHorarioAsync(
                    sesion.FotografoId,
                    nuevaFecha,
                    nuevaFechaFin,
                    sesion.Id);

            if (existeChoque)
                throw new InvalidOperationException(
                    "El fotógrafo ya está ocupado en ese horario.");

            sesion.FechaSesion = nuevaFecha;

            await _sesionRepository.ActualizarAsync(sesion);
        }

        public async Task CancelarSesionAsync(int sesionId)
        {
            var sesion = await _sesionRepository
                .ObtenerPorIdAsync(sesionId);

            if (sesion == null)
                throw new InvalidOperationException(
                    "La sesión no existe.");

            if (sesion.Estado == "Cancelada")
                throw new InvalidOperationException(
                    "La sesión ya está cancelada.");

            if (sesion.Entregado)
                throw new InvalidOperationException(
                    "No se puede cancelar una sesión entregada.");

            sesion.Estado = "Cancelada";

            await _sesionRepository.ActualizarAsync(sesion);
        }
    }
}
