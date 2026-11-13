
using Fotografia.Domain.Entities;
using Fotografia.Infrastructure.Context;
using Fotografia.Infrastructure.Core;

namespace Fotografia.Infrastructure.Repositories
{
    public class FotografoRepository : BaseRepository<Fotografo>
    {
        public FotografoRepository(FotografiaContext context)
            : base(context)
        {
        }
    }
}
