using Game.Core.Database.Records.ScheduledTask;
using Game.Core.Resources.Enums.ScheduledTask;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Game.Database.Entity.Configurations.ScheduledTask
{
    public class IndividualTaskConfiguration : IEntityTypeConfiguration<IndividualTask>
    {
        public void Configure(EntityTypeBuilder<IndividualTask> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Type)
                   .HasConversion(
                    v => v.ToString(),
                    v => (EIndidualTask)Enum.Parse(typeof(EIndidualTask), v))
                .IsRequired();
        }
    }
}
