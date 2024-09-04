using GameCore.Units.Actions.Abilitys;
using GameLogic.Battles.Manager;
using GameLogic.Battles.System;
using GameLogic.Units.Actions.Abilitys.Interfaces;

namespace GameLogic.Units.Actions.Abilitys;

public abstract class RechargingAbility(Battle battle, Unit unit) : Ability(battle, unit), IPeriodicAbility
{
    abstract public BattleTimer Time { get; set; }
}