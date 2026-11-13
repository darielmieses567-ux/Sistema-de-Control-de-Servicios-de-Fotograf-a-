
using Fotografia.Domain.Entities;
using Fotografia.Domain.Interfaces;

namespace Fotografia.Domain.Repository
{
    public interface IFotografoRepository : IRepository<Fotografo>
    {
        Task<bool> TieneSesionEnHorarioAsync(
            int fotografoId,
            DateTime fechaInicio,
            DateTime fechaFin,
            int? sesionExcluirId = null);
    }
}
