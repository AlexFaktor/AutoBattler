using GameLogic.Battles.Manager;
using GameLogic.Units;
using GameLogic.Units.Actions.Abilitys;
using GameLogic.Units.Types;

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
