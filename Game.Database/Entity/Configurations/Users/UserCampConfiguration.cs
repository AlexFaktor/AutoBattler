using Game.Core.Database.Records.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Game.Database.Entity.Configurations.Users
{
    public class UserCampConfiguration : IEntityTypeConfiguration<UserCamp>
    {
        public void Configure(EntityTypeBuilder<UserCamp> builder)
        {
            builder.HasKey(x => x.UserId);

            builder.HasOne(x => x.User)
                   .WithOne()
                   .HasForeignKey<UserCamp>(x => x.UserId)
                   .IsRequired();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
