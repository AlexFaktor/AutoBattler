using Game.Core.Database.Records.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Game.Database.Entity.Configurations
{
    public class UserCharacterConfiguration : IEntityTypeConfiguration<UserCharacter>
    {
        public void Configure(EntityTypeBuilder<UserCharacter> builder)
        {
            builder.HasKey(x => new { x.UserId, x.CharacterId });

            builder.Property(x => x.CharacterId)
                .IsRequired();
        }
    }
}
