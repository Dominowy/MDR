namespace MDR.Domain
{
    public abstract class BaseAggregate : Aggregate
    {
        protected BaseAggregate() : base()
        {

        }

        protected BaseAggregate(Guid id) : base(id)
        {

        }
    }
}
