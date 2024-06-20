using Game.Core.Database.Records.Things;
using Game.Core.Resources.Enums.Game;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Game.Database.Entity.Configurations.Things
{
    public class CharacterRecordConfiguration : IEntityTypeConfiguration<CharacterRecord>
    {
        public void Configure(EntityTypeBuilder<CharacterRecord> builder)
        {
            builder.HasKey(x => new { x.UserId, x.CharacterId });

            builder.HasOne(x => x.User)
                .WithMany(u => u.Characters)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.CharacterId)
                .HasConversion(
                    v => v.ToString(),
                    v => (ECharacter)Enum.Parse(typeof(ECharacter), v));
        }
    }
}
