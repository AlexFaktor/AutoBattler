using GameLogic.Units.Types;

namespace GameLogic.Units.Actions.Abilitys.Interfaces;

public interface IPeriodicAbility
{
    public BattleTimer Time { get; set; }
}
