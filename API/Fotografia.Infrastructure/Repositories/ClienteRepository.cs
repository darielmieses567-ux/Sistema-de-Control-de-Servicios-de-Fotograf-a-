
using Microsoft.EntityFrameworkCore;
using Fotografia.Domain.Entities;
using Fotografia.Domain.Repository;
using Fotografia.Infrastructure.Context;
using Fotografia.Infrastructure.Core;

namespace Fotografia.Infrastructure.Repositories
{
    public class ClienteRepository :
        BaseRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(FotografiaContext context)
            : base(context)
        {
        }

        public async Task<bool> ExisteCorreoAsync(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
                return false;

            string correoNormalizado =
                correo.Trim().ToLowerInvariant();

            return await _context.Clientes.AnyAsync(
                c => c.Correo != null &&
                     c.Correo.Trim().ToLower() == correoNormalizado);
        }

        public async Task<bool> ExisteTelefonoAsync(string telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono))
                return false;

            string telefonoNormalizado = telefono.Trim();

            return await _context.Clientes.AnyAsync(
                c => c.Telefono != null &&
                     c.Telefono.Trim() == telefonoNormalizado);
        }
    }
}
