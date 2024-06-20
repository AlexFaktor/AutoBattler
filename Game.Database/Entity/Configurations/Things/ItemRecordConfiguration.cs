using Game.Core.Database.Records.Things;
using Game.Core.Resources.Enums.Game;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Game.Database.Entity.Configurations.Things
{
    public class ItemRecordConfiguration : IEntityTypeConfiguration<ItemRecord>
    {
        public void Configure(EntityTypeBuilder<ItemRecord> builder)
        {
            builder.HasKey(x => new { x.UserId, x.ItemId });

            builder.HasOne(x => x.User)
                .WithMany(u => u.Items)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.ItemId)
                .HasConversion(
                    v => v.ToString(),
                    v => (EItem)Enum.Parse(typeof(EItem), v));
        }
    }
}
