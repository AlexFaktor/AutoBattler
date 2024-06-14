using Game.Core.Database.Records.Users;
using Game.Core.Resources.Enums.Game;
using Game.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Game.Database.Service.Users
{
    public class UserCharacterService
    {
        private readonly GameDbContext _db;

        public UserCharacterService(GameDbContext db)
        {
            _db = db;
        }

        public async Task<UserCharacter?> AddAsync(UserCharacter character)
        {
            await _db.UserCharacters.AddAsync(character);
            await _db.SaveChangesAsync();
            return character;
        }

        public async Task<List<UserCharacter>> GetAllAsync() => await _db.UserCharacters.ToListAsync();

        public async Task<UserCharacter?> GetAsync(Guid userId, ECharacter characterId)
        {
            return await _db.UserCharacters.FirstOrDefaultAsync(c => c.UserId == userId && c.CharacterId == characterId);
        }

        public async Task<bool> DeleteAsync(Guid userId, ECharacter characterId)
        {
            var character = await _db.UserCharacters.FirstOrDefaultAsync(c => c.UserId == userId && c.CharacterId == characterId);
            if (character is not null)
            {
                _db.UserCharacters.Remove(character);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
