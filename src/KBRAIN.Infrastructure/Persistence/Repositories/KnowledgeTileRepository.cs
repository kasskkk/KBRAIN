using KBRAIN.Application.Tiles;
using KBRAIN.Domain.Tiles;
using Microsoft.EntityFrameworkCore;

namespace KBRAIN.Infrastructure.Persistence.Repositories
{
    public class KnowledgeTileRepository(AppDbContext dbContext) : IKnowledgeTileRepository
    {
        public async Task<IReadOnlyList<KnowledgeTile>> GetPinnedAsync(CancellationToken cancellationToken)
        {
            var tiles = await dbContext.KnowledgeTiles
                .AsNoTracking()
                .Where(x => x.IsPinned)
                .OrderBy(x => x.CreatedAt)
                .ToListAsync(cancellationToken);

            return tiles;
        }
    }
}
