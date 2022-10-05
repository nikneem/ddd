namespace HexMaster.DomainDrivenDesign.Abstractions.Events
{

    public interface IDomainEventNotification<out TEventType> where TEventType : IDomainEvent
    {
        TEventType DomainEvent { get; }
    }
}