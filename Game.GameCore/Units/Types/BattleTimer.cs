        namespace App.GameCore.Units.Types;

public class BattleTimer
{
    public uint Default { get; } = 0;
    public uint Now { get; set; } = 0;
    public ulong NextUse { get; set; } = 0;

    public BattleTimer(float attackBySecond)
    {
        Default =  uint.Parse((1000 / attackBySecond).ToString());
        Now = Default;
        NextUse += Default;
    }

    public BattleTimer(uint _default)
    {
        Default = _default;
        Now = Default;
        NextUse += Default;
    }


    public void Reload(uint timeToNextUse)
    {
        NextUse = NextUse + timeToNextUse;
    }
}
