namespace Game.GameCore.Units.Types;

public class TUnit<T>
{
    public T Default { get; }
    public T Now { get; set; }

    public TUnit(T defaultValue)
    {
        Default = defaultValue;
        Now = defaultValue;
    }
}
