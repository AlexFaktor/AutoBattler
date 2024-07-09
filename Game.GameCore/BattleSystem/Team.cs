using Game.Core.DatabaseRecords.Users;

namespace Game.GameCore.BattleSystem;

public struct Team
{
    public Guid Token { get; } = Guid.NewGuid();
    public GameUser User { get; }
    public Squad Squad { get; }

    public Team()
    {
    }
}
