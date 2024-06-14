﻿using Game.Core.Database.Records.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Game.Database.Entity.Configurations
{
    public class UserCampConfiguration : IEntityTypeConfiguration<UserCamp>
    {
        public void Configure(EntityTypeBuilder<UserCamp> builder)
        {
            builder.HasKey(x => x.UserId);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}