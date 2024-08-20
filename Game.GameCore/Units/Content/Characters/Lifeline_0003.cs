using App.GameCore.Battles.Manager;
using App.GameCore.Battles.System;
using App.GameCore.Tools.ShellImporters.ConfigReaders;
using App.GameCore.Units.Content.Abilitys;

namespace App.GameCore.Units.Content.Characters;

internal class Lifeline_0003 : Character
{
    public Lifeline_0003(UnitConfiguration config, Team team, CharacterConfigReader pathConfig, Battle battle) : base(config, pathConfig, team, battle)
    {
        Actions.Add(new AbilityAutoAttack(battle, this));
    }
}
