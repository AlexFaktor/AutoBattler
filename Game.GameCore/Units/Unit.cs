using App.GameCore.Battles.System;
using App.GameCore.Units.Actions;
using App.GameCore.Units.Enums;
using App.GameCore.Units.Types;

namespace App.GameCore.Units;

public abstract class Unit
{
    // Metadata
    public Guid Token { get; } = Guid.NewGuid();
    public Team Team { get; set; }

    // Info
    public short Id { get; protected set; }
    public string Name { get; protected set; } = string.Empty;

    // Tactics
    public EUnitClass Class { get; protected set; }
    public EUnitClass SubClass { get; protected set; }
    public TUnitResource<float> Initiative { get; protected set; } = new(100); // Event when changed
    public TUnitValue<float> Speed { get; protected set; } = new(5);
    public TUnitValue<float> AttackRange { get; protected set; } = new(30);
    public ETacticalType TacticalType { get; protected set; }
    public TUnitValue<byte> TacticalLevel { get; protected set; } = new(0);

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
    public double Position { get; protected set; }

    protected List<UnitAction> _actions = [];

    // Events
    public event EventHandler<AttackEventArgs>? OnAttack;
    public event EventHandler<DamageReceivedEventArgs>? OnDamageReceived;

    public Unit(Team team)
    {
        Team = team;
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

    public virtual Unit SelectEnemy(List<Unit> enemys) // MAKE RANDOM ENEMY SYSTEM SELECT
    {
        return enemys[0];
    }

    public virtual void ReceiveDamageFromUnit(double damage)
    {
        if (IsShield())
        {
            Shield.Now -= damage;
        }
        else if (IsAlive())
        {
            HealthPoints.Now -= damage;
        }
        else // Dead
            return;
    }

    public bool IsAlive() => HealthPoints.Now > 0;
    public bool IsShield() => Shield.Now > 0;
}

public class AttackEventArgs : EventArgs
{
    public double DamageDealt { get; }
    public bool IsCritical { get; }
    public bool IsMiss { get; }

    public AttackEventArgs(double damageDealt, bool isCritical, bool isMiss)
    {
        DamageDealt = damageDealt;
        IsCritical = isCritical;
        IsMiss = isMiss;
    }
}

public class DamageReceivedEventArgs : EventArgs
{
    public double DamageReceived { get; }

    public DamageReceivedEventArgs(double damageReceived)
    {
        DamageReceived = damageReceived;
    }
}
