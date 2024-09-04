using GameLogic.Battles.Manager;

namespace GameLogic.Battles.System;

public abstract class BattleAction : IBattleAction
{
    protected Battle _battle;

    protected BattleAction(Battle battle)
    {
        _battle = battle;
    }

    public abstract void Action();
    public virtual void RemoveFromBattle()
    {
        _battle.AllBattleActions.Remove(this);
    }
}

public interface IBattleAction
{ 
    void Action();
}
