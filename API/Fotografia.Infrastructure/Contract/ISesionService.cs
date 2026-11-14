
using Fotografia.Domain.Entities;

namespace Fotografia.Infrastructure.Contract
{
    public interface ISesionService
    {
        Task RegistrarSesionAsync(Sesion sesion);

        Task ReprogramarSesionAsync(
            int sesionId,
            DateTime nuevaFecha);

        Task CancelarSesionAsync(int sesionId);
    }
}
