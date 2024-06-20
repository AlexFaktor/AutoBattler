using Game.Core.Database.Records.Users;
using Game.Core.Resources.Enums.Telegram;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Game.Database.Entity.Configurations.Users
{
    public class UserTelegramConfiguration : IEntityTypeConfiguration<UserTelegram>
    {
        public void Configure(EntityTypeBuilder<UserTelegram> builder)
        {
            builder.HasKey(x => x.UserId);

            builder.HasOne(x => x.User)
                   .WithOne()
                   .HasForeignKey<UserTelegram>(x => x.UserId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();

            builder.Property(x => x.TelegramId)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (ETelegramUserStatus)Enum.Parse(typeof(ETelegramUserStatus), v))
                .IsRequired();

            builder.Property(x => x.StatusLevel)
                .IsRequired();

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