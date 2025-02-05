using System.Collections.Generic;
using System.Threading.Tasks;

namespace HexMaster.DomainDrivenDesign.Abstractions;

public interface IDomainEventDispatcher
{
    Task Dispatch(IDomainEvent domainEvent);
    Task Dispatch(List<IDomainEvent> domainEvents);
}