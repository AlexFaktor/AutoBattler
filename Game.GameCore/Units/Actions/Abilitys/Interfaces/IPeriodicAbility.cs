using App.GameCore.Units.Types;

namespace App.GameCore.Units.Actions.Abilitys.Interfaces;

public interface IPeriodicAbility
{
    public BattleTimer Time { get; set; }
}
