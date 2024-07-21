namespace App.GameCore.Units.Types
{
    public class TUnitPercentage
    {
        private float _now;

        public float Default { get; }
        public float Max { get; set; }
        public float Now
        {
            get => _now;
            set
            {
                if (_now != value)
                {
                    OnNowChanging?.Invoke(this, new PercentageChangingEventArgs<float>(_now, value));
                    _now = value;
                    OnNowChanged?.Invoke(this, new PercentageChangedEventArgs<float>(_now));
                }
            }
        }

        public event EventHandler<PercentageChangingEventArgs<float>> OnNowChanging;
        public event EventHandler<PercentageChangedEventArgs<float>> OnNowChanged;

        public TUnitPercentage(float value)
        {
            Default = value;
            Max = value;
            _now = value;
        }
    }

    public class PercentageChangingEventArgs<T> : EventArgs
    {
        public T OldValue { get; }
        public T NewValue { get; }

        public PercentageChangingEventArgs(T oldValue, T newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }

    public class PercentageChangedEventArgs<T> : EventArgs
    {
        public T NewValue { get; }

        public PercentageChangedEventArgs(T newValue)
        {
            NewValue = newValue;
        }
    }
}
