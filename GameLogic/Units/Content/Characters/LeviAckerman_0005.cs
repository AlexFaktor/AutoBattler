using GameLogic.Battles.Manager;
using GameLogic.Battles.System;
using GameLogic.Tools.ShellImporters.ConfigReaders;
using GameLogic.Units.Content.Abilitys;

namespace GameLogic.Units.Content.Characters;

internal class LeviAckerman_0005 : Character
{
    public LeviAckerman_0005(UnitConfiguration config, Team team, CharacterConfigReader pathConfig, Battle battle) : base(config, pathConfig, team, battle)
    {
        Actions.Add(new AbilityAutoAttack(battle, this));
    }
}
