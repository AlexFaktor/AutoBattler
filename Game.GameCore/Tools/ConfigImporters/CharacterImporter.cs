namespace Game.GameCore.Tools.ConfigImporters;

internal class CharacterImporter
{
    public object GetData(short id, CharacterType type)
    {
        return string.Empty;
    }
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
