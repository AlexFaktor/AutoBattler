namespace App.GameCore.Units.Types;

public class TUnitValue<T>
{
    public T Default { get; }

    private T _now;
    public T Now
    {
        get => _now;
        set
        {
            OnNowChanging?.Invoke(this, new ValueChangingEventArgs<T>(_now, value));
            _now = value;
            OnNowChanged?.Invoke(this, new ValueChangedEventArgs<T>(_now));
        }
    }

    public event EventHandler<ValueChangingEventArgs<T>> OnNowChanging;
    public event EventHandler<ValueChangedEventArgs<T>> OnNowChanged;

    public TUnitValue(T defaultValue)
    {
        Default = defaultValue;
        _now = defaultValue;
    }
}

public class ValueChangingEventArgs<T> : EventArgs
{
    public T OldValue { get; }
    public T NewValue { get; }

    public ValueChangingEventArgs(T oldValue, T newValue)
    {
        OldValue = oldValue;
        NewValue = newValue;
    }
}

public class ValueChangedEventArgs<T> : EventArgs
{
    public T NewValue { get; }

    public ValueChangedEventArgs(T newValue)
    {
        NewValue = newValue;
    }
}
