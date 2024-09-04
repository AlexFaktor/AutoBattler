using GameLogic.Battles.Manager;
using GameLogic.Battles.System;
using GameLogic.Units;
using GameLogic.Units.Actions.Abilitys;

namespace GameLogic.Units.Content.Abilitys;

internal class PassiveHealthRecovery : RechargingAbility
{
    public PassiveHealthRecovery(Battle battle, Unit unit) : base(battle, unit)
    {
    }

    public override BattleTimer Time { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public override void Action()
    {
        throw new NotImplementedException();
    }
}
