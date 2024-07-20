using Game.GameCore.Units.Types;

namespace Game.GameCore.Units.Actions.Interfaces;

public interface IPeriodicAbility
{
    public BattleTimer Time { get; set; }
}
