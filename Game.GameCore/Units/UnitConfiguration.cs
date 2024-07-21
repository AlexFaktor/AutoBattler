using App.GameCore.Battles.System;

namespace App.GameCore.Units;

public class UnitConfiguration
{
    public int Id { get; set; } = 0;
    public Team Team { get; set; }

    public UnitConfiguration(Team team)
    { 
        Team = team;
    }
}