using App.GameCore.Units.Enums;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace App.GameCore.Tools.ShellImporters.ConfigReaders;

public class CharacterConfigReader
{
    private readonly string _filePath;
    private List<CharacterConfig> _characterConfigs = [];

    public CharacterConfigReader(string filePath)
    {
        _filePath = filePath;
        LoadConfigs();
    }

    private void LoadConfigs()
    {
        using var reader = new StreamReader(_filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HeaderValidated = null,
            MissingFieldFound = null
        };
        csv.Context.RegisterClassMap<CharacterConfigMap>();
        _characterConfigs = new List<CharacterConfig>(csv.GetRecords<CharacterConfig>());
    }

    public CharacterConfig? GetCharacterConfigById(int id)
    {
        return _characterConfigs.Find(config => config.Id == id);
    }

    private class CharacterConfigMap : ClassMap<CharacterConfig>
    {
        public CharacterConfigMap()
        {
            Map(m => m.Id).Name("ID");
            Map(m => m.Name).Name("NAME");
            Map(m => m.Universe).Name("UNIVERSE");
            Map(m => m.Version).Name("VERSION");
            Map(m => m.Description).Name("DESCRIPTION");
            Map(m => m.UrlImage).Name("URL_IMAGE");
            Map(m => m.Class1).Name("CLASS1");
            Map(m => m.Class2).Name("CLASS2");
            Map(m => m.Initiative).Name("INITIATIVE");
            Map(m => m.Speed).Name("SPEED");
            Map(m => m.AttackRange).Name("ATTACK_RANGE");
            Map(m => m.TacticalType).Name("TACTICAL_TYPE");
            Map(m => m.TacticalLevel).Name("TACTICAL_LEVEL");
            Map(m => m.AbilityHaste).Name("ABILITY_HASTE");
            Map(m => m.Vampirism).Name("VAMPIRISM");
            Map(m => m.Mana).Name("MANA");
            Map(m => m.Damage).Name("DAMAGE");
            Map(m => m.AttackSpeed).Name("ATTACK_SPEED");
            Map(m => m.Accuracy).Name("ACCURACY");
            Map(m => m.CriticalChance).Name("CRITICAL_CHANCE");
            Map(m => m.CriticalDamage).Name("CRITICAL_DAMAGE");
            Map(m => m.ArmorPenetration).Name("ARMOR_PENETRATION");
            Map(m => m.IgnoringArmor).Name("IGNORING_ARMOR");
            Map(m => m.HealthPoints).Name("HEALTH_POINTS");
            Map(m => m.Shield).Name("SHIELD");
            Map(m => m.ShieldEfficiency).Name("SHIELD_EFFICIENCY");
            Map(m => m.HealthPassive).Name("HEALTH_PASSIVE");
            Map(m => m.HealthEfficiency).Name("HEALTH_EFFICIENCY");
            Map(m => m.Dexterity).Name("DEXTERITY");
            Map(m => m.CriticalDefeat).Name("CRITICAL_DEFEAT");
            Map(m => m.Armor).Name("ARMOR");
        }
    }
}

public class CharacterConfig
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Universe { get; set; } = string.Empty;
    public int Version { get; set; }
    public string Description { get; set; } = string.Empty;
    public string UrlImage { get; set; } = string.Empty;
    public UnitClass Class1 { get; set; } = UnitClass.None;
    public UnitClass Class2 { get; set; } = UnitClass.None;
    public float Initiative { get; set; }
    public float Speed { get; set; }
    public float AttackRange { get; set; }
    public TacticalTypes TacticalType { get; set; } = TacticalTypes.Default;
    public int TacticalLevel { get; set; }
    public float AbilityHaste { get; set; }
    public float Vampirism { get; set; }
    public float Mana { get; set; }
    public double Damage { get; set; }
    public float AttackSpeed { get; set; }
    public float Accuracy { get; set; }
    public float CriticalChance { get; set; }
    public float CriticalDamage { get; set; }
    public float ArmorPenetration { get; set; }
    public float IgnoringArmor { get; set; }
    public double HealthPoints { get; set; }
    public double Shield { get; set; }
    public float ShieldEfficiency { get; set; }
    public float HealthPassive { get; set; }
    public float HealthEfficiency { get; set; }
    public float Dexterity { get; set; }
    public float CriticalDefeat { get; set; }
    public float Armor { get; set; }
}


