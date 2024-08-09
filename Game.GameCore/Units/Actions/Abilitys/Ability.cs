using App.GameCore.Battles.Manager;
using App.GameCore.Tools.Formulas;

namespace App.GameCore.Units.Actions.Abilitys;

public abstract class Ability : UnitAction
{
    protected Ability(Battle battle, Unit unit) : base(battle, unit)
    {
    }

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
    protected float GetChanceOfHit(Unit target)
    {
        var accuracy = _unit.Accuracy.Now;
        var dexterity = target.Dexterity.Now;
        var coefInitiative = (float)GameFormulas.GetRatio(_unit.Initiative.Now, _unit.Initiative.Default);
        var coefInitiativeTarger = (float)GameFormulas.GetRatio(target.Initiative.Now, target.Initiative.Default);
        return (accuracy * coefInitiative) - (dexterity * coefInitiativeTarger);
    }
    protected float GetChanceOfCrit(Unit target) // Треба придумати як задіяти ініціативу цілі
    {
        var crit = _unit.CriticalChance.Now;
        var affectedArea = target.CriticalDefeat.Now;
        var coefInitiative = (float)GameFormulas.GetRatio(_unit.Initiative.Now, _unit.Initiative.Default);
        var coefInitiativeTarger = (float)GameFormulas.GetRatio(target.Initiative.Now, target.Initiative.Default);
        return (crit * coefInitiative) + (affectedArea);
    }
}
