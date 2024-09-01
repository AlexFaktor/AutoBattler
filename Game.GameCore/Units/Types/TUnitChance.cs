namespace GameLogic.Units.Types;

public class TUnitChance
{
    private float _now;

    public float Default { get; }
    public float Max { get; } = 100;
    public float Now
    {
        get => _now;
        set
        {
            if (_now != value)
            {
                OnNowChanging?.Invoke(this, new ChanceChangingEventArgs<float>(_now, value));
                _now = value;
                OnNowChanged?.Invoke(this, new ChanceChangedEventArgs<float>(_now));
            }
        }
    }

    public event EventHandler<ChanceChangingEventArgs<float>>? OnNowChanging;
    public event EventHandler<ChanceChangedEventArgs<float>>? OnNowChanged;

    public TUnitChance(float value)
    {
        Default = value;
        _now = value;
    }
}

public class ChanceChangingEventArgs<T> : EventArgs
{
    public T OldValue { get; }
    public T NewValue { get; }

    public ChanceChangingEventArgs(T oldValue, T newValue)
    {
        OldValue = oldValue;
        NewValue = newValue;
    }
}

public class ChanceChangedEventArgs<T> : EventArgs
{
    public T NewValue { get; }

    public ChanceChangedEventArgs(T newValue)
    {
        NewValue = newValue;
    }
}
