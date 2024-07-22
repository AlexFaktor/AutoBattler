using App.GameCore.Battles.System;
using App.GameCore.Content.Units.Characters.Bloodhound0001;
using App.GameCore.Tools.ConfigImporters.ConfigReaders;
using App.GameCore.Units.Enums;

namespace App.GameCore.Units;

public class UnitFactory
{
    private readonly CharacterConfigReader _characterConfigReader;

    public UnitFactory(CharacterConfigReader characterConfigReader)
    {
        _characterConfigReader = characterConfigReader;
    }

    public Unit GetUnit(CharacterConfiguration config, Team team)
    {
        switch (config.Id)
        {
            case (int)EUnit.Bloodhound:
                return new Bloodhound_0001(config, team, _characterConfigReader);
            default:
                throw new Exception("unit creation error");
        }
    }
}
