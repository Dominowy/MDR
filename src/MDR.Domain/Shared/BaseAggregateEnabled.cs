namespace MDR.Domain
{
    public abstract class BaseAggregateEnabled : Aggregate, IHasEnabled
    {
        public bool Enabled { get; private set; } = true;

        protected BaseAggregateEnabled() : base()
        {

        }

        protected BaseAggregateEnabled(Guid id) : base(id)
        {

        }

        public void SetEnabled(bool enabled)
        {
            var anyChange = Enabled != enabled;
            if (!anyChange) return;
            Enabled = enabled;
        }
    }
}
