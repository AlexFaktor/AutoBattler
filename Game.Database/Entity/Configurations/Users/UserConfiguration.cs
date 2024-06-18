﻿using Game.Core.Database.Records.Users;
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
                .WithOne()
                .HasForeignKey<UserTelegram>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Каскадне видалення

            builder.HasOne(x => x.Resources)
                .WithOne()
                .HasForeignKey<UserResources>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Каскадне видалення

            builder.HasOne(x => x.Statistics)
                .WithOne()
                .HasForeignKey<UserStatistics>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Каскадне видалення

            builder.HasOne(x => x.Camp)
                .WithOne()
                .HasForeignKey<UserCamp>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Каскадне видалення

            builder.HasMany(x => x.Characters)
                .WithOne()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Каскадне видалення

            builder.HasMany(x => x.Items)
                .WithOne()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Каскадне видалення
        }
    }
}