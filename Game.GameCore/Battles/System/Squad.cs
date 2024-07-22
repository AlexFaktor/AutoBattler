using App.GameCore.Units;

namespace App.GameCore.Battles.System;

public class Squad
{
    public List<Unit> Units { get; } = [];

    public Squad(SquadConfiguration configuration)
    {

    }

    public bool IsSquadAilve()
    {
        byte AliveSoldiers = 0;
        foreach (var unit in Units)
        {
            if (unit.IsAlive())
                AliveSoldiers++;

        }
        if (AliveSoldiers == 0)
            return false;
        return true;
    }
}

public class SquadConfiguration
{
    public List<CharacterConfiguration> UnitConfigurations { get; set; }
    public SquadConfiguration(List<CharacterConfiguration> unitConfigurations)
    {
        UnitConfigurations = unitConfigurations;
    }
}
