namespace App.GameCore.Battles.System;

public class Team
{
    public Guid Token { get; } = Guid.NewGuid();
    public Player Player { get; }
    public Squad Squad { get; }

    public Team(Player player, SquadConfiguration squadConfiguration)
    {
        Player = player;
        Squad = new(squadConfiguration);
    }
}
