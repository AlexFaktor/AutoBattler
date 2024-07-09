using Game.Manager.BattleSystem;

namespace Game.GameCore.Units.Actions;

public abstract class UnitAction
{
    protected BattleManager _battle;
    protected Unit _unit;

    protected UnitAction(BattleManager battle, Unit unit)
    {
        _battle = battle;
        _unit = unit;
    }

    public abstract void Action();

    public List<Unit> GetEnemys()
    {
        

        return;
    }

}
