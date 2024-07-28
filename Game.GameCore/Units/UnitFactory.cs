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

public class UnitFactory
{
    private readonly CharacterConfigReader _characterConfigReader;

    public UnitFactory(CharacterConfigReader characterConfigReader)
    {
        _characterConfigReader = characterConfigReader;
    }

    public Unit GetUnit(CharacterConfiguration config, Team team)
    {
        return config.Id switch
        {
            (int)EUnit.Bloodhound => new Bloodhound_0001(config, team, _characterConfigReader),
            (int)EUnit.Caustic => new Caustic_0002(config, team, _characterConfigReader),
            (int)EUnit.Lifeline => new Lifeline_0003(config, team, _characterConfigReader),
            (int)EUnit.ArminArlert => new ArminArlert_0004(config, team, _characterConfigReader),
            (int)EUnit.LeviAckerman => new LeviAckerman_0005(config, team, _characterConfigReader),
            (int)EUnit.Guts => new Guts_0006(config, team, _characterConfigReader),
            (int)EUnit.Isidro => new Isidro_0007(config, team, _characterConfigReader),
            (int)EUnit.Schierke => new Schierke_0008(config, team, _characterConfigReader),
            (int)EUnit.Serpico => new Serpico_0009(config, team, _characterConfigReader),
            (int)EUnit.FL4K => new FL4K_0010(config, team, _characterConfigReader),
            _ => throw new Exception("unit creation error"),
        };
    }
}
