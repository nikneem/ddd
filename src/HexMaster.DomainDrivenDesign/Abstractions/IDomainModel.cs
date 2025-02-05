using HexMaster.DomainDrivenDesign.ChangeTracking;
using System.Collections.Generic;

namespace HexMaster.DomainDrivenDesign.Abstractions;

public interface IDomainModel<out TId>
{
    TId Id { get; }
    TrackingState TrackingState { get; } 
    IReadOnlyList<IDomainEvent> DomainEvents { get; }

    void AddDomainEvent(IDomainEvent domainEvent);
    void RemoveDomainEvent(IDomainEvent domainEvent);
    void ClearDomainEvents();
}
