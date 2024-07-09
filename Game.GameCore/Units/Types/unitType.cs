namespace Game.GameCore.Units.Types;

public class unitType<T>
{
    public T Default { get; }
    public T Now { get; }

    public unitType(T defaultValue)
    {
        Default = defaultValue;
        Now = defaultValue;
    }
}
