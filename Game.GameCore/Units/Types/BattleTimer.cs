        namespace App.GameCore.Units.Types;

public class BattleTimer
{
    public uint Default { get; } = 0;
    public uint Now { get; set; } = 0;
    public ulong NextUse { get; set; } = 0;

    public BattleTimer(float attackBySecond)
    {
        Default =  uint.Parse((1000 / attackBySecond).ToString());
        NextUse += Default;
    }

    public BattleTimer(uint _default)
    {
        Default = _default;
        NextUse += Default;
    }


    public void Reload()
    {
        NextUse += Now;
    }
}
