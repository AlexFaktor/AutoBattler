namespace Game.GameCore.Units.Types
{
    public class TUnitResource<T> where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
    {
        private T _now;

        public T Default { get; }
        public T Max { get; set; }
        public T Now
        {
            get => _now;
            set
            {
                if (!_now.Equals(value))
                {
                    OnNowChanging?.Invoke(this, new ResourceChangingEventArgs<T>(_now, value));
                    _now = value;
                    OnNowChanged?.Invoke(this, new ResourceChangedEventArgs<T>(_now));
                }
            }
        }

        public event EventHandler<ResourceChangingEventArgs<T>> OnNowChanging;
        public event EventHandler<ResourceChangedEventArgs<T>> OnNowChanged;

        public TUnitResource(T defaultValue)
        {
            Default = defaultValue;
            Max = defaultValue;
            _now = defaultValue;
        }
    }

    public class ResourceChangingEventArgs<T> : EventArgs
    {
        public T OldValue { get; }
        public T NewValue { get; }

        public ResourceChangingEventArgs(T oldValue, T newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }

    public class ResourceChangedEventArgs<T> : EventArgs
    {
        public T NewValue { get; }

        public ResourceChangedEventArgs(T newValue)
        {
            NewValue = newValue;
        }
    }
}
