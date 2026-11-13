
using Microsoft.EntityFrameworkCore;
using Fotografia.Domain.Entities;
using Fotografia.Domain.Repository;
using Fotografia.Infrastructure.Context;
using Fotografia.Infrastructure.Core;

namespace Fotografia.Infrastructure.Repositories
{
    public class FotografoRepository :
        BaseRepository<Fotografo>, IFotografoRepository
    {
        public FotografoRepository(FotografiaContext context)
            : base(context)
        {
        }

        public async Task<bool> TieneSesionEnHorarioAsync(
            int fotografoId,
            DateTime fechaInicio,
            DateTime fechaFin,
            int? sesionExcluirId = null)
        {
            if (fechaFin <= fechaInicio)
            {
                throw new ArgumentException(
                    "La fecha final debe ser posterior a la inicial.");
            }



            return await _context.Sesiones
                .AnyAsync(s =>
                    s.FotografoId == fotografoId &&
                    s.Estado != "Cancelada" &&
                    (!sesionExcluirId.HasValue ||
                     s.Id != sesionExcluirId.Value) &&
                    s.FechaSesion < fechaFin &&
                    EF.Functions.DateDiffMinute(
                        s.FechaSesion, fechaInicio) <
                        s.DuracionMinutos);
        }
    }
}
