
using Fotografia.Domain.Entities;

namespace Fotografia.Infrastructure.Contract
{
    public interface IEntregaService
    {
        Task ProgramarEntregaAsync(
            int sesionId,
            DateTime fechaEstimada);

        Task MarcarEntregadaAsync(int sesionId);

        Task<List<Sesion>> ObtenerPendientesAsync();

        Task<List<Sesion>> ObtenerAtrasadasAsync();
    }
}
