using Game.GameCore.Battles.System;
using Game.GameCore.Units.Actions;
using Game.GameCore.Units.Enums;
using Game.GameCore.Units.Types;

namespace Game.GameCore.Units;

public abstract class Unit
{
    // Metadata
    public Guid Token { get; } = Guid.NewGuid();
    public Team? Team { get; set; }

    // Info
    public short Id { get; protected set; }
    public string Name { get; protected set; }

    // Tactics
    public EUnitClass Class { get; protected set; }
    public EUnitClass SubClass { get; protected set; }
    public TUnitResource<float> Initiative { get; protected set; }
    public TUnit<float> Speed { get; protected set; }
    public TUnit<float> AttackRange { get; protected set; }
    public ETacticalType TacticalType { get; protected set; }
    public TUnit<int> TacticalLevel { get; protected set; }

    // General
    public TUnitPercentage AbilityHaste { get; protected set; }
    public TUnitPercentage Vaparism { get; protected set; }
    public TUnitResource<float> Mana { get; protected set; }

    // Attack
    public TUnit<double> Damage { get; protected set; }
    public BattleTimer AttackSpeed { get; protected set; }
    public TUnitPercentage Accuracy { get; protected set; }
    public TUnitPercentage CriticalChance { get; protected set; }
    public TUnitPercentage CriticalDamage { get; protected set; }
    public TUnitPercentage ArmorPenetration { get; protected set; }
    public TUnit<float> IgnoringArmor { get; protected set; }

    // Defensive
    public TUnitResource<double> HealthPoints { get; protected set; }
    public TUnitResource<double> Shield { get; protected set; }
    public TUnitPercentage ShieldEfficiency { get; protected set; }
    public TUnit<float> HealthPassive { get; protected set; }
    public TUnitPercentage HealthEfficiency { get; protected set; }
    public TUnitPercentage Dexterity { get; protected set; }
    public TUnitPercentage CriticalDefeat { get; protected set; }
    public TUnit<int> Armor { get; protected set; }

    // Other
    public double Position { get; protected set; }

    protected List<UnitAction> _actions = [];

    // Events
    public event EventHandler<AttackEventArgs> OnAttack;
    public event EventHandler<DamageReceivedEventArgs> OnDamageReceived;

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
