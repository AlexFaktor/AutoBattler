using GameLogic.Battles.Manager;

namespace GameLogic.Units.Actions;

public abstract class Effect(Battle battle, Unit unit) : UnitAction(battle, unit)
{
}
