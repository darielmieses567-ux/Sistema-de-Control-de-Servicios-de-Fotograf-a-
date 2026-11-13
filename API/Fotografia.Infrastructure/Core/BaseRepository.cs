
using Microsoft.EntityFrameworkCore;
using Fotografia.Domain.Core;
using Fotografia.Domain.Repository;
using Fotografia.Infrastructure.Context;

namespace Fotografia.Infrastructure.Core
{
    public class BaseRepository<T> : IRepository<T>
        where T : BaseEntity
    {
        protected readonly FotografiaContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(FotografiaContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<List<T>> ObtenerTodosAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> ObtenerPorIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AgregarAsync(T entidad)
        {
            await _dbSet.AddAsync(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(T entidad)
        {
            _dbSet.Update(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var entidad = await _dbSet.FindAsync(id);

            if (entidad != null)
            {
                _dbSet.Remove(entidad);
                await _context.SaveChangesAsync();
            }
        }
    }
}
