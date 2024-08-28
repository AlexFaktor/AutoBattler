using App.GameCore.Battles.Manager;
using App.GameCore.Battles.System;
using App.GameCore.Tools.ShellImporters.ConfigReaders;
using App.GameCore.Units.Content.Abilitys;

namespace App.GameCore.Units.Content.Characters;

internal class ArminArlert_0004 : Character
{
    public ArminArlert_0004(UnitConfiguration config, Team team, CharacterConfigReader pathConfig, Battle battle) : base(config, pathConfig, team, battle)
    {
        Actions.Add(new AbilityAutoAttack(battle, this));
    }
}
