using Game.Core.Database.Records.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Game.Database.Entity.Configurations
{
    public class UserTelegramConfiguration : IEntityTypeConfiguration<UserTelegram>
    {
        public void Configure(EntityTypeBuilder<UserTelegram> builder)
        {
            builder.HasKey(x => x.UserId);

            builder.Property(x => x.TelegramId)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Username)
                .HasMaxLength(32);

            builder.Property(x => x.FirstName)
                .HasMaxLength(50);

            builder.Property(x => x.LastName)
                .HasMaxLength(50);

            builder.Property(x => x.Phone)
                .HasMaxLength(15);

            builder.Property(x => x.Language)
                .HasMaxLength(10);
        }
    }
}