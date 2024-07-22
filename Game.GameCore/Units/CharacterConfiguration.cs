using App.GameCore.Battles.System;

namespace App.GameCore.Units;

public class CharacterConfiguration
{
    public int Id { get; set; } = 0;
    public Team Team { get; set; }

    public CharacterConfiguration(Team team)
    { 
        Team = team;
    }
}