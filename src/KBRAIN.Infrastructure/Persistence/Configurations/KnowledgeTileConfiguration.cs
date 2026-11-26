using KBRAIN.Domain.Tiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KBRAIN.Infrastructure.Persistence.Configurations
{
    public class KnowledgeTileConfiguration : IEntityTypeConfiguration<KnowledgeTile>
    {
        public void Configure(EntityTypeBuilder<KnowledgeTile> builder)
        {
            builder.ToTable("knowledge_tiles");

            builder.HasKey(tile => tile.Id);

            builder.Property(tile => tile.Id)
                .ValueGeneratedNever();

            builder.Property(tile => tile.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(tile => tile.Content)
                .IsRequired();
        }
    }
}
