
using Fotografia.Domain.Entities;
using Fotografia.Infrastructure.Context;
using Fotografia.Infrastructure.Core;

namespace Fotografia.Infrastructure.Repositories
{
    public class SesionRepository : BaseRepository<Sesion>
    {
        public SesionRepository(FotografiaContext context)
            : base(context)
        {
        }
    }
}
