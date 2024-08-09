﻿using App.GameCore.Battles.Manager;
using App.GameCore.Battles.System;
using App.GameCore.Content.Units.Abilitys;
using App.GameCore.Tools.ShellImporters.ConfigReaders;
using App.GameCore.Units;

namespace App.GameCore.Content.Units.Characters.Serpico0009;

internal class Serpico_0009 : Character
{
    public Serpico_0009(UnitConfiguration config, Team team, CharacterConfigReader pathConfig, Battle battle) : base(config, pathConfig, team, battle)
    {
        Actions.Add(new AbilityAutoAttack(battle, this));
    }
}
