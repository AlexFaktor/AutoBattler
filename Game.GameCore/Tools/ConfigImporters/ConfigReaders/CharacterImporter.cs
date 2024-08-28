using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using App.GameCore.Units.Enums;
using System.Globalization;

namespace App.GameCore.Tools.ShellImporters.ConfigReaders;

internal class CharacterImporter
{
    private readonly string _filePath;
    private readonly List<CharacterData> _characters;

    public CharacterImporter(string filePath)
    {
        _filePath = filePath;
        _characters = LoadCharacters();
    }

    private List<CharacterData> LoadCharacters()
    {
        using var reader = new StreamReader(_filePath);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
        });
        return new List<CharacterData>(csv.GetRecords<CharacterData>());
    }

    public object GetData(short id, CharacterType type)
    {
        var character = _characters.Find(c => c.ID == id);
        if (character == null)
            throw new ArgumentException($"Character with ID {id} not found.");

        return type switch
        {
            CharacterType.Id => character.ID,
            CharacterType.Name => character.NAME,
            CharacterType.Class => EUnitClassParse.Parse(character.CLASS1),
            CharacterType.SubClass => EUnitClassParse.Parse(character.CLASS2),
            CharacterType.Initiative => character.INITIATIVE,
            CharacterType.Speed => character.SPEED,
            CharacterType.AttackRange => character.ATTACK_RANGE,
            CharacterType.TacticalType => character.TACTICAL_TYPE,
            CharacterType.TacticalLevel => character.TACTICAL_LEVEL,
            CharacterType.AbilityHaste => character.ABILITY_HASTE,
            CharacterType.Vaparism => character.VAMPIRISM,
            CharacterType.Mana => character.MANA,
            CharacterType.Damage => character.DAMAGE,
            CharacterType.AttackSpeed => character.ATTACK_SPEED,
            CharacterType.Accuracy => character.ACCURACY,
            CharacterType.CriticalChance => character.CRITICAL_CHANCE,
            CharacterType.CriticalDamage => character.CRITICAL_DAMAGE,
            CharacterType.ArmorPenetration => character.ARMOR_PENETRATION,
            CharacterType.IgnoringArmor => character.IGNORING_ARMOR,
            CharacterType.HealthPoints => character.HEALTH_POINTS,
            CharacterType.Shield => character.SHIELD,
            CharacterType.ShieldEfficiency => character.SHIELD_EFFICIENCY,
            CharacterType.HealthPassive => character.HEALTH_PASSIVE,
            CharacterType.HealthEfficiency => character.HEALTH_EFFICIENCY,
            CharacterType.Dexterity => character.DEXTERITY,
            CharacterType.CriticalDefeat => character.CRITICAL_DEFEAT,
            CharacterType.Armor => character.ARMOR,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null),
        };
    }
}

public class CharacterData
{
    [Name("ID")]
    public short ID { get; set; }

    [Name("NAME")]
    public string NAME { get; set; } = "Unknown";

    [Name("UNIVERSE")]
    public string UNIVERSE { get; set; } = "Unknown";

    [Name("VERSION")]
    public string VERSION { get; set; } = "Unknown";

    [Name("DESCRIPTION")]
    public string DESCRIPTION { get; set; } = "Unknown";

    [Name("URL_IMAGE")]
    public string URL_IMAGE { get; set; } = "Unknown";

    [Name("CLASS1")]
    public string CLASS1 { get; set; } = "Unknown";

    [Name("CLASS2")]
    public string CLASS2 { get; set; } = "Unknown";

    [Name("INITIATIVE")]
    public float INITIATIVE { get; set; }

    [Name("SPEED")]
    public float SPEED { get; set; }

    [Name("ATTACK_RANGE")]
    public float ATTACK_RANGE { get; set; }

    [Name("TACTICAL_TYPE")]
    public string TACTICAL_TYPE { get; set; } = "Unknown";

    [Name("TACTICAL_LEVEL")]
    public float TACTICAL_LEVEL { get; set; }

    [Name("ABILITY_HASTE")]
    public float ABILITY_HASTE { get; set; }

    [Name("VAMPIRISM")]
    public float VAMPIRISM { get; set; }

    [Name("MANA")]
    public float MANA { get; set; }

    [Name("DAMAGE")]
    public double DAMAGE { get; set; }

    [Name("ATTACK_SPEED")]
    public float ATTACK_SPEED { get; set; }

    [Name("ACCURACY")]
    public float ACCURACY { get; set; }

    [Name("CRITICAL_CHANCE")]
    public float CRITICAL_CHANCE { get; set; }

    [Name("CRITICAL_DAMAGE")]
    public float CRITICAL_DAMAGE { get; set; }

    [Name("ARMOR_PENETRATION")]
    public float ARMOR_PENETRATION { get; set; }

    [Name("IGNORING_ARMOR")]
    public float IGNORING_ARMOR { get; set; }

    [Name("HEALTH_POINTS")]
    public double HEALTH_POINTS { get; set; }

    [Name("SHIELD")]
    public double SHIELD { get; set; }

    [Name("SHIELD_EFFICIENCY")]
    public float SHIELD_EFFICIENCY { get; set; }

    [Name("HEALTH_PASSIVE")]
    public float HEALTH_PASSIVE { get; set; }

    [Name("HEALTH_EFFICIENCY")]
    public float HEALTH_EFFICIENCY { get; set; }

    [Name("DEXTERITY")]
    public float DEXTERITY { get; set; }

    [Name("CRITICAL_DEFEAT")]
    public float CRITICAL_DEFEAT { get; set; }

    [Name("ARMOR")]
    public float ARMOR { get; set; }
}

public enum CharacterType
{
    None = 0,
    Id,
    Name,
    Class,
    SubClass,
    Initiative,
    Speed,
    AttackRange,
    TacticalType,
    TacticalLevel,
    AbilityHaste,
    Vaparism,
    Mana,
    Damage,
    AttackSpeed,
    Accuracy,
    CriticalChance,
    CriticalDamage,
    ArmorPenetration,
    IgnoringArmor,
    HealthPoints,
    Shield,
    ShieldEfficiency,
    HealthPassive,
    HealthEfficiency,
    Dexterity,
    CriticalDefeat,
    Armor,
}
