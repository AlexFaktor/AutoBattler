using Game.Core.Database.Records.Users;
using Game.Core.Resources.Enums.Game;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Game.Database.Entity.Configurations.Users
{
    public class UserItemConfiguration : IEntityTypeConfiguration<UserItem>
    {
        public void Configure(EntityTypeBuilder<UserItem> builder)
        {
            builder.HasKey(x => new { x.UserId, x.ItemId });

            builder.HasOne(x => x.User)
                   .WithOne()
                   .HasForeignKey<UserItem>(x => x.UserId)
                   .IsRequired();

            builder.Property(x => x.ItemId)
                .HasConversion(
                    v => v.ToString(),
                    v => (EItem)Enum.Parse(typeof(EItem), v));
        }
    }
}
