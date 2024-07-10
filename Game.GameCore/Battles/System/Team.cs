using Game.Core.DatabaseRecords.Users;

namespace Game.GameCore.Battles.System;

public class Team
{
    public Guid Token { get; } = Guid.NewGuid();
    public GameUser User { get; }
    public Squad Squad { get; }

    public Team()
    {
    }
}
