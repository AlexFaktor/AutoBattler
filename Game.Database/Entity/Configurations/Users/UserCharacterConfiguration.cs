using Game.Core.Database.Records.Camp;
using Game.Core.Database.Records.Users;
using Game.Core.Resources.Enums.Game;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Game.Database.Entity.Configurations.Users
{
    public class UserCharacterConfiguration : IEntityTypeConfiguration<UserCharacter>
    {
        public void Configure(EntityTypeBuilder<UserCharacter> builder)
        {
            builder.HasKey(x => new { x.UserId, x.CharacterId });

            builder.HasOne(x => x.User)
                   .WithOne()
                   .HasForeignKey<UserCharacter>(x => x.UserId)
                   .IsRequired();

            builder.Property(x => x.CharacterId)
                .HasConversion(
                    v => v.ToString(),
                    v => (ECharacter)Enum.Parse(typeof(ECharacter), v));
        }
    }
}
