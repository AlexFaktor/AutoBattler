using GameLogic.Battles.Manager;
using GameLogic.Battles.System;
using GameLogic.Units.Actions.Abilitys;

namespace GameLogic.Units.Content.Abilitys;

internal class PassiveHealthRecovery : RechargingAbility
{
    const float HILL_RATE_PER_SECOND = 1f;
    const uint NEXT_USE = 1000;

    public PassiveHealthRecovery(Battle battle, Unit unit) : base(battle, unit)
    {
        Time = new(NEXT_USE);
    }

    public override BattleTimer Time { get; set; }

    public override void Action()
    {
        var passiveHalth = GetTotalPassiveHealth() / HILL_RATE_PER_SECOND;
        _unit.ReceiceDamage(passiveHalth, this);
        Time.Reload(NEXT_USE);
    }
}
