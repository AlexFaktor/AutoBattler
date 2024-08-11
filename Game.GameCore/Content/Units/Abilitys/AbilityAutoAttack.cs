using App.GameCore.Battles.Manager;
using App.GameCore.Units;
using App.GameCore.Units.Actions.Abilitys;
using App.GameCore.Units.Types;

namespace App.GameCore.Content.Units.Abilitys;

internal class AbilityAutoAttack : RechargingAbility
{
    public AbilityAutoAttack(Battle battle, Unit unit) : base(battle, unit)
    {
        Time = new(unit.AttackSpeed.Default);
    }

    public override BattleTimer Time { get; set; }

    public override void Action()
    {
        var enemys = GetEnemys();
        var target = _unit.SelectEnemy(enemys, _unit.AttackRange.Now);

        if (target is null)
        {
            _unit.Move(enemys);
            return;
        }

        var isHit = (float)_battle.Random.NextDouble() > _unit.ChanceOfHitUnit(target);
        if (!isHit)
        {
            Time.Reload();
            return;
        }
        var isCrit = (float)_battle.Random.NextDouble() > _unit.ChanceOfCritUnit(target);

        target.ReceiveDamageFromUnit(GetTotalPower(isCrit), _unit);

        Time.Reload();
    }
}
