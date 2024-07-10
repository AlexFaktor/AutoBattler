using Game.GameCore.Units;
using Game.Manager.BattleSystem;

namespace Game.GameCore.Battles.System;

public class Squad
{
    public EBattleTactics Tactics { get; set; }
    public List<Unit> Units { get; } = [];

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
