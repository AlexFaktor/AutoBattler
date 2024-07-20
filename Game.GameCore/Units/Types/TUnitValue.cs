namespace Game.GameCore.Units.Types;

public class TUnitValue<T>
{
    public T Default { get; }
    public T Now { get; set; }

    public TUnitValue(T defaultValue)
    {
        Default = defaultValue;
        Now = defaultValue;
    }
}
