using App.GameCore.Battles.Manager;
using App.GameCore.Battles.System;
using App.GameCore.Content.Units.Characters.ArminArlert0004;
using App.GameCore.Content.Units.Characters.Bloodhound0001;
using App.GameCore.Content.Units.Characters.Caustic0002;
using App.GameCore.Content.Units.Characters.FL4K0010;
using App.GameCore.Content.Units.Characters.Guts0006;
using App.GameCore.Content.Units.Characters.Isidro0007;
using App.GameCore.Content.Units.Characters.LeviAckerman0005;
using App.GameCore.Content.Units.Characters.Lifeline0003;
using App.GameCore.Content.Units.Characters.Schierke0008;
using App.GameCore.Content.Units.Characters.Serpico0009;
using App.GameCore.Tools.ShellImporters.ConfigReaders;
using App.GameCore.Units.Enums;

namespace App.GameCore.Units;

public class UnitFactory(CharacterConfigReader characterConfigReader)
{
    private readonly CharacterConfigReader _characterConfigReader = characterConfigReader;

    private Unit GetUnit(UnitConfiguration config, Team team, Battle battle)
    {
        return config.Id switch
        {
            (int)EUnit.Bloodhound => new Bloodhound_0001(config, team, _characterConfigReader, battle),
            (int)EUnit.Caustic => new Caustic_0002(config, team, _characterConfigReader, battle),
            (int)EUnit.Lifeline => new Lifeline_0003(config, team, _characterConfigReader, battle),
            (int)EUnit.ArminArlert => new ArminArlert_0004(config, team, _characterConfigReader, battle),
            (int)EUnit.LeviAckerman => new LeviAckerman_0005(config, team, _characterConfigReader, battle),
            (int)EUnit.Guts => new Guts_0006(config, team, _characterConfigReader, battle),
            (int)EUnit.Isidro => new Isidro_0007(config, team, _characterConfigReader, battle),
            (int)EUnit.Schierke => new Schierke_0008(config, team, _characterConfigReader, battle),
            (int)EUnit.Serpico => new Serpico_0009(config, team, _characterConfigReader, battle),
            (int)EUnit.FL4K => new FL4K_0010(config, team, _characterConfigReader, battle),
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
