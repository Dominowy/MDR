namespace MDR.Domain
{
    public abstract class BaseEntityName : Entity, IHasName
    {
        public string Name { get; private set; }

        protected BaseEntityName() : base()
        {

        }

        protected BaseEntityName(Guid id) : base(id)
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
