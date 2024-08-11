using App.GameCore.Battles.Manager;
using App.GameCore.Battles.System;

namespace App.GameCore.Units.Actions;

public abstract class UnitAction(Battle battle, Unit unit) : BattleAction(battle)
{
    protected Unit _unit = unit;
}
