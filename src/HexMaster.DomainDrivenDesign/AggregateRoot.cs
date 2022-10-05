using System;
using System.Collections.Generic;
using HexMaster.DomainDrivenDesign.Abstractions.Events;
using HexMaster.DomainDrivenDesign.ChangeTracking;

namespace HexMaster.DomainDrivenDesign
{
    public abstract class AggregateRoot<T> :DomainModel<T>
    {
        private readonly Lazy<List<IDomainEvent>> _domainEvents;
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.Value.AsReadOnly();

        protected virtual void AddDomainEvent(IDomainEvent eventItem)
        {
            _domainEvents.Value.Add(eventItem);
        }

        public virtual void ClearDomainEvents()
        {
            _domainEvents.Value.Clear();
        }

        protected AggregateRoot(T id, TrackingState state = null) : base(id, state)
        {
            _domainEvents = new Lazy<List<IDomainEvent>>(new List<IDomainEvent>());
        }
    }
}
