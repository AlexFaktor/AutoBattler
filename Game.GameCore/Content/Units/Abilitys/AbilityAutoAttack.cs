using Game.GameCore.Tools.Formulas;
using Game.GameCore.Units;
using Game.GameCore.Units.Actions;
using Game.Manager.BattleSystem;

namespace Game.GameCore.Content.Units.Abilitys
{
    internal class AbilityAutoAttack : Ability
    {
        public AbilityAutoAttack(BattleManager battle, Unit unit) : base(battle, unit)
        {
        }

        public override void Action()
        {
            var enemys = _battle.AllUnits.Where(u => u.Team.Value.Token != _unit.Team.Value.Token).ToList();

            var target = _unit.SelectEnemy(enemys);
        }

        private double GetTotalPower()
        {
            var damage = _unit.Damage.Now;
            var coefficientInitiative = GameFormulas.GetRatio(_unit.Initiative.Now, _unit.Initiative.Default);
            return damage * coefficientInitiative;
        }
    }
}
