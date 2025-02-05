using HexMaster.DomainDrivenDesign.ChangeTracking;
using System.Collections.Generic;

namespace HexMaster.DomainDrivenDesign.Abstractions;

public interface IDomainModel<out TId>
{
    TId Id { get; }
    TrackingState TrackingState { get; } 
    IReadOnlyList<IDomainNotification> DomainEvents { get; }

    void AddDomainEvent(IDomainNotification domainEvent);
    void RemoveDomainEvent(IDomainNotification domainEvent);
    void ClearDomainEvents();
}
