using App.GameCore.Battles.Manager;
using App.GameCore.Units;

namespace App.GameCore.Battles.System;

public class Team
{
    public Guid Token { get; } = Guid.NewGuid();
    public IPlayer Player { get; }
    public List<Unit> Units { get; } = [];

    public Team(TeamConfiguration configuration, Battle battle, UnitFactory factory)
    {
        Player = configuration.Player;
        Units = factory.GetUnits(configuration.UnitConfigurations, this);
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

public class TeamConfiguration
{
    public IPlayer Player { get; set; }
    public List<UnitConfiguration> UnitConfigurations { get; set; }

    public TeamConfiguration(IPlayer player, List<UnitConfiguration> unitConfigurations)
    {
        Player = player;
        UnitConfigurations = unitConfigurations;
    }
}
