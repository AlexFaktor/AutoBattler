using App.GameCore.Units.Actions.Interfaces;
using App.GameCore.Units.Types;

namespace App.GameCore.Units.Actions;

public abstract class RechargingAbility : Ability, IPeriodicAbility
{
    public BattleTimer Time { get; set; } = new BattleTimer();
    protected RechargingAbility(Battles.Manager.Battle battle, Unit unit) : base(battle, unit)
    {
    }
}
