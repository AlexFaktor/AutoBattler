using GameLogic.Battles.Manager;
using GameLogic.Battles.System;
using GameLogic.Tools.ShellImporters.ConfigReaders;
using GameLogic.Units.Content.Characters;
using GameLogic.Units.Dtos;

namespace GameLogic.Units;

public class UnitFactory(CharacterConfigReader characterConfigReader)
{
    private readonly CharacterConfigReader _characterConfigReader = characterConfigReader;

    private Unit GetUnit(UnitConfiguration config, Team team, Battle battle)
    {
        return config.Id switch
        {
            (int)Enums.Units.Unit0001 => new Unit_0001(config, team, _characterConfigReader, battle),
            (int)Enums.Units.Unit2 => new Unit_0002(config, team, _characterConfigReader, battle),
            (int)Enums.Units.Unit3 => new Unit_0003(config, team, _characterConfigReader, battle),
            (int)Enums.Units.Unit4 => new Unit_0004(config, team, _characterConfigReader, battle),
            (int)Enums.Units.Unit5 => new Unit_0005(config, team, _characterConfigReader, battle),
            (int)Enums.Units.Unit6 => new Unit_0006(config, team, _characterConfigReader, battle),
            (int)Enums.Units.Unit7 => new Unit_0007(config, team, _characterConfigReader, battle),
            (int)Enums.Units.Unit8 => new Unit_0008(config, team, _characterConfigReader, battle),
            (int)Enums.Units.Unit9 => new Unit_0009(config, team, _characterConfigReader, battle),
            (int)Enums.Units.Unit10 => new Unit_0010(config, team, _characterConfigReader, battle),
            _ => throw new Exception("unit creation error"),
        };
    }

    internal List<Unit> GetUnits(List<UnitConfiguration> unitConfigurations, Team team, Battle battle)
    {
        var units = new List<Unit>();

        foreach (var unitConfiguration in unitConfigurations)
        {
            var unit = GetUnit(unitConfiguration, team, battle);
            units.Add(unit);
        }
        return units;
    }
}
