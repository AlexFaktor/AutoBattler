using Game.GameCore.Battles.Manager;
using Game.GameCore.Battles.System;

namespace Game.GameCore.Units.Actions;

public abstract class UnitAction : BattleAction
{
    protected Unit? _unit;

    protected UnitAction(Battle battle, Unit unit) : base(battle)
    {
        _unit = unit;
    }
}
