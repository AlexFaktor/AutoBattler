using Game.Core.Database.Records.Camp;
using Game.Core.Resources.Enums.Game;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Game.Database.Entity.Configurations.Camp
{
    public class CampBuildingsConfiguration : IEntityTypeConfiguration<CampBuilding>
    {
        public void Configure(EntityTypeBuilder<CampBuilding> builder)
        {
            builder.HasKey(x => x.UserId);

            builder.Property(x => x.BuildingId)
                .HasConversion(
                    v => v.ToString(),
                    v => (EBuildings)Enum.Parse(typeof(EBuildings), v));
        }
    }
}
