using KBRAIN.Domain.Tiles;

namespace KBRAIN.Application.Tiles
{
    public interface IKnowledgeTileRepository
    {
        Task<IReadOnlyList<KnowledgeTile>> GetPinnedAsync(CancellationToken cancellationToken);
    }
}
