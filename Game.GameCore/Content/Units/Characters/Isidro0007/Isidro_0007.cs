using App.GameCore.Battles.Manager;
using App.GameCore.Battles.System;
using App.GameCore.Content.Units.Abilitys;
using App.GameCore.Tools.ShellImporters.ConfigReaders;
using App.GameCore.Units;

namespace App.GameCore.Content.Units.Characters.Isidro0007;

internal class Isidro_0007 : Character
{
    public Isidro_0007(UnitConfiguration config, Team team, CharacterConfigReader pathConfig, Battle battle) : base(config, pathConfig, team, battle)
    {
        Actions.Add(new AbilityAutoAttack(battle, this));
    }
}
