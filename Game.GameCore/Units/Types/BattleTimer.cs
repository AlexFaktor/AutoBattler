namespace Game.GameCore.Units.Types;

public class BattleTimer
{
    public uint Default { get; } = 0;
    public uint Now { get; set; } = 0;
    public ulong NextUse { get; set; } = 0;

    public BattleTimer(uint _default, uint now, ulong nextUxe)
    {
        Default = _default;
        Now = now;
        NextUse = nextUxe;
    }

    public BattleTimer()
    { 
    }

    public void Reload()
    {
        NextUse += Now;
    }
}
