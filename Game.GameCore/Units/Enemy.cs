using GameLogic.Battles.Manager;
using GameLogic.Battles.System;
using GameLogic.Tools.ShellImporters.ConfigReaders;

namespace GameLogic.Units;

public abstract class Enemy : Unit
{
    protected Enemy(EnemyConfiguration configuration, EnemyConfigReader configReader, Team team , Battle battle) : base(team, battle)
    {
    }
}
