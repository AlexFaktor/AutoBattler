using Game.GameCore.Units.Actions.Interfaces;
using Game.GameCore.Units.Types;

namespace Game.GameCore.Units.Actions;

public abstract class RechargingAbility : Ability, IReloadable
{
    public BattleTimer Time { get; set; } = new BattleTimer();
    protected RechargingAbility(Battles.Manager.Battle battle, Unit unit) : base(battle, unit)
    {
    }
}
