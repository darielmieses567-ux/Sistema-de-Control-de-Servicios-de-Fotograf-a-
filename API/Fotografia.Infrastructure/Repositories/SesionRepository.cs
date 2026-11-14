
using Microsoft.EntityFrameworkCore;
using Fotografia.Domain.Entities;
using Fotografia.Domain.Repository;
using Fotografia.Infrastructure.Context;
using Fotografia.Infrastructure.Core;

namespace Fotografia.Infrastructure.Repositories
{
    public class SesionRepository :
        BaseRepository<Sesion>, ISesionRepository
    {
        public SesionRepository(FotografiaContext context)
            : base(context)
        {
        }


        public async Task<List<Sesion>> ObtenerPorFotografoAsync(
            int fotografoId)
        {
            return await _context.Sesiones
                .Where(s => s.FotografoId == fotografoId)
                .OrderBy(s => s.FechaSesion)
                .ToListAsync();
        }

        public async Task<List<Sesion>> ObtenerPorClienteAsync(
            int clienteId)
        {
            return await _context.Sesiones
                .Where(s => s.ClienteId == clienteId)
                .OrderBy(s => s.FechaSesion)
                .ToListAsync();
        }


        public async Task<List<Sesion>> ObtenerPendientesEntregaAsync()
        {
            return await _context.Sesiones
                .Where(s =>
                    !s.Entregado &&
                    s.Estado != "Cancelada")
                .OrderBy(s => s.FechaEstimadaEntrega)
                .ToListAsync();
        }
    }
}
