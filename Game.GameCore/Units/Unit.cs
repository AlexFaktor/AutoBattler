using Game.GameCore.BattleSystem;
using Game.GameCore.Units.Actions;
using Game.GameCore.Units.Enums;
using Game.GameCore.Units.Types;
using static System.Net.Mime.MediaTypeNames;

namespace Game.GameCore.Units;

public abstract class Unit
{
    // Metadata
    public Guid Token { get; } = Guid.NewGuid();
    public Team? Team { get; set; }

    // Info
    public short Id { get; set; } = 0;
    public string Name { get; set; } = string.Empty;

    // Tactics
    public EUnitClass Class { get; set; }
    public EUnitClass SubClass { get; set; }
    public unitTypeResource<float> Initiative { get; set; }
    public unitType<float> Speed { get; set; }
    public unitType<float> AttackRange { get; set; }
    public ETacticalType TacticalType { get; set; }
    public unitType<int> TacticalLevel { get; set; }

    // General
    public unitTypePercentage AbilityHaste { get; set; }
    public unitTypePercentage Vaparism { get; set; }
    public unitTypeResource<float> Mana { get; set; }

    // Attack
    public unitType<double> Damage { get; set; }
    public battleTime AttackSpeed { get; set; }
    public unitTypePercentage Accuracy { get; set; }
    public unitTypePercentage CriticalChance { get; set; }
    public unitTypePercentage CriticalDamage { get; set; }
    public unitTypePercentage ArmorPenetration { get; set; }
    public unitType<float> IgnoringArmor { get; set; }

    // Defensive
    public unitTypeResource<double> HealthPoints { get; set; }
    public unitTypeResource<double> Shield { get; set; }
    public unitTypePercentage ShieldEfficiency { get; set; }
    public unitType<float> HealthPassive { get; set; }
    public unitTypePercentage HealthEfficiency { get; set; }
    public unitTypePercentage Dexterity { get; set; }
    public unitTypePercentage CriticalDefeat { get; set; }
    public unitType<int> Armor { get; set; }

    // Other
    public double Position { get; set; }

    private List<UnitAction> _actions = [];

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

    public void ReceiveDamageFromUnit(double damage)
    {
        if (IsShield())
        { 
        
        }
        else if (IsAlive())
        {
            
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
