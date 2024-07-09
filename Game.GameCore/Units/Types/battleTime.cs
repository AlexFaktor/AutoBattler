namespace Game.GameCore.Units.Types;

public class battleTime
{
    public uint Default { get; }
    public uint Now { get; }
    public ulong NextUse { get; } = 0;
}
