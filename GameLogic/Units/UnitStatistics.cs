namespace GameLogic.Units;

public class UnitStatistics
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public double DamageReceived { get; set; }
    public int DodgedOnce { get; set; }
    public double DamageDealt { get; set; }
    public double DamageDealtCrit { get; set; }
    public double PassiveHeal { get; set; }
    public int Hits { get; set; }
    public int CritHits { get; set; }
    public int Misses { get; set; }
    public float DistanceCovered { get; set; }
    public float TimeOfDeath { get; set; }

    public UnitStatistics(Unit unit)
    {
        Id = unit.Token;
        Name = unit.Name;

        unit.OnDamageReceived += Unit_OnDamageReceived;
        unit.OnAttack += Unit_OnAttack;
        unit.OnDodge += Unit_OnDodge;
        unit.OnMove += Unit_OnMove;
        unit.OnCrit += Unit_OnCrit;
        unit.OnHit += Unit_OnHit;
        unit.OnMiss += Unit_OnMiss;
        unit.OnDead += Unit_OnDead;
        unit.OnHealthRecovery += Unit_OnHealthRecovery;
    }

    private void Unit_OnHealthRecovery(object? sender, HealthEventArgs e)
    {
        PassiveHeal += e.HealValue;
    }

    private void Unit_OnDead(object? sender, DeadEventArgs e)
    {
        TimeOfDeath = e.TimeDead;
    }

    private void Unit_OnMiss(object? sender, EventArgs e)
    {
        Misses++;   
    }

    private void Unit_OnHit(object? sender, EventArgs e)
    {
        Hits++;
    }

    private void Unit_OnCrit(object? sender, CritEventArgs e)
    {
        DamageDealtCrit += e.DamageCrit;
        CritHits++;
    }

    private void Unit_OnMove(object? sender, MoveEventArgs e)
    {
        DistanceCovered += e.Speed;
    }

    private void Unit_OnAttack(object? sender, AttackEventArgs e)
    {
        DamageDealt += e.DamageDealt;
    }
    private void Unit_OnDodge(object? sender, DodgeEventArgs e)
    {
        DodgedOnce++;
    }

    private void Unit_OnDamageReceived(object? sender, DamageReceivedEventArgs e)
    {
        DamageReceived += e.DamageReceived;
    }
}
