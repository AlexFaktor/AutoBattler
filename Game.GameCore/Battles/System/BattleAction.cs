using Game.GameCore.Battles.Manager;

namespace Game.GameCore.Battles.System;

public abstract class BattleAction
{
    protected Battle? _battle;

    protected BattleAction(Battle? battle)
    {
        _battle = battle;
    }

    public abstract void Action();
}
