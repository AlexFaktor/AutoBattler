using App.GameCore.Battles.Manager;
using App.GameCore.Units.Actions.Abilitys.Interfaces;
using App.GameCore.Units.Types;

namespace App.GameCore.Units.Actions.Abilitys;

public abstract class RechargingAbility(Battle battle, Unit unit) : Ability(battle, unit), IPeriodicAbility
{
    abstract public BattleTimer Time { get; set; }
}