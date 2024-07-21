using Game.GameCore.Battles.System;

namespace Game.GameCore.Units;

public class UnitConfiguration
{
    public int Id { get; set; } = 0;
    public Team Team { get; set; }

    public UnitConfiguration(Team team)
    { 
        Team = team;
    }
}