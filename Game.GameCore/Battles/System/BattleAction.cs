using App.GameCore.Battles.Manager;

namespace App.GameCore.Battles.System;

public abstract class BattleAction
{
    protected Battle _battle;

    protected BattleAction(Battle battle)
    {
        _battle = battle;
    }

    public abstract void Action();
}
