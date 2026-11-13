
using Fotografia.Domain.Entities;
using Fotografia.Infrastructure.Context;
using Fotografia.Infrastructure.Core;

namespace Fotografia.Infrastructure.Repositories
{
    public class PaqueteRepository : BaseRepository<Paquete>
    {
        public PaqueteRepository(FotografiaContext context)
            : base(context)
        {
        }
    }
}
