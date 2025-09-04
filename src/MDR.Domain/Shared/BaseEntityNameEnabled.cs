namespace MDR.Domain
{
    public abstract class BaseEntityNameEnabled : BaseEntityEnabled, IHasName, IHasEnabled
    {
        public string Name { get; private set; }

        protected BaseEntityNameEnabled() : base()
        {

        }

        protected BaseEntityNameEnabled(Guid id) : base(id)
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
