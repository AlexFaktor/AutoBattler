using App.GameCore.Battles.Manager;
using App.GameCore.Battles.System;
using App.GameCore.Tools.ShellImporters.ConfigReaders;

namespace App.GameCore.Units;

public abstract class Enemy : Unit
{
    protected Enemy(EnemyConfiguration configuration, EnemyConfigReader configReader, Team team , Battle battle) : base(team, battle)
    {
    }
}
