using Game.GameCore.Battles.System;
using Game.GameCore.Tools.ConfigImporters.ConfigReaders;
using Game.GameCore.Units.Enums;

namespace Game.GameCore.Units;

public abstract class Character : Unit
{
    public Character(int id,Team team, CharacterConfigReader configReader) 
    {
        var config = configReader.GetCharacterConfigById(id);

        if (config == null)
            throw new Exception("The character does not exist");
        // Metadata
        Team = team;
        // Info
        Id = (short)config.Id;
        Name = config.Name;
        // Tactics
        Class = EUnitClassParse.Parse(config.Class1);
        SubClass = EUnitClassParse.Parse(config.Class2);
        Initiative = new(config.Initiative);
        Speed = new(config.Speed);
        AttackRange = new(config.AttackRange);
        TacticalType = ETacticalTypeParse.Parse( config.TacticalType);
        TacticalLevel = new(config.TacticalLevel);
        // General
        AbilityHaste = config.AbilityHaste;
        Vaparism = config.Vampirism;
        Mana = new(config.Mana);
        // Attack
        Damage = new(config.Damage);
        AttackSpeed = new(config.AttackSpeed);
        Accuracy = config.Accuracy;
        CriticalChance = config.CriticalChance;
        CriticalDamage = config.CriticalDamage;
        ArmorPenetration = config.ArmorPenetration;
        IgnoringArmor = new(config.IgnoringArmor);
        // Defensive
        HealthPoints = new(config.HealthPoints);
        Shield = new(config.Shield);
        ShieldEfficiency = config.ShieldEfficiency;
        HealthPassive = new(config.HealthPassive);
        HealthEfficiency = config.HealthEfficiency;
        Dexterity = config.Dexterity;
        CriticalDefeat = config.CriticalDefeat;
        Armor = new (config.Armor);
    }
}
