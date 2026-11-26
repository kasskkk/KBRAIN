using KBRAIN.Domain.Tiles;
using Microsoft.EntityFrameworkCore;

namespace KBRAIN.Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<KnowledgeTile> KnowledgeTiles => Set<KnowledgeTile>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
