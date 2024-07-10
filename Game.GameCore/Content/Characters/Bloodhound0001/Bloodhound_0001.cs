using Game.GameCore.Battles.System;
using Game.GameCore.Tools.ConfigImporters;
using Game.GameCore.Units;
using Game.GameCore.Units.Enums;
using Game.GameCore.Units.Types;

namespace Game.GameCore.Content.Characters.Bloodhound0001
{
    internal class Bloodhound_0001 : Character
    {
        public Bloodhound_0001(Team team)
        {
            short id = 1;
            var import = new CharacterImporter();

            // Metadata
            Team = team;
            // Info
            Id = id;
            Name = (string)import.GetData(id, CharacterType.Name);
            // Tactics
            Class = (EUnitClass)import.GetData(id, CharacterType.Class);
            SubClass = (EUnitClass)import.GetData(id, CharacterType.SubClass);
            Initiative = new TUnitResource<float>((float)import.GetData(id, CharacterType.Initiative));
            Speed = new TUnit<float>((float)import.GetData(id, CharacterType.Speed));
            AttackRange = new TUnit<float>((float)import.GetData(id, CharacterType.AttackRange));
            TacticalType = (ETacticalType)import.GetData(id, CharacterType.TacticalType);
            TacticalLevel = new TUnit<int>((int)import.GetData(id, CharacterType.TacticalLevel));
            // General
            AbilityHaste = new TUnitPercentage();
            Vaparism = new TUnitPercentage();
            Mana = new TUnitResource<float>((float)import.GetData(id, CharacterType.Mana));
            // Attack
            Damage = new TUnit<double>((double)import.GetData(id, CharacterType.Damage));
            AttackSpeed = new BattleTimer();
            Accuracy = new TUnitPercentage();
            CriticalChance = new TUnitPercentage();
            CriticalDamage = new TUnitPercentage();
            ArmorPenetration = new TUnitPercentage();
            IgnoringArmor = new TUnit<float>((float)import.GetData(id, CharacterType.IgnoringArmor));
            // Defensive
            HealthPoints = new TUnitResource<double>((double)import.GetData(id, CharacterType.HealthPoints));
            Shield = new TUnitResource<double>((double)import.GetData(id, CharacterType.Shield));
            ShieldEfficiency = new TUnitPercentage();
            HealthPassive = new TUnit<float>((float)import.GetData(id, CharacterType.HealthPassive));
            HealthEfficiency = new TUnitPercentage();
            Dexterity = new TUnitPercentage();
            CriticalDefeat = new TUnitPercentage();
            Armor = new TUnit<int>((int)import.GetData(id, CharacterType.Armor));
            // Other
            // Events
        }
    }
}
