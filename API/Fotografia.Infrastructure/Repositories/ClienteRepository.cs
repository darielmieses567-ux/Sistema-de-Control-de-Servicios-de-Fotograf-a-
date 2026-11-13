
using Fotografia.Domain.Entities;
using Fotografia.Infrastructure.Context;
using Fotografia.Infrastructure.Core;

namespace Fotografia.Infrastructure.Repositories
{
    public class ClienteRepository : BaseRepository<Cliente>
    {
        public ClienteRepository(FotografiaContext context)
            : base(context)
        {
        }
    }
}
