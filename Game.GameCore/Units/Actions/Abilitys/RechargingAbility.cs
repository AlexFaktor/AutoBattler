using App.GameCore.Battles.Manager;
using App.GameCore.Units.Actions.Abilitys.Interfaces;
using App.GameCore.Units.Types;

namespace App.GameCore.Units.Actions.Abilitys;

public abstract class RechargingAbility : Ability, IPeriodicAbility
{
    abstract public BattleTimer Time { get; set; }
    protected RechargingAbility(Battle battle, Unit unit) : base(battle, unit)
    {
    }
}