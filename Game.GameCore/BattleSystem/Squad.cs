using Game.GameCore.Units;
using Game.Manager.BattleSystem;

namespace Game.GameCore.BattleSystem;

public class Squad
{
    public EBattleTactics Tactics { get; set; }
    public List<Unit> Units { get; } = [];
}
