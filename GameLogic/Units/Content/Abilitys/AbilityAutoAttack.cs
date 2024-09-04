using GameLogic.Battles.Manager;
using GameLogic.Battles.System;
using GameLogic.Units.Actions.Abilitys;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            const int TIME_COLLDOWN_IF_MOVE = 100; // If the skill was not used due to the absence of enemies. It is included in the recharge for 100 milliseconds.
            const int HOW_MANY_TIMES_WEAKER_WILL_BE_MOVE_SPEED = 10;
            _unit.Move(enemys, HOW_MANY_TIMES_WEAKER_WILL_BE_MOVE_SPEED);
            Time.Reload(TIME_COLLDOWN_IF_MOVE); 
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
