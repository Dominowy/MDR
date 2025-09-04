namespace MDR.Domain
{
    public abstract class Aggregate : IAggregate 
    {
        public Guid Id { get; private set; }

        protected Aggregate() : base()
        {
        }

        public Aggregate(Guid id)
        {
            Id = id;
        }
    }
}
