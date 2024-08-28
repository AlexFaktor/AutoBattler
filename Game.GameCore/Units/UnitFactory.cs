using App.GameCore.Battles.Manager;
using App.GameCore.Battles.System;
using App.GameCore.Tools.ShellImporters.ConfigReaders;
using App.GameCore.Units.Content.Characters;

namespace App.GameCore.Units;

public class UnitFactory(CharacterConfigReader characterConfigReader)
{
    private readonly CharacterConfigReader _characterConfigReader = characterConfigReader;

    private Unit GetUnit(UnitConfiguration config, Team team, Battle battle)
    {
        return config.Id switch
        {
            (int)Enums.Units.Bloodhound => new Bloodhound_0001(config, team, _characterConfigReader, battle),
            (int)Enums.Units.Caustic => new Caustic_0002(config, team, _characterConfigReader, battle),
            (int)Enums.Units.Lifeline => new Lifeline_0003(config, team, _characterConfigReader, battle),
            (int)Enums.Units.ArminArlert => new ArminArlert_0004(config, team, _characterConfigReader, battle),
            (int)Enums.Units.LeviAckerman => new LeviAckerman_0005(config, team, _characterConfigReader, battle),
            (int)Enums.Units.Guts => new Guts_0006(config, team, _characterConfigReader, battle),
            (int)Enums.Units.Isidro => new Isidro_0007(config, team, _characterConfigReader, battle),
            (int)Enums.Units.Schierke => new Schierke_0008(config, team, _characterConfigReader, battle),
            (int)Enums.Units.Serpico => new Serpico_0009(config, team, _characterConfigReader, battle),
            (int)Enums.Units.FL4K => new FL4K_0010(config, team, _characterConfigReader, battle),
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
