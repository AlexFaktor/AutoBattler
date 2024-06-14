using Game.Core.Database.Records.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Game.Database.Entity.Configurations.Users
{
    public class UserStatisticsConfiguration : IEntityTypeConfiguration<UserStatistics>
    {
        public void Configure(EntityTypeBuilder<UserStatistics> builder)
        {
            builder.HasKey(x => x.UserId);

            builder.Property(x => x.DateOfUserRegistration)
                .IsRequired();

            builder.Property(x => x.NumberInteractionsWithBot)
                .IsRequired();
        }
    }
}