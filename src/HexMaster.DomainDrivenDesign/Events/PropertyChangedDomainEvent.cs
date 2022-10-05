using HexMaster.DomainDrivenDesign.Abstractions.Events;

namespace HexMaster.DomainDrivenDesign.Events
{

    public abstract class PropertyChangedDomainEvent<T> : IDomainEvent
    {
        public string Property { get; }
        public T OldValue { get; }
        public T NewValue { get; }

        protected PropertyChangedDomainEvent(T oldValue, T newValue, string property)
        {
            Property = property;
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}