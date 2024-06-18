using Game.Core.Database.Records.Camp;
using Game.Core.Database.Records.ScheduledTask;
using Game.Core.Database.Records.Things;
using Game.Core.Database.Records.Users;
using Game.Database.Entity.Configurations.Camp;
using Game.Database.Entity.Configurations.Users;
using Microsoft.EntityFrameworkCore;

namespace Game.Database.Context
{
    public class GameDbContext : DbContext
    {
        public DbSet<GlobalTask> GlobalTasks { get; set; }
        public DbSet<IndividualTask> IndividualTasks { get; set; }
        public DbSet<LogExecuteGlobalTask> LogExecuteTasks { get; set; } 
        public DbSet<LogExecuteIndividualTask> LogIndividualTasks { get; set; } 

        public DbSet<UserRecord> Users { get; set; }
        public DbSet<UserCamp> UserCamps { get; set; }
        public DbSet<UserResources> UserResources { get; set; }
        public DbSet<UserTelegram> UserTelegrams { get; set; }
        public DbSet<UserStatistics> UserStatistics { get; set; }

        public DbSet<CharacterRecord> UserCharacters { get; set; }
        public DbSet<ItemRecord> UserItems { get; set; }

        public DbSet<CampBuilding> CampBuildings { get; set; }

        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserCampConfiguration());
            modelBuilder.ApplyConfiguration(new UserCharacterConfiguration());
            modelBuilder.ApplyConfiguration(new UserItemConfiguration());
            modelBuilder.ApplyConfiguration(new UserResourcesConfiguration());
            modelBuilder.ApplyConfiguration(new UserStatisticsConfiguration());
            modelBuilder.ApplyConfiguration(new UserTelegramConfiguration());
            modelBuilder.ApplyConfiguration(new CampBuildingsConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
