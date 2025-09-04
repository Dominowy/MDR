namespace MDR.Domain
{
    public abstract class BaseAggregateNameEnabled : BaseAggregateEnabled, IHasName
    {
        public string Name { get; private set; }

        protected BaseAggregateNameEnabled() : base()
        {

        }

        protected BaseAggregateNameEnabled(Guid id) : base(id)
        {

        }

        public void SetName(string name)
        {
            var anyChange = Name != name;
            if (!anyChange) return;
            Name = name;
        }
    }
}
