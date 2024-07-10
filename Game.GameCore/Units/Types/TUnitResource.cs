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
                    _now = value;
                    OnNowChanged(EventArgs.Empty);
                }
            }
        }

        public event EventHandler NowChanged;

        public TUnitResource(T defaultValue)
        {
            Default = defaultValue;
            Max = defaultValue;
            _now = defaultValue;
        }

        protected virtual void OnNowChanged(EventArgs e)
        {
            NowChanged?.Invoke(this, e);
        }
    }
}
