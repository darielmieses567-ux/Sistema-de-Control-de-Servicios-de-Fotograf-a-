
using Fotografia.Domain.Entities;
using Fotografia.Domain.Interfaces;

namespace Fotografia.Domain.Repository
{
    public interface ISesionRepository : IRepository<Sesion>
    {
        Task<List<Sesion>> ObtenerPorFotografoAsync(
            int fotografoId);

        Task<List<Sesion>> ObtenerPorClienteAsync(
            int clienteId);

        Task<List<Sesion>> ObtenerPendientesEntregaAsync();
    }
}
