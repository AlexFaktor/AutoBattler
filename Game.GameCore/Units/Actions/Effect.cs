using App.GameCore.Battles.Manager;

namespace App.GameCore.Units.Actions;

public abstract class Effect : UnitAction
{
    protected Effect(Battles.Manager.Battle battle, Unit unit) : base(battle, unit)
    {
    }
}
