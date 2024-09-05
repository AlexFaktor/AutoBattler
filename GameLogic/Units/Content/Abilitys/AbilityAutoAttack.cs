using GameLogic.Battles.Manager;
using GameLogic.Battles.System;
using GameLogic.Units.Actions.Abilitys;

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
            const int TIME_COLLDOWN_IF_MOVE = 400; // If the skill was not used due to the absence of enemies. It is included in the recharge for 100 milliseconds.
            const float HOW_MANY_TIMES_WEAKER_WILL_BE_MOVE_SPEED = 2.5f;

            logger.LogAction("autoattack-info", $" {_unit.Name} has no target to attack ");
            _unit.Move(enemys, HOW_MANY_TIMES_WEAKER_WILL_BE_MOVE_SPEED);
            Time.Reload(TIME_COLLDOWN_IF_MOVE);
            return;
        }

        var isHit = (float)_battle.Random.NextDouble() > _unit.ChanceOfHitUnit(target);
        if (!isHit)
        {
            logger.LogAction("autoattack-info", $" {_unit.Name} missed ");
            target.TriggerDodge(_unit);
            _unit.TriggerMiss();
            Time.Reload(_unit.AttackSpeed.Now);
            return;
        }
        _unit.TriggerHit();
        
        var isCrit = (float)_battle.Random.NextDouble() < _unit.ChanceOfCritUnit(target);

        if (isCrit)
            logger.LogAction("autoattack-info", $" {_unit.Name} attacked {target.Name} with crit ");
        else
            logger.LogAction("autoattack-info", $" {_unit.Name} attacked {target.Name}");

        double hitDamage = GetTotalPower(isCrit);

        double hitCrit = GetTotalPower(true);
        double hitWithoutCrit = GetTotalPower(false);

        _unit.TriggerAttack(hitDamage, isCrit, !isHit);
        target.ReceiveDamageFromUnit(hitDamage, _unit, isCrit, hitCrit - hitWithoutCrit);

        Time.Reload(_unit.AttackSpeed.Now);
    }
}
