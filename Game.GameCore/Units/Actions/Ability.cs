using Game.Manager.BattleSystem;

namespace Game.GameCore.Units.Actions;

public abstract class Ability : UnitAction
{
    protected Ability(BattleManager battle, Unit unit) : base(battle, unit)
    {
    }
}
