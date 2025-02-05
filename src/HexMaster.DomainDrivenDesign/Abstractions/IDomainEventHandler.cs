using System.Threading.Tasks;

namespace HexMaster.DomainDrivenDesign.Abstractions;

public interface IDomainEventHandler<in TDomainEvent> : IDomainEventHandler where TDomainEvent : IDomainEvent
{
    Task Handle(TDomainEvent domainEvent);
}
public interface IDomainEventHandler
{
    Task Handle(IDomainEvent @event);
}
