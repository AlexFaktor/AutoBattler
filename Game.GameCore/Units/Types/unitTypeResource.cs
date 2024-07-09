namespace Game.GameCore.Units.Types;

public class unitTypeResource<T>
{
    public T Default { get; }
    public T Max { get; }
    public T Now { get; }

    public unitTypeResource(T defaultValue)
    {
        Default = defaultValue;
        Max = defaultValue;
        Now = defaultValue;
    }
}
