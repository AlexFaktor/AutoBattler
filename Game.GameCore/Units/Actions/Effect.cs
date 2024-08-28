using App.GameCore.Battles.Manager;

namespace App.GameCore.Units.Actions;

public abstract class Effect(Battle battle, Unit unit) : UnitAction(battle, unit)
{
}
