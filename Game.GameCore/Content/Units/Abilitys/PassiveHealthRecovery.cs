using App.GameCore.Battles.Manager;
using App.GameCore.Units;
using App.GameCore.Units.Actions.Abilitys;
using App.GameCore.Units.Types;

namespace App.GameCore.Content.Units.Abilitys;

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
