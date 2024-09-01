using GameLogic.Battles.Manager;
using GameLogic.Tools.Formulas;
using GameLogic.Units;
using GameLogic.Units.Actions;

namespace GameCore.Units.Actions.Abilitys;

public abstract class Ability(Battle battle, Unit unit) : UnitAction(battle, unit)
{
    protected List<Unit> GetEnemys() => _battle.AllUnits.Where(u => u.Team.Token != _unit.Team.Token).ToList();
    protected double GetTotalPower(bool isCrit)
    {
        var damage = _unit.Damage.Now;
        var coefInitiative = GameFormulas.GetRatio(_unit.Initiative.Now, _unit.Initiative.Default);
        var coefCritDamage = _unit.CriticalDamage.Now;

        // Power with crit
        if (isCrit)
        {
            return damage * coefInitiative * (1 + coefCritDamage);
        }
        // Default power
        else
            return damage * coefInitiative;
    }
    
    
}
