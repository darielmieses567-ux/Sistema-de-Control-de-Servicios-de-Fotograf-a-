
using Fotografia.Domain.Core;

namespace Fotografia.Domain.Repository
{
    public interface IRepository<T>
        where T : BaseEntity
    {
        Task<List<T>> ObtenerTodosAsync();

        Task<T?> ObtenerPorIdAsync(int id);

        Task AgregarAsync(T entidad);

        Task ActualizarAsync(T entidad);

        Task EliminarAsync(int id);
    }
}
