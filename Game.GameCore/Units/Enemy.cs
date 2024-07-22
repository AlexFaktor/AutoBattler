using App.GameCore.Battles.System;
using App.GameCore.Tools.ConfigImporters.ConfigReaders;

namespace App.GameCore.Units;

public abstract class Enemy : Unit
{
    protected Enemy(EnemyConfiguration configuration, EnemyConfigReader configReader, Team team) : base(team)
    {
    }
}
