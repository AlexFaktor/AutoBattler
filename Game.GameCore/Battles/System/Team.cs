namespace App.GameCore.Battles.System;

public class Team
{
    public Guid Token { get; } = Guid.NewGuid();
    public IPlayer Player { get; }
    public Squad Squad { get; }

    public Team(IPlayer player, SquadConfiguration squadConfiguration)
    {
        Player = player;
        Squad = new(squadConfiguration);
    }
}
