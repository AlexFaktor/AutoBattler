using Game.GameCore.Units.Types;

namespace Game.GameCore.Units.Actions.Interfaces;

public interface IReloadable
{
    public BattleTimer Time { get; set; }
}
