using App.GameCore.Battles.Manager;

namespace App.GameCore.Units.Actions;

public abstract class Ability : UnitAction
{
    protected Ability(Battles.Manager.Battle battle, Unit unit) : base(battle, unit)
    {
    }
}
