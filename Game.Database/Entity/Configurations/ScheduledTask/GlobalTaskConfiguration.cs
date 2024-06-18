using Game.Core.Database.Records.ScheduledTask;
using Game.Core.Resources.Enums.ScheduledTask;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Game.Database.Entity.Configurations.ScheduledTask
{
    public class GlobalTaskConfiguration : IEntityTypeConfiguration<GlobalTask>
    {
        public void Configure(EntityTypeBuilder<GlobalTask> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Type)
                   .HasConversion(
                    v => v.ToString(),
                    v => (EGlobalTask)Enum.Parse(typeof(EGlobalTask), v))
                .IsRequired();
        }
    }
}
