using App.GameCore.Battles.Manager;

namespace App.GameCore.Units.Actions.Abilitys;

public abstract class Ability : UnitAction
{
    protected Ability(Battle battle, Unit unit) : base(battle, unit)
    {
    }
}
