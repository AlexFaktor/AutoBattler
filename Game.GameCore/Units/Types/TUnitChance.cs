namespace Game.GameCore.Units.Types;

public class TUnitChance
{
    public float Default { get; }
    public float Max { get; } = 100;
    public float Now { get; set; }

    public TUnitChance(float value)
    {
        Default = value;
        Now = value;
    }
}


