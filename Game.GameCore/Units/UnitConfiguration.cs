using App.GameCore.Battles.System;

namespace App.GameCore.Units;

public class UnitConfiguration
{
    public int Id { get; set; } = 0;

    public UnitConfiguration(int id)
    {
        Id = id;
    }
}