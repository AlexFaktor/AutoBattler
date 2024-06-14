using Game.Core.Database.Records.Users;
using Game.Database.Entity.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Game.Database.Context
{
    public class GameDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserCamp> UserCamps { get; set; }
        public DbSet<UserCharacter> UserCharacters { get; set; }
        public DbSet<UserItem> UserItems { get; set; }
        public DbSet<UserResources> UserResources { get; set; }
        public DbSet<UserStatistics> UserStatistics { get; set; }
        public DbSet<UserTelegram> UserTelegrams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserCampConfiguration());
            modelBuilder.ApplyConfiguration(new UserCharacterConfiguration());
            modelBuilder.ApplyConfiguration(new UserItemConfiguration());
            modelBuilder.ApplyConfiguration(new UserResourcesConfiguration());
            modelBuilder.ApplyConfiguration(new UserStatisticsConfiguration());
            modelBuilder.ApplyConfiguration(new UserTelegramConfiguration());
        }
    }
}
