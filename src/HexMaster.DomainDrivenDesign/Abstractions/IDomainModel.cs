using HexMaster.DomainDrivenDesign.ChangeTracking;

namespace HexMaster.DomainDrivenDesign.Abstractions;

public interface IDomainModel<out TId>
{
    TId Id { get; }
    TrackingState TrackingState { get; }
}