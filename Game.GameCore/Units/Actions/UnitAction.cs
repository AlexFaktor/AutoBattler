using App.GameCore.Battles.Manager;
using App.GameCore.Battles.System;

namespace App.GameCore.Units.Actions;

public abstract class UnitAction : BattleAction
{
    protected Unit _unit;

    protected UnitAction(Battle battle, Unit unit) : base(battle)
    {
        _unit = unit;
    }
}
