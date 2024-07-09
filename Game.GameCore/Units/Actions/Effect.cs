using Game.Manager.BattleSystem;

namespace Game.GameCore.Units.Actions;

public abstract class Effect : UnitAction
{
    protected Effect(BattleManager battle, Unit unit) : base(battle, unit)
    {
    }
}
