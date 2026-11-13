
using Fotografia.Domain.Entities;
using Fotografia.Domain.Interfaces;

namespace Fotografia.Domain.Repository
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<bool> ExisteCorreoAsync(string correo);

        Task<bool> ExisteTelefonoAsync(string telefono);
    }
}
