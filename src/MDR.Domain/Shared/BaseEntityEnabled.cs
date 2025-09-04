namespace MDR.Domain
{
    public abstract class BaseEntityEnabled : Entity, IHasEnabled
    {
        public bool Enabled { get; private set; }

        protected BaseEntityEnabled() : base()
        {
            
        }

        protected BaseEntityEnabled(Guid id) : base(id)
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
