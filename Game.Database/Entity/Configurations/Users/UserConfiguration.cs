using Game.Core.Database.Records.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Game.Database.Entity.Configurations.Users
{
    public class UserConfiguration : IEntityTypeConfiguration<GameUser>
    {
        public void Configure(EntityTypeBuilder<GameUser> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Username)
                .IsRequired()
                .HasMaxLength(16);

            builder.Property(x => x.Hashtag)
                .IsRequired()
                .HasMaxLength(6);

            builder.HasOne(x => x.Telegram)
                .WithOne(t => t.User) // Додаємо точне визначення
                .HasForeignKey<UserTelegram>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Resources)
                .WithOne(r => r.User) // Додаємо точне визначення
                .HasForeignKey<UserResources>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Statistics)
                .WithOne(s => s.User) // Додаємо точне визначення
                .HasForeignKey<UserStatistics>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Camp)
                .WithOne(c => c.User) // Додаємо точне визначення
                .HasForeignKey<UserCamp>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Characters)
                .WithOne(c => c.User) // Додаємо точне визначення
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Items)
                .WithOne(i => i.User) // Додаємо точне визначення
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
