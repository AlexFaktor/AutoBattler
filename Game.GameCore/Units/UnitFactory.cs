using Game.GameCore.Battles.System;
using Game.GameCore.Content.Characters.Bloodhound0001;
using Game.GameCore.Tools.ConfigImporters.ConfigReaders;
using Game.GameCore.Units.Enums;

namespace Game.GameCore.Units;

public class UnitFactory
{
    private readonly CharacterConfigReader _characterConfigReader;

    public UnitFactory(CharacterConfigReader characterConfigReader)
    {
        _characterConfigReader = characterConfigReader;
    }

    public Unit GetUnit(UnitConfiguration config, Team team)
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
