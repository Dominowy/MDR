using System;

namespace MDR.Domain
{
    /// <summary>
    /// A thing identified by Id
    /// </summary>
    public interface IEntity
    {
        Guid Id { get; }
    }

    public interface IEntity<T>
    {
        T Id { get; }
    }
}
