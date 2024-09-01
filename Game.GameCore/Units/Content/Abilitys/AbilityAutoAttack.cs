using GameLogic.Battles.Manager;
using GameLogic.Units.Actions.Abilitys;
using GameLogic.Units.Types;

namespace GameLogic.Units.Content.Abilitys;

internal class AbilityAutoAttack : RechargingAbility
{
    public AbilityAutoAttack(Battle battle, Unit unit) : base(battle, unit)
    {
        Time = new(unit.AttackSpeed.Default);
    }

    public override BattleTimer Time { get; set; }

    public override void Action()
    {
        var logger = _battle.Logger;

        var enemys = GetEnemys();
        var target = _unit.SelectEnemy(enemys, _unit.AttackRange.Now);

        if (target is null)
        {
            logger.LogAction("autoattack-info", $" {_unit.Name} has no target to attack ");
            _unit.Move(enemys);
            Time.Reload(1000);
            return;
        }

        var isHit = (float)_battle.Random.NextDouble() > _unit.ChanceOfHitUnit(target);
        if (!isHit)
        {
            logger.LogAction("autoattack-info", $" {_unit.Name} missed ");
            Time.Reload(_unit.AttackSpeed.Now);
            return;
        }
        var isCrit = (float)_battle.Random.NextDouble() > _unit.ChanceOfCritUnit(target);

        if (isCrit)
            logger.LogAction("autoattack-info", $" {_unit.Name} attacked {target.Name} with crit ");
        else
            logger.LogAction("autoattack-info", $" {_unit.Name} attacked {target.Name}");

        target.ReceiveDamageFromUnit(GetTotalPower(isCrit), _unit);

        Time.Reload(_unit.AttackSpeed.Now);
    }
}
