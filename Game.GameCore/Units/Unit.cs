using App.GameCore.Battles.Manager;
using App.GameCore.Battles.System;
using App.GameCore.Tools.Formulas;
using App.GameCore.Units.Actions;
using App.GameCore.Units.Enums;
using App.GameCore.Units.Types;

namespace App.GameCore.Units;

public abstract class Unit
{
    // Metadata
    public Guid Token { get; } = Guid.NewGuid();
    public Team Team { get; set; }
    public Battle Battle { get; set; }

    // Info
    public short Id { get; protected set; }
    public string Name { get; protected set; } = string.Empty;

    // Tactics
    public UnitClass Class { get; protected set; }
    public UnitClass SubClass { get; protected set; }
    public TUnitResource<float> Initiative { get; protected set; } = new(100); // Event when changed
    public TUnitValue<float> Speed { get; protected set; } = new(5);
    public TUnitValue<float> AttackRange { get; protected set; } = new(30);
    public TacticalTypes TacticalType { get; protected set; }
    public TUnitValue<int> TacticalLevel { get; protected set; } = new(0);

    // General
    public TUnitPercentage AbilityHaste { get; protected set; } = new(0);
    public TUnitPercentage Vaparism { get; protected set; } = new(0);
    public TUnitResource<float> Mana { get; protected set; } = new(100);

    // Attack
    public TUnitValue<double> Damage { get; protected set; } = new(0);
    public BattleTimer AttackSpeed { get; protected set; } = new(1000);
    public TUnitChance Accuracy { get; protected set; } = new(0.66f);
    public TUnitChance CriticalChance { get; protected set; } = new(0.05f);
    public TUnitPercentage CriticalDamage { get; protected set; } = new(0.20f);
    public TUnitPercentage ArmorPenetration { get; protected set; } = new(0);
    public TUnitValue<float> IgnoringArmor { get; protected set; } = new(0);

    // Defensive
    public TUnitResource<double> HealthPoints { get; protected set; } = new(1); // Event when changed 
    public TUnitResource<double> Shield { get; protected set; } = new(0); // Event when changed
    public TUnitPercentage ShieldEfficiency { get; protected set; } = new(0f);
    public TUnitValue<float> HealthPassive { get; protected set; } = new(0);
    public TUnitPercentage HealthEfficiency { get; protected set; } = new(0f);
    public TUnitChance Dexterity { get; protected set; } = new(0);
    public TUnitChance CriticalDefeat { get; protected set; } = new(0);
    public TUnitValue<float> Armor { get; protected set; } = new(0);

    // Other
    public float Position { get; protected set; }

    public List<UnitAction> Actions { get; protected set; } = [];

    // Events
    public event EventHandler<AttackEventArgs>? OnAttack;
    public event EventHandler<DamageReceivedEventArgs>? OnDamageReceived;
    public event EventHandler<DeadEventArgs>? OnDead;
    public event EventHandler<ShieldBrokenEventArgs>? OnShieldBroken;

    public Unit(Team team, Battle battle)
    {
        Team = team;
        Battle = battle;
    }

    public void ApplyEffect(Effect effect)
    {

    }

    public void ReloadEffects()
    {
    }
    // MAKE EVENT, WHEN EFFECT DOWN REFRESH EFFECTS

    public void ApplyItems()
    {

    }
    public void ReloadItems()
    {

    }

    public virtual Unit? SelectEnemy(List<Unit> enemys, float attackRange)
    {
        var targetsPrioritetsDistance = GetTargets(GetAttackRadius(attackRange), enemys); // Get enemies in the attack radius and their priority based on distance
        if (targetsPrioritetsDistance.Count < 1)
            return null;
        var targetsPrioritetsClass = UnitClassFormulas.WeightToSelect(Class, SubClass); // Get priorities based on class
        
        var targets = AddСlassСonsideration(targetsPrioritetsClass, targetsPrioritetsDistance); // Combine class and distance priorities
        return GameFormulas.SelectRandomlyWithPriorities(targets, Battle);
    }
    public virtual void ReceiveDamageFromUnit(double damage, Unit attacker)
    {
        if (IsShield())
        {
            Shield.Now -= damage;
            OnDamageReceived?.Invoke(this, new DamageReceivedEventArgs(damage, attacker, toShield: true));

            if (!IsShield())
            {
                OnShieldBroken?.Invoke(this, new ShieldBrokenEventArgs(attacker));
            }
        }
        else if (IsAlive())
        {
            HealthPoints.Now -= damage;
            OnDamageReceived?.Invoke(this, new DamageReceivedEventArgs(damage, attacker, toHealPoints: true));

            if (!IsAlive()) // перевірка чи юніт помер після отримання урону
            {
                OnDead?.Invoke(this, new DeadEventArgs(attacker));
            }
        }
    }

    public bool IsAlive() => HealthPoints.Now > 0;
    public bool IsShield() => Shield.Now > 0;

    protected UnitRadius GetAttackRadius(float attackRange)
    {
        return new UnitRadius()
        {
            Back = Position - attackRange,
            Front = Position + attackRange,
        };
    }
    protected Dictionary<Unit, float> GetTargets(UnitRadius radius, List<Unit> enemys)
    {
        var dictionary = new Dictionary<Unit, float>();

        foreach (var enemy in enemys)
        {
            if (enemy.Position < radius.Front && enemy.Position > radius.Back)
            {
                var distance = Math.Abs(Math.Abs(enemy.Position) - Math.Abs(Position));
                var difference = 1 - (distance / radius.Radius);
                dictionary.Add(enemy, difference);
            }
        }

        return dictionary;
    }
    protected Dictionary<Unit, float> AddСlassСonsideration(Dictionary<UnitClass, float> targetsPrioritetsClass, Dictionary<Unit, float> targets)
    {
        foreach (var target in targets.Keys.ToList())
        {
            UnitClass unitClass = target.Class; // Припускаємо, що у класу Unit є властивість UnitClass
            if (targetsPrioritetsClass.TryGetValue(unitClass, out float priority))
            {
                targets[target] *= priority;
            }
        }

        return targets;
    }

    public float ChanceOfHitUnit(Unit target)
    {
        var accuracy = Accuracy.Now;
        var dexterity = target.Dexterity.Now;
        var coefInitiative = (float)GameFormulas.GetRatio(Initiative.Now, Initiative.Default);
        var coefInitiativeTarger = (float)GameFormulas.GetRatio(target.Initiative.Now, target.Initiative.Default);
        return (accuracy * coefInitiative) - (dexterity * coefInitiativeTarger);
    }
    public float ChanceOfCritUnit(Unit target) // Треба придумати як задіяти ініціативу цілі
    {
        var crit = CriticalChance.Now;
        var affectedArea = target.CriticalDefeat.Now;
        var coefInitiative = (float)GameFormulas.GetRatio(Initiative.Now, Initiative.Default);
        var coefInitiativeTarger = (float)GameFormulas.GetRatio(target.Initiative.Now, target.Initiative.Default);
        return (crit * coefInitiative) + (affectedArea);
    }

    internal void Move(IEnumerable<Unit> enemies)
    {
        if (enemies == null || !enemies.Any())
            return;

        Unit? closestEnemy = null;
        float closestDistance = float.MaxValue;

        foreach (var enemy in enemies)
        {
            float distance = CalculateDistance(Position, enemy.Position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
        {
            if (closestEnemy.Position > Position)
                Position = Position + Speed.Now;
            else if (closestEnemy.Position < Position)
                Position = Position - Speed.Now;
        }

        float CalculateDistance(float positionA, float positionB) => Math.Abs(Math.Abs(positionA) - Math.Abs(positionB));
    }
    public void TeleportTo(float position)
    {
        Position = position;
    }

    public void LogResource()
    {
        var logger = Battle.Logger;

        logger.LogInfo("unit", $" {Name} >> S {Shield.Now:F1}/{Shield.Max:F1}| H {HealthPoints.Now:F1}/{HealthPoints.Max:F1} ");
    }
}

public struct UnitRadius
{
    public float Back { get; set; }
    public float Front { get; set; }
    public readonly float Radius => (Front - Back) / 2;
}

public class AttackEventArgs(double damageDealt, bool isCritical, bool isMiss) : EventArgs
{
    public double DamageDealt { get; } = damageDealt;
    public bool IsCritical { get; } = isCritical;
    public bool IsMiss { get; } = isMiss;
}

public class DamageReceivedEventArgs(double damageReceived, Unit from, bool toShield = false, bool toHealPoints = false) : EventArgs
{
    public double DamageReceived { get; } = damageReceived;
    public Unit Attacker { get; set; } = from;
    public bool IsShield { get; } = toShield;
    public bool IsToHealPoints { get; } = toHealPoints;

}

public class DeadEventArgs(Unit killer) : EventArgs
{
    public Unit Killer { get; } = killer;
}

public class ShieldBrokenEventArgs(Unit whoBrokenShield) : EventArgs
{
    public Unit WhoBrokenShield { get; } = whoBrokenShield;
}
